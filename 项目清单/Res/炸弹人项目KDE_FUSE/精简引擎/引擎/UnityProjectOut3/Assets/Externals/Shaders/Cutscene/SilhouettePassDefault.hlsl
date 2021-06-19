//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_SILHOUETTE_PASS_DEFAULT
#define INCLUDE_GUARD_SILHOUETTE_PASS_DEFAULT

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
	float4	position	: POSITION;		//Adia
	UNITY_VERTEX_INPUT_INSTANCE_ID		//Adia
};

//Adia
//Adia
//Adia
//Adia
struct v2p
{
	float4	position	: SV_Position;	//Adia
};

//Adia
//Adia
//Adia
//Adia
struct pout
{
	fixed color		: SV_Target0;		//Adia
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
	o.position	= UnityObjectToClipPos(i.position);
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
void psMain(in v2p i, out pout o)
{
	o.color		= 1.0;
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
