Shader "SSS/SSS Standard Deferred" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		[NoScaleOffset]_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
		 [Toggle(SUBSURFACE_ALBEDO)] SUBSURFACE_ALBEDO ("Subsurface albedo", Float) = 0
		_SubSurfaceParallax ("SubSurface Parallax", Range(-.002,0)) = -0.000363
		 [Toggle(SUBSURFACE_PARALLAX)] SUBSURFACE_PARALLAX ("Parallax", Float) = 0

        _AlbedoOpacity("Opacity", Range(0, 1)) = 1

		[NoScaleOffset]_SubsurfaceAlbedo ("Subsurface Albedo", 2D) = "white" {}
        _SubsurfaceAlbedoOpacity("Opacity", Range(0, 1)) = 1
        _SubsurfaceAlbedoSaturation("Saturation", Range(0, 1)) = 1

		_AlbedoTile("Tile", Range(1, 20)) = 1
		[NoScaleOffset]_ProfileTex ("Profile", 2D) = "white" {}
		_ProfileColor("Profile Color", Color) = (1.0,1.0,1.0)
        [NoScaleOffset] _OcclusionMap("Occlusion", 2D) = "white" {}
        [NoScaleOffset] _OcclusionStrength("_OcclusionStrength", Range(0,1)) = 1

        _OcclusionColor("Occlusion Color", Color) = (0.0,0.0,0.0)
        _SpecColor("Specular Color", Color) = (0.2,0.2,0.2)
		[NoScaleOffset] _SpecGlossMap("Specular", 2D) = "white" {}
		[NoScaleOffset] _CavityMap("Cavity Map", 2D) = "white" {}
        _CavityStrength("Cavity", Range(0, 1)) = 1
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
        _FresnelIntensity("Fresnel", Range(0,1)) = 1
		//Adia
		[NoScaleOffset]_BumpMap("Normal Map", 2D) = "bump" {}
		_BumpTile("Tile", Range(1, 20)) = 1
        [Toggle(TRANSMISSION)] _Transmission ("Transmission", Float) = 0
		[NoScaleOffset]_TransmissionMap("Mask", 2D) = "white" {}
        _TransmissionColor("Color", Color) = (0.2,0.2,0.2)
        TransmissionOcc("Occlusion", Range(0,1)) = 1
        TransmissionShadows("Shadows", Range(0,1)) = 1
        TransmissionRange("Range", Range(0,5)) = 0.5
        DynamicPassTransmission ("Dynamic Pass", Range(0,1)) = 1
        BasePassTransmission ("Base Pass", Range(0,5)) = 1
        SSS_shader("", float)=1
	}
	SubShader {
		Tags { "RenderType"="Opaque"  "Queue" = "Geometry+0" "IgnoreProjector" = "True"}
		LOD 200

		CGPROGRAM
		//Adia
		#pragma surface surf StandardSpecular fullforwardshadows
		#pragma multi_compile _ SCENE_VIEW
		#pragma multi_compile _ SUBSURFACE_ALBEDO

        #include "../Resources/SSS_Common.hlsl"
		#pragma multi_compile _ TRANSMISSION

		//Adia
		#pragma target 3.0
		half _OcclusionFade;
		

		struct Input 
		{
			float2 uv_MainTex;
			float4 screenPos;
			INTERNAL_DATA
		};

		

	

		//Adia
		//Adia
		//Adia
		UNITY_INSTANCING_BUFFER_START(Props)
			//Adia
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
			//Adia
			half2 uv = IN.uv_MainTex;
			fixed4 Albedo = tex2D(_MainTex, uv * _AlbedoTile);
            float3 Normal = UnpackNormal(tex2D(_BumpMap, uv * _BumpTile));
            o.Normal = Normal;
			SSS_OCCLUSION
			o.Occlusion = lerp(1.0, tex2D(_OcclusionMap, uv).r, _OcclusionStrength);

			#ifdef SCENE_VIEW
			o.Albedo.rgb = Albedo * _Color;
            #else
				#ifdef SUBSURFACE_ALBEDO
				Albedo.rgb = lerp(1.0, Albedo.rgb, _AlbedoOpacity);
				Albedo.rgb = lerp(Luminance(Albedo.rgb) * 6, Albedo.rgb, _SubsurfaceAlbedoSaturation);
				#endif
            #endif
			

            float4 Specular = float4(_SpecColor.rgb, _Glossiness) * tex2D(_SpecGlossMap, uv);

			o.Specular = Specular.rgb;
			o.Smoothness = Specular.a;
			o.Alpha = Albedo.a;
			float3 Emission = 0;
			//Adia
             #if !defined(SCENE_VIEW)
            half3 LightingPass = 0;
            float4 coords = 0;
			coords = UNITY_PROJ_COORD(IN.screenPos);
            coords.w += .0001;
			float2 screenUV = coords.xy / coords.w;
            
               #ifdef UNITY_SINGLE_PASS_STEREO
				float4 scaleOffset = unity_StereoScaleOffset[unity_StereoEyeIndex];
			    screenUV = (screenUV - scaleOffset.zw) / scaleOffset.xy;
               #endif
            if (unity_StereoEyeIndex == 0)
                LightingPass = tex2D(LightingTexBlurred, screenUV).rgb;
            else
                LightingPass = tex2D(LightingTexBlurredR, screenUV).rgb;

            Emission += Albedo * LightingPass;
            #endif

			#if defined(TRANSMISSION) && defined(SCENE_VIEW)
			BASE_TRANSMISSION_DEFERRED
            #endif
            o.Emission = Emission;
		}
		ENDCG
	}
	FallBack "Diffuse"
		CustomEditor "SSS_MaterialEditor"

}
