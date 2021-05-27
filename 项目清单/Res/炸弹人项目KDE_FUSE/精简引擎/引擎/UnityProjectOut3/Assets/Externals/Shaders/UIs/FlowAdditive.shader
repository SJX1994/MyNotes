//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
Shader "Fuse/Custom/UI/FlowAdditive"
{
	Properties
	{
		_MainTex		("Texture",				2D)				= "white" {}
		_ScrollDirX		("Scroll Direction X",	Range(-1, 1))	= 1.0
		_ScrollDirY		("Scroll Direction Y",	Range(-1, 1))	= 1.0
		_TimeScale		("Flow Speed",			Float)			= 1.0

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
		}

		Pass
		{
			Blend		SrcAlpha One
			Cull		Back
			Lighting	Off
			ZWrite		Off
			ZTest[unity_GUIZTestMode]

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest	//Adia
			#pragma glsl_no_auto_normalization					//Adia

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
				float4 vertex		: POSITION;		//Adia
				float4 color		: COLOR;		//Adia
				float2 uv			: TEXCOORD0;	//Adia
			};

			//Adia
			//Adia
			//Adia
			//Adia
			struct v2f
			{
				float4 vertex		: SV_POSITION;	//Adia
				float4 color		: COLOR;		//Adia
				float2 uv			: TEXCOORD0;	//Adia
				//Adia
				float4 worldPos		: TEXCOORD2;	//Adia
				//Adia
			};

			//Adia


			//Adia
			//Adia
			//Adia
			//Adia

			sampler2D	_MainTex;			//Adia
			float4		_MainTex_ST;		//Adia
			float		_ScrollDirX;		//Adia
			float		_ScrollDirY;		//Adia
			float		_TimeScale;			//Adia
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
			v2f vert(appdata v)
			{
				v2f o;
				//Adia
				o.worldPos = v.vertex;
				//Adia

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;

				float2 uv = v.uv + float2(_ScrollDirX, _ScrollDirY) * _Time.y * _TimeScale;
				o.uv = TRANSFORM_TEX(uv, _MainTex);
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
				//Adia
				col *= UnityGet2DClipping( i.worldPos.xy, _ClipRect );
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
