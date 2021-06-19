//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
#ifndef INCLUDE_GUARD_UI_SS_LINE_MULTIPLY
#define INCLUDE_GUARD_UI_SS_LINE_MULTIPLY

#ifdef UI_SS_LINE_MULTIPLY
//Adia
//Adia
//Adia
//Adia

sampler2D	_SSLineMultiplyTex;					//Adia
float		_SSLineMultiplyUVSampleMultiplier;	//Adia
float		_SSLineMultiplyUVOffsetYIntensity;	//Adia
float		_SSLineMultiplyUVFlowIntensityX;	//Adia
float		_SSLineMultiplyUVFlowIntensityY;	//Adia

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
float4 getSSMultiplyColor(float2 vcoord, float4 dstCol)
{
	float2 uv = float2((vcoord.x + vcoord.y * _SSLineMultiplyUVOffsetYIntensity) * _SSLineMultiplyUVSampleMultiplier, 0.5);
	uv += float2(_SSLineMultiplyUVFlowIntensityX, _SSLineMultiplyUVFlowIntensityY) * _Time.x;

	float4 srcCol = tex2D(_SSLineMultiplyTex, uv);
	return srcCol * dstCol;
}

//Adia
#endif //Adia

#endif //Adia
//Adia
//Adia
//Adia
