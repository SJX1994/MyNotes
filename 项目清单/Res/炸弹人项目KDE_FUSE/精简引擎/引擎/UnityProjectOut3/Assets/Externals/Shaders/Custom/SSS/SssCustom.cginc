//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
#ifndef SSS_CUSTOM_INCLUDED //Adia
#define SSS_CUSTOM_INCLUDED

sampler2D	LightingTexBlurred;				//Adia
sampler2D	_SssMap;						//Adia
float		_SssScale;						//Adia
float		_AlbedoOpacity;					//Adia
float		_SubsurfaceAlbedoSaturation;	//Adia

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
inline void calcSssEmissiveColor(half2 texUV, half2 screenPos, half3 albedoColor, inout half3 outEnvEmissiveColor, inout half3 outDiffuseColor)
{
#if !defined(SCENE_VIEW)
	//Adia
	half sssAtten = 1 - tex2D(_SssMap, texUV) * _SssScale;
	outDiffuseColor *= sssAtten;
	outEnvEmissiveColor *= sssAtten;

#if defined(SUBSURFACE_ALBEDO)
	albedoColor.rgb = lerp(float3(1.0, 1.0, 1.0), albedoColor.rgb, _AlbedoOpacity);
	albedoColor.rgb = lerp(Luminance(albedoColor.rgb) * float3(6.0, 6.0, 6.0), albedoColor.rgb, _SubsurfaceAlbedoSaturation);
#endif //Adia

	//Adia
	outEnvEmissiveColor += albedoColor * tex2D(LightingTexBlurred, screenPos).rgb;
#endif //Adia
}

#endif //Adia
//Adia
//Adia
//===========================================================================