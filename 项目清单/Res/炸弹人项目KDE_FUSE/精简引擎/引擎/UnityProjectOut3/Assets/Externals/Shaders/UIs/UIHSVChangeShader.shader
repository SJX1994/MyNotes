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
//Adia
//Adia
//Adia
//Adia
Shader "Fuse/Custom/UI/HSVChangeShader"
//Adia
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		

		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255

		_ColorMask("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0

		//Adia
		_Hue		("Hue", Range(0.0, 1.0)) = 0.0
		_Saturation	("Saturation", Range(-1.0, 1.0)) = 0.0
		_Value		("Value", Range(-1.0, 1.0)) = 0.0
		//Adia
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Stencil
			{
				Ref[_Stencil]
				Comp[_StencilComp]
				Pass[_StencilOp]
				ReadMask[_StencilReadMask]
				WriteMask[_StencilWriteMask]
			}

			Cull Off
			Lighting Off
			ZWrite Off
			ZTest[unity_GUIZTestMode]
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask[_ColorMask]

			Pass
			{
				Name "Default"
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"

				#pragma multi_compile_local _ UNITY_UI_CLIP_RECT
				#pragma multi_compile_local _ UNITY_UI_ALPHACLIP

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord  : TEXCOORD0;
					float4 worldPosition : TEXCOORD1;
					UNITY_VERTEX_OUTPUT_STEREO
				};

				sampler2D _MainTex;
				fixed4 _Color;
				fixed4 _TextureSampleAdd;
				float4 _ClipRect;
				float4 _MainTex_ST;

				//Adia
				half _Hue, _Saturation, _Value;
				//Adia

				v2f vert(appdata_t v)
				{
					v2f OUT;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
					OUT.worldPosition = v.vertex;
					OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

					OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

					OUT.color = v.color * _Color;
					return OUT;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

					#ifdef UNITY_UI_CLIP_RECT
					color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
					#endif

					#ifdef UNITY_UI_ALPHACLIP
					clip(color.a - 0.001);
					#endif

					//Adia
					//Adia
					//Adia
					half hue, saturation;
					half maxColor = max(color.r, max(color.g, color.b));
					half minColor = min(color.r, min(color.g, color.b));
					half delta = maxColor - minColor;
					hue = delta == 0 ? 0 : lerp(lerp(
						(color.r - color.g) / delta + 4,
						(color.r - color.b) / delta + 2, step(color.g, maxColor)),
						(color.g - color.b) / delta, step(color.r, maxColor));
					hue = fmod(hue + _Hue * 6.0, 6.0);
					saturation = maxColor == 0 ? 0 : clamp(delta / maxColor + _Saturation, 0, 1);

					//Adia
					maxColor = clamp(maxColor + _Value, 0, 1);
					minColor = maxColor - (saturation * maxColor);
					color = lerp(lerp(lerp(lerp(lerp(
						half4(maxColor, hue * delta + minColor, minColor, color.a),
						half4((2.0 - hue) * delta + minColor, maxColor, minColor, color.a), step(1, hue)),
						half4(minColor, maxColor, (hue - 2.0) * delta + minColor, color.a), step(2, hue)),
						half4(minColor, (4.0 - hue) * delta + minColor, maxColor, color.a), step(3, hue)),
						half4((hue - 4.0) * delta + minColor, minColor, maxColor, color.a), step(4, hue)),
						half4(maxColor, minColor, (6.0 - hue) * delta + minColor, color.a), step(5, hue));
					//Adia

					return color;
				}
			ENDCG
			}
		}
}
//Adia
//Adia
//===========================================================================