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
Shader "Fuse/Custom/UI/SSFlowWithMask"
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
        [Toggle(UI_SS_FLOW)] _ApplySSFlow		("Apply SS Flow",					Float)	= 0.0			//Adia
        _FlowTex								("Flow Texture",					2D)		= "white" {}	//Adia
		_FlowTexOffset							("Flow Texure Offset",				Vector)	= (0,0,0,0)		//Adia
        _FlowColor								("Flow Color",						Color)	= (1,1,1,1)		//Adia
        _FlowSampleIntensity					("Flow Texture Sample Intensity",	Float)	= 1.0			//Adia
		_FlowVector								("Flow Vector",						Vector)	= (0,0,0,0)		//Adia
		_FlowTimeScale							("Flow Speed",						Float)	= 1.0			//Adia

        [Toggle(UI_FADE)] _ApplyFade			("Apply Fade",						Float)	= 0.0			//Adia
        _FadeTimeScale							("Fade Speed",						Float)	= 1.0			//Adia

        [Toggle(UI_MAIN_AS_MASK)] _MainAsMask	("Use Main Texture As Mask",		Float)	= 0.0			//Adia
        //Adia
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

            //Adia
            #pragma multi_compile_local _ UI_SS_FLOW
            #pragma multi_compile_local _ UI_FADE
            #pragma multi_compile_local _ UI_MAIN_AS_MASK
            //Adia

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                fixed4 color    : COLOR;
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
            sampler2D	_FlowTex;				//Adia
            fixed4		_FlowTexOffset;			//Adia
            fixed4		_FlowColor;				//Adia
            fixed		_FlowSampleIntensity;	//Adia
            fixed4		_FlowVector;			//Adia
            fixed		_FlowTimeScale;			//Adia
            fixed4		_ScreenCoord;			//Adia
            fixed		_FadeTimeScale;			//Adia
            //Adia

            //Adia
            v2f vert(appdata_t v, out float4 vertex : SV_POSITION)
            //Adia
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                //Adia
                vertex = UnityObjectToClipPos(OUT.worldPosition);
                //Adia
                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                OUT.color = v.color * _Color;
                return OUT;
            }

            //Adia
            fixed4 frag(v2f IN, UNITY_VPOS_TYPE vpos : VPOS) : SV_Target
            //Adia
            {
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                //Adia
                half4 srcCol = float4(1, 1, 1, 0);
                #ifdef UI_SS_FLOW
                vpos.xy /= _ScreenParams.x;	//Adia
                vpos.x -= _ScreenCoord.x;
                vpos.y += _ScreenCoord.y;
                vpos.xy *= _FlowSampleIntensity;
                vpos.xy += _FlowTexOffset;
                srcCol = tex2D(_FlowTex, vpos.xy + _FlowVector.xy * _Time.y * _FlowTimeScale) * _FlowColor;
                #endif

                #ifdef UI_FADE
                srcCol.a *= (sin(_Time.y * _FadeTimeScale) + 1) * 0.5;
                #endif

                #ifndef UI_MAIN_AS_MASK
                //Adia
                float ma = color.a * (1 - srcCol.a);
                color = float4(srcCol.rgb * srcCol.a + color.rgb * ma / (srcCol.a + ma), color.a);
                #else
                color = srcCol * color.a;
                #endif
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
