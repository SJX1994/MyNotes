//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
Shader "Fuse/Custom/UI/UIFontThickness"
{
	Properties{
		_MainTex		("Base(RGB), Alpha(A)",	2D)		= "white" {}		//Adia

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
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}
		ColorMask [_ColorMask]

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
			//Adia
			#pragma multi_compile __ UNITY_UI_CLIP_RECT
			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			//Adia

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"


			//Adia
			//Adia
			//Adia
			//Adia

			//Adia
			//Adia
			//Adia
			//Adia
			struct appdata_vc
			{
				float4 vertex		: POSITION;			//Adia
				float4 color		: COLOR;			//Adia
				float2 texcoord0	: TEXCOORD0;		//Adia
				float2 param		: TEXCOORD2;		//Adia
			};

			//Adia
			//Adia
			//Adia
			//Adia
			struct v2f
			{
				float4 pos			: SV_POSITION;		//Adia
				float4 col			: COLOR0;			//Adia
				float2 uv			: TEXCOORD0;		//Adia
				float2 borders		: TEXCOORD1;		//Adia
				float4 mask			: TEXCOORD2;		//Adia
				//Adia
				float4 worldPos		: TEXCOORD3;		//Adia
				//Adia
			};

			//Adia


			//Adia
			//Adia
			//Adia
			//Adia

			#define CHANNEL_MULTIPLY	(0.1)	//Adia

			//Adia


			//Adia
			//Adia
			//Adia
			//Adia

			sampler2D	_MainTex;			//Adia
			//Adia
			bool		_UseClipRect;		//Adia
			float4		_ClipRect;			//Adia
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
			float smstep(float min, float max, float v)
			{
				v = saturate( (v - min) / (max - min) );
				return saturate( v * v * (3 - 2 * v) );
			}

			//Adia
			//Adia
			//Adia
			//Adia
			//Adia
			//Adia
			float getColorCode(float v, int reference) {
				return round( v ) == reference;
			}

			//Adia
			//Adia
			//Adia
			//Adia
			//Adia
			v2f vert(appdata_vc v)
			{
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f, o);

				//Adia
				o.worldPos = v.vertex;
				//Adia
				o.pos = UnityObjectToClipPos(v.vertex);
				o.col = v.color;

				o.uv = v.texcoord0;
				o.borders.x = v.param.x;									//Adia
				o.borders.y = min(o.borders.x, v.param.x - v.param.y);		//Adia

				//Adia
				float code = floor(v.texcoord0) * CHANNEL_MULTIPLY;
				o.mask = float4(getColorCode(code, 1), getColorCode(code, 2), getColorCode(code, 4), getColorCode(code, 8));
				return o;
			}

			//Adia
			//Adia
			//Adia
			//Adia
			//Adia
			float4 frag(v2f i) : SV_Target
			{ 
				float4 pick = tex2D(_MainTex, i.uv) * i.mask;
				float dist = length(pick);
				float b0 = i.borders.x;
				float b1 = i.borders.y;
				float m = smstep(b1, b0, dist);
				float4 drawColor = i.col;
				drawColor.a *= m;

				//Adia
				#ifdef UNITY_UI_CLIP_RECT
				drawColor *= UnityGet2DClipping(i.worldPos.xy, _ClipRect);
				#endif

				#ifdef UNITY_UI_ALPHACLIP
				clip(drawColor.a - 0.001);
				#endif
				//Adia

				return drawColor;
			}

			//Adia

			ENDCG
		}
	}
}
//Adia
//Adia
//Adia
