//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
#ifndef INCLUDE_GUARD_UI_SS_LINE_OVERLAY
#define INCLUDE_GUARD_UI_SS_LINE_OVERLAY

#ifdef UI_SS_LINE_OVERLAY
//Adia
//Adia
//Adia
//Adia

sampler2D	_SSLineOverlayTex;					//Adia
float4		_SSLineOverlayColor;				//Adia
float		_SSLineOverlayUVSampleMultiplier;	//Adia
float		_SSLineOverlayUVOffsetYIntensity;	//Adia
float		_SSLineOverlayUVFlowIntensity;		//Adia

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
float4 getSSLineOverlayColor(float2 vcoord, float4 dstCol)
{
	float2 uv = float2((vcoord.x + vcoord.y * _SSLineOverlayUVOffsetYIntensity) * _SSLineOverlayUVSampleMultiplier, 0.5);
	uv.x += _Time.x * _SSLineOverlayUVFlowIntensity;

	float4 srcCol = tex2D(_SSLineOverlayTex, uv) * _SSLineOverlayColor;

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
