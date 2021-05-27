//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_BATTLE_EFFECT_SHADER_PASS_CLEAR_TARGET
#define INCLUDE_GUARD_BATTLE_EFFECT_SHADER_PASS_CLEAR_TARGET

//Adia
//Adia
//Adia
//Adia

#include "UnityCG.cginc"

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
	float4 position : POSITION;		//Adia
};

//Adia
//Adia
//Adia
//Adia
struct v2p
{
	float4 position : SV_Position;	//Adia
};

//Adia
//Adia
//Adia
//Adia
struct pout
{
	half4 color		: SV_Target0;		//Adia
	half4 color1	: SV_Target1;		//Adia
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
	o.position = mul(unity_ObjectToWorld, i.position);
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
void psMain(in v2p i, out pout o)
{
	o.color = float4(0, 0, 0, 0);
	o.color1 = float4(0, 0, 0, 0);
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
