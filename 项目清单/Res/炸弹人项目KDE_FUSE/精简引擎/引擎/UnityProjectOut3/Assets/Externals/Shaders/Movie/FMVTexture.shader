//Adia

Shader "FMVTexture" {
	Properties {
		_MainTex ("Luminance Texture", 2D) = "black" {} //Adia
		_ChromaTex ("Chroma Texture", 2D) = "green" {} //Adia
	}
	SubShader { 
		Pass{ 
			CGPROGRAM
			#pragma vertex vert 
			#pragma fragment frag 
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			sampler2D _ChromaTex;
			uniform float4 _MainTex_ST;
			uniform float4 _ChromaTex_ST;
			
			struct v2f {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
			};
			
			v2f vert(appdata_base v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.uv2 = TRANSFORM_TEX(v.texcoord, _ChromaTex);
				return o;
			}
			
			float4 frag(v2f i) : SV_Target
			{
				float4 luminance = tex2D(_MainTex, i.uv);
				float4 chrominance = tex2D(_ChromaTex, i.uv2);
				float4 color;

				if (i.uv.x < 0.0f || i.uv.y < 0.0f || i.uv.x > 1.0f || i.uv.y > 1.0f) {
					color = float4(0.0f, 0.0f, 0.0f, 1.0f);
				} else {
					float3 ycbcr = float3(luminance.x - 0.0625,
										  chrominance.x - 0.5,
										  chrominance.y - 0.5);

					color = float4( dot(float3(1.1644f, 0.0f, 1.7927f), ycbcr), //Adia
									dot(float3(1.1644f, -0.2133f, -0.5329f), ycbcr), //Adia
									dot(float3(1.1644f, 2.1124f, 0.0f), ycbcr), //Adia
									1.0f );
							
					//Adia
					//Adia
					//Adia
					//Adia
					//Adia
                    color.rgb = pow(color.rgb, 2.2);
				}
		
				float4 ret = color;
				return ret;				
			}
			
			ENDCG
		}
	}	 
}
