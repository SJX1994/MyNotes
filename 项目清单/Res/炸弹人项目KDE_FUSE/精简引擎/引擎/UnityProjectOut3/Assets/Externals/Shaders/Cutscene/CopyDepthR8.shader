//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
Shader "Hidden/Fuse/Custom/Cutscene/CopyDepthR8"
{
	SubShader
	{
		Cull Off
		ZWrite Off
		ZTest Always

		//Adia
		//Adia
		Pass
		{
			Name "Default"

			HLSLPROGRAM

			//Adia
			//Adia
			//Adia

			#pragma vertex 		vsMain
			#pragma fragment 	psMain

			#include "CopyDepthR8ShaderPassDefault.hlsl"

			ENDHLSL
		}
	}
}
//Adia
//Adia
//Adia
