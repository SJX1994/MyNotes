precision mediump float;


uniform mediump float aniTime;
uniform mediump float kzTime;

uniform mediump float aniSpeed;
uniform mediump float aniFrequency;
uniform mediump float aniAmplitude;

//贴图
uniform sampler2D Texture; 
uniform mediump vec2 blurDir;
uniform mediump vec2 kzTextureSize0;
uniform mediump vec2 TextureOffset;
uniform mediump vec2 TextureTiling;
attribute vec4 kzColor0;
attribute vec2 kzTextureCoordinate0;

#if KANZI_SHADER_USE_NORMALMAP_TEXTURE
attribute vec3 kzTangent; 
varying mediump vec3 vTangent;
varying mediump vec3 vBinormal;
#endif

varying vec2 texCoord;
varying vec3 objectPos;
varying vec3 worldPos;
varying vec4 vertexColor;

 #define PI 3.1415926538

uniform float _Amplitude_A;
uniform float _Wavelength_A;
uniform float _Speed_A;
uniform vec2 _Direction_A;

uniform float _Amplitude_B;
uniform float _Wavelength_B;
uniform float _Speed_B;
uniform vec2 _Direction_B;

uniform float _lightAngle;
uniform bool _SubPlant;

//forLight
attribute vec3 kzPosition;
attribute vec3 kzNormal;

uniform highp mat4 kzProjectionCameraWorldMatrix;
uniform highp mat4 kzWorldMatrix;
uniform highp mat4 kzNormalMatrix;
uniform highp vec3 kzCameraPosition;

varying mediump vec3 vNormal;
varying mediump vec3 vViewDirection;

//light

#if KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS
uniform mediump vec3 DirectionalLightDirection[KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS];
varying mediump vec3 vDirectionalLightDirection[KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS];
#endif

#if KANZI_SHADER_NUM_POINT_LIGHTS
uniform mediump vec3 PointLightPosition[KANZI_SHADER_NUM_POINT_LIGHTS];
varying mediump vec3 vPointLightDirection[KANZI_SHADER_NUM_POINT_LIGHTS];
#endif

uniform highp vec3 TestValue;

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

float MakeCos(float High,float cycle, float moveH , float moveV,float Target)
{
    return High*cos(cycle*Target + moveV) + moveH;
}
mat4 objectSpaceMatrix()
{
    mat4 objSpaceMat  = mat4(
       vec4 ( 0.5,  0.5, 0.5 , 1.0),
        vec4  (-0.5,  0.5, 0.5, 1.0),
        vec4  ( 0.5, -0.5, 0.5, 1.0),
       vec4   (-0.5, -0.5, 0.5, 1.0)
    );
    return objSpaceMat;
}
vec4 matrixChange(vec3 Posit,vec3 Scale, vec3 Rotat,vec4 Target )
{
    //位移矩阵
    vec3 transValues = Posit;
    mat4  transfrom = mat4(
    vec4( 1.0,        0.0,         0.0,  transValues.x ),
    vec4( 0.0,        1.0,         0.0,  transValues.y ),
    vec4( 0.0,        0.0,         1.0,  transValues.z ),
    vec4( 0.0,        0.0,         0.0,  1.0 ) );
    //缩放矩阵
    vec3 scaleV = Scale;
    mat4  scale = mat4(
    vec4( scaleV.x, 0.0,          0.0,  0.0 ),
    vec4( 0.0,      scaleV.y,     0.0,  0.0 ),
    vec4( 0.0,      0.0,     scaleV.z,  0.0 ),
    vec4( 0.0,      0.0,          0.0,  1.0 ) );    
    //旋转矩阵z
    float angleZ=Rotat.z;
    mat4  rotationZ = mat4(
    vec4( cos(angleZ), -sin(angleZ),0.0,  0.0 ),
    vec4( sin(angleZ),  cos(angleZ),0.0,  0.0 ),
    vec4( 0.0,        0.0,          1.0,  0.0 ),
    vec4( 0.0,        0.0,          0.0,  1.0 ) );
    //旋转矩阵x
    float angleX=Rotat.x;
    mat4  rotationX = mat4(
    vec4( 1.0,        0.0,         0.0,  0.0 ),
    vec4( 0.0,cos(angleX),-sin(angleX),  0.0 ),
    vec4( 0.0,sin(angleX), cos(angleX),  0.0 ),
    vec4( 0.0,        0.0,         0.0,  1.0 ) );
    //旋转矩阵z
    float angleY=Rotat.y;
    mat4  rotationY = mat4(
    vec4(  cos(angleY), 0.0, sin(angleY),  0.0 ),
    vec4(  0.0        , 1.0,         0.0,  0.0 ),
    vec4( -sin(angleY), 0.0, cos(angleY),  0.0 ),
    vec4(          0.0, 0.0,         0.0,  1.0 ) );

    return Target*transfrom*scale*rotationX*rotationY*rotationZ;
}

vec4 vertexFlagAni(vec4 vertPos , vec2 texCoordAni , float objMaskZ) 
{
    vertPos.y =  vertPos.y  + (sin (
                (
                    texCoordAni.y - (kzTime*aniSpeed) * aniFrequency 
                )
                
            )


        
        )*(1.0-objMaskZ);
   
    return vertPos;
}
struct TBP
{
  vec3 tangent;
  vec3 binormal;
  vec3 position;
};

TBP CreatWave(
float _Amplitude,
float _Wavelength,
float _Speed,
vec2 _Direction,
vec3 posAdd,
vec3 tangent,
vec3 binormal
)
{
    TBP tbp;

    //vertex
        
        float k = 2.0 * PI / _Wavelength;
        //地球重力9.8
        float c = sqrt(9.8/k);
        vec2 d = normalize(_Direction);
        float f = k * (dot(d,posAdd.xz) - c * kzTime * _Speed);
        float a = _Amplitude/k;
        

        posAdd.x = d.x * (a * cos(f));
        posAdd.y = a * sin(f);
        posAdd.z = d.y * (a * cos(f));
    //旋转法线
     tangent += vec3(
        1.0 - d.x*d.x*(_Amplitude*sin(f)),
        d.x * (_Amplitude*cos(f)),
        -d.x * d.y * (_Amplitude*sin(f))
        );
     binormal += vec3(
        -d.x * d.y * (_Amplitude * sin(f)),
        d.y * (_Amplitude * cos(f)),
        1.0 - d.y * d.y * (_Amplitude * sin(f))
    );

    tbp.position = posAdd;
    tbp.tangent = tangent;
    tbp.binormal = binormal;
    return tbp;
}

void main()
{
    
    vec4 myPos = vec4(kzPosition.xyz, 1.0);
    texCoord = (kzTextureCoordinate0 + TextureOffset) * TextureTiling;

    //去除UV边界
    vec4 Tex = texture2D(Texture,texCoord);
     Tex.rgb = SaturateV3(Tex.rgb);
    // Tex += texture2D(Texture, texCoord-0.001);
    // Tex += texture2D(Texture, texCoord+pixel*1.5);
    // Tex += texture2D(Texture, texCoord-pixel*1.5);

    
   

    //old_
         objectPos = normalize(kzPosition);
         worldPos = (kzWorldMatrix * vec4(kzPosition, 1.0)).xyz;
         worldPos = (
            matrixChange(
                vec3(0.0,0.0,0.0),
                vec3(1.0,1.0,1.0),
                vec3(0.0,0.0, float(-_lightAngle+90.0)/57.0  ),
                vec4(worldPos, 1.0)
                )).xyz;

        // objectPos = (
        //     matrixChange(
        //         vec3(0.0,0.0,0.0),
        //         vec3(1.0,1.0,1.0),
        //         vec3(0.0,0.0,2.35),
        //         objectSpaceMatrix() * vec4(kzPosition, 1.0)
        //         )).xyz;
        // objectPos.z = smoothstep(-5.5,5.5,objectPos.y);
        // myPos = vertexFlagAni(myPos,texCoord,objectPos.z);
    vec3 p  = myPos.xyz;
    vec3 tangent = vec3(1, 0, 0);
    vec3 binormal = vec3(0, 0, 1);
    vec3 tangent2 = vec3(1, 0, 0);
    vec3 binormal2 = vec3(0, 0, 1);
    
    TBP m_tbp = CreatWave(_Amplitude_A ,_Wavelength_A,_Speed_A,_Direction_A,p,tangent,binormal);

    TBP m_tbpTwo = CreatWave(_Amplitude_B,_Wavelength_B,_Speed_B,_Direction_B,p,tangent,binormal);
    p += m_tbp.position;
    p += m_tbpTwo.position;
    myPos.xyz = p;


      //frag
    vec4 positionWorld = kzWorldMatrix * vec4(kzPosition.xyz, 1.0);
    vViewDirection = positionWorld.xyz - kzCameraPosition;
    vec4 N = kzNormalMatrix * vec4(kzNormal, 0.0);
    //求导
    // vec3 tangent = normalize(vec3(1.0 -  _Amplitude * sin(f),_Amplitude*cos(f),0.0));
    // vec3 WaveNormal = vec3(-tangent.y,tangent.x,0.0);
   // vNormal = N.xyz;
 //  vNormal = vec3(cross( WaveNormal.xyz,N.xyz ));
   // 旋转法线
    // vec3 tangent = vec3(
    //     1.0 - d.x*d.x*(_Amplitude_A*sin(f)),
    //     d.x * (_Amplitude_A*cos(f)),
    //     -d.x * d.y * (_Amplitude_A*sin(f))
    //     );
    // vec3 binormal = vec3(
    //     -d.x * d.y * (_Amplitude_A * sin(f)),
    //     d.y * (_Amplitude_A * cos(f)),
    //     1.0 - d.y * d.y * (_Amplitude_A * sin(f))
    // );
    tangent = m_tbp.tangent;
    binormal = m_tbp.binormal;
    tangent2 = m_tbpTwo.tangent;
    binormal2 = m_tbpTwo.binormal;
    vec3 WaveNormal = normalize(cross(binormal, tangent));
    vec3 WaveNormal2 = normalize(cross(binormal2, tangent2));
   
    //vNormal = SaturateV3( vec3(WaveNormal.xyz+mix(vec3(1.0),vec3(0.0),N.xyz) ));

    vNormal = SaturateV3( vec3(WaveNormal.xyz + WaveNormal2.xyz + N.xyz + N.xyz*pow(Tex.r,1.0)));
    if(_SubPlant)
    {
        #if KANZI_SHADER_USE_NORMALMAP_TEXTURE
            vNormal = N.xyz;
            vTangent = normalize((kzNormalMatrix * vec4(kzTangent.xyz, 0.0)).xyz);
            vBinormal = cross(vNormal, vTangent);
            
        #endif

        
    }
    //vNormal = N.xyz;

    //vNormal = SaturateV3( vec3(WaveNormal.xyz + WaveNormal2.xyz + N.xyz ));


   
    vertexColor = kzColor0;

    
   // gl_Position = kzProjectionCameraWorldMatrix * vec4(myPos.xyz, 1.0);

 

  //  gl_Position = kzProjectionCameraWorldMatrix * vec4(kzPosition.xyz+N.xyz*pow(Tex.r,5.0), 1.0);

 //    gl_Position = kzProjectionCameraWorldMatrix * vec4(kzPosition.xyz*pow(kzColor0.rgb,vec3(0.2)), 1.0);
    // myPos = matrixChange(
    //     vec3(0.0,0.0,0.0),
    //     vec3(1.0,1.0,1.0),
    //     vec3(TestValue.x,TestValue.y,TestValue.z),
    //     myPos*kzWorldMatrix
    //     );
    myPos.xyz = myPos.xyz*pow(vertexColor.rgb,vec3(0.1));
    myPos = matrixChange(
        vec3(0.0,0.0,0.0),
        vec3(1.0,1.0,1.0),
        vec3(0.0,0.0,0.0),
        myPos
        );
   gl_Position = kzProjectionCameraWorldMatrix * vec4(myPos.xyz, 1.0);
    gl_Position = kzProjectionCameraWorldMatrix * vec4(kzPosition.xyz, 1.0);
    if(_SubPlant)
    {
        gl_Position = kzProjectionCameraWorldMatrix * vec4(kzPosition.xyz*pow(vertexColor.rgb,vec3(0.1)), 1.0);
    }

#if KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS
    int i;
    for (i = 0; i < KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS; ++i)
    {
        vDirectionalLightDirection[i] = normalize(-DirectionalLightDirection[i]);
    }
#endif

#if KANZI_SHADER_NUM_POINT_LIGHTS
    int ip;
    for (ip = 0; ip < KANZI_SHADER_NUM_POINT_LIGHTS; ++ip)
    {
        vPointLightDirection[ip] = positionWorld.xyz - PointLightPosition[ip];
    }
#endif


}