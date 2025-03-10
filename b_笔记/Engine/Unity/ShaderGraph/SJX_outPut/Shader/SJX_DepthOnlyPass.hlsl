#ifndef LIGHTWEIGHT_DEPTH_ONLY_PASS_INCLUDED
#define LIGHTWEIGHT_DEPTH_ONLY_PASS_INCLUDED

#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
#include "SJX_Vert/Vert.cginc"
#include "SJX_Frag/Frag.cginc"

struct Attributes
{
    float4 position     : POSITION;
    float2 texcoord     : TEXCOORD0;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings
{
    float2 uv           : TEXCOORD0;
    float4 positionCS   : SV_POSITION;
    UNITY_VERTEX_INPUT_INSTANCE_ID
    UNITY_VERTEX_OUTPUT_STEREO
};

Varyings DepthOnlyVertex(Attributes input)
{
    Varyings output = (Varyings)0;
    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

    output.uv = TRANSFORM_TEX(input.texcoord, _BaseMap);

    // half3 newPos = input.position.xyz;
    // //newPos.xyz *= sin((_Time.yyy*_SJX_Coustom_Float)*half3(0.1,0.1,0.1));
    // newPos.xyz *= sin((_Time.yyy)*half3(0.1,0.1,0.1));
    // input.position.xyz = newPos.xyz;
     half3 SJX_newPos = vert(input.position.xyz,input.texcoord);

    
    output.positionCS = TransformObjectToHClip(SJX_newPos);
    return output;
}

half4 DepthOnlyFragment(Varyings input) : SV_TARGET
{
    Alpha(SampleAlbedoAlpha(input.uv, TEXTURE2D_ARGS(_BaseMap, sampler_BaseMap)).a, _BaseColor, _Cutoff);
    return 0;
}
#endif
