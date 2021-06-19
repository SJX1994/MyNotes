//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_BATTLE_EFFECT_SHADER_PASS_APPLY_GET_ITEM
#define INCLUDE_GUARD_BATTLE_EFFECT_SHADER_PASS_APPLY_GET_ITEM

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
	float4	position	: POSITION;		//Adia
	float2	uv			: TEXCOORD0;	//Adia
	UNITY_VERTEX_INPUT_INSTANCE_ID		//Adia
};

//Adia
//Adia
//Adia
//Adia
struct v2p
{
	float4	position		: SV_Position;	//Adia
	float2	uv				: TEXCOORD0;	//Adia
	float	normalizedTime	: TEXCOORD2;	//Adia
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

sampler2D	_getItemPatternTexture;						//Adia
float4		_getItemPatternTexture_ST;					//Adia
sampler2D	_getItemCurveTexture;						//Adia
float4		_getItemUvAnimParam;						//Adia
float		_getItemColorStrength;						//Adia

UNITY_INSTANCING_BUFFER_START(Props)
UNITY_DEFINE_INSTANCED_PROP(float4, _getItemParam)		//Adia
UNITY_INSTANCING_BUFFER_END(Props)

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
	o.position			= clipPos;
	o.uv				= TRANSFORM_TEX(i.uv, _getItemPatternTexture);
	o.normalizedTime	= UNITY_ACCESS_INSTANCED_PROP(Props, _getItemParam).x;
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
void psMain(in v2p i, out pout o)
{
	float2 uvOffset = _getItemUvAnimParam.xy * i.normalizedTime;
	float dispRate = tex2D(_getItemCurveTexture, float2(i.normalizedTime, 0.5)).r;
	float4 color = tex2D(_getItemPatternTexture, i.uv + uvOffset);
	float4 outColor = color * dispRate * _getItemColorStrength;
	outColor.a = saturate(outColor.a);
	o.color		= outColor;
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
