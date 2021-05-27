#include "SVFXIntegratorWidget.h"
#include "LevelEditor.h"
#include "Widgets/Docking/SDockTab.h"
#include "Widgets/Layout/SBox.h"
#include "Widgets/Text/STextBlock.h"
#include "ToolMenus.h"
#include "Components/ScrollBox.h"
#include "Widgets/Layout/SGridPanel.h"
#include "Widgets/Layout/SUniformGridPanel.h"
#include "Internationalization/BreakIterator.h"
#include <AssetRegistryModule.h>
#include "Widgets/Notifications/SProgressBar.h"
#include "SAssetPerforceChecker.h"
#include "Widgets/Notifications/SProgressBar.h"
#include "IAssetTools.h"
#include "AssetToolsModule.h"
#include "ISourceControlModule.h"
#include "ISourceControlProvider.h"
#include "SourceControlOperations.h" 

#define LOCTEXT_NAMESPACE "FVFXIntegratorModule"

SVFXIntegratorWidget::SVFXIntegratorWidget()
	: bIsActiveTimerRegistered(false)
{
}

SVFXIntegratorWidget::~SVFXIntegratorWidget()
{

}

TSharedRef<ITableRow> SVFXIntegratorWidget::OnGenerateRowWrapped(TSharedPtr<FString> InItem, const TSharedRef<STableViewBase>& OwnerTable)
{
	return
		SNew(STableRow< TSharedPtr<FString> >, OwnerTable).Padding(FMargin(5, 1))
		[
			SNew(STextBlock).Text(FText::FromString(*InItem.Get())).MinDesiredWidth(400)
		];
}

TSharedRef<ITableRow> SVFXIntegratorWidget::OnGenerateRow(TSharedPtr<FString> InItem, const TSharedRef<STableViewBase>& OwnerTable)
{
	return
		SNew(STableRow< TSharedPtr<FString> >, OwnerTable).Padding(5)
		[
			SNew(STextBlock).Text(FText::FromString(*InItem.Get())).MinDesiredWidth(800)
		];
}

FReply	SVFXIntegratorWidget::Replace()
{
UObject* originalAsset = originalAssetThumbnail->GetAsset();
	UObject* optimizedAsset = optimizedAssetThumbnail->GetAsset();

	FAssetData originalAssetData = originalAssetThumbnail->GetAssetData();
	FAssetData optimizedAssetData = optimizedAssetThumbnail->GetAssetData();

	originalAsset = originalAssetData.GetAsset();
	optimizedAsset = optimizedAssetData.GetAsset();

	FString originalPackageFileName = originalAssetData.GetPackage()->FileName.ToString();
	TSet<FName> referencedAssets = deleteModel->GetAssetReferences();
	TArray<FString> referencedAssetsString;
	TArray<UPackage*> packagesToCheckOut;
	for (FName referencedAsset : referencedAssets)
	{
		UPackage* package = LoadPackage(nullptr, *referencedAsset.ToString(), LOAD_None);
		if (package)
		{
			packagesToCheckOut.Add(package);
		}

		referencedAssetsString.Add(referencedAsset.ToString());
	}
	packagesToCheckOut.Add(originalAssetData.GetPackage());

	const FVector2D DEFAULT_WINDOW_SIZE = FVector2D(1000, 700);
	bool cancelled = true;
	TSharedRef<SWindow> checkAssetsWindow = SNew(SWindow)
		.Title(FText::FromString("P4 Assets Check"))
		.ClientSize(DEFAULT_WINDOW_SIZE);

	TSharedRef<SAssetPerforceChecker> checkAssetsDialog = SNew(SAssetPerforceChecker, packagesToCheckOut)
		.ParentWindow(checkAssetsWindow)
		.CancelledPtr(&cancelled);

	checkAssetsWindow->SetContent(checkAssetsDialog);

	// Modal windows will block here
	GEditor->EditorAddModalWindow(checkAssetsWindow);

	if (cancelled)
	{
		GLog->Log("User cancelled replacement");
	}
	else if (deleteModel->DoReplaceReferences(optimizedAssetData))
	{
		// If the replace references succeded, it means our original asset is now a Redirector
		// We can then cast it to a Redirector and do a fixup
		UObject* originalObject = originalAssetData.GetAsset();
		UObjectRedirector* redirector = Cast<UObjectRedirector>(originalObject);
		if (redirector != nullptr)
		{
			IAssetTools& assetTools = FModuleManager::LoadModuleChecked<FAssetToolsModule>("AssetTools").Get();

			TArray<UObjectRedirector*> redirectors;
			redirectors.Add(redirector);
			assetTools.FixupReferencers(redirectors);

			TArray<FString> filesToRevert;
			filesToRevert.Add(originalPackageFileName);
			if (ISourceControlModule::Get().GetProvider().Execute(ISourceControlOperation::Create<FRevert>(), filesToRevert) == ECommandResult::Succeeded)
			{
				GLog->Log("Revert success");
			}
			else
			{
				GLog->Log("Revert Failure");
			}
		}
	}

	return FReply::Handled();
}

FReply	SVFXIntegratorWidget::OnListVFX()
{
	FAssetRegistryModule& AssetRegistryModule = FModuleManager::LoadModuleChecked<FAssetRegistryModule>("AssetRegistry");
	TArray<FAssetData> assetData;
	AssetRegistryModule.Get().GetAssetsByClass(FName("ParticleSystem"), assetData);
	originalAssetsPaths.Empty();
	originalMeshObjectArray.Empty();

	for (auto const& asset : assetData)
	{
		FString currentAssetPath = asset.GetFullName();
		// Choping the path because raw asset's paths have the asset's type before the path (exemple: "SkeletalMesh'/Game/etc..'")
		currentAssetPath.RightChopInline(currentAssetPath.Find("/Game/"));
		originalAssetsPaths.Add(currentAssetPath);
		if (!asset.GetFullName().Contains(TEXT("/Game/")) || asset.GetFullName().Contains(TEXT("Game/R6DM")))
			continue;
		originalMeshObjectArray.Add(MakeShared<FString>(currentAssetPath));
	}
    assetsList->RequestListRefresh();

	GetExistingOptimizedAssets();

	return FReply::Handled();
}

void	SVFXIntegratorWidget::GetExistingOptimizedAssets()
{
	optimizedAssetsPaths.Empty();
	optimizedAssetsNames.Empty();

	FAssetRegistryModule& AssetRegistryModule = FModuleManager::LoadModuleChecked<FAssetRegistryModule>("AssetRegistry");
	TArray<FAssetData> assetData;
	FARFilter Filter;
	Filter.PackagePaths.Add("/Game/R6DM/Art/VFX");
	Filter.bRecursivePaths = true;
	UE_LOG(LogTemp, Log, TEXT("Getting existing optimized assets"));
	AssetRegistryModule.Get().GetAssets(Filter, assetData);
	for (auto vfx : assetData)
	{
		FString vfxPath = vfx.GetFullName();
		vfxPath.RightChopInline(vfxPath.Find("/Game/"));
		optimizedAssetsPaths.Add(vfxPath);
		optimizedMeshObjectArray.Add(MakeShared<FString>(vfxPath));
		int lastSlash;
		vfxPath.FindLastChar('.', lastSlash);
		FString vfxName = vfxPath.RightChop(lastSlash + 1);
		optimizedAssetsNames.Add(vfxName);
	}
}

void	SVFXIntegratorWidget::OnMouseButtonClick(TSharedPtr<FString> InItem)
{
	FAssetRegistryModule& AssetRegistryModule = FModuleManager::LoadModuleChecked<FAssetRegistryModule>("AssetRegistry");
	TArray<FAssetData> assetData;

	isReplacePossible = false;
	TArray<TSharedPtr<FString>> selectedAssets = assetsList->GetSelectedItems();
	if (selectedAssets.Num() != 1)
		return;
	FString originalAssetPath = *selectedAssets[0];
	
	int dotIndex;
	originalAssetPath.FindLastChar('.', dotIndex);
	FString originalAssetString = originalAssetPath.RightChop(dotIndex + 1);
	originalAssetName->SetText(FText::FromString(*originalAssetPath.Left(dotIndex)));
	assetName->SetText(FText::FromString(*originalAssetString));

	FString optimizedAssetPath;
	int optimizedAssetIndex = optimizedAssetsNames.Find(originalAssetString);
	if (optimizedAssetIndex != INDEX_NONE)
	{
		optimizedAssetPath = optimizedAssetsPaths[optimizedAssetIndex];
		optimizedAssetPath.FindLastChar('.', dotIndex);
		optimizedAssetName->SetText(FText::FromString(*optimizedAssetPath.Left(dotIndex)));
		UE_LOG(LogTemp, Log, TEXT("Found %s. Its path is %s"), *originalAssetString, *optimizedAssetPath);
	}
	else
	{
		optimizedAssetName->SetText(FText::FromString("None"));
	}
	FAssetData optimizedAssetData = AssetRegistryModule.Get().GetAssetByObjectPath(*optimizedAssetPath);
	if (optimizedAssetData.IsValid())
	{
		isReplacePossible = true;
	}
	optimizedAssetThumbnail->SetAsset(optimizedAssetData);
	optimizedAssetThumbnail->RefreshThumbnail();
	
	FAssetData originalAssetData = AssetRegistryModule.Get().GetAssetByObjectPath(*originalAssetPath);
	originalAssetThumbnail->SetAsset(originalAssetData);
	originalAssetThumbnail->RefreshThumbnail();
	if (!originalAssetData.IsValid())
	{
		isReplacePossible = false;
		UE_LOG(LogTemp, Log, TEXT("Failed to load original asset"));
	}

	// The DeleteModel is an asynchrone object that require a continuous tick function 
	// This allow the model to find the used references
	// The timer need to be register anytime we select a new asset, since they are stopped when the work is done
	if (originalAssetData.IsValid())
	{
		TArray<UObject*> objectToDelete;
		objectToDelete.Add(originalAssetData.GetAsset());
		deleteModel = MakeShared<FAssetDeleteModel>(objectToDelete);

		deleteModel->OnStateChanged().AddRaw(this, &SVFXIntegratorWidget::HandleDeleteModelStateChanged);

		if (!bIsActiveTimerRegistered)
		{
			RegisterActiveTimer(0.0f, FWidgetActiveTimerDelegate::CreateSP(this, &SVFXIntegratorWidget::TickDeleteModel));
		}
	}
}

EActiveTimerReturnType SVFXIntegratorWidget::TickDeleteModel(double currentTime, float deltaTime)
{
	deleteModel->Tick(deltaTime);

	if (deleteModel->GetState() == FAssetDeleteModel::EState::Finished)
	{
		bIsActiveTimerRegistered = false;
		return EActiveTimerReturnType::Stop;
	}

	return EActiveTimerReturnType::Continue;
}

TSharedRef<SWidget> SVFXIntegratorWidget::BuildProgessDialog()
{
	return SNew(SVerticalBox)

	+ SVerticalBox::Slot()
	.VAlign(VAlign_Center)
	.FillHeight(1.0f)
	[
		SNew(SVerticalBox)

		+ SVerticalBox::Slot()
		.Padding(5.0f, 0)
		[
			SNew(STextBlock)
			.Text(this, &SVFXIntegratorWidget::ScanningText)
			//.AutoWrapText(true)
		]

		+ SVerticalBox::Slot()
		.AutoHeight()
		.Padding(5.0f, 10.0f)
		[
			SNew(SProgressBar)
			.Percent(this, &SVFXIntegratorWidget::ScanningProgressFraction)
		]
	];
}

TSharedRef<SWidget> SVFXIntegratorWidget::BuildReferenceDialog()
{
	FString referenceString;

	referencesObjectArray.Empty();
	TSet<FName> refsOnDisk = deleteModel->GetAssetReferences();
	for (FName ref : refsOnDisk)
	{
		referenceString += ref.ToString() + "\n";
		referencesObjectArray.Add(MakeShared<FString>(referenceString));
	}

	return SNew(SVerticalBox)
	+ SVerticalBox::Slot()
	.VAlign(VAlign_Center)
	.MaxHeight(400.0f)
	[
		SAssignNew(referencesList, SListView< TSharedPtr<FString> >)
		.ListItemsSource(&referencesObjectArray)
		.OnGenerateRow(this, &SVFXIntegratorWidget::OnGenerateRowWrapped)
		.SelectionMode(ESelectionMode::Single)
	];
	//return SNew(SBox).HeightOverride(400);
}

FText SVFXIntegratorWidget::ScanningText() const
{
	return deleteModel->GetProgressText();
}

TOptional<float> SVFXIntegratorWidget::ScanningProgressFraction() const
{
	return deleteModel->GetProgress();
}

void	SVFXIntegratorWidget::HandleDeleteModelStateChanged(FAssetDeleteModel::EState newState)
{
	switch (newState)
	{
	case FAssetDeleteModel::StartScanning:
		rootContainer->SetContent(BuildProgessDialog());
		break;
	case FAssetDeleteModel::Finished:
		rootContainer->SetContent(BuildReferenceDialog());
		break;
	case FAssetDeleteModel::Scanning:
	case FAssetDeleteModel::UpdateActions:
	case FAssetDeleteModel::Waiting:
		break;
	}
}

void	SVFXIntegratorWidget::OnTextChanged(const FText &text)
{
	if (text.IsEmpty())
	{
		OnListVFX();
		return;
	}
	TArray<FString> foundElements = originalAssetsPaths.FilterByPredicate([=](FString current) {return current.Contains(text.ToString()); });
	originalMeshObjectArray.Empty();
	for (auto elem : foundElements)
	{
		originalMeshObjectArray.Add(MakeShared<FString>(elem));
	}
	assetsList->RequestListRefresh();
}

void	SVFXIntegratorWidget::Construct(const FArguments& inArgs)
{
	isReplacePossible = false;
	
	UE_LOG(LogTemp, Log, TEXT("VFXIntegrator open"));

	originalThumbnailPool = MakeShareable(new FAssetThumbnailPool(16, false));
	optimizedThumbnailPool = MakeShareable(new FAssetThumbnailPool(16, false));

	originalAssetThumbnail = MakeShareable(new FAssetThumbnail(nullptr, 79, 83, originalThumbnailPool));
	optimizedAssetThumbnail = MakeShareable(new FAssetThumbnail(nullptr, 79, 83, optimizedThumbnailPool));

	columnNamesHeaderRow = SNew(SHeaderRow);

	ChildSlot
		[
			SNew(SBox)
			.HAlign(HAlign_Center)
			.VAlign(VAlign_Center)
			[
				SNew(SGridPanel)
				+ SGridPanel::Slot(0, 0)
				.HAlign(HAlign_Center)
				.Padding(0.0f, 25.0f, 0.0f, 25.0f)
				[
					SNew(SHorizontalBox)
					+SHorizontalBox::Slot()
					.HAlign(HAlign_Center)
					[
						SNew(SButton)
						.Text(LOCTEXT("VFX listing", "List VFX assets"))
						.OnClicked(this, &SVFXIntegratorWidget::OnListVFX)
					]
				]
				+ SGridPanel::Slot(0, 1)
				.Padding(0, 0, 0, 10)
				.HAlign(HAlign_Center)
				[
					SNew(SEditableTextBox)
					.Padding(FMargin(5, 5, 5, 5))
					.HintText(LOCTEXT("Hint text", "Search for assets.."))
					.MinDesiredWidth(790)
					.SelectAllTextWhenFocused(true)
					.OnTextChanged(FOnTextChanged::CreateSP(this, &SVFXIntegratorWidget::OnTextChanged))
				]
				+ SGridPanel::Slot(0, 2)
				.HAlign(HAlign_Center)
				[
					SNew(SBox)
					.HAlign(HAlign_Center)
					.WidthOverride(800)
					.MinDesiredWidth(800)
					.MaxDesiredWidth(800)
					[
						SNew(SBorder)
						[
							SNew(SVerticalBox)
							+ SVerticalBox::Slot()
							.HAlign(HAlign_Center)
							.MaxHeight(400)
							[
								SAssignNew(assetsList, SListView< TSharedPtr<FString> >)
								.ListItemsSource(&originalMeshObjectArray)
								.OnGenerateRow(this, &SVFXIntegratorWidget::OnGenerateRow)
								.OnMouseButtonClick(this, &SVFXIntegratorWidget::OnMouseButtonClick)
								.SelectionMode(ESelectionMode::Single)
								.HeaderRow(columnNamesHeaderRow)
							]
						]
					]
				]
				+ SGridPanel::Slot(0, 3)
				.HAlign(HAlign_Center)
				.Padding(0.0f, 25.0f, 0.0f, 10.0f)
				[
					SNew(SBox)
					.WidthOverride(800)
					.HeightOverride(350)
					[
						SNew(SBorder)
						.HAlign(HAlign_Left)
						[
							SNew(SGridPanel)
							+ SGridPanel::Slot(0, 0)
							
							[
								SNew(SHorizontalBox)
								+ SHorizontalBox::Slot()
								.Padding(20.0f, 15.0f, 0.0f, 0.0f)
								.HAlign(HAlign_Left)
								.MaxWidth(300)
								[
									SAssignNew(assetName, STextBlock).Text(LOCTEXT("Asset's name", "None"))
								]
								+ SHorizontalBox::Slot()
								.Padding(20.0f, 15.0f, 0.0f, 15.0f)
								.HAlign(HAlign_Left)
								[
									SNew(SButton)
									.Text(LOCTEXT("Replace", "Replace"))
									.IsEnabled_Lambda([this]() -> bool
									{
										return isReplacePossible && deleteModel->GetState() == FAssetDeleteModel::EState::Finished;
									})
									.OnClicked(this, &SVFXIntegratorWidget::Replace)

								]
							]
							+ SGridPanel::Slot(0, 1)
							[
								SNew(SHorizontalBox)
								+ SHorizontalBox::Slot()
								.AutoWidth()
								.MaxWidth(150)
								.Padding(10, 0)
								[
									SNew(SVerticalBox)
									+ SVerticalBox::Slot()
									.Padding(10)
									.AutoHeight()
									[
										SNew(STextBlock).Text(LOCTEXT("Original asset", "Original asset"))
										.Justification(ETextJustify::Center)
									]
									+ SVerticalBox::Slot()
									.HAlign(HAlign_Center)
									.MaxHeight(83)
									[
										SNew(SHorizontalBox)
										+ SHorizontalBox::Slot()
										.MaxWidth(79)
										.HAlign(HAlign_Center)
										[
											originalAssetThumbnail->MakeThumbnailWidget()
										]
									]
									+ SVerticalBox::Slot()
									.Padding(10)
									.AutoHeight()
									[
										SAssignNew(originalAssetName, STextBlock)
										.Text(LOCTEXT("Original asset value", ""))
										.Justification(ETextJustify::Center)
										.AutoWrapText(true)
									]
								]
								+ SHorizontalBox::Slot()
								.Padding(10, 0)
								.MaxWidth(150)
								.AutoWidth()
								[
									SNew(SVerticalBox)
									+ SVerticalBox::Slot()
									.Padding(10)
									.AutoHeight()
									[
										SNew(STextBlock).Text(LOCTEXT("Optimized asset", "Optimized asset"))
										.Justification(ETextJustify::Center)
									]
									+ SVerticalBox::Slot()
									.HAlign(HAlign_Center)
									.MaxHeight(83)
									[
										SNew(SHorizontalBox)
										+ SHorizontalBox::Slot()
										.MaxWidth(79)
										.HAlign(HAlign_Center)
										[
											optimizedAssetThumbnail->MakeThumbnailWidget()
										]
									]
									+ SVerticalBox::Slot()
									.Padding(10)
									.AutoHeight()
									[
										SAssignNew(optimizedAssetName, STextBlock)
										.Text(LOCTEXT("Optimized asset value", ""))
										.Justification(ETextJustify::Center)
										.AutoWrapText(true)
									]
								]
								+ SHorizontalBox::Slot()
								.Padding(10, 0)
								.AutoWidth()
								.MaxWidth(400)
								[
									SNew(SVerticalBox)
									+ SVerticalBox::Slot()
									.Padding(10)
									.AutoHeight()
									[
										SNew(STextBlock).Text(LOCTEXT("Asset references", "Asset's references"))
										.Justification(ETextJustify::Left)
									]
									+ SVerticalBox::Slot()
									.VAlign(VAlign_Center)
									[
										SNew(SBorder)
										[
											SAssignNew(rootContainer, SBox)
											.HAlign(HAlign_Center)
										]
									]
									+ SVerticalBox::Slot()
								]
							]
						]
					]
				]
			]
		];

	columnNamesHeaderRow->ClearColumns();

    columnNamesHeaderRow->AddColumn
    (
        SHeaderRow::Column(FName("MeshName"))
        .DefaultLabel(LOCTEXT("ListHeaderOriginalName", "Original mesh path"))
        .ManualWidth(800)
        [
            SNew(SBox)
            .Padding(FMargin(0, 4, 0, 4))
            .VAlign(VAlign_Fill)
            [
                SNew(STextBlock)
                .Text(LOCTEXT("ListHeaderOriginalName", "Original mesh path"))
                .Justification(ETextJustify::Left)
            ]
        ]
    );

}