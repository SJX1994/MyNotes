// Copyright Epic Games, Inc. All Rights Reserved.

#include "VFXIntegrator.h"
#include "VFXIntegratorStyle.h"
#include "VFXIntegratorCommands.h"
#include "LevelEditor.h"
#include "Widgets/Docking/SDockTab.h"
#include "Widgets/Layout/SBox.h"
#include "Widgets/Text/STextBlock.h"
#include "ToolMenus.h"
#include "Components/ScrollBox.h"
#include "Widgets/Layout/SGridPanel.h"
#include <AssetRegistryModule.h>
#include "Particles/ParticleSystem.h"
#include "SVFXIntegratorWidget.h"



static const FName VFXIntegratorTabName("VFXIntegrator");

#define LOCTEXT_NAMESPACE "FVFXIntegratorModule"

void FVFXIntegratorModule::StartupModule()
{
	// This code will execute after your module is loaded into memory; the exact timing is specified in the .uplugin file per-module
	
	FVFXIntegratorStyle::Initialize();
	FVFXIntegratorStyle::ReloadTextures();

	FVFXIntegratorCommands::Register();
	
	PluginCommands = MakeShareable(new FUICommandList);

	PluginCommands->MapAction(
		FVFXIntegratorCommands::Get().OpenPluginWindow,
		FExecuteAction::CreateRaw(this, &FVFXIntegratorModule::PluginButtonClicked),
		FCanExecuteAction());

	UToolMenus::RegisterStartupCallback(FSimpleMulticastDelegate::FDelegate::CreateRaw(this, &FVFXIntegratorModule::RegisterMenus));
	
	FGlobalTabmanager::Get()->RegisterNomadTabSpawner(VFXIntegratorTabName, FOnSpawnTab::CreateRaw(this, &FVFXIntegratorModule::OnSpawnPluginTab))
		.SetDisplayName(LOCTEXT("FVFXIntegratorTabTitle", "VFXIntegrator"))
		.SetMenuType(ETabSpawnerMenuType::Hidden);
}

void FVFXIntegratorModule::ShutdownModule()
{
	// This function may be called during shutdown to clean up your module.  For modules that support dynamic reloading,
	// we call this function before unloading the module.

	UToolMenus::UnRegisterStartupCallback(this);

	UToolMenus::UnregisterOwner(this);

	FVFXIntegratorStyle::Shutdown();

	FVFXIntegratorCommands::Unregister();

	FGlobalTabmanager::Get()->UnregisterNomadTabSpawner(VFXIntegratorTabName);
}

/*TSharedRef<ITableRow> FVFXIntegratorModule::OnGenerateRow()
{
	return
		SNew(SMultiColumnTableRow<UObject>);
}*/

TSharedRef<SDockTab> FVFXIntegratorModule::OnSpawnPluginTab(const FSpawnTabArgs& SpawnTabArgs)
{
	return SNew(SDockTab).TabRole(ETabRole::NomadTab)
		[
			SNew(SVFXIntegratorWidget)
		];
}

void FVFXIntegratorModule::PluginButtonClicked()
{
	FGlobalTabmanager::Get()->InvokeTab(VFXIntegratorTabName);
}

void FVFXIntegratorModule::RegisterMenus()
{
	// Owner will be used for cleanup in call to UToolMenus::UnregisterOwner
	FToolMenuOwnerScoped OwnerScoped(this);

	{
		UToolMenu* Menu = UToolMenus::Get()->ExtendMenu("LevelEditor.MainMenu.Window");
		{
			FToolMenuSection& Section = Menu->FindOrAddSection("WindowLayout");
			Section.AddMenuEntryWithCommandList(FVFXIntegratorCommands::Get().OpenPluginWindow, PluginCommands);
		}
	}

	{
		UToolMenu* ToolbarMenu = UToolMenus::Get()->ExtendMenu("LevelEditor.LevelEditorToolBar");
		{
			FToolMenuSection& Section = ToolbarMenu->FindOrAddSection("VFXIntegrator");
			{
				FToolMenuEntry& Entry = Section.AddEntry(FToolMenuEntry::InitToolBarButton(FVFXIntegratorCommands::Get().OpenPluginWindow));
				Entry.SetCommandList(PluginCommands);
			}
		}
	}
}

#undef LOCTEXT_NAMESPACE
	
IMPLEMENT_MODULE(FVFXIntegratorModule, VFXIntegrator)