//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_BATTLE_EFFECT_SHADER_PASS_APPLY_OPTICAL_CAMOUFLAGE
#define INCLUDE_GUARD_BATTLE_EFFECT_SHADER_PASS_APPLY_OPTICAL_CAMOUFLAGE

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
	float3	toCameraVec		: TEXCOORD1;	//Adia
};

//Adia
//Adia
//Adia
//Adia
struct pout
{
	half4 color		: SV_Target0;		//Adia
};

//Adia


//Adia
//Adia
//Adia
//Adia

sampler2D	_LightedBuffer;					//Adia
sampler2D	_WorldNormal;					//Adia

float4		_opticalCamouflageRimColor;		//Adia

//Adia
//Adia
//Adia
//Adia
float4		_opticalCamouflageParam;

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
	o.toCameraVec = _WorldSpaceCameraPos - mul(unity_ObjectToWorld, i.position).xyz;
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
	float3 worldNormal = normalize(tex2D(_WorldNormal, i.uv).xyz * 2.0f - 1.0f);
	float3 toCameraVec = normalize(i.toCameraVec);
	float d = pow(1 - saturate(dot(worldNormal, toCameraVec)), _opticalCamouflageParam.y);
	float3 viewNormal = normalize(mul(UNITY_MATRIX_V, float4(worldNormal, 0.0f)));

	float4 color = tex2D(_LightedBuffer, i.uv + (uvOffset * viewNormal.xy * _opticalCamouflageParam.x));
	color = lerp(color, _opticalCamouflageRimColor, d);
	o.color = color;
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
