
#if ADAPTIVE_ROUGHNESS
#extension GL_OES_standard_derivatives : enable
#endif
precision highp float;

varying vec3 vNormal;
varying vec2 texCoord;
uniform highp mat4 kzProjectionCameraWorldMatrix;
uniform highp mat4 kzCameraNormalMatrix;
uniform highp mat4 kzNormalMatrix;
varying vec3 ViewDirection;
varying vec3 ViewDirectionSkyBox;
varying vec3 ViewDirectionSkyBox2;
varying vec3 ViewDirectionSkyBox3;


#if TEXTURE_COLOR
uniform sampler2D Texture;
#endif




#if TEXTURE_NORMAL
varying vec3 vTangent;
varying vec3 vBinormal;
varying mat3 vTBN;
uniform sampler2D NormalMap;
#endif

//uniform sampler2D NormalMap2;

varying vec2 vTexCoord;
uniform vec3 kzCameraPosition;

uniform samplerCube CubeMap;
uniform samplerCube CubeMap2;
uniform samplerCube CubeMapTop;

uniform vec4 BaseColor;
uniform vec4 ColorChange;
uniform float RoughnessFloat;
uniform float Density;
uniform float Metalness;
uniform float BlendIntensity;
uniform float DarkIntensity;
uniform vec4 Emissive;
uniform vec4 FresnelColor;
uniform vec4 FresnelColor2;
uniform vec4 Sky1Color;
uniform vec4 Sky2Color;

varying vec4 vRCol;
varying vec4 vDCol;
varying vec4 vRCol2;
varying vec4 vDCol2;
varying float vF;
varying float rHP;
varying vec3 vPos;
varying vec3 worldPos;
varying vec3 objectPos;


uniform bool flipMesh;
uniform vec2 heightFade;
//m_plastic
uniform bool Plastic;
//carPaintChange
uniform bool changeCarPaint;
//m_ear
uniform bool CarEar;
//d_tire
uniform bool Tire;
uniform bool Windows;
uniform bool Back;
uniform float DesaturationPower;
uniform float TestValue;
//m_Metal
uniform bool Metal;
//m_Light
#if SJX_LIGHT_SPOTS

    uniform vec4                SpotLightColor[SJX_LIGHT_SPOTS];
    uniform highp vec3     SpotLightDirection[SJX_LIGHT_SPOTS];
    uniform highp vec3     SpotLightConeParameters[SJX_LIGHT_SPOTS];
    uniform highp vec3     SpotLightAttenuation[SJX_LIGHT_SPOTS];
    varying highp vec3             vSpotLightDirection[SJX_LIGHT_SPOTS];

#endif

//math
    #define PI 3.1415926538

    float Saturate(float target)
    {
        return clamp(target,0.0,1.0);
    }
    vec3 SaturateV3(vec3 target)
    {
        return clamp(target,0.0,1.0);
    }
    vec4 SaturateV4(vec4 target)
    {
        return clamp(target,0.0,1.0);
    }
    vec3 Desaturation( vec3 color ,float power)
    {
        vec3 gray = vec3( dot( color , vec3( 0.2126 , 0.7152 , 0.0722 ) ) );

        return mix( color , gray , power );
    }
    float GetAngleBetweenPoints(vec3 a, vec3 b)
    {
       return acos((a.x*b.x + a.y*b.y + a.z*b.z)/( sqrt(pow(a.x,2.0)+pow(a.y,2.0)+pow(a.z,2.0)) * sqrt(pow(b.x,2.0)+pow(b.y,2.0)+pow(b.z,2.0))));
    }
    float MakeCos(float High,float cycle, float moveH , float moveV,float Target)
    {
        return High*cos(cycle*Target + moveV) + moveH;
    }
    float easeOutBlend(float Num)
    {
        return Num*Num *(3.0 - 2.0 * Num);

    }
//pragma
    vec3 m_normal( )
    {
        vec3 N;
        N = vNormal;
        N.y = flipMesh ? -N.y : N.y;
        N = gl_FrontFacing ? normalize(N):normalize(-N);
        return N;
    }

    float m_skyboxMask(float front)
    {
        float a2 = rHP;
        #if ADAPTIVE_ROUGHNESS
            float arf = length(fwidth(m_normal()));
            a2 += (arf*(RoughnessFloat));
        #endif
        a2 = min(1.0,(a2 + front*(a2))*a2); 
        return a2; 
    }

    float m_frontMask(float size, float bright)
    {

        return pow(min(Saturate(bright),dot(m_normal(),-normalize(vec3(ViewDirection.x,ViewDirection.y-16.0,ViewDirection.z-10.0)))),size);
    }

    vec4 m_texColor()
    {
        
        vec4 Tex = vec4(1.0);

        #if TEXTURE_COLOR
            Tex = texture2D(Texture,vTexCoord);
        #endif

        return Tex;
    }

    vec4 m_skyboxColor(vec3 viewDir ,samplerCube cubeMap)
    {
        float bias = 1.0;

        vec3 cubeCoord = reflect(viewDir,m_normal());

        //cubeCoord.z = smoothstep (-19.9,19.2,cubeCoord.z);
        //cubeCoord.x = smoothstep (0.0,0.0,cubeCoord.x);

        vec4 cubeBoxRGBA = SaturateV4(textureCube(cubeMap,cubeCoord,bias));

        return cubeBoxRGBA;

    }

    vec4 m_Add2Light(vec4 lighting ,vec4 Spec,vec4 SpecAdd,vec4 matCol,float Power)
    {
        float matDens = mix(Density,1.0,Metalness);
        Spec += SpecAdd;
        Spec *= mix(vec4(1.0),matCol,Metalness);

        lighting += mix((vDCol+vDCol2)*matCol,Spec*max(matCol.r+Power,max(matCol.g+Power,matCol.b+Power)),matDens);
        return lighting;
    }

    float m_linearizeDepth(float near,float far) 
    {
        float z =gl_FragCoord.z * 2.0 - 1.0; // back to NDC 
        return pow((2.0 * near * far) / (far + near - z * (far - near)),0.2);    
    }
    highp float m_distribution(highp float NdotH, float roughness)
    {
        highp float a = roughness * roughness;
        highp float a2 = a * a;
        highp float NdotH2 = NdotH * NdotH;
        highp float f = (NdotH2 * (a2 - 1.0) + 1.0);
        return a2 / (PI * f * f);
    }

    mediump float m_geometry(highp float NdotV, highp float NdotL, float roughness)
    {
        float a = roughness + 1.0;
        float k = (a * a) / 8.0;
        highp float GGXV = NdotV / (NdotV * (1.0 - k) + k);
        highp float GGXL = NdotL / (NdotL * (1.0 - k) + k);
        return GGXV * GGXL;
    }
    mediump vec3 m_fresnel(mediump float cosT, vec3 F0)
    {
    return F0 + (1.0-F0) * pow( 1.0 - cosT, 5.0);
    }
//pack
    vec3 d_tire( vec3 lightcolor )
    {
        
        float scanMask =smoothstep(ViewDirectionSkyBox2.z-53.0,ViewDirectionSkyBox2.z-12.0,worldPos.z) - smoothstep(ViewDirectionSkyBox2.z+172.0,ViewDirectionSkyBox2.z+190.0,worldPos.z);
        float maskY = smoothstep(-2.0,3.5,objectPos.y);
        // 左 右 轮
        float maskX = 1.0-(smoothstep(-12.5,-10.0,objectPos.x)-smoothstep(10.0,12.5,objectPos.x));
        float maskXoneSide = step(0.0,-objectPos.x);
        vec3 color0 = mix(FresnelColor.rgb*0.5,FresnelColor2.rgb*2.5,maskY);
        vec3 color1 = mix(FresnelColor2.rgb,FresnelColor.rgb,maskY);
        vec3 color = mix(color1,color0,maskXoneSide);
        vec3 fCol = mix(lightcolor,color,maskX) ;
        vec3 Cout = mix(lightcolor,fCol,lightcolor.r);

    // lightcolor *= fCol;
    fCol = mix(vec3(0.0),m_texColor().rgb*(color*2.5),maskX)*max(scanMask,0.72);
        //fCol = vec3(scanMask);
        return fCol;
        
    }

    vec3 d_metal()
    {
        return vec3(1.0);
    }
    //light Struct + function
    struct makeLightStruct 
    {
        //x1,x2,偏移,强度
        vec4 Xsmooth;
        vec4 Ysmooth;
        vec4 Zsmooth;
        bool Filp;

    } makeLight;

    vec3 d_makeLightFunction(makeLightStruct LS, vec3 Color)
    {
        //车尾到车头为法线坐标下的z轴
        //车顶到车底为法线坐标下的y轴
        //两侧车门为x轴
        float brightF = 1.0;
        float sizeF = 1.13;
        float F = LS.Filp ? 1.0 - Saturate(m_frontMask(sizeF,brightF)) : Saturate(m_frontMask(sizeF,brightF));
        float outMask = 
        pow(
            smoothstep(LS.Ysmooth[0],LS.Ysmooth[1],m_normal().y + LS.Ysmooth[2])
            ,LS.Ysmooth[3])
        *
        smoothstep(
                    Saturate(pow(
                        smoothstep(LS.Xsmooth[0],LS.Xsmooth[1],m_normal().x + LS.Xsmooth[2])
                        ,LS.Xsmooth[3]
                    ))
                    ,1.0
                    ,F
                )
        *
            (
            LS.Zsmooth[0]==0.0?
            1.0
            :
            smoothstep(
                Saturate(pow(
                    smoothstep(LS.Zsmooth[0],LS.Zsmooth[1],m_normal().z + LS.Zsmooth[2])
                    ,LS.Zsmooth[3]
                ))
                ,1.0
                ,F
                ))
        ;
        Color *= vec3(outMask);
        return Color;
    }
void main()
{
    
    //-----
        //vec3 N = vNormal;
    //基础变量
        //法线
        vec3 N = m_normal();
        highp vec3 TBN =  vec3(0.0,0.0,0.0);
        //视线
        vec3 V = normalize(ViewDirection);
        //前向遮罩
        float brightF = 1.0;
        float sizeF = 1.13;//Saturate(-TestValue+1.13);
        float F =  m_frontMask(sizeF,brightF);
        //遮罩
        float skyBoxMask = m_skyboxMask(F);
        //基础颜色
        vec4 matCol = m_texColor()*BaseColor;
        //物体坐标色：
        vec3 m_objectPos = vec3(objectPos.x,objectPos.y,objectPos.z+18.0);
        //最终输出
        vec3  fCol = vec3(0.0,0.0,0.0);
        float fA = 1.0;
        //天空盒子
        vec4 Spec = vec4(0.0);  
        vec4 lighting = vec4(0.0);
        
        
#if TEXTURE_NORMAL
        highp vec3 textureNormal = texture2D(NormalMap, vTexCoord).xyz;
        highp vec3 NormalFactor = mix(vec3(0.0, 0.0, 1.0), textureNormal * 2.0 - 1.0, TestValue);
        TBN = normalize(vec3(vTangent * NormalFactor.x + 
                    vBinormal * NormalFactor.y +
                    vNormal * NormalFactor.z)); 
#endif



#if SJX_LIGHT_SPOTS
    for (int i = 0; i < SJX_LIGHT_SPOTS; ++i)
    {
        highp vec3 L = normalize(-vSpotLightDirection[i]);
        highp float LdotN = dot(L, TBN);
        if (LdotN > 0.0)
        {
            highp float cosDirection = dot(L, -SpotLightDirection[i]);
            highp float cosOuter = SpotLightConeParameters[i].x;
            highp float t = cosDirection - cosOuter;
            if (t > 0.0)
            {
                highp vec3 H = normalize(-V + L);
                highp float NdotH = max(0.0, dot(TBN, H));
                highp float NdotV = max(0.0, dot(TBN, -V));
                highp float NdotL = max(0.0, dot(TBN, L));
                mediump float HdotV = max(0.0, dot(H, -V));
                


                highp vec3  c = SpotLightAttenuation[i];
                mediump float d = length(vSpotLightDirection[i]);
                highp float denom = (0.01 + c.x + c.y * d + c.z * d * d) * SpotLightConeParameters[i].z;
                highp float attenuation = min(t / denom, 1.0);
                
                vec3 F0 = mix(vec3(0.04), matCol.rgb, Metalness);
                highp float dist = m_distribution(NdotH, RoughnessFloat);
                highp float geo = m_geometry(NdotV, NdotL, RoughnessFloat);
                mediump vec3 fresnel = m_fresnel(HdotV, F0);
                
                highp vec3 specularNum = (dist * geo) * fresnel;
                highp float specularDenom = max(0.0001, 4.0 * NdotV * NdotL);
                highp vec3 specular = specularNum / specularDenom;
                mediump vec3 diffuse = (vec3(1.0) - fresnel) * matCol.rgb / PI * (1.0 - Metalness);
                mediump vec3 lightColor = (diffuse + specular) * SpotLightColor[i].rgb * attenuation * NdotL;
                fCol += lightColor ;
            }
        }
    }
#endif  

    
               
     
    
#if SJX_MASK_LIGHTING
    //遮罩们
        // 扫光 = 前 - 后
        //  float scanMask =smoothstep(ViewDirectionSkyBox2.z-53.0,ViewDirectionSkyBox2.z-12.0,objectPos.z) - smoothstep(ViewDirectionSkyBox2.z+42.0,ViewDirectionSkyBox2.z+70.0,objectPos.z);
        
        vec3 vvv = vec3(1.0,0.0,0.0);
        float Angle =GetAngleBetweenPoints(vvv,kzCameraPosition);
        //Angle = 0.0;
        float scanMask = smoothstep(60.3-Angle*9.0,65.3-Angle*9.0,ViewDirection.z)-smoothstep(70.3+Angle*3.0,76.3+Angle*3.0,ViewDirection.z);
        float scanMask2 = smoothstep(60.3-Angle*9.0,65.3-Angle*9.0,ViewDirection.z)-smoothstep(70.3+Angle*3.0,76.3+Angle*3.0,ViewDirection.z);
       
        
       
        
        float sacnMaskNx = smoothstep(0.9,1.32,-N.x+0.75);
        float frontMask = smoothstep(0.29,0.32,F);
        float frontMask2= smoothstep(0.10,0.12,F);
        float frontMask3 =  smoothstep(0.5,0.8,F);
        float frontMask4 =  smoothstep(0.6,0.72,F+Angle*0.21);
        float frontMask5 = Windows?  smoothstep(0.5,0.82,F+Angle*0.45): smoothstep(0.37,0.72,F+Angle*0.45);
        float frontMask6 = Windows?  smoothstep(0.25,0.33,F): smoothstep(0.25,0.33,F);
        float frontMask7 = Windows?  smoothstep(0.40,0.50,F): smoothstep(0.40,0.50,F);
        float frontMask8 = Windows?  smoothstep(0.40,0.50,F): smoothstep(0.40,0.50,F);
        //float topMask = smoothstep(1.0,0.0,abs(N.x));
        float NxMask = smoothstep(-0.9,1.62,N.x+0.75);
        float NxMask0 = smoothstep(-0.9,1.6,N.x+0.75);
        float NxMask1 = smoothstep(-0.9,4.2,N.x+0.75);
        float NzMask = smoothstep(0.04,0.06,N.z);
        
        float NyMask = Windows ?  smoothstep(0.5,0.5,N.y+0.35) - smoothstep(0.72,0.88,N.y+0.45) :  smoothstep(0.54,0.63,N.y+0.42) - smoothstep(0.81,0.99,N.y+0.42) ;
        float NyMask1 = smoothstep(0.0,0.25,N.y);

        float scanMaskWithNx = scanMask * sacnMaskNx ;
        float scanMaskWithNx2 = scanMask2 * sacnMaskNx;

        float objmaskY = smoothstep(-3.3,0.9,1.0-m_objectPos.y);
        float objmaskY2 = smoothstep(6.1,12.6,m_objectPos.y);
        float objmaskY3 = smoothstep(3.1,6.0,m_objectPos.y);
        float objmaskY4 = smoothstep(0.1,6.6,m_objectPos.y);
        float objmaskY4_1 = smoothstep(0.1,26.6,m_objectPos.y);
        float objmaskY5 = smoothstep(12.3,12.6,m_objectPos.y);
        float objmaskZ = smoothstep(-28.1,-18.1,1.0-m_objectPos.z) -  smoothstep(19.0,26.0,1.0-m_objectPos.z);
        float objmaskZ2 = smoothstep(-25.0,-23.0,1.0-m_objectPos.z) -  smoothstep(23.5,24.9,1.0-m_objectPos.z);
        float objmaskZ3 = smoothstep(-25.0,-20.0,1.0-m_objectPos.z) -  smoothstep(15.5,22.9,1.0-m_objectPos.z);
        float objmaskZ3_1 = smoothstep(-25.0,-20.0,1.0-m_objectPos.z) -  smoothstep(25.5,30.9,1.0-m_objectPos.z);
        float objmaskX = smoothstep(-11.0,-1.0,1.0-m_objectPos.x);
        float carTopMask = 1.0-smoothstep(1.2,0.6,abs(N.x));

        float objmaskX2 = smoothstep(0.0,15.0,m_objectPos.x);
        float objmaskX3 = smoothstep(-10.0,-5.0,m_objectPos.x);

        float SlashL =Saturate( Saturate( step(0.0,-objectPos.y+3.0 + (objectPos.z/2.3+9.6)  )) - Saturate(step(0.0,objectPos.y-1.5+ (objectPos.z/29.3+0.6) )) )*2.0* step(0.0,-objectPos.y+2.2 - (objectPos.z/3.0+3.6)   ) ;
        

    //空间反射1 (蓝色)
        vec4 skyboxColor = m_skyboxColor(ViewDirectionSkyBox,CubeMap);
        vec4 cubox1 =Windows?mix(vec4(0.0),Sky1Color,skyboxColor) : mix(vec4(0.0),Sky1Color,skyboxColor*2.0);
        //cubox1 =vec4( pow(max(cubox1.r,0.4),4.0));
       
        cubox1 
        = frontMask 
        * (1.0-NxMask1)
        * Saturate(smoothstep(-39.5,-30.5,objectPos.z)) 
        * cubox1;
        //显示
        //Spec += cubox1;
        //lighting.rgb  = vec3(step(-39.5,objectPos.z));
        lighting = Windows?
        lighting + cubox1
        :
         m_Add2Light (lighting,Spec,cubox1,matCol,0.5);
       
    //空间反射2 (橙色)
        vec4 skyboxColor2 = m_skyboxColor(ViewDirectionSkyBox2,CubeMap2);
        skyboxColor2 = vec4( pow(max(skyboxColor2.r,0.1),1.4));
        skyboxColor2 = SaturateV4(skyboxColor2);
        vec4 cubox2 =  mix(vec4(-0.1),Sky2Color,skyboxColor2*2.0);
        vec4 cubox3 = mix(vec4(-0.1),Sky1Color*0.3+Sky2Color*0.7,skyboxColor2*2.0) ;
        
        //合
            cubox2  
            = frontMask
            * objmaskZ3
            * (1.0-NxMask)
            * (scanMaskWithNx + NxMask)
            * cubox2;

            cubox3  
            = frontMask
            * objmaskZ2
            * (NxMask0+SlashL)
            * (scanMaskWithNx2 + NxMask)
            * smoothstep(0.7,0.8,Saturate(F) )
            * cubox3;

        //显示
            //Spec += cubox2;
            
            lighting = m_Add2Light (lighting,Spec,cubox2,matCol,0.1);
            lighting = m_Add2Light (lighting,Spec,cubox3/1.9,matCol,0.15);

       
      
        
        
        
        
        
    
        
    //空间反射3 (白色)    
        vec4 skyboxColor3 = m_skyboxColor(ViewDirectionSkyBox3,CubeMapTop);
        vec4 cubox4 = mix(vec4(0.0),Sky1Color,skyboxColor3*2.0);

        cubox4
        = (1.0-frontMask)
        * NzMask
        * cubox4;
        
        lighting = m_Add2Light (lighting,Spec,cubox4,matCol,0.7);
       // lighting.rgb = vec3(objmaskZ3);

    
   
    // Add Light----------------------------------------------------------------
        vec3 fresCol = FresnelColor.rgb* FresnelColor.a * 10.0;
        vec3 fresCol2 = FresnelColor2.rgb* FresnelColor2.a * 10.0;

        //橙色 左侧光
            makeLightStruct LS = makeLightStruct(vec4(1.0,-1.0,-0.9,1.0),vec4(0.45,0.1,0.0,1.0),vec4(0.0),false);
            vec3 oLW = d_makeLightFunction(LS,fresCol*0.5);
            makeLightStruct LS2 = makeLightStruct(vec4(1.7,-0.7,-0.9,1.0),vec4(0.0,1.0,0.0,1.0),vec4(-1.0,1.0,-3.0,1.0),false);
            vec3 oL  = d_makeLightFunction(LS2,fresCol*0.42);
            float cosTopMaskOL = Saturate(smoothstep(13.8,14.0,objectPos.y -MakeCos(0.33,12.0,-0.2,-1.0,objectPos.z*0.02) ) );
           // lighting.rgb += Windows ? objmaskY2  * oLW : objmaskY3 * objmaskZ2 * (1.0-cosTopMaskOL) * oL;
           //窗户
           lighting.rgb += Windows ? (step(8.2,objectPos.y))  * oLW : vec3(0.0);
           //lighting.rgb = vec3(step(8.2,objectPos.y));
           // lighting.rgb = vec3(); 
             //橙色轮廓光
                vec3 OL4 = vec3(0.0, 0.0, 0.0);
                vec3 OL5 = vec3(0.0, 0.0, 0.0);
                float maskAngle = mod(Angle,1.0);
                if(Angle>2.0)
                {
                    float contourMask =0.42* Saturate(smoothstep(F*70.0-31.0,F*70.0-20.0,(objectPos.x-9.0)+(objectPos.z*1.2+18.0)) );
                    OL4 =(((1.0-frontMask6)*0.8+contourMask)*Saturate( step(-6.5,objectPos.x)) * pow(maskAngle,1.5) * (1.0- objmaskY) * fresCol  ) *3.0;
                    OL5 =((1.0-frontMask7) * pow(maskAngle,1.5) * (1.0- objmaskY) * fresCol2  ) ;
                   lighting.rgb += Windows?   OL4 : Back?  (objmaskY5+frontMask6)* OL4  :  objmaskY4_1* OL4 ;
                }
                else 
                {
                    OL4 = vec3(0.0, 0.0, 0.0) ;
                    OL5 = vec3(0.0, 0.0, 0.0) ;
                }
                vec3 OL6 = Windows?
                    vec3(0.0, 0.0, 0.0)
                    : 
                pow(skyboxColor.r,2.0) *(1.0-NyMask1)* frontMask6 * objmaskX3 * (1.0-objmaskZ3_1)* Saturate(objectPos.z)  *  fresCol   ;
                
                //lighting.rgb = vec3(objectPos.z);
               lighting.rgb += OL5/6.0;
                lighting.rgb += OL6;
           
        //Fill_Light_01
            //MASK
            //float MakeCos(float High,float cycle, float moveH , float moveV,float Target)
            //float mama = step(5.5,objectPos.x - 5.17*cos(0.2*objectPos.z-15.1) ) ;
        
            float newAngle= 1.0-Saturate(pow((Angle-1.9),0.6))*20.0;
            float mamaFrontL = Saturate (smoothstep(0.4,0.75,N.z) - (1.0- step(0.17,N.y)))  *  smoothstep(-9.1,-1.0,objectPos.x);
            float stayLight = 0.0;
        
            
            stayLight= Angle > 1.99 ? 12.8 - (Angle+1.5) :    8.8 ;

            float mama = smoothstep(5.0,20.5-objectPos.z*1.5,objectPos.x - MakeCos(10.17-newAngle*1.0,0.1,stayLight,2.6,objectPos.z) ) * step(0.0,N.y-0.5) * step(-7.0,objectPos.x) * step(-3.5,objectPos.z) ;
            float mama2 = smoothstep(5.0,22.5-objectPos.z*1.0,objectPos.x-1.9 - MakeCos(10.17-newAngle*1.0,0.1,11.8+newAngle,2.6,objectPos.z) ) * step(0.0,N.y-0.7) * step(-7.0,objectPos.x) * step(-3.5,objectPos.z) ;
           float mamaHard = step(1.01,N.x+N.y)*smoothstep(0.9,1.1,1.0-N.x)+ N.z*N.x;
            // if(Angle< 1.2)
            // {
            //     mama = 0.0;
            // }
            
             if(Angle< 1.8)
            {
                mama = mama  * Saturate((Angle-0.9)*1.0) ;
                mamaFrontL = mamaFrontL* Saturate((Angle-0.9)*2.0);
                
            }
            vec3 OL2 = objmaskX2 * NyMask * NxMask0 * frontMask3 * objmaskZ2 * objmaskY3 * fresCol;
           
           //lighting.rgb = vec3(step(5.5,objectPos.y));
            lighting = Windows?
            lighting
            :
            CarEar?
            vec4(0.0,0.0,0.0,1.0)
            : 
            m_Add2Light (lighting,Spec,vec4(OL2,1.0),matCol,0.21);

            vec4 FrontUpLighting =vec4( (mama2*0.7 + mamaHard*2.0  ) *( mama ) *0.55  * fresCol ,1.0);
            vec4 FrontLowerLighting = vec4( Saturate(mamaFrontL-step(5.5,objectPos.y))*0.15  * fresCol  ,1.0);
           lighting = m_Add2Light (lighting,Spec,FrontUpLighting,matCol,0.9);
            lighting = m_Add2Light (lighting,Spec,FrontLowerLighting,matCol,0.6);
           // lighting = vec4(vec3(objmaskY3),1.0);
            
        //橙色 底光2
            
            //右侧
                // vec3 OL3 = objmaskY * objmaskZ * objmaskX * carTopMask * frontMask2 *fresCol ;
                // lighting.rgb += SaturateV3(OL3/6.0);
                float zMaskSideLight =Saturate( smoothstep(5.0,12.21,objectPos.z) );

                float CosMask = 
                1.0 - 
                Saturate(
                smoothstep(-1.2,4.9,
                

                   // objectPos.x +8.0  + 5.0*0.01*cos((objectPos.z-29.0)/15.0) + (objectPos.y+1.0)
                    objectPos.x +8.0  + MakeCos(0.05 , 1.0/15.0 , (objectPos.y+1.0), 0.0,(objectPos.z-29.0) )
                ) 
                ) ;
                float yMask2 = (1.0-
                Saturate((objectPos.y-1.6)*3.0)-objectPos.x/85.0);

                float yMask = (1.0-
                Saturate((objectPos.y-1.6)*3.0)) ;

                float CosMask2 = 
                1.0 - 
                Saturate(
                smoothstep(-6.1,2.1,
                

                    objectPos.x +9.0  + 5.0*1.91*cos((objectPos.z-15.0)/10.2) + (objectPos.y+8.7)

                ) 
                )
                -( yMask * pow(objmaskZ3,2.2)) + zMaskSideLight
                
                ;
                
               
          
           
                vec4 OL3 =vec4(vec3(( Saturate( CosMask-CosMask2*2.0 + smoothstep(0.0,1.0,(yMask*(pow(objmaskZ3,3.0)))*1.6))/  pow((Angle),1.9)*2.0* objmaskX*0.3  )  * objmaskX*5.5  * frontMask2 *Saturate( 1.2- SlashL)  * fresCol),1.0); 

               
                 lighting = m_Add2Light (lighting,Spec,OL3/6.0,matCol,0.72);
                //lighting.rgb = vec3(smoothstep(7.0,12.21,objectPos.z));
                
            //左侧
               
               float CosMaskL = 
                1.0 - 
                Saturate(
                smoothstep(-3.0,1.0,
                
                    //MakeCos(float High,float cycle, float moveH , float moveV,float Target)
                  //- objectPos.x +8.0  + 5.0*0.01*cos((objectPos.z-29.0)/15.0) + (objectPos.y+1.0)
                 MakeCos(-2.0 , 0.088 , (objectPos.y-3.3), 10.3,(objectPos.z-29.0) )

                ) 
                ) ;

                float CosMaskL2 = 
                1.0 - 
                Saturate(
                smoothstep(-2.0,1.0,
                
                    //MakeCos(float High,float cycle, float moveH , float moveV,float Target)
                  //- objectPos.x +8.0  + 5.0*0.01*cos((objectPos.z-29.0)/15.0) + (objectPos.y+1.0)
                 MakeCos(-3.0 , 0.10 , (objectPos.y-0.3), 10.3,(objectPos.z-22.0) )

                ) 
                )+ zMaskSideLight ;
                float yMaskL = 
                1.0-Saturate((objectPos.y-1.6)*3.0 ) *0.5 ;

                float fMask = (CosMaskL - CosMaskL2 +( yMaskL/2.0 * objmaskZ3  ) ) * Saturate( smoothstep(3.0,9.1,objectPos.x)  );

                vec4 OL3L = vec4(fMask * frontMask2 * fresCol  ,1.0) ; 

                 if(Angle< 1.8)
                {
                    OL3L.rgb = OL3L.rgb  * Saturate((Angle-1.2)*2.0) ;
                   
                }
                  
                OL3L.rgb = (1.0 - SlashL) * OL3L.rgb;

                OL3L.rgb = CarEar ? 

                (smoothstep(0.3,0.9,N.y)) * OL3L.rgb *3.0 

                :
                OL3L.rgb
                 ;
                
                lighting = m_Add2Light (lighting,Spec,OL3L/4.2,matCol,0.91);

                float SlashFL = Saturate( smoothstep(-3.0,3.0,-objectPos.y) - SlashL);
                vec4 OL4L = vec4( vec3(Saturate(SlashFL * pow(objmaskZ,4.0) * frontMask2 ) * (1.0-objmaskX) * fresCol)  ,1.0) ; 

                lighting  = m_Add2Light (lighting,Spec,OL4L/3.2,matCol,0.72);


                
             //lighting.rgb = vec3(smoothstep(0.3,0.9,N.y));   

       


        //蓝色轮廓光
            vec3 BL4 = objmaskX3 * NyMask1 * (1.0-frontMask4) * objmaskY2 * ( 1.0-step(-4.3,objectPos.z)) * fresCol2 ;
            lighting.rgb += Windows? vec3(BL4/1.5) : vec3(BL4/2.2);
            vec3 BL4h = Windows? 
               
                    objmaskX3 * NyMask1 * (1.0-frontMask5) * ((1.0-objmaskY2)*2.0) * fresCol2
                   
            : 
            objmaskX3 *objmaskY4* NyMask1 * (1.0-frontMask5)  *fresCol2 ;
            
            lighting.rgb += Windows? vec3(BL4h/0.5): vec3(BL4h/1.5);

           // lighting.rgb = vec3( log(Angle+0.5));
            
        //蓝色 右侧光
            

            float BackFloat = Back? 0.5:0.0;
            
            vec3 colorBB = SaturateV3(
            smoothstep(0.06,-0.26,-N.y+0.13)
            // *smoothstep(
            //     Saturate(
            //         smoothstep(3.0,-0.4,(-N.x)-0.29+BackFloat)
            //         ),1.0,(F))
            *smoothstep(
                smoothstep(1.21,0.33,(-N.x)+BackFloat),1.0,(F)
                )
            
                    *fresCol2*0.2
            );

            lighting.rgb += Windows? 
            //     SaturateV3(
            //    ( smoothstep(-0.0,-0.2,-N.y))
            //     *smoothstep(
            //         smoothstep(1.0,-0.2,(-N.x)-0.9),1.0,(F)
            //         )*fresCol2 
            //     )
                vec3(0.0)
            :
            colorBB;


            float mamaFrontR = Saturate (smoothstep(F-0.2,1.15,N.z) - (1.0- step(0.17,N.y)))  *  smoothstep(-9.1,-1.0,-objectPos.x) -step(5.5,objectPos.y);
            float m_F = F + (smoothstep(0.5,0.9,log(Angle)));
            float mamaHardR = - smoothstep(m_F-0.17,m_F-0.15,N.z)*N.x - step(-5.5,-objectPos.y);
            vec4 FMRB = vec4(pow(Saturate(Saturate(mamaHardR)  + Saturate(mamaFrontR))*step(0.25,F),0.7) * vec3(fresCol2.r,fresCol2.g,fresCol2.b+0.1) *1.0,1.0);
            FMRB.rgb = CarEar?
            (smoothstep(0.3,0.9,N.y))* FMRB.rgb*1.2 
            :
            FMRB.rgb
            ;
            lighting =    m_Add2Light (lighting,Spec,FMRB,matCol,0.7);
          // lighting.rgb = vec3(FMRB);
          
        //后备箱光照
            vec3 backVD = vec3(ViewDirectionSkyBox2.x+log(Angle)*200.0,ViewDirectionSkyBox2.y-70.0,ViewDirectionSkyBox2.z-20.0);
            vec4 skyboxColorBack = m_skyboxColor(backVD,CubeMap);
            vec3 backVD2 = vec3(ViewDirectionSkyBox2.x+25.0,ViewDirectionSkyBox2.y-150.0,ViewDirectionSkyBox2.z-20.0);
            vec4 skyboxColorBack2 = m_skyboxColor(backVD2,CubeMap);
            vec3 backVD3 = vec3(ViewDirectionSkyBox.x,ViewDirectionSkyBox.y+15.0,ViewDirectionSkyBox.z);
            vec4 skyboxColorBack3 = m_skyboxColor(backVD3,CubeMap);

            float maskBack1 = smoothstep(0.0,0.5,skyboxColorBack.r) * step(-5.4,-objectPos.y)*step(39.4,-objectPos.z)* Saturate( smoothstep(-10.0,-5.0,objectPos.x))*(1.0-smoothstep(38.4,50.4,-objectPos.z));
            float maskBack2 = smoothstep( 0.0,0.3,skyboxColorBack2.r - min(0.05,skyboxColorBack2.r))*smoothstep(2.5,6.6,objectPos.y)*smoothstep(-10.4,-7.4,-objectPos.y)*(1.0-smoothstep(42.4,43.4,-objectPos.z)) ;
            float maskBack3 = (skyboxColorBack3.r - min(0.1,skyboxColorBack3.r)) * frontMask6;
            
            float finemaskBacks = maskBack3*0.3 + maskBack1 + maskBack2;
            float zMaskBack = smoothstep(39.3,42.3,-objectPos.z);
            
            vec3 mixColor = mix(fresCol2*2.1,fresCol/3.0,Saturate( smoothstep(-3.0,7.0,objectPos.x) ));

            vec3 fBackCol = finemaskBacks * zMaskBack * mixColor ;
            vec3 fBackColBack = finemaskBacks * zMaskBack * mixColor ;
            lighting = Windows ? 
            
             lighting
             : 
             Back? 
             //lighting.rgb + fBackCol 
                 m_Add2Light (lighting,Spec,vec4(fBackCol,1.0),matCol,1.2)
             : 
                m_Add2Light (lighting,Spec,vec4(fBackCol,1.0),matCol,0.5+log(Angle)*0.5);
            
           // lighting.rgb = vec3( smoothstep(-10.4,-7.4,-objectPos.y) );

    
    
    
    
    //----------------------------------------------------------------
    vec3 particleColor = lighting.rgb;
   
    lighting.rgb += matCol.rgb * Emissive.rgb * Emissive.a ;
    fCol = 
    Tire? 
    d_tire(lighting.rgb).rgb:
        Plastic?
         (Saturate (N.x*1.6) + Saturate(-N.x*1.6) - Saturate(smoothstep(6.1,7.0,objectPos.z))*0.4 ) *  SaturateV3( pow(lighting.rgb,vec3(0.71)) ):
            lighting.rgb;
    //fCol =  vec3( );
    fA = BaseColor.a * BlendIntensity; 
    fCol -= vec3(DarkIntensity);
    if(changeCarPaint)
    {
       // vec3 m_carPaint = vec3(log(ColorChange.r),log(ColorChange.g),log(ColorChange.b));
       
         fCol = fCol + ColorChange.rgb*0.3 ;
        
    }
   
#else
#endif
    gl_FragColor.rgb = Desaturation(fCol,DesaturationPower) * fA *1.2;
    gl_FragColor.a = fA; 
}


//sjx 25.8.2021