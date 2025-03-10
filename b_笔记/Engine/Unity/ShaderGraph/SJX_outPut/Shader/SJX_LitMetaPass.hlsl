#ifndef LIGHTWEIGHT_LIT_META_PASS_INCLUDED
#define LIGHTWEIGHT_LIT_META_PASS_INCLUDED

#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/MetaInput.hlsl"
#include "SJX_Vert/Vert.cginc"
#include "SJX_Frag/Frag.cginc"

Varyings LightweightVertexMeta(Attributes input)
{
    Varyings output;
    
    // half3 SJX_newPos = vert(input.positionOS.xyz);
    // input.positionOS.xyz = SJX_newPos;

    output.positionCS = MetaVertexPosition(input.positionOS, input.uv1, input.uv2,
        unity_LightmapST, unity_DynamicLightmapST);
    output.uv = TRANSFORM_TEX(input.uv0, _BaseMap);
    return output;
}

half4 LightweightFragmentMeta(Varyings input) : SV_Target
{
    SurfaceData surfaceData;
    InitializeStandardLitSurfaceData(input.uv, surfaceData);

    BRDFData brdfData;
    InitializeBRDFData(surfaceData.albedo, surfaceData.metallic, surfaceData.specular, surfaceData.smoothness, surfaceData.alpha, brdfData);

    MetaInput metaInput;
    metaInput.Albedo = brdfData.diffuse + brdfData.specular * brdfData.roughness * 0.5;
    metaInput.SpecularColor = surfaceData.specular;
    metaInput.Emission = surfaceData.emission;

    return MetaFragment(metaInput);
}

#endif
