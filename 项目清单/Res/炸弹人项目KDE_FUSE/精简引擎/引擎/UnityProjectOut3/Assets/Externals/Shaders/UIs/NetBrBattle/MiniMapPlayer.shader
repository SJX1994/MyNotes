//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
Shader "Fuse/Custom/UI/MiniMapPlayer"
{
	Properties
	{
		_MainTex	("Texture",	2D)		= "white" {}
		_Color		("Color",	Color)	= (0.5, 0.5, 0.5, 0.5)
	}
	
	SubShader
	{
		Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha

		//Adia
		//Adia
		Pass
		{
			Name "MiniMapPlayer"
			Tags {
				"LightMode" = "MiniMapPlayer"
			}

			HLSLPROGRAM

			//Adia
			//Adia
			//Adia

			#pragma multi_compile_instancing		//Adia

			#pragma vertex 		vsMain
			#pragma fragment 	psMain

			#include "MiniMapPlayerShaderPass.hlsl"

			ENDHLSL
		}
	}
}
//Adia
//Adia
//Adia
