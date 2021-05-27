//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
Shader "Fuse/Custom/UI/FlowAnimationWithPseudoAA"
{
	Properties
	{
		_MainTex			("Texture",					2D)					= "white" {}
		_AspectRatio		("Aspect Ratio (H / W)",	Float)				= 1.0
		_ScrollDirX			("Scroll Direction X",		Range(-1, 1))		= 1.0
		_ScrollDirY			("Scroll Direction Y",		Range(-1, 1))		= 1.0
		_TimeScale			("Flow Speed",				Float)				= 1.0
		_PseudoAAAxisX		("Pseudo AA Power Axis X",	Range(0, 1))		= 1.0
		_PseudoAAAxisY		("Pseudo AA Power Axis Y",	Range(0, 1))		= 1.0
		_PseudoAA			("Pseudo AA",				Range(1, 1000))		= 500

		//Adia
		[HideInInspector]_StencilComp( "Stencil Comparison", Float ) = 8
		[HideInInspector]_Stencil( "Stencil ID", Float ) = 0
		[HideInInspector]_StencilOp( "Stencil Operation", Float ) = 0
		[HideInInspector]_StencilWriteMask( "Stencil Write Mask", Float ) = 255
		[HideInInspector]_StencilReadMask( "Stencil Read Mask", Float ) = 255
		[HideInInspector]_ColorMask( "Color Mask", Float ) = 15
	}
	SubShader
	{
		Tags{
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"Queue" = "Transparent"
		}
		LOD 100

		//Adia
		Stencil{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}
		ColorMask [_ColorMask]

		Pass
		{
			Blend		SrcAlpha OneMinusSrcAlpha
			Cull		Back
			Lighting	Off
			ZWrite		Off
			ZTest[unity_GUIZTestMode]

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest	//Adia
			#pragma glsl_no_auto_normalization					//Adia
			//Adia
			#pragma multi_compile __ UNITY_UI_CLIP_RECT
			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			//Adia

			#include "UnityUI.cginc"
			#include "UnityCG.cginc"

			//Adia
			//Adia
			//Adia
			//Adia

			//Adia
			//Adia
			//Adia
			//Adia
			struct appdata
			{
				float4 vertex		: POSITION;			//Adia
				float4 color		: COLOR;			//Adia
				float2 uv			: TEXCOORD0;		//Adia
				float2 vpos			: TEXCOORD1;		//Adia
			};

			//Adia
			//Adia
			//Adia
			//Adia
			struct v2f
			{
				float4 vertex		: SV_POSITION;		//Adia
				float4 color		: COLOR;			//Adia
				float2 uv			: TEXCOORD0;		//Adia
				float2 vpos			: TEXCOORD1;		//Adia
				//Adia
				float4 worldPos		: TEXCOORD2;		//Adia
				//Adia
			};

			//Adia


			//Adia
			//Adia
			//Adia
			//Adia

			sampler2D	_MainTex;				//Adia
			float4		_MainTex_ST;			//Adia
			float		_AspectRatio;			//Adia
			float		_ScrollDirX;			//Adia
			float		_ScrollDirY;			//Adia
			float		_TimeScale;				//Adia
			float		_PseudoAAAxisX;			//Adia
			float		_PseudoAAAxisY;			//Adia
			float		_PseudoAA;				//Adia
			//Adia
			bool		_UseClipRect;			//Adia
			float4		_ClipRect;				//Adia
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
			v2f vert(appdata v)
			{
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				//Adia
				o.worldPos = v.vertex;
				//Adia

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;

				float2 uv = v.uv + float2(_ScrollDirX, _ScrollDirY) * _Time.y * _TimeScale;
				uv.y *= _AspectRatio;
				o.uv = TRANSFORM_TEX(uv, _MainTex);
				o.vpos = v.vpos;

				return o;
			}

			//Adia
			//Adia
			//Adia
			//Adia
			//Adia
			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv) * i.color;
				fixed2 t = abs(i.vpos * 2 - 1) * float2(_PseudoAAAxisX, _PseudoAAAxisY);
				col.a *= 1.0 - length(pow(t, _PseudoAA));

				//Adia
				#ifdef UNITY_UI_CLIP_RECT
				col.a *= UnityGet2DClipping(i.worldPos.xy, _ClipRect);
				#endif

				#ifdef UNITY_UI_ALPHACLIP
				clip(col.a - 0.001);
				#endif
				//Adia

				return col;
			}

			//Adia

			ENDCG
		}
	}
}
//Adia
//Adia
//Adia
