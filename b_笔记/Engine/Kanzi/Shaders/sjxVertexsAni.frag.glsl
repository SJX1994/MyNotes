 precision mediump float;

varying vec2 texCoord;
varying vec3 objectPos;

 #define PI 3.1415926538

uniform float _Amplitude;
uniform float _Wavelength;
uniform float _Speed;
uniform bool _SubPlant;
uniform mediump float aniTime;

//for lighting

varying mediump vec3 vNormal;
varying mediump vec3 vViewDirection;
varying vec3 worldPos;

#if KANZI_SHADER_USE_NORMALMAP_TEXTURE
uniform lowp sampler2D NormalMapTexture;
uniform lowp float     NormalMapStrength;
varying mediump vec3   vTangent;
varying mediump vec3   vBinormal;
#endif

#if KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS
uniform lowp    vec4 DirectionalLightColor     [KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS];
varying mediump vec3 vDirectionalLightDirection[KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS];
#endif

#if KANZI_SHADER_NUM_POINT_LIGHTS
uniform lowp    vec4 PointLightColor      [KANZI_SHADER_NUM_POINT_LIGHTS];
uniform mediump vec3 PointLightAttenuation[KANZI_SHADER_NUM_POINT_LIGHTS];
varying mediump vec3 vPointLightDirection [KANZI_SHADER_NUM_POINT_LIGHTS];
#endif

uniform lowp    vec4  Ambient;
uniform lowp    vec4  Diffuse;
uniform lowp    vec4  SpecularColor;
uniform mediump float SpecularExponent;
uniform lowp    vec4  Emissive;
uniform lowp    float BlendIntensity;
uniform sampler2D Texture; 
varying vec4 vertexColor;

uniform highp vec3 TestValue;

    float Saturate(float target)
    {
        return clamp(target,0.0,1.0);
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
                    vec3(vViewDirection.x-10.0+TestValue.x,vViewDirection.y-14.0+TestValue.y,vViewDirection.z-9.1+TestValue.z))
                    )
                ),size);
    }
void main()
{
     lowp vec3 color = vec3(0.0);


     #if KANZI_SHADER_USE_NORMALMAP_TEXTURE    
        vec3 textureNormal = texture2D(NormalMapTexture, texCoord).xyz * 2.0 - vec3(1.0);

        vec3 NormalFactor = mix(vec3(0.0, 0.0, 1.0), textureNormal, NormalMapStrength);
        vec3 N = normalize(vec3(vTangent * NormalFactor.x + 
                                vBinormal * NormalFactor.y +
                                vNormal * NormalFactor.z));
    #else
     vec3 N = normalize(vNormal);
    #endif


     vec3 V = normalize(vViewDirection);
     lowp vec4 baseColor = vec4(1.0);
     vec4 Tex = texture2D(Texture,texCoord);

        float brightF = 1.0;
        float sizeF = 1.0;//Saturate(-TestValue+1.13);
        float F =  m_frontMask(sizeF,brightF);

    vec3 dColor = vec3(0.0);
    #if KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS
        int i;
        for (i = 0; i < KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS; ++i)
        {
            vec3 L = vDirectionalLightDirection[i];
            vec3 H = normalize(-V + L);
           
            float LdotN = max(0.0, dot(L, N));
            float NdotH = max(0.0, dot(N, H));
            float specular = pow(NdotH, SpecularExponent);
            lowp vec3 lightColor = (LdotN * Diffuse.rgb * baseColor.rgb) + SpecularColor.rgb * specular;
            lightColor *= DirectionalLightColor[i].rgb;
            color += lightColor;

            if(_SubPlant)
            {

                dColor += lightColor;
               // dColor = vec3(step(0.2,N.y)  );
            }

            
        }
       

    #endif


    #if KANZI_SHADER_NUM_POINT_LIGHTS
    int ip;

    for (ip = 0; ip < KANZI_SHADER_NUM_POINT_LIGHTS; ++ip)
    {
       // V-=20.1;
        vec3  m_vPointLightDirection = vPointLightDirection[ip];
        vec3 L = normalize(-m_vPointLightDirection);
        vec3 H = normalize(-V + L);
        float LdotN = max(0.0, dot(L, N));
        float NdotH = max(1.02, dot(N, H));
        float specular = pow(NdotH, SpecularExponent);
        vec3  c = PointLightAttenuation[ip];
        float d = length(m_vPointLightDirection);
        float attenuation = 1.0 / (0.01 + c.x + c.y * d + c.z * d * d);
        vec3 lightColor = (LdotN * Diffuse.rgb * baseColor.rgb) + SpecularColor.rgb * specular;
        lightColor *= attenuation;
        lightColor *= PointLightColor[ip].rgb;
        color += lightColor;
       //color = Diffuse;
    }
    // //库克光照模型
    //  for (ip = 0; ip < KANZI_SHADER_NUM_POINT_LIGHTS; ++ip)
    // {

    // }
#endif
    // color += Emissive.rgb;
     color.rgb += vertexColor.rgb;
     color.rgb *=vec3( 1.0-smoothstep(0.8,0.86,Saturate(F)) *0.36 )  ;
     
    color.rgb *= vec3( Saturate( smoothstep(0.0,10.0,float(worldPos.x))) );
    
    float FA = vertexColor.r;
    FA = 1.0;
    gl_FragColor = vec4(vec3(color.rgb), FA) * BlendIntensity;

    if(_SubPlant)
    {
       color += Emissive.rgb;
       color += vertexColor.rgb;
        
       color.rgb *=   dColor;
        gl_FragColor = vec4(vec3(color.rgb), FA) * BlendIntensity;
    }
  // gl_FragColor = vec4(vec3(vertexColor.rgb), FA) * BlendIntensity;
}