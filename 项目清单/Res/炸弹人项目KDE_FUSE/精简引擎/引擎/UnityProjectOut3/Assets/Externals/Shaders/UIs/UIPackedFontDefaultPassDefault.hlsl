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
#ifndef INCLUDE_GUARD_UI_PACKED_FONT_DEFAULT_PASS_DEFAULT
#define INCLUDE_GUARD_UI_PACKED_FONT_DEFAULT_PASS_DEFAULT

//Adia
//Adia
//Adia
//Adia

#include "UnityCG.cginc"
#include "UnityUI.cginc"
#include "UISSLineOverlay.cginc"
#include "UIOverlay.cginc"

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
	float4 vertex		: POSITION;			//Adia
	float4 color		: COLOR;			//Adia
	float2 texcoord0	: TEXCOORD0;		//Adia
	float2 texcoord2	: TEXCOORD2;		//Adia
#ifdef UI_SS_LINE_OVERLAY
	float2 texcoord3	: TEXCOORD3;		//Adia
#endif
};

//Adia
//Adia
//Adia
//Adia
struct v2p
{
	float4 pos			: SV_POSITION;		//Adia
	float4 col			: COLOR0;			//Adia
	float2 uv			: TEXCOORD0;		//Adia
	float4 borders		: TEXCOORD1;		//Adia
	float4 mask			: TEXCOORD2;		//Adia
#ifdef UI_SS_LINE_OVERLAY
	float2 vcoord		: TEXCOORD3;		//Adia
#endif
	//Adia
	float4 worldPos		: TEXCOORD4;		//Adia
	//Adia
};

//Adia
//Adia
//Adia
//Adia
struct pout
{
	float4 color		: SV_Target0;		//Adia
};

//Adia


//Adia
//Adia
//Adia
//Adia

#define CHANNEL_MULTIPLY	(0.1)	//Adia

//Adia


//Adia
//Adia
//Adia
//Adia

sampler2D	_MainTex;			//Adia
float4		_MainColor;			//Adia
float		_Spread;			//Adia
float		_MainAA;			//Adia

//Adia
float4		_ClipRect;
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
//Adia
//Adia
float smstep(float min, float max, float v)
{
	v = saturate( (v - min) / (max - min) );
	return saturate( v * v * (3 - 2 * v) );
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
float getColorCode(float v, int reference) {
	return round( v ) == reference;
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
void vsMain(in vin i, out v2p o)
{
	UNITY_INITIALIZE_OUTPUT(v2p, o);

	//Adia
	o.worldPos = i.vertex;
	//Adia
	o.pos = UnityObjectToClipPos( i.vertex );
	o.col = i.color * _MainColor;

	o.uv = i.texcoord0;

#ifdef TEXT_THICKNESS
	o.borders.x = i.texcoord2.x;									//Adia
	o.borders.y = min(o.borders.x, i.texcoord2.x - i.texcoord2.y);	//Adia
#else
	float center = 0.5 / 1.0;
	float pixcel = center / _Spread;
	float hMainAnti = pixcel * _MainAA;
	o.borders.x = center + hMainAnti;								//Adia
	o.borders.y = center - hMainAnti;								//Adia
#endif

#ifdef UI_SS_LINE_OVERLAY
	o.vcoord = i.texcoord3;
#endif

	//Adia
	float code = floor(i.texcoord0) * CHANNEL_MULTIPLY;
	o.mask = float4(getColorCode(code, 1), getColorCode(code, 2), getColorCode(code, 4), getColorCode(code, 8));
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
void psMain(in v2p i, out pout o)
{
	float4 pick = tex2D(_MainTex, i.uv) * i.mask;
	float  dist = (pick.r + pick.g + pick.b + pick.a);

	float b0 = i.borders.x;
	float b1 = i.borders.y;

	float m = smstep(b1, b0, dist);

	float4 drawColor = i.col;
	drawColor.a *= m;

#ifdef UI_SS_LINE_OVERLAY
	drawColor = getSSLineOverlayColor(i.vcoord, drawColor);
#endif

#ifdef UI_OVERLAY
	drawColor = getOverlayColor(i.uv, drawColor);
#endif

	//Adia
#ifdef UNITY_UI_CLIP_RECT
	drawColor *= UnityGet2DClipping(i.worldPos.xy, _ClipRect);
#endif

#ifdef UNITY_UI_ALPHACLIP
	clip(drawColor.a - 0.001);
#endif
	//Adia

	o.color = drawColor;
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
