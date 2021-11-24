// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

#ifndef FRAG
#define FRAG

#include "SJX_BaseTest_HLSL/SJX_LitInput.hlsl"


struct sinControllerFrag 
{
        //A振幅,B周期,C相移,D垂直移位
        float A;
        float B;
        float C;
        float D;


};

float Saturate(float target)
{
    return clamp(target,0.0,1.0);
}




half3 SJX_SampleEmission(float2 uv, half3 emissionColor, TEXTURE2D_PARAM(emissionMap, sampler_emissionMap))
{
#ifndef _EMISSION
    return 0;
#else
    return SAMPLE_TEXTURE2D(emissionMap, sampler_emissionMap, uv).rgb * emissionColor;
#endif
}

float4 frag(float4 color, float2 uv,float3 N ,float3 ObjectPos, float3 shadowMask,half3 emissionColor)

{

        ObjectPos = normalize(ObjectPos);

        float3 em = SJX_SampleEmission(uv,emissionColor,_EmissionMap,sampler_EmissionMap);
        float m_time = fmod(_Time*10.0,1.0);
        color.rgb = em 
        *
        smoothstep(0.5-m_time,0.5+m_time,Saturate(ObjectPos.z- (5.1*
        sin( ( PI*10.1 ) *ObjectPos.y+_Time*100.0  ) +1.3
        
        ))
        
        );
      
    return color;


}

#endif