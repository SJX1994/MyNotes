uniform lowp float BlendIntensity;
uniform lowp vec4 Ambient;
varying lowp vec3 vNormal;
varying mediump vec3 vViewDirection;

#if KANZI_SHADER_USE_BASECOLOR_TEXTURE
varying mediump vec2 vTexCoord;
uniform sampler2D Texture;
#endif

#if KANZI_SHADER_NUM_SPOT_LIGHTS || KANZI_SHADER_NUM_POINT_LIGHTS || KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS
varying lowp vec3 vAmbDif;
varying lowp vec3 vSpec;
#endif

#if KANZI_SHADER_USE_REFLECTION_CUBE
uniform lowp samplerCube TextureCube;
uniform lowp vec4        CubemapColor;
#endif

void main()
{
    precision lowp float;       
    lowp vec3 color = vec3(0.0); 
    
#if KANZI_SHADER_USE_BASECOLOR_TEXTURE
    lowp vec4 baseColor = texture2D(Texture, vTexCoord).rgba;
#else
    lowp vec4 baseColor = vec4(1.0);
#endif

    color += baseColor.rgb;

#if KANZI_SHADER_NUM_SPOT_LIGHTS || KANZI_SHADER_NUM_POINT_LIGHTS || KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS
    color *= vAmbDif;
    color += vSpec;
#endif
    
#if KANZI_SHADER_USE_REFLECTION_CUBE
    vec3 R = reflect(normalize(vViewDirection), normalize(vNormal));
    color += textureCube(TextureCube, R).rgb * CubemapColor.rgb;
#endif

    gl_FragColor = vec4(color, baseColor.a) * BlendIntensity;
}