Shader "SSS/Samples/Sample" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		[NoScaleOffset]_ProfileTex ("Profile", 2D) = "white" {}
		_ProfileColor("Profile Color", Color) = (1.0,1.0,1.0)
		[NoScaleOffset]_MainTex ("Albedo (RGB)", 2D) = "white" {}
        [NoScaleOffset] _OcclusionMap("Occlusion", 2D) = "white" {}
		//Adia
		//Adia
		[NoScaleOffset]_BumpMap("Normal Map", 2D) = "bump" {}
        [hideininspector]SSS_shader("", float)=1//Adia
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		//Adia
		#pragma surface surf SSS_Basic nofog nometa fullforwardshadows nodynlightmap nodirlightmap  
		#pragma multi_compile _ SCENE_VIEW
        //Adia
		//Adia
		#pragma target 3.0
        #include "../Resources/SSS_Common.hlsl"
		
		

		struct Input 
        {
			float2 uv_MainTex;
            float4 screenPos;
	        float3 worldPos;
	        float3 viewDir;
		};
        struct DataStructure
        {
	        fixed3 Albedo;  //Adia
	        fixed3 Normal;  //Adia
	        fixed3 Emission;
	        fixed Alpha;
            fixed3 Occlusion;
            fixed Glossiness;
	       
        };

//Adia
half4 LightingSSS_Basic(DataStructure s, half3 lightDir, half3 viewDir, half atten)
{
    #if defined (__INTELLISENSE__)
    #define SCENE_VIEW
    #define UNITY_SINGLE_PASS_STEREO
    #endif
    #if defined(SCENE_VIEW)
	//Adia
	half Lambert = max(0, dot(s.Normal, lightDir));
	

	half3 Light = atten * _LightColor0.rgb;
    half3 Final = Lambert * s.Albedo * Light;
    return float4(Final, 1);
    #else
    return 0;
    #endif
}

void surf (Input IN, inout DataStructure o) 
    {
			//Adia
            half2 uv = IN.uv_MainTex;
            o.Normal = UnpackNormal(tex2D(_BumpMap, uv));
            o.Occlusion = tex2D(_OcclusionMap, uv).rgb;
            float3 Albedo = tex2D (_MainTex, uv).xyz;
            #ifdef SCENE_VIEW
			o.Albedo = Albedo * o.Occlusion * _Color.xyz;
            #else
            o.Albedo = 0;
            #endif

			o.Glossiness = _Glossiness;
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

            o.Emission = Albedo * LightingPass;
            #endif
			
		}
		ENDCG
	}
	FallBack "Diffuse"
}
