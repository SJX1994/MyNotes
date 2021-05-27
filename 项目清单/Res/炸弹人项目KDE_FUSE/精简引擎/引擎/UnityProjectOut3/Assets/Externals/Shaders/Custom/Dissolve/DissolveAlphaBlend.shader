//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

Shader "Fuse/Custom/Dissolve/Dissolve AlphaBlend"
{
	Properties
	{
		_MainTex				("Texture",					2D)					= "white" {}
		_DissolveMap			("DissolveMap",				2D)					= "white" {}
		_GradientRange			("GradientRange",			Range(0.0, 1.0))	= 0.2
		_DissolveThreshold		("DissolveThreshold",		Range(0.0, 1.0))	= 0.01
		_TintColor				("Color",					Color)				= (0, 0, 0, 1)

		_ColorStrength			("ColorStrength",			Float)				= 1
		_TextureTilingOffset	("TextureTilingOffset",		Vector)				= (1, 1, 0, 0)
	}

	SubShader
	{
		Tags
		{
			"Queue"				= "Transparent"
			"IgnoreProjector"	= "True"
			"RenderType"		= "Transparent"
			"PreviewType"		= "Plane"
		}

		//Adia
		//Adia
		Pass
		{
			Blend			SrcAlpha	OneMinusSrcAlpha
			ColorMask		RGB
			Cull			Off
			Lighting		Off
			ZWrite			Off
			ZTest			LEqual

			HLSLPROGRAM
			#pragma multi_compile_instancing
			#pragma multi_compile _ HX_BILLBOARD
			#pragma multi_compile _ HX_TEXTURE_TILING_OFFSET

			//Adia
			//Adia
			//Adia

			#pragma vertex 		vsMain
			#pragma fragment 	psMain

			#include "DissolveShaderPassMain.hlsl"

			ENDHLSL
		}
	}
}
//Adia
//Adia
//Adia
