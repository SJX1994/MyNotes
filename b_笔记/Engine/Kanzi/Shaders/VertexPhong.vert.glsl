attribute vec3 Position;
attribute vec3 Normal;

uniform highp mat4 ProjectionCameraWorldMatrix;
uniform highp mat4 WorldMatrix;
uniform highp mat4 NormalMatrix;
uniform highp vec3 CameraPosition;

#if KANZI_SHADER_USE_MORPHING
attribute vec3 MorphTarget0Position;
attribute vec3 MorphTarget1Position;
attribute vec3 MorphTarget2Position;
attribute vec3 MorphTarget0Normal;
attribute vec3 MorphTarget1Normal;
attribute vec3 MorphTarget2Normal;
uniform mediump float MorphWeights[3];
#endif

#if KANZI_SHADER_USE_BASECOLOR_TEXTURE
attribute vec2 TextureCoordinate0;
varying mediump vec2 vTexCoord;
uniform mediump vec2 TextureOffset;
uniform mediump vec2 TextureTiling;
#endif

#if KANZI_SHADER_SKINNING_BONE_COUNT 
attribute vec4 Weight;
attribute vec4 MatrixIndices;
uniform highp vec4 MatrixPalette[KANZI_SHADER_SKINNING_BONE_COUNT*4]; 
#endif

#if KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS
uniform mediump vec3 DirectionalLightDirection[KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS];
uniform lowp    vec4 DirectionalLightColor     [KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS];
#endif

#if KANZI_SHADER_NUM_POINT_LIGHTS
uniform lowp vec4 PointLightColor[KANZI_SHADER_NUM_POINT_LIGHTS];
uniform mediump vec3 PointLightAttenuation[KANZI_SHADER_NUM_POINT_LIGHTS];
uniform mediump vec3 PointLightPosition[KANZI_SHADER_NUM_POINT_LIGHTS];
#endif

#if KANZI_SHADER_NUM_SPOT_LIGHTS
uniform mediump vec3  SpotLightPosition[KANZI_SHADER_NUM_SPOT_LIGHTS];
uniform mediump vec4  SpotLightColor         [KANZI_SHADER_NUM_SPOT_LIGHTS];
uniform mediump vec3  SpotLightDirection     [KANZI_SHADER_NUM_SPOT_LIGHTS];
uniform mediump vec3  SpotLightConeParameters[KANZI_SHADER_NUM_SPOT_LIGHTS];
uniform mediump vec3  SpotLightAttenuation   [KANZI_SHADER_NUM_SPOT_LIGHTS];
#endif

uniform lowp float BlendIntensity;
uniform lowp vec4 Emissive;
uniform lowp vec4 Ambient;
uniform lowp vec4 Diffuse;
uniform lowp vec4 SpecularColor;
uniform mediump float SpecularExponent;

varying mediump vec3 vViewDirection;
varying lowp vec3 vAmbDif;
varying lowp vec3 vSpec;
varying lowp vec3 vNormal; 

void main()
{
    precision mediump float;
    
    
#if KANZI_SHADER_SKINNING_BONE_COUNT
    mat4 localToSkinMatrix;
    int i1 = 3 * int(MatrixIndices.x);
    int i2 = 3 * int(MatrixIndices.y);
    int i3 = 3 * int(MatrixIndices.z);
    int i4 = 3 * int(MatrixIndices.w);
    vec4 b1 = Weight.x * MatrixPalette[i1] + Weight.y * MatrixPalette[i2]
        + Weight.z * MatrixPalette[i3] + Weight.w * MatrixPalette[i4];
    vec4 b2 = Weight.x * MatrixPalette[i1 + 1] + Weight.y * MatrixPalette[i2 + 1]
        + Weight.z * MatrixPalette[i3 + 1] + Weight.w * MatrixPalette[i4 + 1];
    vec4 b3 = Weight.x * MatrixPalette[i1 + 2] + Weight.y * MatrixPalette[i2 + 2]
        + Weight.z * MatrixPalette[i3 + 2] + Weight.w * MatrixPalette[i4 + 2];
   
    localToSkinMatrix[0] = vec4(b1.xyz, 0.0);
    localToSkinMatrix[1] = vec4(b2.xyz, 0.0);
    localToSkinMatrix[2] = vec4(b3.xyz, 0.0);
    localToSkinMatrix[3] = vec4(b1.w, b2.w, b3.w, 1.0);
    mat4 localToWorldMatrix = WorldMatrix * localToSkinMatrix;
    
    vec4 positionWorld = localToWorldMatrix * vec4(Position.xyz, 1.0);
    vViewDirection = positionWorld.xyz - CameraPosition; 
    vec3 V = normalize(vViewDirection);
    vec4 Norm = mat4(localToWorldMatrix[0],
                  localToWorldMatrix[1], 
                  localToWorldMatrix[2], 
                  vec4(0.0, 0.0, 0.0, 1.0)) * vec4(Normal.xyz, 0.0);
    vNormal = normalize(Norm.xyz);
    gl_Position = ProjectionCameraWorldMatrix * localToSkinMatrix * vec4(Position.xyz, 1.0);
#elif KANZI_SHADER_USE_MORPHING
    vec3 position = MorphTarget0Position * MorphWeights[0] + MorphTarget1Position * MorphWeights[1] + MorphTarget2Position * MorphWeights[2];
    vec4 positionWorld = WorldMatrix * vec4(position.xyz, 1.0);
    vec3 V = normalize(positionWorld.xyz - CameraPosition);
    vec3 normal =normalize( (MorphTarget0Normal * MorphWeights[0]) +
                            (MorphTarget1Normal * MorphWeights[1]) +
                            (MorphTarget2Normal * MorphWeights[2]));
    vec4 Norm = NormalMatrix * vec4(normal.xyz, 0.0);
    vNormal = normalize(Norm.xyz);
    vViewDirection = positionWorld.xyz - CameraPosition;
    gl_Position = ProjectionCameraWorldMatrix * vec4(position.xyz, 1.0);
#else
    gl_Position = ProjectionCameraWorldMatrix * vec4(Position.xyz, 1.0);  
    vec4 positionWorld = WorldMatrix * vec4(Position.xyz, 1.0);
    vViewDirection = positionWorld.xyz - CameraPosition;
    vec3 V = normalize(vViewDirection);
    vec4 Norm = NormalMatrix * vec4(Normal, 0.0);
    vNormal = normalize(Norm.xyz);
#endif
    
#if KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS || KANZI_SHADER_NUM_POINT_LIGHTS || KANZI_SHADER_NUM_SPOT_LIGHTS
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

    
    
#if KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS
    for (i = 0; i < KANZI_SHADER_NUM_DIRECTIONAL_LIGHTS; ++i)
    {
        if(length(DirectionalLightDirection[i])> 0.01)
        {
            L = normalize(-DirectionalLightDirection[i]);
        }
        H = normalize(-V + L);
        LdotN = max(0.0, dot(L, vNormal));
        NdotH = max(0.0, dot(vNormal, H));        
        specular = pow(NdotH, SpecularExponent);
        vAmbDif += (LdotN * Diffuse.rgb) * DirectionalLightColor[i].rgb;
        vSpec += SpecularColor.rgb * specular * DirectionalLightColor[i].rgb;        
    }
#endif
    
#if KANZI_SHADER_NUM_POINT_LIGHTS
    for (i = 0; i < KANZI_SHADER_NUM_POINT_LIGHTS; ++i)
    {
        pointLightDirection = positionWorld.xyz - PointLightPosition[i];
        L = normalize(-pointLightDirection);
        H = normalize(-V + L);
        LdotN = max(0.0, dot(L, vNormal));
        NdotH = max(0.0, dot(vNormal, H));
        specular = pow(NdotH, SpecularExponent);
        c = PointLightAttenuation[i];
        d = length(pointLightDirection);
        attenuation = 1.0 / max(0.001, (c.x + c.y * d + c.z * d * d));        
        vAmbDif += (LdotN * Diffuse.rgb) * attenuation * PointLightColor[i].rgb;
        vSpec +=  SpecularColor.rgb * specular * attenuation * PointLightColor[i].rgb;
        
    }
#endif

#if KANZI_SHADER_NUM_SPOT_LIGHTS
    for (i = 0; i < KANZI_SHADER_NUM_SPOT_LIGHTS; ++i)
    {
        spotLightDirection = positionWorld.xyz - SpotLightPosition[i];
        L = normalize(-spotLightDirection);
        LdotN = dot(L, vNormal);
        
        if(LdotN > 0.0)
        {
            float cosDirection = dot(L, -SpotLightDirection[i]);
            float cosOuter = SpotLightConeParameters[i].x;
            float t = cosDirection - cosOuter;
            if (t > 0.0)
            {
                vec3 H = normalize(-V + L);
                float NdotH = max(0.0, dot(vNormal, H));
                float specular = pow(NdotH, SpecularExponent);
                vec3  c = SpotLightAttenuation[i];
                float d = length(spotLightDirection);
                float denom = (0.01 + c.x + c.y * d + c.z * d * d) * SpotLightConeParameters[i].z;
                float attenuation = min(t / denom, 1.0);
                vAmbDif += (LdotN * Diffuse.rgb) * attenuation * SpotLightColor[i].rgb;
                vSpec += SpecularColor.rgb * specular * attenuation * SpotLightColor[i].rgb;
            }
        }        
    }    
#endif

    vSpec += Emissive.rgb;
    
#if KANZI_SHADER_USE_BASECOLOR_TEXTURE
    vTexCoord = TextureCoordinate0 * TextureTiling + TextureOffset;
#endif
}