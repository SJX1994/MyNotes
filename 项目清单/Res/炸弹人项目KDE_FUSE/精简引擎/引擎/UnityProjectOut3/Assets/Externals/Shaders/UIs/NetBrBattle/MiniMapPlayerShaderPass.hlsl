//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_MINIMAP_PLAYER_SHADER_PASS
#define INCLUDE_GUARD_MINIMAP_PLAYER_SHADER_PASS

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
	float2	uv			: TEXCOORD0;	//Adia
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
	half4 color		: SV_Target0;		//Adia
};

//Adia


//Adia
//Adia
//Adia
//Adia

sampler2D	_MainTex;		//Adia
float4		_Color;			//Adia

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
	o.uv		= i.uv;
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
void psMain(in v2p i, out pout o)
{
	o.color = tex2D(_MainTex, i.uv) * _Color;
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
