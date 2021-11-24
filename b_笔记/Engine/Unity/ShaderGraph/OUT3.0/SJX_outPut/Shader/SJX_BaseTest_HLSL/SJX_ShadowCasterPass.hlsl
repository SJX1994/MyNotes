#ifndef LIGHTWEIGHT_SHADOW_CASTER_PASS_INCLUDED
#define LIGHTWEIGHT_SHADOW_CASTER_PASS_INCLUDED

#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
#include "SJX_ShaderLib/Shadows.hlsl"

#include "SJX_BaseTest_VertFrag/SJX_Vert/Vert.cginc"


float3 _LightDirection;

struct Attributes
{
    float4 positionOS   : POSITION;
    float3 normalOS     : NORMAL;
    float2 texcoord     : TEXCOORD0;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings
{
    float2 uv           : TEXCOORD0;
    float4 positionCS   : SV_POSITION;
};

float4 GetShadowPositionHClip(Attributes input)
{
    float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
    float3 normalWS = TransformObjectToWorldNormal(input.normalOS);

    float4 positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, _LightDirection));

#if UNITY_REVERSED_Z
    positionCS.z = min(positionCS.z, positionCS.w * UNITY_NEAR_CLIP_VALUE);
#else
    positionCS.z = max(positionCS.z, positionCS.w * UNITY_NEAR_CLIP_VALUE);
#endif

    return positionCS;
}

Varyings ShadowPassVertex(Attributes input)
{
    Varyings output;
    UNITY_SETUP_INSTANCE_ID(input);



    // half3 newPos = input.positionOS.xyz;
    // newPos.xyz *= sin(_Time.yyy*half3(0.1,0.1,0.1));
    // input.positionOS.xyz = newPos.xyz;

    half3 SJX_newPos = vert(input.positionOS.xyz,input.texcoord);

    input.positionOS.xyz = SJX_newPos;

    output.uv = TRANSFORM_TEX(input.texcoord, _BaseMap);
    output.positionCS = GetShadowPositionHClip(input);
    return output;
}

half4 ShadowPassFragment(Varyings input) : SV_TARGET
{
    //_BaseColor = float4(1.0,1.0,1.0,1.0);
    

    Alpha(SampleAlbedoAlpha(input.uv, TEXTURE2D_ARGS(_BaseMap, sampler_BaseMap)).a, _BaseColor, _Cutoff);

    
    

    return 0;
}

#endif
