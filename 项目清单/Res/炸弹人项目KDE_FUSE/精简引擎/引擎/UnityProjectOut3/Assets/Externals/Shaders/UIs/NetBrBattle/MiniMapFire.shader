//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
Shader "Fuse/Custom/UI/MiniMapFire"
{
	Properties
	{
		_MainTex	("Texture",		2D)			= "white" {}
		_Color		("Color",		Color)		= (0.5, 0.5, 0.5, 0.5)
		_uvOffset	("UV Offset",	Vector)		= (0.0, 0.0, 0.0, 0.0)
	}
	
	SubShader
	{
		//Adia
		//Adia
		Pass
		{
			Name "MiniMapFire"
			Tags {
				"LightMode" = "MiniMapFire"
			}

			Blend SrcAlpha OneMinusSrcAlpha

			HLSLPROGRAM

			//Adia
			//Adia
			#pragma enable_d3d11_debug_symbols

			#pragma multi_compile_instancing		//Adia

			#pragma vertex 		vsMain
			#pragma fragment 	psMain

			#include "MiniMapFireShaderPass.hlsl"

			ENDHLSL
		}
	}
}
//Adia
//Adia
//Adia
