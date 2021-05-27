// Copyright Epic Games, Inc. All Rights Reserved.

#include "ExtendEditor.h"
#include "ExtendEditorStyle.h"
#include "ExtendEditorCommands.h"
#include "Misc/MessageDialog.h"
#include "ToolMenus.h"

#include "Framework/MultiBox/MultiBoxBuilder.h"

#include "LevelEditor.h"
#include "Engine/Engine.h"
#include "ContentBrowserModule.h"

static const FName ExtendEditorTabName("ExtendEditor");

#define LOCTEXT_NAMESPACE "FExtendEditorModule"

void FExtendEditorModule::StartupModule()
{
	// This code will execute after your module is loaded into memory; the exact timing is specified in the .uplugin file per-module
	
	//
	FExtendEditorStyle::Initialize();
	FExtendEditorStyle::ReloadTextures();
	//
	FExtendEditorCommands::Register();

	PluginCommands = MakeShareable(new FUICommandList);
	//
	PluginCommands->MapAction(
	    FExtendEditorCommands::Get().PluginAction,
	    FExecuteAction::CreateRaw(this, &FExtendEditorModule::PluginButtonClicked),
	    FCanExecuteAction());
	//
	FLevelEditorModule &LevelEditorModule = FModuleManager::LoadModuleChecked<FLevelEditorModule>("LevelEditor");
	//window
	{
		TSharedPtr<FExtender> MenuExtender = MakeShareable(new FExtender());
		MenuExtender->AddMenuExtension(
		    "WindowLayout",
		    EExtensionHook::After,
		    PluginCommands,
		    FMenuExtensionDelegate::CreateRaw(this, &FExtendEditorModule::AddMenuExtension));
		LevelEditorModule.GetMenuExtensibilityManager()->AddExtender(MenuExtender);
	}
	//
	{
		TSharedPtr<FExtender> MenuBarExtender = MakeShareable(new FExtender());
		MenuBarExtender->AddMenuBarExtension(
		    "Help",
		    EExtensionHook::After,
		    PluginCommands,
		    FMenuBarExtensionDelegate::CreateRaw(this, &FExtendEditorModule::AddMenuBarExtension));
		LevelEditorModule.GetMenuExtensibilityManager()->AddExtender(MenuBarExtender);
	}
	//
	{
		TSharedPtr<FExtender> ToolbarExtender = MakeShareable(new FExtender);
		ToolbarExtender->AddToolBarExtension(
		    "Game", EExtensionHook::After,
		    PluginCommands,
		    FToolBarExtensionDelegate::CreateRaw(this, &FExtendEditorModule::AddToolBarExtension));

		LevelEditorModule.GetToolBarExtensibilityManager()->AddExtender(ToolbarExtender);
	}
	//UToolMenus::RegisterStartupCallback(FSimpleMulticastDelegate::FDelegate::CreateRaw(this, &FExtendEditorModule::RegisterMenus));
	
	//sense menu

	{
		auto& MenuButtonArray = LevelEditorModule.GetAllLevelViewportContextMenuExtenders();
		MenuButtonArray.Add(FLevelEditorModule::FLevelViewportMenuExtender_SelectedActors::CreateRaw(this, &FExtendEditorModule::SelectedCurrentActors));
		//save agent
		LevelViewportMenuExtender_SelectedActors = MenuButtonArray.Last().GetHandle();
	}
	//folder menu
	FContentBrowserModule& ContentBrowserModule = FModuleManager::LoadModuleChecked<FContentBrowserModule>("ContentBrowser");
	{
		auto& MenuExtenderDelegates = ContentBrowserModule.GetAllPathViewContextMenuExtenders();
		MenuExtenderDelegates.Add(FContentBrowserMenuExtender_SelectedPaths::CreateRaw(this, &FExtendEditorModule::GetPathFromEditor));
	}

}

void FExtendEditorModule::ShutdownModule()
{
	// This function may be called during shutdown to clean up your module.  For modules that support dynamic reloading,
	// we call this function before unloading the module.

	UToolMenus::UnRegisterStartupCallback(this);

	UToolMenus::UnregisterOwner(this);

	FExtendEditorStyle::Shutdown();

	FExtendEditorCommands::Unregister();
}

void FExtendEditorModule::PluginButtonClicked()
{
	FString var = TEXT("adiaBtn been Clicked");

	// Put your "OnButtonClicked" stuff here
	FText DialogText = FText::Format(
	    LOCTEXT("PluginButtonDialogText", "Add codes to {0} in {1} to override this button's actions"),
	    FText::FromString(TEXT("FExtendEditorModule::PluginButtonClicked()")),
	    FText::FromString(TEXT("ExtendEditor.cpp")));
	UE_LOG(LogTemp,Warning,TEXT("address is is is : %s"),*var);
	FMessageDialog::Open(EAppMsgType::Ok, DialogText);
}


void FExtendEditorModule::RegisterMenus()
{
	// Owner will be used for cleanup in call to UToolMenus::UnregisterOwner
	FToolMenuOwnerScoped OwnerScoped(this);
	//
	{
		UToolMenu *Menu = UToolMenus::Get()->ExtendMenu("LevelEditor.MainMenu.Window");
		{
			FToolMenuSection &Section = Menu->FindOrAddSection("WindowLayout");

			Section.AddMenuEntryWithCommandList(FExtendEditorCommands::Get().PluginAction, PluginCommands);
		}
	}
	//
	{
		UToolMenu *ToolbarMenu = UToolMenus::Get()->ExtendMenu("LevelEditor.LevelEditorToolBar");
		{
			FToolMenuSection &Section = ToolbarMenu->FindOrAddSection("Game");
			{
				FToolMenuEntry &Entry = Section.AddEntry(FToolMenuEntry::InitToolBarButton(FExtendEditorCommands::Get().PluginAction));
				Entry.SetCommandList(PluginCommands);
			}
		}
	}
	//
	{
		//todo
	}
}

void FExtendEditorModule::AddMenuExtension(FMenuBuilder &Builder)
{
	//
	Builder.BeginSection(TEXT("MyFancyPlugIn"));
	{
		Builder.AddMenuSeparator();
		//
		Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
		Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
		Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
		Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
	}

	Builder.EndSection();
}

void FExtendEditorModule::AddMenuBarExtension(FMenuBarBuilder &Builder)
{
	Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
	Builder.AddPullDownMenu(
		LOCTEXT("ADIA","adiaPlugins"),
		LOCTEXT("ADIA_DESCRIPTION","made by 752523247"),
		FNewMenuDelegate::CreateRaw(this,&FExtendEditorModule::PutDownBar),
		"ADIA1785"
	);
}

void FExtendEditorModule::AddToolBarExtension(FToolBarBuilder &Builder)
{
	Builder.AddToolBarButton(FExtendEditorCommands::Get().PluginAction);

	Builder.BeginSection("AdiaTools");
	Builder.BeginBlockGroup();
	{
		Builder.AddToolBarButton(
			FExtendEditorCommands::Get(). PluginAction,
			TEXT("AdiaAppOneone"),
			TAttribute<FText>(),
			TAttribute<FText>(),
			TAttribute<FSlateIcon>(),
			TEXT("AdiaAppOne")
			
		);
	}
	Builder.EndBlockGroup();
	Builder.EndSection();
}

//archive: ↓↓↓

void FExtendEditorModule::PutDownBar(FMenuBuilder& Builder)
{
	Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
	Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
	Builder.AddMenuSeparator();
	Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
	Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
	Builder.AddSubMenu(
		LOCTEXT("TESTTEST","testTest"),
		LOCTEXT("LOOP","loop"),
		FNewMenuDelegate::CreateRaw(this, &FExtendEditorModule::PutDownSubBar),
		false
		);
	Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
	Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
}

void FExtendEditorModule::PutDownSubBar(FMenuBuilder& Builder)
{
	Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
	Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);

	Builder.BeginSection(TEXT("MyFancyPlugIn"));
	{
		Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
		Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
	}
	Builder.EndSection();
	Builder.AddMenuSeparator();
	Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
	Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
}

void FExtendEditorModule::EditorPrint(FString& MyString)
{
	if (GEngine)
	{
		GEngine->AddOnScreenDebugMessage(-1, 50.f, FColor::Green, MyString);
	}
}

TSharedRef<FExtender> FExtendEditorModule::SelectedCurrentActors(const TSharedRef<FUICommandList> MyUICommandList, const TArray<AActor*> AllActor)
{
	TSharedRef<FExtender> Extender = MakeShareable(new FExtender);
	
	if (AllActor.Num() > 0)
	{
		FString TestHUDString = FString(TEXT("Adia_Log:\n you have selected "));
		TestHUDString += FString::FromInt(AllActor.Num());
		TestHUDString += FString(TEXT(" objects in sence "));
		EditorPrint(TestHUDString);
		// select actor
		FLevelEditorModule& LevelEditorModule = FModuleManager::LoadModuleChecked<FLevelEditorModule>("LevelEditor");
		TSharedRef<FUICommandList> LevelCommand = LevelEditorModule.GetGlobalLevelEditorActions();

		Extender->AddMenuExtension(
			"ActorControl",
			EExtensionHook::After,
			LevelCommand,
			FMenuExtensionDelegate::CreateRaw(this, &FExtendEditorModule::AddMenuSelectButton));
		
		
	}

	return Extender;
}

void FExtendEditorModule::AddMenuSelectButton(FMenuBuilder& Builder)
{
	Builder.AddMenuEntry(FExtendEditorCommands::Get().PluginAction);
}

TSharedRef<FExtender> FExtendEditorModule::GetPathFromEditor(const TArray<FString>& NewPaths)
{
	TSharedRef<FExtender> Extender = MakeShareable(new FExtender);

	if (NewPaths.Num() > 0)
	{
		FString TestHUDString = FString(TEXT("Adia_Log:\n you have selected "));
		TestHUDString += FString::FromInt(NewPaths.Num());
		TestHUDString += FString(TEXT(" file in content "));
		EditorPrint(TestHUDString);
		// select actor
		FLevelEditorModule& LevelEditorModule = FModuleManager::LoadModuleChecked<FLevelEditorModule>("ContentBrowser");

		Extender->AddMenuExtension(
			"NewFolder",
			EExtensionHook::After,
			PluginCommands,
			FMenuExtensionDelegate::CreateRaw(this, &FExtendEditorModule::AddMenuSelectButton)
		);


	}

	for (auto Tmp : NewPaths)
	{
		EditorPrint(Tmp);
	}

	return Extender;
}



//archive: ↑↑↑
#undef LOCTEXT_NAMESPACE

IMPLEMENT_MODULE(FExtendEditorModule, ExtendEditor)