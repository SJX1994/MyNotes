#pragma once

#include "CoreMinimal.h"
#include "Widgets/SCompoundWidget.h"
#include "Widgets/Input/SSpinBox.h"
#include "Brushes/SlateImageBrush.h"
#include "Styling/SlateStyle.h"
#include "Widgets/Views/STableRow.h"
#include "AssetDeleteModel.h"

class SVFXIntegratorWidget;

/*
 * Class that represent a Slate widget
 * It is created in FVFXIntegratorModule
 * A CompoundWidget can be attached to Slate container using SNew (in our case SNew(SVFXIntegratorWidget))
 */
class SVFXIntegratorWidget : public SCompoundWidget
{
public:
    SLATE_BEGIN_ARGS(SVFXIntegratorWidget) { }

    SLATE_END_ARGS()

	SVFXIntegratorWidget();
    virtual ~SVFXIntegratorWidget();

    /**
     * This will automatically get called when Slate want to build our widget
     */
    void Construct(const FArguments& inArgs);

private:
	TSharedRef<ITableRow> OnGenerateRowWrapped(TSharedPtr<FString> InItem, const TSharedRef<STableViewBase>& OwnerTable);

	TSharedRef<ITableRow> OnGenerateRow(TSharedPtr<FString> InItem, const TSharedRef<STableViewBase>& OwnerTable);

	FReply 	OnListVFX();

	FReply	Replace();

	void	OnMouseButtonClick(TSharedPtr<FString> InItem);

	void	OnTextChanged(const FText &text);

	void	GetExistingOptimizedAssets();

	bool	isReplacePossible;

	bool	isReplaceEnabled();

	EActiveTimerReturnType	TickDeleteModel(double currentTime, float deltaTime);

	void	HandleDeleteModelStateChanged(FAssetDeleteModel::EState newState);

	TSharedRef<SWidget> BuildProgessDialog();

	TSharedRef<SWidget> BuildReferenceDialog();

	FText	ScanningText() const;

	TOptional<float> ScanningProgressFraction() const;

	// Here we save the path of all the particle systems assets
	TArray<FString>	originalAssetsPaths;

	// Here we save the path of all the assets in Game/R6DM/Art/VFX
	TArray<FString>	optimizedAssetsPaths;
	// Here we only keep their names in order to search for it later
	TArray<FString>	optimizedAssetsNames;

	TSharedPtr<STextBlock> assetName;

	/** SListView widget */
	TSharedPtr< SListView<TSharedPtr<FString>> > assetsList;

	/** SListView source */
    TArray<TSharedPtr<FString>> originalMeshObjectArray;

	/** SListView source */
    TArray<TSharedPtr<FString>> optimizedMeshObjectArray;

	/** SListView headers */
    TSharedPtr<SHeaderRow> columnNamesHeaderRow;

	TSharedPtr<STextBlock> originalAssetName;

	TSharedPtr<STextBlock> optimizedAssetName;

	TSharedPtr<TArray<FString>> levels;

	// Thumbnails
	TSharedPtr<FAssetThumbnailPool> originalThumbnailPool;

	TSharedPtr<FAssetThumbnailPool> optimizedThumbnailPool;
	
	TSharedPtr<FAssetThumbnail> originalAssetThumbnail;

	TSharedPtr<FAssetThumbnail> optimizedAssetThumbnail;

	/** Delete model specifics */
	TSharedPtr<FAssetDeleteModel> deleteModel;

	TSharedPtr<SBox> rootContainer;

	TSharedPtr< SListView<TSharedPtr<FString>> > referencesList;

	TArray<TSharedPtr<FString>> referencesObjectArray;

	bool bIsActiveTimerRegistered;
};