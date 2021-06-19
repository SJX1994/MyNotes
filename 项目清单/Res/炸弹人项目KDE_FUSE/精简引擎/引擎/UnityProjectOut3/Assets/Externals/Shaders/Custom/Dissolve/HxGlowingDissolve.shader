//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

Shader "Fuse/Custom/Dissolve/GlowingDissolve"
{
	Properties
	{
		_MainTex									("Texture",							2D)				= "white" {}
		[HDR] _TintColor							("Tint Color",						Color)			= (0.5,0.5,0.5,0.5)
		_NoiseTex									("Dissolve Map",					2D)				= "white" {}
		[KeywordEnum(UV0, UV1)] _HX_DISSOLVE_MAP	("Dissolve Map UV",					Float)			= 0
		_ParamTex									("Param Texture",					2D)				= "white" {}
		_NormalizedTime								("Normalized Time",					Range(0, 1))	= 0
		_PeakRange									("Peak Range",						Range(0, 1))	= 0.1
		_PeakStrength								("Peak Strength",					Float)			= 3
		_TexLuminanceDissolveThreshold				("TexLuminance Dissolve Threshold",	Range(0, 1))	= 0.02
		[KeywordEnum(OFF, ON)] _HX_DISSOLVE_CUTOUT	("Cutout",							Float)			= 0
		_CutoutThreshold							("CutoutThreshold",					Range(0, 1))	= 0
	}

	SubShader
	{
		Tags {
			"RenderType"	= "Opaque"
			"Queue"			= "Geometry"
		}

		//Adia
		//Adia
		Pass
		{
			Name "FORWARD"

			Blend			SrcAlpha OneMinusSrcAlpha
			ColorMask		RGB
			Cull			Back
			Lighting		Off
			ZWrite			On
			ZTest			LEqual

			HLSLPROGRAM
			#pragma target 5.0
			#pragma multi_compile_instancing
			#pragma multi_compile __ _HX_DISSOLVE_MAP_UV1
			#pragma multi_compile __ _HX_DISSOLVE_CUTOUT_ON

			//Adia
			//Adia
			//Adia

			#pragma vertex 		vsMain
			#pragma fragment 	psMain

			#include "HxGlowingDissolveShaderPassMain.hlsl"

			ENDHLSL
		}
	}
}
//Adia
//Adia
//Adia
