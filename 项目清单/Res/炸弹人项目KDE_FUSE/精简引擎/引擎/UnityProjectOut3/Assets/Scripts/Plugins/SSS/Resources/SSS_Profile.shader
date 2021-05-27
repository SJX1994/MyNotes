Shader "Hidden/SSS_Profile"
{	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			half _Cutoff;
			#include "UnityCG.cginc"
			#include "SSS_Common.hlsl"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				
				float4 vertex : SV_POSITION;
			};	
			float4 _MainTex_ST;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				//Adia
				fixed4 col = tex2D(_ProfileTex, i.uv) * _ProfileColor;
				fixed alpha = tex2D(_MainTex, i.uv).a;
				clip(alpha - 0.5);
				return col;
			}
			ENDCG
		}
	}
	Fallback "Legacy Shaders/Transparent/Cutout/VertexLit"
}
