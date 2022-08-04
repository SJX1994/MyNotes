precision mediump float;

uniform sampler2D Texture;

varying highp vec2 vTexCoord;
varying mediump vec3 vViewDirection;
varying float vPointLightMask;
varying vec3 objectPos;
uniform bool highLight;

uniform highp vec3 TestValue;

#if KANZI_SHADER_NUM_POINT_LIGHTS
    uniform lowp    vec4 PointLightColor      [KANZI_SHADER_NUM_POINT_LIGHTS];
    uniform mediump vec3 PointLightAttenuation[KANZI_SHADER_NUM_POINT_LIGHTS];
    varying mediump vec3 vPointLightDirection [KANZI_SHADER_NUM_POINT_LIGHTS];
    varying lowp vec3 vNormal;
    uniform lowp    vec4  Ambient;
    uniform lowp    vec4  Diffuse;
    uniform lowp    vec4  SpecularColor;
    uniform mediump float SpecularExponent;
    uniform lowp    vec4  Emissive;
    uniform lowp    float BlendIntensity;
    uniform lowp vec4 baseColor;
    varying lowp vec3 vAmbDif;
    varying lowp vec3 vSpec;
    
#endif
float Saturate(float target)
{
    return clamp(target,0.0,1.0);
}
float random(vec2 UV)
{
     return fract(sin(dot(UV, vec2(12.9898, 78.233))) *  43758.5453);
    // return fract(sin(UV.x)*1000.0);

}
vec3 m_normal( )
{
    vec3 N;
    N = vNormal;
    N = gl_FrontFacing ? normalize(N):normalize(-N);
    return N;
}
float m_frontMask(float size, float bright)
{
    return pow(
        min(
            Saturate(bright)
            ,
            dot(m_normal(),-normalize(
                vec3(vViewDirection.x-10.0-100.0,vViewDirection.y-14.0+10.0,vViewDirection.z-9.1+10.0))
                )
            ),size);
}
void main()
{

    vec3 fColor =  texture2D(Texture, vTexCoord).rgb * baseColor.rgb;
    float fA = texture2D(Texture, vTexCoord).a ;
    //vec3 fColor = vec3(random(vTexCoord/20.0));
    float pointLightMask = 0.0;
#if KANZI_SHADER_NUM_POINT_LIGHTS 
    
    pointLightMask = fColor.r * vAmbDif.r *fA +  vSpec.r * fA;
    fColor *= vAmbDif * fA;
    fColor += vSpec * fA;
    // fColor = vec3(fract(vSpec.r));
    // fA = 1.0;
    pointLightMask = mix(0.0,1.0,pow( Saturate(pointLightMask),1.0 )) ;
    fA *= pointLightMask;
    fA = Saturate(fA);
    fA -= 1.0 - Saturate(pointLightMask);
    vec3 m_pos = normalize(objectPos);
     
     if(highLight == true)
     {
        // float HLmask = smoothstep(-0.4,1.1-1.28,m_pos.x)-smoothstep(0.0,0.43,m_pos.x);
        float HLmask = smoothstep(0.46,0.47,vTexCoord.y) - smoothstep(0.48,0.49,vTexCoord.y);
         fColor *= HLmask;
         fA = 0.;
     }else
     {
         //fColor *= pow(m_frontMask(0.5,1000.0),2.1)*1.9;
     }
    // fColor *= step(0.26,vTexCoord.x) - step(0.77,vTexCoord.x);
#endif
   
    gl_FragColor = vec4(fColor,fA) * BlendIntensity;

}