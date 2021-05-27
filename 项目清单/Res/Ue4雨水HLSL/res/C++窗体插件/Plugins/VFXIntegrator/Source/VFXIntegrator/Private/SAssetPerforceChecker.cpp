#include "SAssetPerforceChecker.h"
#include "IAssetRegistry.h"
#include "AssetRegistryModule.h"
#include "ISourceControlModule.h"
#include "ISourceControlProvider.h"
#include "SourceControlOperations.h"
#include "Misc/ScopedSlowTask.h"

#define LOCTEXT_NAMESPACE "SAssetPerforceChecker"

void SAssetPerforceChecker::Construct(const FArguments& inArgs, TArray<UPackage*> packagesToCheckOut)
{
    m_packagesToCheckOut = packagesToCheckOut;

	m_parentWindow = inArgs._ParentWindow;
	m_cancelled = inArgs._CancelledPtr.Get();

	// We set the cancelled ptr to true in case the user close the popup from the red cross, since we cannot intercept this event
	(*m_cancelled) = true;

    GetPackagesStates();

    ChildSlot
    [
        SNew(SVerticalBox)

        + SVerticalBox::Slot()
        .FillHeight(0.9f)
        [
            SNew(SBorder)
            [
                SAssignNew(m_assetsList, SListView<TSharedPtr<FListEntry>>)
                    .ListItemsSource(&m_assetListSource)
                    .OnGenerateRow(this, &SAssetPerforceChecker::OnGenerateRow)
                    .SelectionMode(ESelectionMode::None)
            ]
        ]

        + SVerticalBox::Slot()
        .AutoHeight()
        .Padding(0.0f, 10.0f, 0.0f, 10.0f)
        [
            SNew(STextBlock)
            .Text(LOCTEXT("Info", "Please make sure that all assets are up to date, and checked out by yourself before continuing.\nAny asset out to date, or checked out by another user will cause the operation to fail."))
        ]

        + SVerticalBox::Slot()
        .AutoHeight()
        [
            SNew(SHorizontalBox)
            + SHorizontalBox::Slot()
            [
                SNew(SButton)
                .Text(LOCTEXT("Refresh", "Refresh"))
                .OnClicked_Lambda([this]() -> FReply
                {
                    m_assetListSource.Empty();
                    GetPackagesStates();
                    m_assetsList->RequestListRefresh();

                    return FReply::Handled();
                })
            ]

            + SHorizontalBox::Slot()
            [
                SNew(SButton)
                .Text(LOCTEXT("Checkout All", "Checkout All"))
                .OnClicked_Lambda([this]() -> FReply
                {
                    m_assetListSource.Empty();
                    CheckoutAll();
                    GetPackagesStates();
                    m_assetsList->RequestListRefresh();

                    return FReply::Handled();
                })
            ]

            + SHorizontalBox::Slot()
            [
                SNew(SButton)
                .Text(LOCTEXT("Sync All", "Sync All"))
                .OnClicked_Lambda([this]() -> FReply
                {
                    m_assetListSource.Empty();
                    SyncAll();
                    GetPackagesStates();
                    m_assetsList->RequestListRefresh();

                    return FReply::Handled();
                })
            ]

			+ SHorizontalBox::Slot()
			.Padding(20.0f, 0.0f, 0.0f, 0.0f)
            [
                SNew(SButton)
                .Text(LOCTEXT("Cancel", "Cancel"))
                .OnClicked_Lambda([this]() -> FReply
                {
                    Cancel();

                    return FReply::Handled();
                })
            ]

            + SHorizontalBox::Slot()
            [
                SNew(SButton)
                .Text(LOCTEXT("Continue", "Continue"))
                .OnClicked_Lambda([this]() -> FReply
                {
                    Continue();

                    return FReply::Handled();
                })
            ]
        ]
    ];
}

TSharedRef<ITableRow> SAssetPerforceChecker::OnGenerateRow(TSharedPtr<FListEntry> InItem, const TSharedRef<STableViewBase>& OwnerTable)
{
	FLinearColor color;
	if ((*InItem.Get()).IsCheckedOut)
	{
		color = FLinearColor(0.0f, 1.0f, 0.0f, 1.0f);
	}
	else if ((*InItem.Get()).CanBeCheckedOut)
	{
		color = FLinearColor(1.0f, 1.0f, 1.0f, 1.0f);
	}
	else
	{
		color = FLinearColor(1.0f, 0.0f, 0.0f, 1.0f);
	}

	FString message = (*InItem.Get()).AssetPath + " " + (*InItem.Get()).AssetStatus;

    return
        SNew(STableRow< TSharedPtr<FString> >, OwnerTable)
        [
            SNew(SEditableTextBox)
				.Text(FText::FromString(message))
				.BackgroundColor(color)
        ];
}

namespace FileHelperPackageUtil
{
    /**
     * DoesPackageExist helper that rely on the AssetRegistry to validate if a package exists instead of hitting the FS
     * Fallback to the FS if the asset registry initial scan isn't done or we aren't in Editor
     */
    bool DoesPackageExist(UPackage* Package, FString* OutFilename = nullptr)
    {
        // Test using asset registry to figure out existence
        IAssetRegistry& AssetRegistry = FAssetRegistryModule::GetRegistry();
        if (!AssetRegistry.IsLoadingAssets() || !GIsEditor)
        {
            TArray<FAssetData> Data;
            FAssetRegistryModule::GetRegistry().GetAssetsByPackageName(Package->GetFName(), Data, true);

            if (Data.Num() > 0 && OutFilename)
            {
                *OutFilename = FPackageName::LongPackageNameToFilename(Package->GetName(), Package->ContainsMap() ? FPackageName::GetMapPackageExtension() : FPackageName::GetAssetPackageExtension());
            }

            return Data.Num() > 0;
        }
        return FPackageName::DoesPackageExist(Package->GetName(), nullptr, OutFilename);
    }
}

void SAssetPerforceChecker::GetPackagesStates()
{
    FScopedSlowTask progress(m_packagesToCheckOut.Num(), LOCTEXT("GetPackagesStates", "Getting packages states ..."));
    progress.MakeDialog();

    ISourceControlProvider& sourceControlProvider = ISourceControlModule::Get().GetProvider();
    if (ISourceControlModule::Get().IsEnabled() && sourceControlProvider.IsAvailable())
    {
        for (auto package : m_packagesToCheckOut)
        {
            progress.EnterProgressFrame(1.0f, FText::FromString(package->GetName()));

            if (!package)
            {
                continue;
            }

            FString filename;
            if (FileHelperPackageUtil::DoesPackageExist(package, &filename))
            {
                auto sourceControlState = sourceControlProvider.GetState(package, EStateCacheUsage::ForceUpdate);
                if (sourceControlState)
                {
					FString assetPath = package->FileName.ToString();
					bool canBeCheckedOut = sourceControlState->CanCheckout();
					bool isCheckedOut = sourceControlState->IsCheckedOut();
					FString assetStatus = sourceControlState->GetDisplayName().ToString();

					m_assetListSource.Add(MakeShared<FListEntry>(assetPath,
						canBeCheckedOut,
						isCheckedOut,
						assetStatus));
                }
            }
        }
    }
}

void SAssetPerforceChecker::SyncAll()
{
    FScopedSlowTask progress(m_packagesToCheckOut.Num(), LOCTEXT("SyncAll", "Sync all ..."));
    progress.MakeDialog();

    TArray<UPackage*> packagesToSync;

    ISourceControlProvider& sourceControlProvider = ISourceControlModule::Get().GetProvider();
    if (ISourceControlModule::Get().IsEnabled() && sourceControlProvider.IsAvailable())
    {
        for (auto package : m_packagesToCheckOut)
        {
            progress.EnterProgressFrame(1.0f, FText::FromString(package->GetName()));

            if (!package)
            {
                continue;
            }

            FString filename;
            if (FileHelperPackageUtil::DoesPackageExist(package, &filename))
            {
                auto sourceControlState = sourceControlProvider.GetState(package, EStateCacheUsage::ForceUpdate);
                if (sourceControlState && !sourceControlState->IsCurrent())
                {
                    packagesToSync.Add(package);
                }
            }
        }
    }

    if (packagesToSync.Num() > 0)
    {
        ISourceControlModule::Get().GetProvider().Execute(ISourceControlOperation::Create<FSync>(), packagesToSync);
    }
}

void SAssetPerforceChecker::CheckoutAll()
{
    FScopedSlowTask progress(m_packagesToCheckOut.Num(), LOCTEXT("CheckoutAll", "Checkout all ..."));
    progress.MakeDialog();

    TArray<UPackage*> packagesToCheckout;

    ISourceControlProvider& sourceControlProvider = ISourceControlModule::Get().GetProvider();
    if (ISourceControlModule::Get().IsEnabled() && sourceControlProvider.IsAvailable())
    {
        for (auto package : m_packagesToCheckOut)
        {
            progress.EnterProgressFrame(1.0f, FText::FromString(package->GetName()));

            if (!package)
            {
                continue;
            }

            FString filename;
            if (FileHelperPackageUtil::DoesPackageExist(package, &filename))
            {
                auto sourceControlState = sourceControlProvider.GetState(package, EStateCacheUsage::ForceUpdate);
                if (sourceControlState && sourceControlState->CanCheckout())
                {
                    packagesToCheckout.Add(package);
                }
            }
        }
    }

    if (packagesToCheckout.Num() > 0)
    {
        ISourceControlModule::Get().GetProvider().Execute(ISourceControlOperation::Create<FCheckOut>(), packagesToCheckout);
    }
}

void SAssetPerforceChecker::Cancel()
{
	(*m_cancelled) = true;
    m_parentWindow.Get()->RequestDestroyWindow();
}

void SAssetPerforceChecker::Continue()
{
	bool allOk = true;
	for (TSharedPtr<FListEntry> entry : m_assetListSource)
	{
		if (!entry->IsCheckedOut)
		{
			allOk = false;
			break;
		}
	}

	if (!allOk)
	{
		FText message = LOCTEXT("WarningMessageboxMessage", "At least one asset is not checked out. The redirector fixup process will most likely fail.\nAre you sure you want to continue ?");
		FText title = LOCTEXT("WarningMessageboxTitle", "Warning");

		if (EAppReturnType::No == FMessageDialog::Open(EAppMsgType::YesNo, message, &title))
		{
			return;
		}
	}

	(*m_cancelled) = false;
    m_parentWindow.Get()->RequestDestroyWindow();
}

#undef LOCTEXT_NAMESPACE