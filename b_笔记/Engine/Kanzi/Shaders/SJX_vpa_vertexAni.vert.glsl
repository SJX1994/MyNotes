precision mediump float;
attribute vec3 kzPosition;

uniform highp mat4 kzProjectionCameraWorldMatrix;
uniform float VPA_Animation;
uniform float _Amplitude_A;
uniform float _Wavelength_A;
uniform float _Speed_A;
uniform vec2 _Direction_A;
uniform float _Amplitude_B;
uniform float _Wavelength_B;
uniform float _Speed_B;
uniform vec2 _Direction_B;


varying highp vec2 vTexCoord;
uniform sampler2D Texture; 
uniform sampler2D PerlinNoiseTexture;
uniform mediump vec2 TextureOffset;
uniform mediump vec2 TextureTiling;
attribute vec2 kzTextureCoordinate0;

uniform highp vec3 TestValue;

//light
uniform highp mat4 kzNormalMatrix;
attribute vec3 kzNormal;
uniform highp mat4 kzWorldMatrix;
varying mediump vec3 vViewDirection;
uniform highp vec3 kzCameraPosition;

uniform lowp float BlendIntensity;
uniform lowp vec4 Emissive;
uniform lowp vec4 Ambient;
uniform lowp vec4 Diffuse;
uniform lowp vec4 SpecularColor;
uniform mediump float SpecularExponent;

varying lowp vec3 vAmbDif;
varying lowp vec3 vSpec;
varying lowp vec3 vNormal; 
varying float vPointLightMask;
varying vec3 objectPos;

#if KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS
uniform mediump vec3 DirectionalLightDirection[KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS];
varying mediump vec3 vDirectionalLightDirection[KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS];
#endif


#if KANZI_SHADER_NUM_POINT_LIGHTS
uniform lowp vec4 PointLightColor[KANZI_SHADER_NUM_POINT_LIGHTS];
uniform mediump vec3 PointLightAttenuation[KANZI_SHADER_NUM_POINT_LIGHTS];
uniform mediump vec3 PointLightPosition[KANZI_SHADER_NUM_POINT_LIGHTS];
#endif


#define PI 3.1415926538
float Saturate(float target)
{
    return clamp(target,0.0,1.0);
}
vec3 SaturateV3(vec3 target)
{
    return clamp(target,0.0,1.0);
}
float random(vec2 UV)
{
     return fract(sin(dot(UV, vec2(12.9898, 78.233))) * 100.5453);
    // return fract(sin(UV.x)*1000.0);


}
struct TBP
{
  vec3 tangent;
  vec3 binormal;
  vec3 position;
};
//格斯特纳波
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
        float f = k * (dot(d,posAdd.xz) - c * VPA_Animation * _Speed);
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

    

//局部变量
    vec4 myPos = vec4(kzPosition.xyz, 1.0);
    vec3 p  = myPos.xyz;
    vec3 tangent = vec3(1, 0, 0);
    vec3 binormal = vec3(0, 0, 1);
    vec3 tangent2 = vec3(1, 0, 0);
    vec3 binormal2 = vec3(0, 0, 1);
    vTexCoord = kzTextureCoordinate0 * TextureTiling + TextureOffset;
    float TexA = texture2D(Texture,vTexCoord).a;
    vec2 ani_vTexCoord = vec2(vTexCoord.x+VPA_Animation*0.1,vTexCoord.y);
    float PerlinNoiseTex = texture2D(PerlinNoiseTexture,ani_vTexCoord).r;
    

    

//取值：
    vNormal = (kzNormalMatrix * vec4(kzNormal, 0.0)).xyz ;
    vec4 positionWorld = kzWorldMatrix * vec4(kzPosition.xyz, 1.0);
    vViewDirection = positionWorld.xyz - kzCameraPosition;
    vec3 V = normalize(vViewDirection);
    vec4 Norm = kzNormalMatrix * vec4(kzNormal, 0.0);
    vNormal = normalize(Norm.xyz);
    objectPos = normalize(kzPosition);

#if KANZI_SHADER_NUM_POINT_LIGHTS
    //全局灯光变量
    int i;
    vec3 L = vec3(1.0, 0.0, 0.0);
    vec3 H = vec3(1.0, 0.0, 0.0);
    float LdotN, NdotH;
    float specular;
    vec3 c;
    float d, attenuation;
    vAmbDif = Ambient.rgb;
    vSpec = vec3(0.0);    
    vec3 pointLightDirection;  
    vec3 spotLightDirection;
#endif   
    

#if KANZI_SHADER_NUM_POINT_LIGHTS
    
    
    for (i = 0; i < KANZI_SHADER_NUM_POINT_LIGHTS; ++i)
    {
        //波浪生成
            TBP m_tbp = CreatWave(_Amplitude_A ,_Wavelength_A,_Speed_A,_Direction_A,p,tangent,binormal);
            TBP m_tbpB = CreatWave(_Amplitude_B ,_Wavelength_B,_Speed_B,_Direction_B,p,tangent,binormal);

        pointLightDirection = positionWorld.xyz - PointLightPosition[i];
        pointLightDirection.y = positionWorld.y + (PointLightPosition[i].y-5.0);
        L = normalize(-pointLightDirection);
        H = normalize(-V + L);

        //法线注入
            
            tangent = m_tbp.tangent;
            binormal = m_tbp.binormal;
            tangent2 = m_tbpB.tangent;
            binormal2 = m_tbpB.binormal;
            vec3 WaveNormal = normalize(cross(binormal, tangent));
            vec3 WaveNormal2 = normalize(cross(binormal2, tangent2));
            vec4 N = kzNormalMatrix * vec4(kzNormal, 0.0);
            vNormal = SaturateV3( vec3(WaveNormal.xyz + WaveNormal2.xyz + N.xyz + N.xyz*pow(TexA,1.0)));

        LdotN = max(0.0, dot(L, vNormal));
        NdotH = max(0.0, dot(vNormal, H));
        specular = pow(NdotH, SpecularExponent);
        c = PointLightAttenuation[i];
        d = length(pointLightDirection);
        attenuation = 1.0 / max(0.001, (c.x + c.y * d + c.z * d * d));        
        vAmbDif += (LdotN * Diffuse.rgb) * attenuation * PointLightColor[i].rgb;
        vSpec +=  SpecularColor.rgb * specular * attenuation * PointLightColor[i].rgb;
        
        //位置注入
            float mask = vSpec.r + vAmbDif.r;
             p += m_tbp.position * (smoothstep(1.45,0.26,vTexCoord.x) - smoothstep(0.77,-0.26,vTexCoord.x));
             p += m_tbpB.position* (smoothstep(1.45,0.26,vTexCoord.x) - smoothstep(0.77,-0.26,vTexCoord.x));
             p += vec3(0.0,mask*0.005,0.0);

            p.y += PerlinNoiseTex*0.1* (smoothstep(1.45,0.26,vTexCoord.x) - smoothstep(0.77,-0.26,vTexCoord.x));
    }
#endif
    
    
    
   
    
    myPos.xyz = p;
    
    
    gl_Position = kzProjectionCameraWorldMatrix * vec4(myPos.xyz, 1.0);
    
}