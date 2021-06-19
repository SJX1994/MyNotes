//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
Shader "Fuse/Custom/UI/UIFont(Default)"
{
	Properties
	{
		_MainTex											("Base(RGB), Alpha(A)",						2D)				= "white" {}			//Adia
		[HDR] _MainColor									("Main Color",								Color) 			= (1.0, 1.0, 1.0, 1.0)	//Adia
		_Spread												("DF Spread",								Float)			= 1.0					//Adia
		_MainAA												("Main Anti-aliasing Pixcel",				Range(0, 1))	= 0.07					//Adia

		[Toggle(TEXT_THICKNESS)] _ApplyThickness			("Apply Thickness Effect",					Float) 			= 0						//Adia

		[Toggle(UI_OVERLAY)] _ApplyOverlay					("Apply Overlay",							Float) 			= 0						//Adia
		_OverlayTex											("Overlay Texture",							2D) 			= "white" {}			//Adia
		_OverlayColor										("Overlay Color",							Color) 			= (1.0, 1.0, 1.0, 1.0)	//Adia
		_OverlayUVSampleMultiplier							("Overlay UV Sample Multiplier",			Float)			= 1.0					//Adia

		[Toggle(UI_SS_LINE_OVERLAY)] _ApplySSLineOverlay	("Apply SS Line Overlay",					Float) 			= 0						//Adia
		_SSLineOverlayTex									("SS Line Overlay Texture",					2D)				= "white" {}			//Adia
		_SSLineOverlayColor									("SS Line Overlay Color",					Color)			= (1.0, 1.0, 1.0, 1.0)	//Adia
		_SSLineOverlayUVSampleMultiplier					("SS Line Overlay UV Sample Multiplier",	Float)			= 1.0					//Adia
		_SSLineOverlayUVOffsetYIntensity					("SS Line Overlay UV Offset Y Intensity",	Float)			= 1.0					//Adia
		_SSLineOverlayUVFlowIntensity						("SS Line Overlay UV Flow Intensity",		Float)			= 1.0					//Adia

		//Adia
		[HideInInspector]_StencilComp( "Stencil Comparison", Float ) = 8
		[HideInInspector]_Stencil( "Stencil ID", Float ) = 0
		[HideInInspector]_StencilOp( "Stencil Operation", Float ) = 0
		[HideInInspector]_StencilWriteMask( "Stencil Write Mask", Float ) = 255
		[HideInInspector]_StencilReadMask( "Stencil Read Mask", Float ) = 255
		[HideInInspector]_ColorMask( "Color Mask", Float ) = 15
		//Adia
	}

	SubShader
	{
		Tags
		{
			"IgnoreProjector"	= "True"
			"RenderType"		= "Transparent"
			"Queue"				= "Transparent"
		}
		LOD 200

		//Adia
		Stencil{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
		}
		ColorMask[_ColorMask]
		//Adia

		//Adia
		//Adia
		Pass
		{
			Name "Default"

			Blend		SrcAlpha OneMinusSrcAlpha
			Cull		Off
			Lighting	Off
			ZWrite		Off
			ZTest[unity_GUIZTestMode]
			Fog
			{
				Mode		Off
			}

			HLSLPROGRAM

			//Adia
			//Adia
			//Adia

			#pragma vertex 		vsMain
			#pragma fragment 	psMain
			#pragma fragmentoption ARB_precision_hint_fastest	//Adia
			#pragma glsl_no_auto_normalization					//Adia
			//Adia
			#pragma multi_compile __ UNITY_UI_CLIP_RECT
			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			//Adia
			#pragma multi_compile __ TEXT_THICKNESS
			#pragma multi_compile __ UI_OVERLAY
			#pragma multi_compile __ UI_SS_LINE_OVERLAY

			#include "UIPackedFontDefaultPassDefault.hlsl"

			ENDHLSL
		}
	}
}
//Adia
//Adia
//Adia
