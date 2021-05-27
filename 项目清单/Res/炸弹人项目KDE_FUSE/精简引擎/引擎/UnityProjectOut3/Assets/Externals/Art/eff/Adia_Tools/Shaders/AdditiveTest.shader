//Adia



Shader "Adia/Additve" {
	Properties{
		
		//Adia
		_MainTex("Particle Texture", 2D) = "white" {}
		//Adia
		_XPosFormMainTex("X position",float) = 0
		//Adia
		_YPosFormMainTex("Y position",float) = 0
		//Adia
		_WidthForSubTex("Width",float) = 0
		//Adia
		_HeightForSubTex("Height",float) = 0
		//Adia
		_XMainTex("X max pixel",float) = 1024
		//Adia
		_YMainTex("Y max pixel",float) = 2048

	}

		Category{
			Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" }
			Blend SrcAlpha One
			ColorMask RGB
			Cull Off Lighting Off ZWrite Off

			SubShader {
				Pass {

					Blend SrcAlpha One

					CGPROGRAM
					

					#pragma vertex vert
					#pragma fragment frag
					#pragma target 3.0
					

					#include "UnityCG.cginc"
					
					sampler2D _MainTex;

				

					struct v2f {
						float4 pos   : SV_POSITION;
            			    	float2 uv    : TEXCOORD0;
					};
					float4 _MainTex_ST;
					float _XPosFormMainTex;
					float _YPosFormMainTex;
					float _XMainTex;
					float _YMainTex;
					float _WidthForSubTex;
					float _HeightForSubTex;

					v2f vert(appdata_base v)
					{
						v2f m_v2f;

						m_v2f.pos=UnityObjectToClipPos (v.vertex);

						//Adia
						

						//Adia
                				m_v2f.uv = float2 ( v.texcoord.x + _XPosFormMainTex , v.texcoord.y + _YPosFormMainTex );

						return m_v2f;
					}
					

					fixed4 frag(v2f m_v2f) : COLOR
					{
						fixed4 c = tex2D (_MainTex, m_v2f.uv) ;

						return c;
					}

					ENDCG
					}
				}
	}

		Fallback "Fuse/Custom/Particles/Additive"
								
}

