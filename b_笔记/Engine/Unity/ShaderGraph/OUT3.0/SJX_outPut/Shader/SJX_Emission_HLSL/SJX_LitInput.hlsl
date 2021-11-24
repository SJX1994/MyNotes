#ifndef LIGHTWEIGHT_LIT_INPUT_INCLUDED
#define LIGHTWEIGHT_LIT_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"
#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/SurfaceInput.hlsl"

CBUFFER_START(UnityPerMaterial)

float4 _BaseMap_ST;
half4 _BaseColor;
half4 _SpecColor;
half4 _EmissionColor;
half _Cutoff;
half _Smoothness;
half _Metallic;
half _BumpScale;
half _OcclusionStrength;
CBUFFER_END






inline void InitializeStandardLitSurfaceData(float2 uv, out SurfaceData outSurfaceData)
{
    
    outSurfaceData.alpha = 1.0;
    outSurfaceData.albedo = 1.0;
    outSurfaceData.metallic = 1.0h;
    outSurfaceData.specular = half3(0.0,0.0,0.0);
    outSurfaceData.metallic = 0.0;
    outSurfaceData.specular = half3(0.0h, 0.0h, 0.0h);
    outSurfaceData.smoothness = 0.0;
    outSurfaceData.normalTS = SampleNormal(uv, TEXTURE2D_ARGS(_BumpMap, sampler_BumpMap), _BumpScale);
    outSurfaceData.occlusion = 0.0;
    outSurfaceData.emission = 0.0;
}

#endif // LIGHTWEIGHT_INPUT_SURFACE_PBR_INCLUDED
