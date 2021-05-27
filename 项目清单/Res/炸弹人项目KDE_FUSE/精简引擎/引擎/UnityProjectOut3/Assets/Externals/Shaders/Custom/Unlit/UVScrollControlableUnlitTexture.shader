//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
Shader "Fuse/Custom/Unlit/UVScrollControlableUnlitTexture"
{
	Properties
	{
		_MainTex	("Texture",		2D)			= "white" {}	//Adia
		_Color		("Color",		Color)		= (1, 1, 1, 1)	//Adia
		_AlphaClip	("AlphaClip",	Float)		= 0.5			//Adia
	}

	SubShader
	{
		//Adia
		//Adia
		Pass
		{
			ZWrite	On
			ZTest	LEqual
			BlendOp	Add
			Blend	SrcAlpha	OneMinusSrcAlpha

			HLSLPROGRAM

			//Adia
			//Adia
			//Adia

			#pragma vertex 		vsMain
			#pragma fragment 	psMain

			#include "UVScrollControlableUnlitTextureShaderPassMain.hlsl"

			ENDHLSL
		}
	}
}
//Adia
//Adia
//Adia
