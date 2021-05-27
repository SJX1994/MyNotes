//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_BATTLE_EFFECT_SHADER_PASS_APPLY_HEARTBEAT
#define INCLUDE_GUARD_BATTLE_EFFECT_SHADER_PASS_APPLY_HEARTBEAT

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
	half4 color : SV_Target0;	//Adia
};

//Adia


//Adia
//Adia
//Adia
//Adia

sampler2D	_gBufferNormal;				//Adia
sampler2D	_heartbeatCurveTexture;		//Adia
float		_heartbeatDuration;			//Adia

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
	//Adia
	float alphaByDotNormalCameraVec = 0;
	float3 worldNormal = tex2D(_gBufferNormal, i.uv) * 2 + 1;
	alphaByDotNormalCameraVec = dot(normalize(worldNormal), normalize(i.toCameraVec));
	alphaByDotNormalCameraVec = 1 - saturate(alphaByDotNormalCameraVec) * 0.1;

	//Adia
	float time = _Time.y;
	float peek = tex2D(_heartbeatCurveTexture, float2(time / _heartbeatDuration, 0.5f)).r;

	float alpha = alphaByDotNormalCameraVec * pow(peek, 4);
	alpha = saturate(alpha);
	o.color = float4(_heartbeatColor.rgb, _heartbeatColor.a * alpha);
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
