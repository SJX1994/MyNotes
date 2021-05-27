//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
Shader "Fuse/Custom/BattleEffect"
{
	Properties
	{
		_teamColorTexture					("TeamColorTexture",			2D)			= "white" {}

		[HDR] _opticalCamouflageRimColor	("OpticalCamouflageRimColor",	Color)		= (0, 0, 0, 0)
		_opticalCamouflageParam				("OpticalCamouflageParam",		Float)		= (15, 4, 0, 0)

		_heartbeatCurveTexture				("HeartbeatCurveTexture",		2D)			= "white" {}
		_heartbeatDuration					("HeartbeatDuration",			Float)		= 2
		_heartbeatColor						("HeartbeatColor",				Color)		= (1, 0, 0, 1)

		_getItemPatternTexture				("GetItemPatternTexture",		2D)			= "white" {}
		_getItemCurveTexture				("GetItemCurveTexture",			2D)			= "white" {}
		_getItemUvAnimParam					("GetItemUvAnimParam",			Vector)		= (0, 0, 0, 0)
		_getItemParam						("GetItemParam",				Vector)		= (0, 0, 0, 0)
		_getItemColorStrength				("GetItemColorStrength",		Float)		= 1

		_StencilWriteMask					("StencilWriteMask",			Int)		= 1
	}
	
	SubShader
	{
		//Adia
		//Adia
		Pass
		{
			Name "ClearTarget"
			Tags {
				"LightMode" = "ClearTarget"
			}

			ZWrite	False
			ZTest	Always
			Cull	Off
			Blend	One		Zero

			Stencil {
				WriteMask	255
				Ref			0
				Comp		Always
				Pass		Zero
				ZFail		Zero
			}

			HLSLPROGRAM

			//Adia
			//Adia
			//Adia

			#pragma vertex 		vsMain
			#pragma fragment 	psMain

			#include "BattleEffectShaderPassClearTarget.hlsl"

			ENDHLSL
		}

		//Adia
		//Adia
		Pass
		{
			Name "BattleEffectParam"
			Tags {
				"LightMode" = "BattleEffectParam"
			}

			ZWrite	True
			ZTest	LEqual
			Cull	Back
			Blend	One		Zero

			Stencil {
				ReadMask	0
				WriteMask	[_StencilWriteMask]
				Ref			255
				Comp		Always
				Pass		Replace
			}

			HLSLPROGRAM

			//Adia
			//Adia
			//Adia

			#pragma multi_compile_instancing		//Adia


			#pragma vertex 		vsMain
			#pragma fragment 	psMain

			#include "BattleEffectShaderPassBattleEffectParam.hlsl"

			ENDHLSL
		}

		//Adia
		//Adia
		Pass
		{
			Name "BlurBattleEffectParam"
			Tags {
				"LightMode" = "BlurBattleEffectParam"
			}

			ZWrite	False
			ZTest	Always
			Blend	One		Zero

			Stencil {
				ReadMask	2
				WriteMask	1
				Ref			3
				Comp		NotEqual
				Pass		Replace
			}

			HLSLPROGRAM

			//Adia
			//Adia
			//Adia

			#pragma multi_compile_instancing		//Adia

			#pragma vertex 		vsMain
			#pragma fragment 	psMain

			#include "BattleEffectShaderPassBlurBattleEffectParam.hlsl"

			ENDHLSL
		}

		//Adia
		//Adia
		Pass
		{
			Name "ApplyOutline"
			Tags {
				"LightMode" = "ApplyOutline"
			}

			ZWrite	False
			ZTest	Always
			Cull	Back
			Blend	SrcAlpha	OneMinusSrcAlpha

			Stencil {
				ReadMask	1
				WriteMask	1
				Ref			1
				Comp		Equal
				Pass		Zero
			}

			HLSLPROGRAM

			//Adia
			//Adia
			//Adia

			#pragma multi_compile_instancing		//Adia

			#pragma vertex 		vsMain
			#pragma fragment 	psMain

			#include "BattleEffectShaderPassApplyOutline.hlsl"

			ENDHLSL
		}

		//Adia
		//Adia
		Pass
		{
			Name "ApplyOpticalCamouflage"
			Tags {
				"LightMode" = "ApplyOpticalCamouflage"
			}

			ZWrite	False
			ZTest	Always
			Cull	Back
			Blend	SrcAlpha	OneMinusSrcAlpha

			Stencil {
				ReadMask	4
				WriteMask	4
				Ref			4
				Comp		Equal
				Pass		Zero
			}

			HLSLPROGRAM

			//Adia
			//Adia
			//Adia

			#pragma multi_compile_instancing		//Adia

			#pragma vertex 		vsMain
			#pragma fragment 	psMain

			#include "BattleEffectShaderPassApplyOpticalCamouflage.hlsl"

			ENDHLSL
		}

		//Adia
		//Adia
		Pass
		{
			Name "ApplyHeartbeat"
			Tags {
				"LightMode" = "ApplyHeartbeat"
			}

			ZWrite	False
			ZTest	Always
			Cull	Back
			BlendOp	Add
			Blend	SrcAlpha	OneMinusSrcAlpha

			Stencil {
				ReadMask	8
				WriteMask	8
				Ref			8
				Comp		Equal
				Pass		Zero
			}

			HLSLPROGRAM

			//Adia
			//Adia
			//Adia

			#pragma multi_compile_instancing		//Adia

			#pragma vertex 		vsMain
			#pragma fragment 	psMain

			#include "BattleEffectShaderPassApplyHeartbeat.hlsl"

			ENDHLSL
		}

		//Adia
		//Adia
		Pass
		{
			Name "ApplyGetItem"
			Tags {
				"LightMode" = "ApplyGetItem"
			}

			ZWrite	False
			ZTest	Always
			Cull	Back
			BlendOp	Add
			Blend	SrcAlpha	OneMinusSrcAlpha

			Stencil {
				ReadMask	16
				WriteMask	16
				Ref			16
				Comp		Equal
				Pass		Zero
			}

			HLSLPROGRAM

			//Adia
			//Adia
			//Adia

			#pragma multi_compile_instancing		//Adia

			#pragma vertex 		vsMain
			#pragma fragment 	psMain

			#include "BattleEffectShaderPassApplyGetItem.hlsl"

			ENDHLSL
		}
	}
	CustomEditor "BattleEffectShaderGUI"
}
//Adia
//Adia
//Adia
