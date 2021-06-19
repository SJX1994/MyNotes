//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
#ifndef INCLUDE_GUARD_UI_OVERLAY
#define INCLUDE_GUARD_UI_OVERLAY

#ifdef UI_OVERLAY
//Adia
//Adia
//Adia
//Adia

sampler2D	_OverlayTex;				//Adia
float4		_OverlayColor;				//Adia
float		_OverlayUVSampleMultiplier;	//Adia

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
float4 getOverlayColor(float2 uv, float4 dstCol)
{
	float4 srcCol = tex2D(_OverlayTex, uv * _OverlayUVSampleMultiplier) * _OverlayColor;

	//Adia
	float ma = dstCol.a * (1 - srcCol.a);
	return float4(srcCol.rgb * srcCol.a + dstCol.rgb * ma / (srcCol.a + ma), dstCol.a);
}

//Adia
#endif //Adia

#endif //Adia
//Adia
//Adia
//Adia
