//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_HX_GLOWING_DISSOLVE_SHADER_PASS_MAIN
#define INCLUDE_GUARD_HX_GLOWING_DISSOLVE_SHADER_PASS_MAIN

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
	float4 vertex	: POSITION;			//Adia
	float2 uv		: TEXCOORD0;		//Adia

#if _HX_DISSOLVE_MAP_UV1
	float2 uv1		: TEXCOORD1;		//Adia
#endif //Adia

	UNITY_VERTEX_INPUT_INSTANCE_ID		//Adia
};

//Adia
//Adia
//Adia
//Adia
struct v2p
{
	float4	vertex	: SV_POSITION;	//Adia
	float4	uv		: TEXCOORD0;	//Adia

	float4	color	: TEXCOORD1;	//Adia
	float4	param	: TEXCOORD2;	//Adia
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

sampler2D	_NoiseTex;		//Adia
float4		_NoiseTex_ST;	//Adia

//Adia
//Adia
//Adia
//Adia
sampler2D	_ParamTex;

float	_PeakRange;							//Adia
float	_PeakStrength;						//Adia
float	_TexLuminanceDissolveThreshold;		//Adia

float	_CutoutThreshold;					//Adia

//Adia
UNITY_INSTANCING_BUFFER_START(Props)
	UNITY_DEFINE_INSTANCED_PROP(float4,		_TintColor)				//Adia
	UNITY_DEFINE_INSTANCED_PROP(float,		_NormalizedTime)		//Adia
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
float luminance(float4 color)
{
	return 0
		+ color.r * 0.299f
		+ color.g * 0.587f
		+ color.b * 0.114f
		;
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
void vsMain(in vin i, out v2p o)
{
	UNITY_SETUP_INSTANCE_ID(i);

	o.vertex	= UnityObjectToClipPos(i.vertex);
	o.uv.xy		= TRANSFORM_TEX(i.uv, _MainTex);
#if defined(_HX_DISSOLVE_MAP_UV1)
	o.uv.zw		= TRANSFORM_TEX(i.uv1, _NoiseTex);
#else //Adia
	o.uv.zw		= TRANSFORM_TEX(i.uv, _NoiseTex);
#endif //Adia
	o.color		= UNITY_ACCESS_INSTANCED_PROP(Props, _TintColor);

	float colorLuminance	= luminance(o.color);
	float normalizedTime	= UNITY_ACCESS_INSTANCED_PROP(Props, _NormalizedTime);
	o.param = float4(colorLuminance, normalizedTime, 0, 0);
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
void psMain(in v2p i, out pout o)
{
	float	normalizedTime	= i.param.y;
	float	noise			= tex2D(_NoiseTex, i.uv.zw).r;
	float4	param			= tex2D(_ParamTex, float2(normalizedTime, 0.5f));
	float4	texColor		= tex2D(_MainTex, i.uv.xy);
	float	texLuminance	= luminance(texColor);

	//Adia
	float redHeatRate				= param.r;		//Adia
	float dissolveByNoise			= param.g;		//Adia
	float dissolveByAlbedLuminance	= param.b;		//Adia

#if defined(_HX_DISSOLVE_CUTOUT_ON)
	clip(texColor.a - _CutoutThreshold);
#endif //Adia

	//Adia
	clip(texLuminance + _TexLuminanceDissolveThreshold - dissolveByAlbedLuminance);	//Adia
	clip(noise - dissolveByNoise);													//Adia

	float	colorLuminance	= i.param.x;										//Adia
	float	redHeat			= redHeatRate * (1 - noise + dissolveByNoise);		//Adia
	float	peak			= 1 - step(dissolveByNoise + _PeakRange, noise);	//Adia
	float4	tintColor		= i.color;

	//Adia
	float4	color = texColor;										//Adia
	color.a = 1.0f;													//Adia
	color.rgb *= (1 - redHeatRate) * 0.5;							//Adia
	color.rgb += pow(tintColor * redHeat, 2);						//Adia
	color.rgb += pow(colorLuminance * peak * _PeakStrength, 2);		//Adia

	o.color = color;
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
