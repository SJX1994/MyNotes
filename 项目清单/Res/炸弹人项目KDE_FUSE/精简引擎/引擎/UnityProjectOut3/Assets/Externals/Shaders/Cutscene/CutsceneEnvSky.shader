//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
Shader "Fuse/Custom/Cutscene/CutsceneEnvSky"
{
	Properties
	{
		_MainTex	("_Texture",	2D)		= "white" {}
		[HDR] _Color	("_Color",		Color)	= (1,1,1,1)
		[HDR] _bgColor	("_bgColor",	Color)	= (1,1,1,1)
	}
	
	SubShader
	{
		//Adia
		//Adia
		Pass
		{
			Name "Default"
			Tags {
				"LightMode" = "Always"
			}

			HLSLPROGRAM

			//Adia
			//Adia
			//Adia

			#pragma vertex		vsMain
			#pragma fragment	psMain

			#include "CutsceneEnvSkyShaderPassDefault.hlsl"

			ENDHLSL
		}
	}
}
//Adia
//Adia
//Adia
