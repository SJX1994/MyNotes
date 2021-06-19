// Copyright Epic Games, Inc. All Rights Reserved.

#include "VFXIntegratorCommands.h"

#define LOCTEXT_NAMESPACE "FVFXIntegratorModule"

void FVFXIntegratorCommands::RegisterCommands()
{
	UI_COMMAND(OpenPluginWindow, "VFXIntegrator", "Bring up VFXIntegrator window", EUserInterfaceActionType::Button, FInputGesture());
}

#undef LOCTEXT_NAMESPACE
