//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_BATTLE_EFFECT_SHADER_PASS_BLUR_BATTL_EEFFECT_PARAM
#define INCLUDE_GUARD_BATTLE_EFFECT_SHADER_PASS_BLUR_BATTL_EEFFECT_PARAM

//Adia
//Adia
//Adia
//Adia

#include "UnityCG.cginc"
#include "BattleEffectCommon.hlsl"

//Adia


//Adia
//Adia
//Adia
//Adia

//Adia
//Adia
//Adia
//Adia
struct vin
{
	float4 position : POSITION;			//Adia
	UNITY_VERTEX_INPUT_INSTANCE_ID		//Adia
};

//Adia
//Adia
//Adia
//Adia
struct v2p
{
	float4	position	: SV_Position;	//Adia
	float2	uv			: TEXCOORD0;	//Adia
};

//Adia
//Adia
//Adia
//Adia
struct pout
{
	//Adia
	//Adia
	//Adia
	//Adia
	//Adia
	half4 outlineParam : SV_Target0;
};

//Adia


//Adia
//Adia
//Adia
//Adia

#define ALPHA_RATE		(1.0f / 4)		//Adia

//Adia


//Adia
//Adia
//Adia
//Adia

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
void vsMain(in vin i, out v2p o)
{
	UNITY_SETUP_INSTANCE_ID(i);

	float4 clipPos = UnityObjectToClipPos(i.position);
	o.position = clipPos;
	o.uv = clipPosToScreenUv(clipPos);
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
void psMain(in v2p i, out pout o)
{
	float4 param = 0;
	float2 uvOffset = getBattleEffectParamTextureUVOffset();

	//Adia
	param = collectOutlineParam(param, tex2D(_battleEffectParamTexture, i.uv));

	//Adia
	param = collectOutlineParam(param, tex2D(_battleEffectParamTexture, i.uv + uvOffset * float2( 1.0f,  0.0f)));
	param = collectOutlineParam(param, tex2D(_battleEffectParamTexture, i.uv + uvOffset * float2(-1.0f,  0.0f)));
	param = collectOutlineParam(param, tex2D(_battleEffectParamTexture, i.uv + uvOffset * float2( 0.0f,  1.0f)));
	param = collectOutlineParam(param, tex2D(_battleEffectParamTexture, i.uv + uvOffset * float2( 0.0f, -1.0f)));
	param = collectOutlineParam(param, tex2D(_battleEffectParamTexture, i.uv + uvOffset * float2( 1.0f,  1.0f)));
	param = collectOutlineParam(param, tex2D(_battleEffectParamTexture, i.uv + uvOffset * float2( 1.0f, -1.0f)));
	param = collectOutlineParam(param, tex2D(_battleEffectParamTexture, i.uv + uvOffset * float2(-1.0f,  1.0f)));
	param = collectOutlineParam(param, tex2D(_battleEffectParamTexture, i.uv + uvOffset * float2(-1.0f, -1.0f)));

	//Adia
	param = collectOutlineParam(param, tex2D(_battleEffectParamTexture, i.uv + uvOffset * float2( 2.0f,  0.0f)));
	param = collectOutlineParam(param, tex2D(_battleEffectParamTexture, i.uv + uvOffset * float2(-2.0f,  0.0f)));
	param = collectOutlineParam(param, tex2D(_battleEffectParamTexture, i.uv + uvOffset * float2( 0.0f,  2.0f)));
	param = collectOutlineParam(param, tex2D(_battleEffectParamTexture, i.uv + uvOffset * float2( 0.0f, -2.0f)));

	param.a = smoothstep(0, 1, param.a * ALPHA_RATE);
	o.outlineParam = param;
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
