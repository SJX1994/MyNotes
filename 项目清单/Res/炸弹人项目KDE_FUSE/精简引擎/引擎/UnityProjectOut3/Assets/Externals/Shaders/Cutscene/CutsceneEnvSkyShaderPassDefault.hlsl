//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_CUTSCENE_ENV_SKY_SHADER_PASS_DEFAULT
#define INCLUDE_GUARD_CUTSCENE_ENV_SKY_SHADER_PASS_DEFAULT

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
};

//Adia
//Adia
//Adia
//Adia
struct v2p
{
	float4	position	: SV_Position;	//Adia
	float4	projPos		: TEXCOORD0;	//Adia
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

#define MAIN_TEX_HIGHT		(1080.0)			//Adia
#define MAIN_TEX_PIVOT		(float2(0.5, 0.0))	//Adia

//Adia


//Adia
//Adia
//Adia
//Adia

Texture2D		_MainTex;								//Adia
float4			_MainTex_TexelSize;						//Adia
float4			_Color;									//Adia
float4			_bgColor;								//Adia

SamplerState	_mainTexSamplerClampUMirrorVLinear;		//Adia

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
	o.projPos	= ComputeScreenPos(o.position);
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
	float scale = MAIN_TEX_HIGHT / _ScreenParams.y;

	float2 uv = i.projPos.xy / i.projPos.w - MAIN_TEX_PIVOT;
	uv.xy *= _ScreenParams.xy / _MainTex_TexelSize.zw * scale;
	o.color = _MainTex.Sample(_mainTexSamplerClampUMirrorVLinear, uv + MAIN_TEX_PIVOT) * _Color;
	o.color.rgb = lerp(_bgColor.rgb, o.color.rgb, o.color.a);
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
