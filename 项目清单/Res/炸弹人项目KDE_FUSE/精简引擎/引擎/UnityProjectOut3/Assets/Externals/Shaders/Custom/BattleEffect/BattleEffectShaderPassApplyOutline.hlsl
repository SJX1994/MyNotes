//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_BATTLE_EFFECT_SHADER_PASS_APPLY_OUTLINE
#define INCLUDE_GUARD_BATTLE_EFFECT_SHADER_PASS_APPLY_OUTLINE

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
	half4 color : SV_Target0;			//Adia
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
	float2 uvOffset = getBattleEffectParamTextureUVOffset();
	float4 param = 0;
	//Adia
	param = collectOutlineParam(param, tex2D(_battleEffectParamTexture, i.uv + uvOffset * float2( 0.0f,  0.0f)));

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

	half4 color = getOutlineColorFromTex(param);
	color.a = saturate(color.a * param.a * ALPHA_RATE);
	o.color = color;
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
