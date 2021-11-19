precision mediump float;

//base
uniform mat4 WorldMatrix;
uniform mat4 NormalMatrix;
uniform vec3 CameraPosition;
uniform sampler2D Texture;

varying vec3 vNormal;
uniform vec4 BaseColorCarDisk;

//坐标切分（节省空间）
varying vec2 vTexCoord;
varying vec2 vTexCoord0;
varying vec2 vTexCoord1;
varying vec2 vTexCoord2;

//真实片元光照
#define PI 3.1415926538
varying mediump vec3 vSpotLightDirection[2];
varying mediump float vSpotCutoff[2];
varying mediump vec3 vViewDirection;

uniform mediump vec3 SpotLightDirection[2];
uniform mediump vec3 SpotLightPosition[2];
uniform mediump float SpotLightCutoffAngle[2];
uniform mediump vec4 SpotLightColor[2];
uniform mediump vec3 SpotLightAttenuation[2];
uniform mediump float SpotLightExponent[2];

uniform mediump float SpecularExponent;
uniform mediump vec4 Diffuse;
uniform mediump vec4 SpecularColor;

//光照流程控制
uniform bool isRipUnderCar;
uniform bool isRipUnderCarLight;
uniform bool isRipUnderCarLightToggle;

//Disk光圈控制
uniform mediump float Time;
uniform mediump float TimeScale;
uniform mediump float EdgeShap;

float Saturate(float target)
    {
        return clamp(target,0.0,1.0);
    }
vec3 drawCircle(float radius,float colorDepth,vec4 Tex)
{
    return clamp(pow((Tex.rgb)*radius,vec3(15.0)),0.0,colorDepth);
    
}
vec3 drawCircleAni( float TimeDelay ,vec4 Tex)
{
    float iTime = fract(Time*TimeScale-TimeDelay);
    float fadeOutTime = clamp(0.85 - iTime,0.0,1.0);
    return clamp(sin(clamp((Tex.rgb + ( iTime - 1.0))*EdgeShap,0.0,1.0)*PI)*fadeOutTime,0.0,0.1) ;
}
vec4 drawButtonDisk(float fA, vec3 fCol)
{
    vec3 N = vNormal;
    vec4 Tex = texture2D(Texture,vTexCoord0);
    vec4 TexFackLight;
    vec4 shadow;
    float z;
    
    
    //静态样式
    vec3 circle0 = drawCircle(9.0,0.5,Tex);
    vec3 circle1 = drawCircle(1.6,0.5,Tex);
    vec3 circle2 = (drawCircle(7.2,0.61,Tex) - drawCircle(5.8,0.61,Tex))*0.3;
    
    //动态样式
    vec3 circle0Ani = drawCircleAni(0.0,Tex);
    vec3 circle1Ani = drawCircleAni(0.2,Tex);
    vec3 circle2Ani = drawCircleAni(0.4,Tex);
    vec3 circle3Ani = drawCircleAni(0.6,Tex);
    vec3 circle4Ani = drawCircleAni(0.8,Tex);
    
    vec3 circleAnis = circle0Ani + circle1Ani + circle2Ani + circle3Ani + circle4Ani;
    
    fA = (circle0 - (circle1 + circleAnis )+( -circle2 - circleAnis)).r* 1.0 * BaseColorCarDisk.a;
    
    fCol = (1.0 - Tex.rgb)*BaseColorCarDisk.rgb;
    
    return vec4(fCol,fA);
}
vec3 realLight(vec3 fCol)
{
    vec3 L[2];
    vec3 H[2];
    float LdotN, NdotH;
    float specular;
    vec3 lightColor;
    vec3 c;
    float d, attenuation;
    vec3 color = vec3(0.0);
    vec3 Nl = normalize(vNormal);
    vec3 Vl = normalize(vViewDirection); 
    
    L[0] = normalize(-vSpotLightDirection[0]);
    H[0] = normalize(-Vl + L[0]);
    
    
    L[1] = normalize(-vSpotLightDirection[1]);
    H[1] = normalize(-Vl + L[1]); 
    
    // 支持 spot light 0.
    if(length(SpotLightColor[0].rgb  ) > 0.01 && isRipUnderCar == true)
    {
        LdotN = dot(L[0], Nl);
        if(LdotN > 0.0)
        {
            LdotN = max(0.0, LdotN);
            NdotH = max(0.0, dot(Nl, H[0]));
            
            vec3 D = normalize(SpotLightDirection[0]);
            float spotEffect = dot(D, -L[0]);
            c = SpotLightAttenuation[0];
            d = length(L[0]);
            specular = pow(NdotH, SpecularExponent);
            
            if(spotEffect > vSpotCutoff[0])
            {
                spotEffect = pow(spotEffect, SpotLightExponent[0]);
                attenuation = spotEffect / (c.x + c.y * d + c.z * d * d);
                lightColor = (Diffuse.rgb * LdotN) + SpecularColor.rgb * specular;
                lightColor *= attenuation;
                lightColor *= SpotLightColor[0].rgb;
                
                fCol += lightColor;
                
            }
        }
    }
    // 支持 spot light 1.
    if(length(SpotLightColor[1].rgb) > 0.01 && isRipUnderCar == true)
    {
        LdotN = dot(L[1], Nl);
        if(LdotN > 0.0)
        {
            LdotN = max(0.0, LdotN);
            NdotH = max(0.0, dot(Nl, H[1]));
            
            vec3 D = normalize(SpotLightDirection[1]);
            float spotEffect = dot(D, -L[1]);
            c = SpotLightAttenuation[1];
            d = length(L[1]);
            specular = pow(NdotH, SpecularExponent);
            
            if(spotEffect > vSpotCutoff[1])
            {
                spotEffect = pow(spotEffect, SpotLightExponent[1]);
                attenuation = spotEffect / (c.x + c.y * d + c.z * d * d);
                lightColor = (Diffuse.rgb * LdotN) + SpecularColor.rgb * specular;
                lightColor *= attenuation;
                lightColor *= SpotLightColor[1].rgb;
                
                fCol+= lightColor;
            }
        }
    }
    return fCol;
}
vec3 fackLight(vec3 fCol)
{
    return fCol+texture2D(Texture,vTexCoord1).rgb*2.0;
}
vec4 fackShadow(vec3 fCol,float fA)
{
    return vec4(clamp(fCol - pow(texture2D(Texture,vTexCoord2).rgb,vec3(1.2)),0.0,1.0),clamp(fA,0.0,1.0) + texture2D(Texture,vTexCoord2).r);
}
vec3 deepEffect(vec3 fCol)
{
    
    float near = 0.4;
    float far = 50.0;

    float z = 1.0 - clamp( near / (far + near - (gl_FragCoord.z* 2.0 - 1.0) * (far - near) )*4.0,0.0,1.0);
    
    return fCol + vec3(pow(drawCircle(9.0,0.5,texture2D(Texture,vTexCoord0))*z,vec3(1.2)));
}
void main()
{
    float fA;
    vec3 fCol;
     vec4 Tex = texture2D(Texture,vTexCoord0);
    // Disk
    fCol = drawButtonDisk(fA,fCol).rgb;
    fA = drawButtonDisk(fA,fCol).a;
    
    
    // 实时片元光照 or 静态贴图光照
    fCol =isRipUnderCarLightToggle?isRipUnderCarLight ? realLight(fCol):fackLight(fCol):fCol;
    
    // 正片叠底
    fCol = fCol * fA ;
    
    // 3D阴影 or 静态贴图阴影 todo
    fCol = fackShadow(fCol,fA).rgb;
    fA = Saturate(drawCircle(9.0,0.5,Tex).r) ;
    float mask = Saturate(drawCircle(9.0,0.5,Tex).r);
    //深度渐变
    fCol += deepEffect(fCol)*0.2*BaseColorCarDisk.rgb;
    
    //fCol = vec3(1.0);
    gl_FragColor.rgb = fCol ;
    
    gl_FragColor.a = fA * BaseColorCarDisk.a  ;

    
    // Disk
    // vec4 ffCol =
    //  drawButtonDisk(fA,fCol).r 
    //  * min(0.3,realLight(fCol).r)
    //  * vec4(1.0)
    //  ;
  
    //fA = drawButtonDisk(fA,fCol).a * min(0.2,realLight(fCol).r) * vec3(1.0);
   // gl_FragColor = ffCol;
}