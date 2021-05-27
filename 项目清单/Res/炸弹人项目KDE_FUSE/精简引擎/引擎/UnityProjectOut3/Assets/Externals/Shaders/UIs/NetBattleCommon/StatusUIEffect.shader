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
Shader "Fuse/Custom/UI/StatusUIEffect"
//Adia
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0

		//Adia
		_FlowTex								("Flow Texture",					2D)		= "white" {}	//Adia
		_FlowSampleIntensity					("Flow Texture Sample Intensity",	Float)	= 1.0			//Adia
		_FlowTimeScale							("Flow Speed",						Float)	= 1.0			//Adia
		//Adia

        _FadeTimeScale							("Fade Speed",						Float)	= 1.0			//Adia
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

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
				//Adia
				float2 param    : TEXCOORD3;	//Adia
				//Adia
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
				//Adia
				float2 param    : TEXCOORD3;	//Adia
				//Adia
                UNITY_VERTEX_OUTPUT_STEREO
            };

			//Adia
			#define PLAYER_MAX 64		//Adia
			//Adia

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;

			//Adia
			sampler2D	_FlowTex;						//Adia
			fixed		_FlowSampleIntensity;			//Adia
			fixed		_FlowTimeScale;					//Adia
			fixed		_FadeTimeScale;					//Adia

			fixed		_EnabledFlags[PLAYER_MAX];		//Adia
			fixed4		_ScreenCoords[PLAYER_MAX];		//Adia
			fixed4		_FlowColors[PLAYER_MAX];		//Adia
			fixed4		_FlowTexOffsets[PLAYER_MAX];	//Adia
			fixed4		_FlowVectors[PLAYER_MAX];		//Adia
			//Adia

			//Adia
			v2f vert(appdata_t v, out float4 vertex : SV_POSITION)
			//Adia
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                OUT.color = v.color * _Color;

				//Adia
				vertex = UnityObjectToClipPos(OUT.worldPosition);
				OUT.param = v.param;
				//Adia
                return OUT;
            }

			//Adia
			fixed4 frag(v2f IN, UNITY_VPOS_TYPE vpos : VPOS) : SV_Target
			//Adia
            {
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd);

				//Adia
				int instanceId = round(fmod(IN.param.y, PLAYER_MAX));
				fixed enabled = _EnabledFlags[instanceId];
				if( enabled ) {
					fixed4 screenCoord = _ScreenCoords[instanceId];
					fixed4 flowColor = _FlowColors[instanceId];
					fixed4 flowTexOffset = _FlowTexOffsets[instanceId];
					fixed4 flowVector = _FlowVectors[instanceId];

					vpos.xy /= _ScreenParams.x;	//Adia
					vpos.x -= screenCoord.x;
					vpos.y += screenCoord.y;
					vpos.xy *= _FlowSampleIntensity;
					vpos.xy += flowTexOffset;

					fixed4 flowCol = tex2D(_FlowTex, vpos.xy + flowVector.xy * _Time.y * _FlowTimeScale) * flowColor;
					flowCol.a *= (sin(_Time.y * _FadeTimeScale) + 1) * 0.5;

					fixed ma = color.a * (1 - flowCol.a);
					fixed4 colorUnmasked = float4(flowCol.rgb * flowCol.a + color.rgb * IN.color.rgb * ma / (flowCol.a + ma), color.a);
					fixed4 colorMasked = flowCol * color.a;
					color = lerp(colorMasked, colorUnmasked, IN.color.a);
				}
				else {
					color *= IN.color;
				}
				//Adia

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                return color;
            }
        ENDCG
        }
    }
}
//Adia
//Adia
//Adia
