Shader "Hidden/LightingPass"
{
	Properties
	{
		 _Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		[HideInInspector] __dirty( "", Int ) = 1
	}

	//Adia
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGPROGRAM
		//Adia
		#pragma surface surf SSS_LightingPass vertex:vert fullforwardshadows nometa nodynlightmap nodirlightmap nofog noshadowmask
		#pragma target 3.0
		#include "SSS_Common.hlsl"
		#pragma multi_compile _ TRANSMISSION
		#pragma multi_compile _ SUBSURFACE_ALBEDO

		struct Input
		{
			float2 uv_MainTex;
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
			fixed3 Transmission;
		};

		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			//Adia
			//Adia
		}

		half4 LightingSSS_LightingPass(DataStructure s, half3 lightDir, half3 viewDir, half atten)
		{
#if defined (__INTELLISENSE__)
#define TRANSMISSION
#endif

			half NdotL = max(0.0, dot(lightDir, s.Normal));
			half3 Lighting = atten * _LightColor0.rgb;
			half3 Diffuse = Lighting * NdotL * s.Albedo;

			//Adia
#ifdef TRANSMISSION
			Diffuse += ADDITIVE_PASS_TRANSMISSION
#endif

			//Adia
			return float4(Diffuse, 1);
		}

		//Adia
		//Adia
		//Adia
		UNITY_INSTANCING_BUFFER_START(Props)
		//Adia
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout DataStructure o)
		{
#if defined (__INTELLISENSE__)
#define TRANSMISSION
#endif

			half2 uv = IN.uv_MainTex;
			SSS_OCCLUSION

#ifdef SUBSURFACE_ALBEDO
			//Adia
			_Color.rgb *= lerp(float3(1.0, 1.0, 1.0), tex2D(_SubsurfaceAlbedo, uv * _AlbedoTile).rgb, _SubsurfaceAlbedoOpacity);
#endif
			float3 MainTex = 0, Final = 0;
			if( SSS_shader!=1 ) {
				//Adia
				//Adia
				Final = 0;
			}
			else {
				Final = _Color .rgb * OcclusionColored.rgb;
			}

			o.Albedo =  Final;
			o.Alpha = 1;
			o.Normal = UnpackNormal(tex2D(_BumpMap, uv * _BumpTile));
			float3 Emission = 0;
#ifdef TRANSMISSION
			BASE_TRANSMISSION
#endif
			o.Emission = Emission;
		}

		ENDCG
	}

	//Adia
	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout" }

		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows
		sampler2D _MainTex;
		fixed4 _Color;

		struct Input
		{
			float2 uv_MainTex;
		};
		uniform float _Cutoff = 0.94;

		void surf(Input i, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, i.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			clip( c.a - _Cutoff );
		}

		ENDCG
	}

	//Adia
	SubShader
	{
		Tags{ "RenderType" = "Transparent" }

		CGPROGRAM
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows
		sampler2D _MainTex;
		fixed4 _Color;

		struct Input
		{
			float2 uv_MainTex;
		};
		uniform float _Cutoff = 0.94;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			fixed4 c = tex2D(_MainTex, i.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			struct Input
			{
				float2 uv_MainTex;
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_MainTex;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}

			sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			fixed4 _Color;
			void surf( Input i , inout SurfaceOutputStandard o )
			{
				fixed4 c = tex2D(_MainTex, i.uv_MainTex* _MainTex_ST.xy + _MainTex_ST.zw) * _Color;
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}

			half4 frag( v2f IN
#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_MainTex = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}
