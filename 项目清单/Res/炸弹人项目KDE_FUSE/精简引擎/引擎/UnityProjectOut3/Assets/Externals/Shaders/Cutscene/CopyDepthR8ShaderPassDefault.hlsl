//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_COPY_DEPTH_R8_SHADER_PASS_DEFAULT
#define INCLUDE_GUARD_COPY_DEPTH_R8_SHADER_PASS_DEFAULT

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
	fixed r		: SV_Target0;		//Adia
};

//Adia


//Adia
//Adia
//Adia
//Adia

sampler2D	_CameraDepthTexture;	//Adia

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
	o.r = tex2D(_CameraDepthTexture, i.uv).r;
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
