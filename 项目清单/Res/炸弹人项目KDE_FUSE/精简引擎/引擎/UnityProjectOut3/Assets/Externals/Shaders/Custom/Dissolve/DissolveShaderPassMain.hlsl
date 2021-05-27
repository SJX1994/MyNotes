//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_DISSOLVE_SHADER_PASS_MAIN
#define INCLUDE_GUARD_DISSOLVE_SHADER_PASS_MAIN

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
	float4	color		: COLOR;		//Adia
	UNITY_VERTEX_INPUT_INSTANCE_ID		//Adia
};

//Adia
//Adia
//Adia
//Adia
struct v2p
{
	float4	position	: SV_Position;	//Adia
	float4	uv			: TEXCOORD0;	//Adia
	float4	color		: TEXCOORD1;	//Adia
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

sampler2D	_MainTex;				//Adia
float4		_MainTex_ST;			//Adia
			
sampler2D	_DissolveMap;			//Adia
float4		_DissolveMap_ST;		//Adia

float		_GradientRange;			//Adia
float		_DissolveThreshold;		//Adia
float		_ColorStrength;			//Adia

UNITY_INSTANCING_BUFFER_START(Props)
	UNITY_DEFINE_INSTANCED_PROP(float4, _TintColor)					//Adia
#if HX_TEXTURE_TILING_OFFSET
	UNITY_DEFINE_INSTANCED_PROP(float4, _TextureTilingOffset)		//Adia
#endif //Adia
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
//Adia
void vsMain(in vin i, out v2p o)
{
	UNITY_SETUP_INSTANCE_ID(i);

	float4 pos = i.position;
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_BILLBOARD)
	//Adia
	pos = mul(unity_ObjectToWorld, float4(pos.xyz, 0.0f));
	pos.w = i.position.w;

	//Adia
	pos = mul(UNITY_MATRIX_I_V, float4(pos.xyz, 0.0f));
	pos.w = i.position.w;

	//Adia
	pos.xyz += unity_ObjectToWorld._14_24_34;

	//Adia
	pos = UnityWorldToClipPos(pos);
#else //Adia
	pos = UnityObjectToClipPos(pos);
#endif //Adia
	o.position = pos;

	o.uv.xy = TRANSFORM_TEX(i.uv, _MainTex);
	o.uv.zw = TRANSFORM_TEX(i.uv, _DissolveMap);

#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET)
	float4 texSheetAnimUV = UNITY_ACCESS_INSTANCED_PROP(Props, _TextureTilingOffset);
	o.uv.xy = o.uv.xy * texSheetAnimUV.xy + texSheetAnimUV.zw;
	o.uv.zw = o.uv.zw * texSheetAnimUV.xy + texSheetAnimUV.zw;
#endif //Adia

	o.color = i.color * UNITY_ACCESS_INSTANCED_PROP(Props, _TintColor);
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
void psMain(in v2p i, out pout o)
{
	fixed4 texColor = tex2D(_MainTex, i.uv.xy);

	fixed dissolveMask = tex2D(_DissolveMap, i.uv.zw).r;
	fixed rangedDissolveMask = dissolveMask * max(1.0 - _GradientRange, 0) + _GradientRange;
	fixed invAlpha = (1.0f - i.color.a);
	clip(texColor.a * rangedDissolveMask - invAlpha - _DissolveThreshold);

	//Adia
	fixed4 color = fixed4(_ColorStrength * texColor.rgb * i.color.rgb, smoothstep(invAlpha, invAlpha + _GradientRange, rangedDissolveMask));
	color.a = saturate(color.a); //Adia

	o.color = color;
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
