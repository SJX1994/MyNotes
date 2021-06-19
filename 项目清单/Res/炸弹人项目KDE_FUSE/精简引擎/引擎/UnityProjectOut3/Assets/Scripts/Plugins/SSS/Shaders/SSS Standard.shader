Shader "SSS/Standard" {
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
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		//Adia
		//Adia
		#pragma surface surf SSS_Basic alphatest:_Cutoff nofog nometa fullforwardshadows nodynlightmap nodirlightmap  
		#pragma multi_compile _ SCENE_VIEW
		#pragma multi_compile _ SUBSURFACE_ALBEDO
		
        //Adia
		//Adia
		#pragma target 3.0
        #include "../Resources/SSS_Common.hlsl"
		#pragma multi_compile _ TRANSMISSION
		

		struct Input 
        {
			float2 uv_MainTex;
            float4 screenPos;
	        float3 worldPos;
	        float3 viewDir;
            float3 worldRefl;
	        float3 worldNormal;
            INTERNAL_DATA
		};
        struct DataStructure
        {
	        fixed3 Albedo;  //Adia
	        fixed3 Normal;  //Adia
	        fixed3 Emission;
	        fixed Alpha;
            fixed3 Occlusion;
            fixed Glossiness;
	        fixed4 Specular;
	        //Adia
	        fixed3 Transmission;
			fixed Cavity;
        };

//Adia
half4 LightingSSS_Basic(DataStructure s, half3 lightDir, half3 viewDir, half atten)
{
    #if defined (__INTELLISENSE__)
    #define SCENE_VIEW
    #define TRANSMISSION
    #define UNITY_SINGLE_PASS_STEREO
    #endif

    float3 N = s.Normal;
    float3 L = lightDir;
	float3 E = normalize(viewDir);
	float3 h = (E + L);
	float3 H = Unity_SafeNormalize(h);
	half NdotL = max(0, dot(N, lightDir));
	float VdotH = saturate(dot(E, H));
    float NdotH = saturate(dot(N, H));
    float NdotV = saturate(dot(N, E));
	half3 Light = atten * _LightColor0.rgb;
    
    
    
	float perceptualRoughness = SmoothnessToPerceptualRoughness (s.Specular.a);
	float roughness = PerceptualRoughnessToRoughness(perceptualRoughness);
	
	roughness = max(roughness, 0.002);
	half D = GGXTerm(NdotH, roughness );
    half3 F = FresnelTerm (s.Specular.rgb * s.Cavity, saturate(dot(L, h)));
	half V = SmithJointGGXVisibilityTerm(NdotL, NdotV, roughness);
    half3 Highlight = V * D * F * NdotL;
	 //Adia
    Highlight *= any(s.Specular.rgb) ? 1.0 : 0.0;
    half4 FinalLighting = 0;
    half3 Diffuse = NdotL * s.Albedo * Light;
    FinalLighting.rgb = Highlight;
    FinalLighting.rgb *= s.Occlusion.r;
    FinalLighting.rgb *= Light * UNITY_PI;
    
    #if defined(SCENE_VIEW)
		//Adia
		#ifdef TRANSMISSION
		Diffuse += ADDITIVE_PASS_TRANSMISSION
		#endif
		return float4(Diffuse + FinalLighting.rgb, 1);
    #else	
		return float4(FinalLighting.rgb, 1);
    #endif
}

void surf (Input IN, inout DataStructure o) 
    {
            #if defined (__INTELLISENSE__)
            #define TRANSMISSION
            #endif

            half2 uv = IN.uv_MainTex;
            
            float3 Normal = UnpackNormal(tex2D(_BumpMap, uv * _BumpTile));
            o.Normal = Normal;
            
            SSS_OCCLUSION
           
            float4 Albedo = tex2D(_MainTex, uv * _AlbedoTile);
				

			//Adia
            #ifdef SCENE_VIEW
			o.Albedo = Albedo.rgb * o.Occlusion.rgb * _Color.rgb;
            #else
			
            o.Albedo = 0;
            #endif
			
            float4 Specular = float4(_SpecColor.rgb, _Glossiness) * tex2D(_SpecGlossMap, uv);
			
			//Adia
			o.Alpha = Albedo.a;
			o.Specular = Specular;
            float3 V = (IN.viewDir);
            half NdotV = max(0, dot(Normal, V));
            float3 R = WorldReflectionVector(IN, Normal);
            //Adia
            
            half Cavity = lerp(1.0, tex2D(_CavityMap, uv).r, _CavityStrength);
	        o.Cavity = Cavity;
	        
            
            //Adia
			half oneMinusReflectivity = 1 - SpecularStrength(Specular.rgb);
			half grazingTerm = saturate(Specular.a + (1-oneMinusReflectivity));
            //Adia
            //Adia
	        //Adia
			float perceptualRoughness = SmoothnessToPerceptualRoughness (Specular.a);
			float roughness = PerceptualRoughnessToRoughness(perceptualRoughness);
			//Adia
			half surfaceReduction;
			#ifdef UNITY_COLORSPACE_GAMMA
				surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;      //Adia
			#else
				surfaceReduction = 1.0 / (roughness*roughness + 1.0);           //Adia
			#endif
			half SpecularOcclusion = Jimenez_SpecularOcclusion(NdotV, Occlusion.r);
            float3 EnvironmentReflections = /*_Exposure **/ Unity_GlossyEnvironment(UNITY_PASS_TEXCUBE(unity_SpecCube0), 
            unity_SpecCube0_HDR, R, perceptualRoughness) * SpecularOcclusion * FresnelLerp(Specular.rgb * Cavity, grazingTerm, lerp(1.0, NdotV, _FresnelIntensity))
			* surfaceReduction;//Adia
            float3 Emission = EnvironmentReflections;
			//Adia
            #if !defined(SCENE_VIEW)
			{
				#ifdef SUBSURFACE_ALBEDO
				Albedo.rgb = lerp(1.0, Albedo.rgb, _AlbedoOpacity);
				Albedo.rgb = lerp(Luminance(Albedo.rgb) * 6, Albedo.rgb, _SubsurfaceAlbedoSaturation);
				#endif
				half3 LightingPass = 0;
				float4 coords = 0;
            
				coords = UNITY_PROJ_COORD(IN.screenPos);
				coords.w += 1e-9f;
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
			}
            #endif
            
            #if defined(TRANSMISSION) && defined(SCENE_VIEW)
            BASE_TRANSMISSION
            #endif
            o.Emission = Emission;
            //Adia
           
			
		}
		ENDCG
	}
	FallBack "Diffuse"
	CustomEditor "SSS_MaterialEditor"
}
