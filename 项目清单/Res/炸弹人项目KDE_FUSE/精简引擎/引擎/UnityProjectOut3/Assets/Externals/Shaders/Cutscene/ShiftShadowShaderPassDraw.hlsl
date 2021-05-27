//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_SHIFT_SHADOW_SHADER_PASS_DRAW
#define INCLUDE_GUARD_SHIFT_SHADOW_SHADER_PASS_DRAW

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
	fixed4	color		: COLOR;		//Adia
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

#define SILHOUETTE_THRESHOLD	(0.1)				//Adia

//Adia


//Adia
//Adia
//Adia
//Adia

sampler2D	_MainTex;				//Adia
fixed4		_Color;					//Adia
float4		_MainTex_ST;			//Adia
float4		_MainTex_TexelSize;		//Adia

sampler2D	_silhouetteGrabTex;		//Adia
sampler2D	_silhouetteTex;			//Adia
float		_shiftShadowOffsetX;	//Adia
float		_shiftShadowOffsetY;	//Adia
fixed4		_shiftShadowColor;		//Adia

fixed4		_outlineColor;			//Adia

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
	o.position = UnityObjectToClipPos(i.position);
	o.uv = TRANSFORM_TEX(i.uv, _MainTex);
	o.color = _Color;
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
void psMain(in v2p i, out pout o)
{
	float2 aspect = float2(_MainTex_TexelSize.w / _MainTex_TexelSize.z, 1);

	fixed baseColor = step(SILHOUETTE_THRESHOLD, tex2D(_silhouetteTex, i.uv).r);
	fixed silhouette = step(SILHOUETTE_THRESHOLD, tex2D(_silhouetteGrabTex, i.uv).r) - baseColor;
	float4 color = silhouette == 1 ? _outlineColor : tex2D(_MainTex, i.uv) * i.color;

	if( _shiftShadowOffsetX != 0 ||
		_shiftShadowOffsetY != 0 ) {
		i.uv += float2(_shiftShadowOffsetX, _shiftShadowOffsetY) * aspect;
		color = step(SILHOUETTE_THRESHOLD, tex2D(_silhouetteGrabTex, i.uv).r) == 1 && !(silhouette == 1 || baseColor == 1) ? _shiftShadowColor : color;
	}

	o.color = color;
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
