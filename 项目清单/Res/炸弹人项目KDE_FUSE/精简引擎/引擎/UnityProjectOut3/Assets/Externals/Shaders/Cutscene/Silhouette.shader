//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
Shader "Hidden/Fuse/Custom/Cutscene/Silhouette"
{
	SubShader
	{
		Cull Back
		ZWrite Off
		ZTest Equal

		//Adia
		//Adia
		Pass
		{
			Name "Default"

			HLSLPROGRAM

			//Adia
			//Adia
			//Adia

			#pragma multi_compile_instancing		//Adia

			#pragma vertex 		vsMain
			#pragma fragment 	psMain

			#include "SilhouettePassDefault.hlsl"

			ENDHLSL
		}
	}
}
//Adia
//Adia
//Adia
