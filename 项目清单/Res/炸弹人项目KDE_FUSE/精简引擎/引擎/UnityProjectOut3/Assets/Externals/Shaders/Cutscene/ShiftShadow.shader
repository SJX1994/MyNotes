//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
Shader "Fuse/Custom/Cutscene/ShiftShadow"
{
	Properties
	{
		[PerRendererData] _MainTex						("Sprite Texture",			2D)				= "white" {}
		_Color											("Tint",					Color)			= (1,1,1,1)

		_StencilComp									("Stencil Comparison",		Float)			= 8
		_Stencil										("Stencil ID",				Float)			= 0
		_StencilOp										("Stencil Operation",		Float)			= 0
		_StencilWriteMask								("Stencil Write Mask",		Float)			= 255
		_StencilReadMask								("Stencil Read Mask",		Float)			= 255

		_ColorMask										("Color Mask",				Float)			= 15

		_mainTex_DT										("Depth Texture",			2D)				= "white" {}
		_shiftShadowOffsetX								("Shift Shadow Offset X",	Range(-1, 1))	= 0
		_shiftShadowOffsetY								("Shift Shadow Offset Y",	Range(-1, 1))	= 0
		[HDR] _shiftShadowColor							("Shift Shadow Color",		Color)			= (0, 0, 0, 1)

		_outlineColor									("Outline Color",			Color)			= (0, 0, 0, 1)
	}
	
	SubShader
	{
		Tags
		{
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Stencil
		{
			Ref			[_Stencil]
			Comp		[_StencilComp]
			Pass		[_StencilOp]
			ReadMask	[_StencilReadMask]
			WriteMask	[_StencilWriteMask]
		}

		Cull		Off
		Lighting	Off
		ZWrite		Off
		ZTest		[unity_GUIZTestMode]
		Blend		One Zero
		ColorMask	[_ColorMask]

		//Adia
		//Adia
		Pass
		{
			Name "JudgeOutline"
	
			HLSLPROGRAM
	
			//Adia
			//Adia
			//Adia
	
			#pragma vertex 		vsMain
			#pragma fragment 	psMain
	
			#include "ShiftShadowShaderPassJudgeOutline.hlsl"
	
			ENDHLSL
		}

		//Adia
		//Adia
		GrabPass
		{
			"_silhouetteGrabTex"
		}

		//Adia
		//Adia
		Pass
		{
			Name "Draw"

			HLSLPROGRAM

			//Adia
			//Adia
			//Adia

			#pragma multi_compile _ SILHOUETTE_CLIP

			#pragma vertex 		vsMain
			#pragma fragment 	psMain

			#include "ShiftShadowShaderPassDraw.hlsl"

			ENDHLSL
		}
	}
}
//Adia
//Adia
//Adia
