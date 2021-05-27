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
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

//Adia
Shader "Fuse/Custom/Character"
//Adia
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo", 2D) = "white" {}

        _Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

        _Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5
        _GlossMapScale("Smoothness Scale", Range(0.0, 1.0)) = 1.0
        [Enum(Metallic Alpha,0,Albedo Alpha,1)] _SmoothnessTextureChannel ("Smoothness texture channel", Float) = 0

        [Gamma] _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
        _MetallicGlossMap("Metallic", 2D) = "white" {}

        [ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
        [ToggleOff] _GlossyReflections("Glossy Reflections", Float) = 1.0

        _BumpScale("Scale", Float) = 1.0
        [Normal] _BumpMap("Normal Map", 2D) = "bump" {}

        _Parallax ("Height Scale", Range (0.005, 0.08)) = 0.02
        _ParallaxMap ("Height Map", 2D) = "black" {}

        _OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0
        _OcclusionMap("Occlusion", 2D) = "white" {}

        _EmissionColor("Color", Color) = (0,0,0)
        _EmissionMap("Emission", 2D) = "white" {}

        _DetailMask("Detail Mask", 2D) = "white" {}

        _DetailAlbedoMap("Detail Albedo x2", 2D) = "grey" {}
        _DetailNormalMapScale("Scale", Float) = 1.0
        [Normal] _DetailNormalMap("Normal Map", 2D) = "bump" {}

        [Enum(UV0,0,UV1,1)] _UVSec ("UV Set for secondary textures", Float) = 0


        //Adia
        [HideInInspector] _Mode ("__mode", Float) = 0.0
        [HideInInspector] _SrcBlend ("__src", Float) = 1.0
        [HideInInspector] _DstBlend ("__dst", Float) = 0.0
        [HideInInspector] _ZWrite ("__zw", Float) = 1.0

		//Adia
		_SssMap		("SSS Map",		2D)					= "white" {}					//Adia
		_SssScale	("SSS Scale",	Range(0.0, 1.0))	= 1.0							//Adia

		//Adia
		[Toggle(TRANSMISSION)] _Transmission("Transmission", Float) = 0					//Adia
		[NoScaleOffset]_TransmissionMap("Mask", 2D) = "white" {}						//Adia
		_TransmissionColor("Color", Color) = (0.2,0.2,0.2)								//Adia
		TransmissionShadows("Shadows", Range(0,1)) = 1									//Adia
		TransmissionOcc("Occlusion", Range(0,1)) = 1									//Adia
		TransmissionRange("Range", Range(0,5)) = 0.5									//Adia
		DynamicPassTransmission("Dynamic Pass", Range(0,1)) = 1							//Adia
		BasePassTransmission("Base Pass", Range(0,5)) = 1								//Adia

		[Toggle(SUBSURFACE_ALBEDO)] SUBSURFACE_ALBEDO ("Subsurface albedo", Float) = 0	//Adia
		_AlbedoOpacity("Opacity", Range(0, 1)) = 1										//Adia
		_SubsurfaceAlbedoOpacity("Opacity", Range(0, 1)) = 1							//Adia
		[NoScaleOffset]_SubsurfaceAlbedo ("Subsurface Albedo", 2D) = "white" {}			//Adia
		_SubsurfaceAlbedoSaturation("Saturation", Range(0, 1)) = 1						//Adia

		[NoScaleOffset]_ProfileTex ("Profile", 2D) = "white" {}							//Adia
		_ProfileColor("Profile Color", Color) = (1.0,1.0,1.0)							//Adia

		_OcclusionColor("Occlusion Color", Color) = (0.0,0.0,0.0)						//Adia

		SSS_shader("SSS_shader", float)=1												//Adia
		//Adia
		//Adia
    }

    CGINCLUDE
        #define UNITY_SETUP_BRDF_INPUT MetallicSetup
    ENDCG

    SubShader
    {
        Tags { "RenderType"="Opaque" "PerformanceChecks"="False" }
        LOD 300


        //Adia
        //Adia
        Pass
        {
            Name "FORWARD"
            Tags { "LightMode" = "ForwardBase" }

            Blend [_SrcBlend] [_DstBlend]
            ZWrite [_ZWrite]

            CGPROGRAM
            #pragma target 3.0

            //Adia

            //Adia
            //Adia
            #pragma shader_feature _NORMALMAP
            #pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
            //Adia
            #pragma shader_feature _EMISSION
            //Adia
            #pragma shader_feature _METALLICGLOSSMAP
            #pragma shader_feature ___ _DETAIL_MULX2
            #pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature _ _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature _ _GLOSSYREFLECTIONS_OFF
            #pragma shader_feature _PARALLAXMAP
            //Adia

            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma multi_compile_instancing
            //Adia
            //Adia

            #pragma vertex vertBase
            #pragma fragment fragBase
            #include "UnityStandardCoreForward.cginc"

            ENDCG
        }
        //Adia
        //Adia
        Pass
        {
            Name "FORWARD_DELTA"
            Tags { "LightMode" = "ForwardAdd" }
            Blend [_SrcBlend] One
            Fog { Color (0,0,0,0) } //Adia
            ZWrite Off
            ZTest LEqual

            CGPROGRAM
            #pragma target 3.0

            //Adia


            //Adia
            #pragma shader_feature _NORMALMAP
            #pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
            #pragma shader_feature _METALLICGLOSSMAP
            #pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature _ _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature ___ _DETAIL_MULX2
            #pragma shader_feature _PARALLAXMAP
            //Adia

            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            //Adia
            //Adia

            #pragma vertex vertAdd
            #pragma fragment fragAdd
            #include "UnityStandardCoreForward.cginc"

            ENDCG
        }
        //Adia
        //Adia
        Pass {
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }

            ZWrite On ZTest LEqual

            CGPROGRAM
            #pragma target 3.0

            //Adia


            //Adia
            #pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
            #pragma shader_feature _METALLICGLOSSMAP
            #pragma shader_feature _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature _PARALLAXMAP
            //Adia
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_instancing
            //Adia
            //Adia

            #pragma vertex vertShadowCaster
            #pragma fragment fragShadowCaster

            #include "UnityStandardShadow.cginc"

            ENDCG
        }
        //Adia
        //Adia
        Pass
        {
            Name "DEFERRED"
            Tags { "LightMode" = "Deferred" }

            CGPROGRAM
            #pragma target 3.0
            #pragma exclude_renderers nomrt


            //Adia

            //Adia
            #pragma shader_feature _NORMALMAP
            #pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
            //Adia
            #pragma shader_feature _EMISSION
            //Adia
            #pragma shader_feature _METALLICGLOSSMAP
            #pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature _ _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature ___ _DETAIL_MULX2
            #pragma shader_feature _PARALLAXMAP
            //Adia

            #pragma multi_compile_prepassfinal
            #pragma multi_compile_instancing
            //Adia
            //Adia

            #pragma vertex vertDeferred
//Adia
            #pragma fragment characterFragDeferred
//Adia

            #include "UnityStandardCore.cginc"

//Adia
			#pragma multi_compile _ SCENE_VIEW				//Adia
			#pragma multi_compile _ SUBSURFACE_ALBEDO		//Adia
			#pragma multi_compile _ TRANSMISSION			//Adia
			#pragma multi_compile _ SSS_CALC				//Adia

			//Adia
			#ifdef __INTELLISENSE__
			#define SSS_CALC
			#endif //Adia

			#ifdef SSS_CALC
			#include "Assets/Externals/Shaders/Custom/SSS/SssCustom.cginc"
			#endif //Adia
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
		//Adia
		//Adia
		void characterFragDeferred (
			VertexOutputDeferred i,
			out half4 outGBuffer0 : SV_Target0,
			out half4 outGBuffer1 : SV_Target1,
			out half4 outGBuffer2 : SV_Target2,
			out half4 outEmission : SV_Target3          //Adia
		#if defined(SHADOWS_SHADOWMASK) && (UNITY_ALLOWED_MRT_COUNT > 4)
			,out half4 outShadowMask : SV_Target4       //Adia
		#endif
		)
		{
			#if (SHADER_TARGET < 30)
				outGBuffer0 = 1;
				outGBuffer1 = 1;
				outGBuffer2 = 0;
				outEmission = 0;
				#if defined(SHADOWS_SHADOWMASK) && (UNITY_ALLOWED_MRT_COUNT > 4)
					outShadowMask = 1;
				#endif
				return;
			#endif

			UNITY_APPLY_DITHER_CROSSFADE(i.pos.xy);

			FRAGMENT_SETUP(s)
			UNITY_SETUP_INSTANCE_ID(i);

			//Adia
			UnityLight dummyLight = DummyLight ();
			half atten = 1;

			//Adia
			half occlusion = Occlusion(i.tex.xy);
		#if UNITY_ENABLE_REFLECTION_BUFFERS
			bool sampleReflectionsInDeferred = false;
		#else
			bool sampleReflectionsInDeferred = true;
		#endif

			UnityGI gi = FragmentGI (s, occlusion, i.ambientOrLightmapUV, atten, dummyLight, sampleReflectionsInDeferred);

			half3 emissiveColor = UNITY_BRDF_PBS (s.diffColor, s.specColor, s.oneMinusReflectivity, s.smoothness, s.normalWorld, -s.eyeVec, gi.light, gi.indirect).rgb;

			#ifdef SSS_CALC
			//Adia
			float2 screenPos = i.pos.xy / _ScreenParams.xy;
			float3 albedo = tex2D(_MainTex, i.tex.xy).rgb;
			calcSssEmissiveColor(i.tex.xy, screenPos, albedo, /*inout*/ emissiveColor.rgb, /*inout*/ s.diffColor);
			#endif //Adia

			#ifdef _EMISSION
				emissiveColor += Emission (i.tex.xy);
			#endif

			#ifndef UNITY_HDR_ON
				emissiveColor.rgb = exp2(-emissiveColor.rgb);
			#endif

			UnityStandardData data;
			data.diffuseColor   = s.diffColor;
			data.occlusion      = occlusion;
			data.specularColor  = s.specColor;
			data.smoothness     = s.smoothness;
			data.normalWorld    = s.normalWorld;

			UnityStandardDataToGbuffer(data, outGBuffer0, outGBuffer1, outGBuffer2);

			//Adia
			outEmission = half4(emissiveColor, 1);

			//Adia
			#if defined(SHADOWS_SHADOWMASK) && (UNITY_ALLOWED_MRT_COUNT > 4)
				outShadowMask = UnityGetRawBakedOcclusions(i.ambientOrLightmapUV.xy, IN_WORLDPOS(i));
			#endif
		}
//Adia

            ENDCG
        }

        //Adia
        //Adia
        //Adia
        Pass
        {
            Name "META"
            Tags { "LightMode"="Meta" }

            Cull Off

            CGPROGRAM
            #pragma vertex vert_meta
            #pragma fragment frag_meta

            #pragma shader_feature _EMISSION
            //Adia
            #pragma shader_feature _METALLICGLOSSMAP
            #pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature ___ _DETAIL_MULX2
            //Adia
            #pragma shader_feature EDITOR_VISUALIZATION

            #include "UnityStandardMeta.cginc"
            ENDCG
        }
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "PerformanceChecks"="False" }
        LOD 150

        //Adia
        //Adia
        Pass
        {
            Name "FORWARD"
            Tags { "LightMode" = "ForwardBase" }

            Blend [_SrcBlend] [_DstBlend]
            ZWrite [_ZWrite]

            CGPROGRAM
            #pragma target 2.0

            //Adia
            #pragma shader_feature _NORMALMAP
            #pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
            //Adia
            #pragma shader_feature _EMISSION
            //Adia
            #pragma shader_feature _METALLICGLOSSMAP
            #pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature _ _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature _ _GLOSSYREFLECTIONS_OFF
            //Adia
            //Adia
            //Adia

            #pragma skip_variants SHADOWS_SOFT DIRLIGHTMAP_COMBINED

            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog

            #pragma vertex vertBase
            #pragma fragment fragBase
            #include "UnityStandardCoreForward.cginc"

            ENDCG
        }
        //Adia
        //Adia
        Pass
        {
            Name "FORWARD_DELTA"
            Tags { "LightMode" = "ForwardAdd" }
            Blend [_SrcBlend] One
            Fog { Color (0,0,0,0) } //Adia
            ZWrite Off
            ZTest LEqual

            CGPROGRAM
            #pragma target 2.0

            //Adia
            #pragma shader_feature _NORMALMAP
            #pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
            #pragma shader_feature _METALLICGLOSSMAP
            #pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature _ _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature ___ _DETAIL_MULX2
            //Adia
            //Adia
            #pragma skip_variants SHADOWS_SOFT

            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog

            #pragma vertex vertAdd
            #pragma fragment fragAdd
            #include "UnityStandardCoreForward.cginc"

            ENDCG
        }
        //Adia
        //Adia
        Pass {
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }

            ZWrite On ZTest LEqual

            CGPROGRAM
            #pragma target 2.0

            //Adia
            #pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
            #pragma shader_feature _METALLICGLOSSMAP
            #pragma shader_feature _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            //Adia
            #pragma skip_variants SHADOWS_SOFT
            #pragma multi_compile_shadowcaster

            #pragma vertex vertShadowCaster
            #pragma fragment fragShadowCaster

            #include "UnityStandardShadow.cginc"

            ENDCG
        }

        //Adia
        //Adia
        //Adia
        Pass
        {
            Name "META"
            Tags { "LightMode"="Meta" }

            Cull Off

            CGPROGRAM
            #pragma vertex vert_meta
            #pragma fragment frag_meta

            #pragma shader_feature _EMISSION
            //Adia
            #pragma shader_feature _METALLICGLOSSMAP
            #pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature ___ _DETAIL_MULX2
            //Adia
            #pragma shader_feature EDITOR_VISUALIZATION

            #include "UnityStandardMeta.cginc"
            ENDCG
        }
    }


    FallBack "VertexLit"
	//Adia
    CustomEditor "CharacterShaderGUI"
	//Adia
}
