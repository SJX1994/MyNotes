﻿// unity 2019 .1.4f1 LWRP
Shader "SJX/SJX_shader_test"
{
    Properties
    {

        //SJX自定义变量：
             _SJX_Coustom_Float_VertexAniSpeed("sjx_VertexAniSpeed", Float) = 1.0
             _SJX_Coustom_Float_FragPosTest("sjx_FragPosTest", Float) = 1.0
             _SJX_ShadowMapTex("sjx_ShadowMapTex", 2D) = "white" {}
             [KeywordEnum(Off,On)]  _SJXworkFlow("SJXworkFlow",float) = 0
        // Specular vs Metallic workflow
            [HideInInspector] _WorkflowMode("WorkflowMode", Float) = 1.0
            
            [MainColor] _BaseColor("Color", Color) = (0.5,0.5,0.5,1)
            [MainTexture] _BaseMap("Albedo", 2D) = "white" {}

            _Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

            _Smoothness("Smoothness", Range(0.0, 1.0)) = 0.5
            _GlossMapScale("Smoothness Scale", Range(0.0, 1.0)) = 1.0
            _SmoothnessTextureChannel("Smoothness texture channel", Float) = 0

            [Gamma] _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
            _MetallicGlossMap("Metallic", 2D) = "white" {}

            _SpecColor("Specular", Color) = (0.2, 0.2, 0.2)
            _SpecGlossMap("Specular", 2D) = "white" {}

            [ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
            [ToggleOff] _EnvironmentReflections("Environment Reflections", Float) = 1.0

            _BumpScale("Scale", Float) = 1.0
            _BumpMap("Normal Map", 2D) = "bump" {}

            _OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0
            _OcclusionMap("Occlusion", 2D) = "white" {}

            _EmissionColor("Color", Color) = (0,0,0)
            _EmissionMap("Emission", 2D) = "white" {}

            // Blending state
            [HideInInspector] _Surface("__surface", Float) = 0.0
            [HideInInspector] _Blend("__blend", Float) = 0.0
            [HideInInspector] _AlphaClip("__clip", Float) = 0.0
            [HideInInspector] _SrcBlend("__src", Float) = 1.0
            [HideInInspector] _DstBlend("__dst", Float) = 0.0
            [HideInInspector] _ZWrite("__zw", Float) = 1.0
            [HideInInspector] _Cull("__cull", Float) = 2.0

            _ReceiveShadows("Receive Shadows", Float) = 1.0        
            // Editmode props
            [HideInInspector] _QueueOffset("Queue offset", Float) = 0.0
            
            // ObsoleteProperties
            [HideInInspector] _MainTex("BaseMap", 2D) = "white" {}
            [HideInInspector] _Color("Base Color", Color) = (0.5, 0.5, 0.5, 1)
            [HideInInspector] _GlossMapScale("Smoothness", Float) = 0.0
            [HideInInspector] _Glossiness("Smoothness", Float) = 0.0
            [HideInInspector] _GlossyReflections("EnvironmentReflections", Float) = 0.0
    }

    SubShader
    {
        // Lightweight Pipeline tag is required. If Lightweight render pipeline is not set in the graphics settings
        // this Subshader will fail. One can add a subshader below or fallback to Standard built-in to make this
        // material work with both Lightweight Render Pipeline and Builtin Unity Pipeline
        Tags{"RenderType" = "Opaque" "RenderPipeline" = "LightweightPipeline" "IgnoreProjector" = "True"}
        LOD 300

        // ------------------------------------------------------------------
        //  Forward pass. Shades all light in a single pass. GI + emission + Fog
        Pass
        {
            // Lightmode matches the ShaderPassName set in LightweightRenderPipeline.cs. SRPDefaultUnlit and passes with
            // no LightMode tag are also rendered by Lightweight Render Pipeline
            Name "ForwardLit"
            Tags{"LightMode" = "LightweightForward"}

            Blend[_SrcBlend][_DstBlend]
            ZWrite[_ZWrite]
            Cull[_Cull]

            HLSLPROGRAM
            // Required to compile gles 2.0 with standard SRP library
            // All shaders must be compiled with HLSLcc and currently only gles is not using HLSLcc by default
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0

            // -------------------------------------
            // Material Keywords
                    #pragma shader_feature _NORMALMAP
                    #pragma shader_feature _ALPHATEST_ON
                    #pragma shader_feature _ALPHAPREMULTIPLY_ON
                    #pragma shader_feature _EMISSION
                    #pragma shader_feature _METALLICSPECGLOSSMAP
                    #pragma shader_feature _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
                    #pragma shader_feature _OCCLUSIONMAP

                    #pragma shader_feature _SPECULARHIGHLIGHTS_OFF
                    #pragma shader_feature _ENVIRONMENTREFLECTIONS_OFF
                    #pragma shader_feature _SPECULAR_SETUP
                    #pragma shader_feature _RECEIVE_SHADOWS_OFF

            // -------------------------------------
            // Lightweight Pipeline keywords
                #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
                #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
                #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
                #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
                #pragma multi_compile _ _SHADOWS_SOFT
                #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE

            // -------------------------------------
            // Unity defined keywords
                #pragma multi_compile _ DIRLIGHTMAP_COMBINED
                #pragma multi_compile _ LIGHTMAP_ON
                #pragma multi_compile_fog

            //--------------------------------------
            // GPU Instancing
                #pragma multi_compile_instancing

                #pragma vertex LitPassVertex
                #pragma fragment LitPassFragment
                
                #pragma shader_feature _SJXWORKFLOW_OFF _SJXWORKFLOW_ON

                //变量
                #include "SJX_LitInput.hlsl"
                //obj顶点片元通道
                #include "SJX_LitForwardPass.hlsl"
                // #if _SJXWORKFLOW_ON
                //     #include "SJX_LitInput.hlsl"

                // #endif
                ENDHLSL
        }

        Pass
        {
            Name "ShadowCaster"
            Tags{"LightMode" = "ShadowCaster"}

            ZWrite On
            ZTest LEqual
            Cull[_Cull]

            HLSLPROGRAM
            // Required to compile gles 2.0 with standard srp library
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature _ALPHATEST_ON

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma shader_feature _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment

            #include "SJX_LitInput.hlsl"
            //阴影
            #include "SJX_ShadowCasterPass.hlsl"
            ENDHLSL
        }

        Pass
        {
            Name "DepthOnly"
            Tags{"LightMode" = "DepthOnly"}

            ZWrite On
            ColorMask 0
            Cull[_Cull]

            HLSLPROGRAM
            // Required to compile gles 2.0 with standard srp library
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0

            #pragma vertex DepthOnlyVertex
            #pragma fragment DepthOnlyFragment

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature _ALPHATEST_ON
            #pragma shader_feature _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing

            #include "SJX_LitInput.hlsl"
            //自阴影
            #include "SJX_DepthOnlyPass.hlsl"
            ENDHLSL
        }


        // This pass it not used during regular rendering, only for lightmap baking.
        Pass
        {
            Name "Meta"
            Tags{"LightMode" = "Meta"}

            Cull Off

            HLSLPROGRAM
            // Required to compile gles 2.0 with standard srp library
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x

            #pragma vertex LightweightVertexMeta
            #pragma fragment LightweightFragmentMeta

            #pragma shader_feature _SPECULAR_SETUP
            #pragma shader_feature _EMISSION
            #pragma shader_feature _METALLICSPECGLOSSMAP
            #pragma shader_feature _ALPHATEST_ON
            #pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            #pragma shader_feature _SPECGLOSSMAP

            #include "SJX_LitInput.hlsl"
            #include "SJX_LitMetaPass.hlsl"

            ENDHLSL
        }

    }
    FallBack "Hidden/InternalErrorShader"
    CustomEditor "UnityEditor.Rendering.LWRP.ShaderGUI.LitShader"
}

