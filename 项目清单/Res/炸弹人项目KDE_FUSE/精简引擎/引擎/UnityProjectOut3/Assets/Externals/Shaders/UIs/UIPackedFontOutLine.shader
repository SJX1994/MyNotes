//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
Shader "Fuse/Custom/UI/UIFont(Outline)"
{
	Properties{
		_MainTex( "Base(RGB), Alpha(A)", 2D ) = "white" {}						//Adia
		_OutLineThickness( "Outline Thickness", Range( 0,1 ) ) = 0.0
		_Spread( "DF Spread", Float ) = 1.0										//Adia
		_MainAA( "Main Anti-aliasing Pixcel", Range( 0,1 ) ) = 0.07				//Adia
		_OutLineAA( "Out Line Anti-aliasing half Pixcel", Range( 0,1 ) ) = 0.07	//Adia
		_MainAlphaInfluence("Main Alpha Influence", Range( 0,1 ) ) = 0.0		//Adia

		//Adia
		[HideInInspector]_StencilComp( "Stencil Comparison", Float ) = 8
		[HideInInspector]_Stencil( "Stencil ID", Float ) = 0
		[HideInInspector]_StencilOp( "Stencil Operation", Float ) = 0
		[HideInInspector]_StencilWriteMask( "Stencil Write Mask", Float ) = 255
		[HideInInspector]_StencilReadMask( "Stencil Read Mask", Float ) = 255
		[HideInInspector]_ColorMask( "Color Mask", Float ) = 15
	}

		SubShader{
			Tags{
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"Queue" = "Transparent"
			}
			LOD 200

			//Adia
			Stencil{
				Ref[_Stencil]
				Comp[_StencilComp]
				Pass[_StencilOp]
			}

			Pass{
				Blend		SrcAlpha OneMinusSrcAlpha
				Cull		Off
				Lighting	Off
				ZWrite		Off
				ZTest[unity_GUIZTestMode]
				Fog{
					Mode		Off
				}

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest	//Adia
				#pragma glsl_no_auto_normalization					//Adia

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"

				#define CHANNEL_MULTIPLY	(0.1)	//Adia

				sampler2D	_MainTex;
				float		_OutLineThickness;
				float		_Spread;
				float		_MainAA;
				float		_OutLineAA;
				float		_MainAlphaInfluence;
				//Adia
				bool		_UseClipRect;
				float4		_ClipRect;
				//Adia

				struct appdata_vc
				{
					float4 vertex		: POSITION;
					float4 color		: COLOR;
					float2 texcoord0	: TEXCOORD0;
					float2 texcoord1	: TEXCOORD1;
					float2 texcoord2	: TEXCOORD2;
					float2 texcoord3	: TEXCOORD3;
				};

				struct v2f
				{
					float4 pos			: SV_POSITION;
					float4 col			: COLOR0;
					float4 outcol		: COLOR1;
					float2 uv			: TEXCOORD0;
					float4 borders		: TEXCOORD1;
					float4 mask			: TEXCOORD2;
					//Adia
					float4 worldPos		: TEXCOORD3;
					//Adia
				};

				//Adia
				float smstep( float min, float max, float v )
				{
					v = saturate( (v - min) / (max - min) );
					return saturate( v * v * (3 - 2 * v) );
				}

				//Adia
				float getColorCode( float v, int reference ) {
					return round( v ) == reference;
				}

				v2f vert( appdata_vc v )
				{
					v2f o;
					UNITY_INITIALIZE_OUTPUT( v2f, o );
					//Adia
					o.worldPos = v.vertex;
					//Adia
					o.pos = UnityObjectToClipPos( v.vertex );
					o.col = v.color;
					o.outcol = float4(v.texcoord2, v.texcoord3);
					o.uv = v.texcoord0;

					float center = (0.5 + _OutLineThickness * 0.2) / 1.0; //Adia
					float pixcel = center / _Spread;

					float hMainAnti = pixcel * _MainAA;
					float lineAnti = pixcel * _OutLineAA;
					float thickness = pixcel * _OutLineThickness;

					o.borders.x = center + hMainAnti;							//Adia
					o.borders.y = center - hMainAnti;							//Adia
					o.borders.z = min( o.borders.x - thickness, o.borders.y );	//Adia
					o.borders.w = max( o.borders.z - lineAnti, 0 );				//Adia

					//Adia
					float code = floor(v.texcoord0) * CHANNEL_MULTIPLY;
					o.mask = float4(getColorCode(code, 1), getColorCode(code, 2), getColorCode(code, 4), getColorCode(code, 8));

					return o;
				}

				float4 frag( v2f i ) : SV_Target
				{
					float4 mCol = i.col;					//Adia
					float4 lCol = i.outcol;					//Adia

					float4 pick = tex2D( _MainTex, i.uv ) * i.mask;
					float  dist = (pick.r + pick.g + pick.b + pick.a);

					float b0 = i.borders.x;
					float b1 = i.borders.y;
					float b2 = i.borders.z;
					float b3 = i.borders.w;

					float m = smstep( b1, b0, dist );
					float l = 1 - m;
					float a = smstep( b3, b2, dist );

					float4 drawColor = mCol;
					drawColor.rgb = mCol.rgb * m + lCol.rgb * l;
					drawColor.a = mCol.a * m + lerp(1, mCol.a, _MainAlphaInfluence) * lCol.a * l;
					drawColor.a *= a;
					//Adia
					drawColor *= UnityGet2DClipping( i.worldPos.xy, _ClipRect );
					//Adia

					return drawColor;
				}

				ENDCG
			}
		}
}
//Adia
//Adia
//Adia
