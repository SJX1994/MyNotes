//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_UV_SCROLL_CONTROLABLE_UNLIT_TEXTURE_SHADER_PASS_MAIN
#define INCLUDE_GUARD_UV_SCROLL_CONTROLABLE_UNLIT_TEXTURE_SHADER_PASS_MAIN

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
	float4 vertex	: POSITION;		//Adia

	//Adia
	//Adia
	//Adia
	//Adia
	float4 uv		: TEXCOORD0;
};

//Adia
//Adia
//Adia
//Adia
struct v2p
{
	float4 vertex	: SV_POSITION;	//Adia
	float2 uv		: TEXCOORD0;	//Adia
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
float4		_MainTex_ST;	//Adia
float4		_Color;			//Adia
float		_AlphaClip;		//Adia

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
	o.vertex = UnityObjectToClipPos(i.vertex);
	float2 uv = TRANSFORM_TEX(i.uv, _MainTex);
	o.uv.x = lerp(i.uv.x, uv.x, i.uv.z);
	o.uv.y = lerp(i.uv.y, uv.y, i.uv.w);
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
void psMain(in v2p i, out pout o)
{
	fixed4 color = tex2D(_MainTex, i.uv) * _Color;
	clip(color.a - _AlphaClip);

	o.color = color;
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
