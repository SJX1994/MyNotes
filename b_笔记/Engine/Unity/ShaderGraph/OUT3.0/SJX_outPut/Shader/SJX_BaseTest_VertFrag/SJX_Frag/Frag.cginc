// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

#ifndef FRAG
#define FRAG

#include "SJX_BaseTest_HLSL/SJX_LitInput.hlsl"


struct sinControllerFrag 
{
        //A振幅,B周期,C相移,D垂直移位
        float A;
        float B;
        float C;
        float D;


};
float Saturate(float target)
{
    return clamp(target,0.0,1.0);
}
float4x4 objectSpaceMatrix()
{
    float4x4 objSpaceMat  = float4x4(
       float4 ( 0.5,  0.5, 0.5 , 1.0),
        float4  (-0.5,  0.5, 0.5, 1.0),
        float4  ( 0.5, -0.5, 0.5, 1.0),
       float4   (-0.5, -0.5, 0.5, 1.0)
    );
    return objSpaceMat;
}

float4 matrixChange(float3 Posit,float3 Scale, float3 Rotat,float4 Target )
{
    //位移矩阵
    float3 transValues = Posit;
    float4x4  transfrom = float4x4(
    float4( 1.0,        0.0,         0.0,  transValues.x ),
    float4( 0.0,        1.0,         0.0,  transValues.y ),
    float4( 0.0,        0.0,         1.0,  transValues.z ),
    float4( 0.0,        0.0,         0.0,  1.0 ) );
    //缩放矩阵
    float3 scaleV = Scale;
    float4x4  scale = float4x4(
    float4( scaleV.x, 0.0,          0.0,  0.0 ),
    float4( 0.0,      scaleV.y,     0.0,  0.0 ),
    float4( 0.0,      0.0,     scaleV.z,  0.0 ),
    float4( 0.0,      0.0,          0.0,  1.0 ) );    
    //旋转矩阵z
    float angleZ=Rotat.z;
    float4x4  rotationZ = float4x4(
    float4( cos(angleZ), -sin(angleZ),0.0,  0.0 ),
    float4( sin(angleZ),  cos(angleZ),0.0,  0.0 ),
    float4( 0.0,        0.0,          1.0,  0.0 ),
    float4( 0.0,        0.0,          0.0,  1.0 ) );
    //旋转矩阵x
    float angleX=Rotat.x;
    float4x4  rotationX = float4x4(
    float4( 1.0,        0.0,         0.0,  0.0 ),
    float4( 0.0,cos(angleX),-sin(angleX),  0.0 ),
    float4( 0.0,sin(angleX), cos(angleX),  0.0 ),
    float4( 0.0,        0.0,         0.0,  1.0 ) );
    //旋转矩阵z
    float angleY=Rotat.y;
    float4x4  rotationY = float4x4(
    float4(  cos(angleY), 0.0, sin(angleY),  0.0 ),
    float4(  0.0        , 1.0,         0.0,  0.0 ),
    float4( -sin(angleY), 0.0, cos(angleY),  0.0 ),
    float4(          0.0, 0.0,         0.0,  1.0 ) );

    return mul(mul(mul(mul(mul(Target,transfrom),scale),rotationX),rotationY),rotationZ);
}

float FragAni(float targetFloat  , sinController SC) 
{
    targetFloat =  targetFloat  + ( SC.A * sin (
                (
                  ( 2.0*PI/SC.B )  - (_Time.x *  _SJX_Coustom_Float_VertexAniSpeed) + SC.C
                )
                
            )
            + SC.D


        
        );
   
    return targetFloat;
}

inline float4 ProjectionToTextureSpace(float4 pos)
{
    //textureSpacePos
    float4 TSP = pos;

    #if defined(UNITY_HALF_TEXEL_OFFSET)

        TSP.xy = float2(TSP.x,TSP.y * _ProjectionParams.x)+TSP.w * _ScreenParams.zw;

    #else
        TSP.xy = float2(TSP.x,TSP.y * _ProjectionParams.x)+TSP.w;
    #endif
    TSP.xy = float2(TSP.x/TSP.w,TSP.y/TSP.w)*0.5f;
    return TSP;
} 
inline float4 UnityObjectToClipPos(in float3 pos)  
{  
    // More efficient than computing M*VP matrix product  
    return mul(UNITY_MATRIX_VP, mul(unity_ObjectToWorld, float4(pos, 1.0)));  
} 

float4 frag(float4 color, float2 uv,float3 N ,float3 ObjectPos, float3 shadowMask)

{

    //float4 m_positionCS = normalize(positionCS);
    //color += float4(0.5,0.5,1.0,1.0);
    //color.rgb = worldNormal*0.5+0.5;
    
        // color.rgb += _SJX_Coustom_Float.xxx;

        sinController SC;
        SC.A = 0.1;
        SC.B = 1.0;
        SC.C = 0.0;
        SC.D = 0.1;
        float fade = 0.0;
        float fade2 = 0.0;
        ObjectPos = normalize(ObjectPos);
      
        ObjectPos = matrixChange(
            float3(0.0,0.0,0.0),
            float3(1.0,1.0,1.0),
            float3(0.0,0.0,1.0),
            float4(ObjectPos.xyz,1.0)
        );

        float m_sin = fmod(sin(_Time*10.0),1.0);

         fade = smoothstep(
                            m_sin-0.01
                            ,m_sin
                            ,ObjectPos.y
                            );

         fade2 = smoothstep(
                            m_sin+0.1-0.01
                            ,m_sin+0.1
                            ,ObjectPos.y
                            );
        //fade =  step(_SJX_Coustom_Float,ObjectPos.y);
        // fade = FragAni(ObjectPos.y,SC);
        
       // fade = mul( UNITY_MATRIX_VP , float4(ObjectPosWS.x,ObjectPosWS.y,ObjectPosWS.z,1.0)).x;
        
        float Nmask =  smoothstep(-0.07,0.03,N.y).xxx;

        color.rg += (Saturate(fade.xxx) - Saturate(fade2.xxx))*Saturate(Nmask)*0.3;

        //color.rgb = step(_SJX_Coustom_Float_FragPosTest,shadowPos.xxx) ;

        float4 MVPpos = UnityObjectToClipPos( float4(ObjectPos.xyz,1.0));



        float4 ppos = ProjectionToTextureSpace(MVPpos);

       // float3 lightColor = _LightColor0.rgb;
       float4 mixTex = tex2D( _SJX_ShadowMapTex , uv);
       //color.rgba =  unity_ShadowColor;

       //unity_ShadowColor = float4(1.0,1.0,1.0,1.0);

        color.rgb = lerp(color.rgb,mixTex.rrr-0.1,Saturate(pow(1.0-shadowMask.r,2.5)) );
      
    return color;


}

#endif