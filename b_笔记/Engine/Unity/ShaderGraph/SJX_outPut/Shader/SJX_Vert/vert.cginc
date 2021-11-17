#ifndef VERT
#define VERT

struct sinController 
{
        //A振幅,B周期,C相移,D垂直移位
        float A;
        float B;
        float C;
        float D;


};

float vertexAni(float vertPos  , sinController SC) 
{
    vertPos =  vertPos  + ( SC.A * sin (
                (
                  ( 2.0*PI/SC.B )  - (_Time.x * _SJX_Coustom_Float) + SC.C
                )
                
            )
            + SC.D


        
        );
   
    return vertPos;
}

float3 vert(float3 vertPosition, float texCoord )
{
    
   // vertPosition.xyz *= sin(_Time.yyy*half3(0.1,0.1,0.1));
 // vertPosition.xyz = positionOS.xyz;
     // vertPosition.y += sin(_Time.x); 
      sinController SC ;
      SC.A = 0.1;
      SC.B = 1.0;
      SC.C = 0.0;
      SC.D = 0.1;
      vertPosition.y = vertexAni( vertPosition.y,SC);
    return vertPosition;
}

#endif