//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_BATTLE_EFFECT_SHADER_PASS_OUTPUT_OUTLINE_PARAM
#define INCLUDE_GUARD_BATTLE_EFFECT_SHADER_PASS_OUTPUT_OUTLINE_PARAM

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
	float4 normal	: NORMAL;			//Adia
	UNITY_VERTEX_INPUT_INSTANCE_ID		//Adia
};

//Adia
//Adia
//Adia
//Adia
struct v2p
{
	float4	position	: SV_Position;	//Adia
	float4	normal		: TEXCOORD0;	//Adia
	float4	clipPos		: TEXCOORD1;	//Adia
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
	half4 battleEffectParam : SV_Target0;

	half4 normal			: SV_Target1;		//Adia
};

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
	o.clipPos = clipPos;
	o.normal = mul(unity_ObjectToWorld, float4(i.normal.xyz, 0.0f));
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
void psMain(in v2p i, out pout o)
{
	o.battleEffectParam = createBattleEffectParam(i.clipPos);

	float3 normal = normalize(i.normal.xyz) * 0.5f + 0.5f;
	o.normal = float4(normal, 1.0f);
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
