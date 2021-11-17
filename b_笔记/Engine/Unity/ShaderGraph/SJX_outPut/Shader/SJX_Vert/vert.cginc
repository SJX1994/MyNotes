#ifndef VERT
#define VERT


float3 vert(float3 vertPosition)
{
    
   // vertPosition.xyz *= sin(_Time.yyy*half3(0.1,0.1,0.1));
    return vertPosition;
}

#endif