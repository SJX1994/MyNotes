#ifndef VERT
#define VERT

#include "SJX_BaseTest_HLSL/SJX_LitInput.hlsl"

struct sinController 
{
        //A振幅,B周期,C相移,D垂直移位
        float A;
        float B;
        float C;
        float D;


};


float3 vert(float3 vertPosition, float texCoord )
{
    
 
    return vertPosition;
}

#endif