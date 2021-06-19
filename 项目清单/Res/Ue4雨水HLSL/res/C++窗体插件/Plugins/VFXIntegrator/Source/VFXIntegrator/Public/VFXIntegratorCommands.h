// Copyright Epic Games, Inc. All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "Framework/Commands/Commands.h"
#include "VFXIntegratorStyle.h"

class FVFXIntegratorCommands : public TCommands<FVFXIntegratorCommands>
{
public:

	FVFXIntegratorCommands()
		: TCommands<FVFXIntegratorCommands>(TEXT("VFXIntegrator"), NSLOCTEXT("Contexts", "VFXIntegrator", "VFXIntegrator Plugin"), NAME_None, FVFXIntegratorStyle::GetStyleSetName())
	{
	}

	// TCommands<> interface
	virtual void RegisterCommands() override;

public:
	TSharedPtr< FUICommandInfo > OpenPluginWindow;
};