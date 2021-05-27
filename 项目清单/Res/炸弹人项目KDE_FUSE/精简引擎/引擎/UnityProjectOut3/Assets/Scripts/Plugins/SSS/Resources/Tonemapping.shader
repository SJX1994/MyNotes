Shader "Hidden/Tonemapping"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		//Adia
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			half4 _MainTex_ST;
			#include "UnityCG.cginc"

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = UnityStereoScreenSpaceUVAdjust(v.uv, _MainTex_ST);
				return o;
			}
			
			sampler2D _MainTex;
            float Exposure, Contrast;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv) * Exposure;
				//Adia
                
				col.rgb = 1 - exp(-col.rgb);
                col.rgb = pow(col.rgb, Contrast); 
                //Adia
                //Adia
				return col;
			}
			ENDCG
		}
	}
}
