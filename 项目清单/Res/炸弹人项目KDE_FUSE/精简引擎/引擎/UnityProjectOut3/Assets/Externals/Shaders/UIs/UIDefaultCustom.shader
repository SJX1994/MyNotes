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
Shader "Fuse/Custom/UI/DefaultCustom"
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
        [Toggle(UI_SS_LINE_OVERLAY)] _ApplySSLineOverlay	("Apply SS Line Overlay",					Float) 			= 0						//Adia
        _SSLineOverlayTex									("SS Line Overlay Texture",					2D)				= "white" {}			//Adia
        _SSLineOverlayColor									("SS Line Overlay Color",					Color)			= (1.0, 1.0, 1.0, 1.0)	//Adia
        _SSLineOverlayUVSampleMultiplier					("SS Line Overlay UV Sample Multiplier",	Float)			= 1.0					//Adia
        _SSLineOverlayUVOffsetYIntensity					("SS Line Overlay UV Offset Y Intensity",	Float)			= 1.0					//Adia
        _SSLineOverlayUVFlowIntensity						("SS Line Overlay UV Flow Intensity",		Float)			= 1.0					//Adia

        [Toggle(UI_SS_LINE_MULTIPLY)] _ApplySSLineMultiply	("Apply SS Line Multiply",					Float) 			= 0						//Adia
        _SSLineMultiplyTex									("SS Line Multiply Texture",				2D)				= "white" {}			//Adia
        _SSLineMultiplyUVSampleMultiplier					("SS Line Multiply UV Sample Multiplier",	Float)			= 1.0					//Adia
        _SSLineMultiplyUVOffsetYIntensity					("SS Line Multiply UV Offset Y Intensity",	Float)			= 1.0					//Adia
        _SSLineMultiplyUVFlowIntensityX						("SS Line Multiply UV Flow Intensity X",	Float)			= 1.0					//Adia
        _SSLineMultiplyUVFlowIntensityY						("SS Line Multiply UV Flow Intensity Y",	Float)			= 1.0					//Adia
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
            //Adia
            #include "UISSLineOverlay.cginc"
            #include "UISSLineMultiply.cginc"
            //Adia

            //Adia
            #pragma multi_compile __ UNITY_UI_CLIP_RECT
            #pragma multi_compile __ UNITY_UI_ALPHACLIP
            //Adia
            //Adia
            #pragma multi_compile __ UI_SS_LINE_OVERLAY
            #pragma multi_compile __ UI_SS_LINE_MULTIPLY

            #if defined(UI_SS_LINE_OVERLAY) || defined(UI_SS_LINE_MULTIPLY)
            #define USE_CUSTOM_VARIABLES
            #endif
            //Adia

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                //Adia
                #ifdef USE_CUSTOM_VARIABLES
                float2 texcoord3 : TEXCOORD3;		//Adia
                #endif
                //Adia
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                //Adia
                #ifdef USE_CUSTOM_VARIABLES
                float2 vcoord   : TEXCOORD2;		//Adia
                #endif
                //Adia
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                OUT.color = v.color * _Color;

                //Adia
                #ifdef USE_CUSTOM_VARIABLES
                OUT.vcoord = v.texcoord3;
                #endif
                //Adia
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                //Adia
                #ifdef UI_SS_LINE_OVERLAY
                color = getSSLineOverlayColor(IN.vcoord, color);
                #endif
                #ifdef UI_SS_LINE_MULTIPLY
                color = getSSMultiplyColor(IN.vcoord, color);
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
