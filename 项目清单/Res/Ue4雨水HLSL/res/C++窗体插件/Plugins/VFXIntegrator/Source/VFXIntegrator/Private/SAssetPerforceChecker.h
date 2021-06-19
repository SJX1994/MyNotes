#pragma once

#include "CoreMinimal.h"
#include "Misc/Attribute.h"

class SAssetPerforceChecker : public SCompoundWidget
{
private:
	struct FListEntry
	{
		FListEntry(FString assetPath, bool canBeCheckedOut, bool isCheckedOut, FString assetStatus)
			: AssetPath(assetPath)
			, CanBeCheckedOut(canBeCheckedOut)
			, IsCheckedOut(isCheckedOut)
			, AssetStatus(assetStatus)
		{
		}

		FString AssetPath;
		bool CanBeCheckedOut;
		bool IsCheckedOut;
		FString AssetStatus;
	};

public:
	SLATE_BEGIN_ARGS(SAssetPerforceChecker)
	{
	}
		SLATE_ATTRIBUTE(TSharedPtr<SWindow>, ParentWindow)

		SLATE_ATTRIBUTE(bool*, CancelledPtr)

	SLATE_END_ARGS()

public:
	void Construct(const FArguments& inArgs, TArray<UPackage*> packagesToCheckOut);

private:
	void GetPackagesStates();
	void SyncAll();
	void CheckoutAll();
	void Continue();
	void Cancel();

	TSharedRef<ITableRow> OnGenerateRow(TSharedPtr<FListEntry> InItem, const TSharedRef<STableViewBase>& OwnerTable);

private:
	TArray<UPackage*> m_packagesToCheckOut;

	TAttribute<TSharedPtr<SWindow>> m_parentWindow;

	/** SListView widget */
	TSharedPtr<SListView<TSharedPtr<FListEntry>>> m_assetsList;

	/** SListView source */
    TArray<TSharedPtr<FListEntry>> m_assetListSource;

	bool* m_cancelled;
};
