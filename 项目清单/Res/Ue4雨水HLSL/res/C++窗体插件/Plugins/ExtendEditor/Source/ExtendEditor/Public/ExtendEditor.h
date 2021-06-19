// Copyright Epic Games, Inc. All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "Modules/ModuleManager.h"

class FToolBarBuilder;
class FMenuBuilder;

class FExtendEditorModule : public IModuleInterface
{
public:

	/** IModuleInterface implementation */
	virtual void StartupModule() override;
	virtual void ShutdownModule() override;
	
	/** This function will be bound to Command. */
	void PluginButtonClicked();
	
private:

	void RegisterMenus();
	
	void AddMenuExtension(FMenuBuilder& Builder);
	void AddMenuBarExtension(FMenuBarBuilder& Builder);
	void AddToolBarExtension(FToolBarBuilder& Builder);

	void PutDownBar(FMenuBuilder& Builder);
	void PutDownSubBar(FMenuBuilder& Builder);

	void EditorPrint(FString& MyString);


	TSharedRef<FExtender> SelectedCurrentActors(const TSharedRef<FUICommandList> MyUICommandList, const TArray<AActor*> AllActor);

	void AddMenuSelectButton(FMenuBuilder& Builder);
	
	TSharedRef<FExtender>	GetPathFromEditor(const TArray<FString>& NewPaths);

	
	
private:
	// agent statement/creat

	TSharedPtr<class FUICommandList> PluginCommands;

	FDelegateHandle LevelViewportMenuExtender_SelectedActors;
};
