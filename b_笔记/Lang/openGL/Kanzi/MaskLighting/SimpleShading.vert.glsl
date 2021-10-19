
precision highp float;
    
attribute vec3 kzPosition;
uniform mat4 kzProjectionCameraWorldMatrix;

attribute vec3 kzNormal;
varying vec3 vNormal;

uniform vec3 kzCameraPosition;
varying vec3 ViewDirection;
varying vec3 ViewDirectionSkyBox;
varying vec3 ViewDirectionSkyBox2;
varying vec3 ViewDirectionSkyBox3;

uniform mat4 kzWorldMatrix;
uniform mat4 kzNormalMatrix;

attribute vec2 kzTextureCoordinate0;
uniform vec2 TextureTiling;
uniform vec2 TextureOffset;

varying vec2 vTexCoord;

#if TEXTURE_NORMAL

attribute vec3 kzTangent;
varying vec3 vTangent;
varying vec3 vBinormal;
varying mat3 vTBN;

#endif

uniform samplerCube CubeMap;
uniform samplerCube CubeMap2;
uniform samplerCube CubeMapTop;

varying vec4 vRCol;
varying vec4 vDCol;
varying vec4 vRCol2;
varying vec4 vDCol2;
varying vec4 vRCol3;
varying vec4 vDCol3;
uniform float RoughnessFloat;
varying float vF;
varying float rHP;
uniform float Metalness;
uniform float Density;
varying vec3 vPos;
varying vec3 worldPos;
varying vec3 worldPosSkyBox;
varying vec3 worldPosSkyBox2;
varying vec3 worldPosSkyBox3;
varying vec3 objectPos;

//天空盒子
uniform vec3 SkyPosit;
uniform vec3 SkyScale;
uniform vec3 SkyRotat;
uniform vec3 Sky2Posit;
uniform vec3 Sky2Scale;
uniform vec3 Sky2Rotat;
uniform vec3 Sky3Posit;
uniform vec3 Sky3Scale;
uniform vec3 Sky3Rotat;
uniform bool Windows;
uniform float TestValue;

//光照
#if SJX_LIGHT_SPOTS
uniform highp vec3 SpotLightPosition[SJX_LIGHT_SPOTS];
varying highp vec3 vSpotLightDirection[SJX_LIGHT_SPOTS];
#endif

//math
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
void main()
{
   

    vNormal = ((kzNormalMatrix * vec4(kzNormal, 1.0)).xyz);
    
    vTexCoord = (kzTextureCoordinate0 + TextureOffset) * TextureTiling;
    
    #if TEXTURE_NORMAL
        vTangent = ((kzNormalMatrix * vec4(kzTangent.xyz, 0.0)).xyz);
        vBinormal = (cross(vNormal, vTangent));
        vTBN = mat3((vTangent), 
                    (vBinormal),
                    (vNormal));
    #endif
    objectPos = normalize(kzPosition);

    objectPos = (
        matrixChange(
            vec3(0.0,-24.9,0.0),
            vec3(1.0,1.0,1.0),
            vec3(0.0,0.0,0.0),
            kzWorldMatrix * vec4(kzPosition, 1.0)
            )).xyz;

    worldPosSkyBox = (
        matrixChange(
            SkyPosit,
            SkyScale,
            SkyRotat,
            kzWorldMatrix * vec4(kzPosition, 1.0)
            )).xyz;
    
    worldPosSkyBox2 = (
        matrixChange(
            Sky2Posit,
            Sky2Scale,
            Sky2Rotat,
            kzWorldMatrix * vec4(kzPosition, 1.0)
            )).xyz;

    worldPosSkyBox3 = (
        matrixChange(
            Sky3Posit,
            Sky3Scale,
            Sky3Rotat,
            kzWorldMatrix * vec4(kzPosition, 1.0)
            )).xyz;

    worldPos = (kzWorldMatrix * vec4(kzPosition, 1.0)).xyz;
    
    // worldPos =  (
    //     matrixChange(
    //         Sky2Posit,
    //         Sky2Scale,
    //         Sky2Rotat,
    //         kzWorldMatrix * vec4(kzPosition, 1.0)
    //         )).xyz;


    ViewDirectionSkyBox = worldPosSkyBox-kzCameraPosition;
    ViewDirectionSkyBox2 = worldPosSkyBox2-kzCameraPosition;
    ViewDirectionSkyBox3 = worldPosSkyBox3-kzCameraPosition;
    
    ViewDirection = vec3((worldPos-kzCameraPosition)[0],(worldPos-kzCameraPosition)[1],(worldPos-kzCameraPosition)[2]);

    //vPos = worldPos;

    vec3 vN = normalize(vNormal); 

    vec3 vV = Windows? normalize(ViewDirectionSkyBox) : normalize(ViewDirection);

    rHP = pow(RoughnessFloat,0.5);

    vF = max(0.0,-dot(vV,vN));

    vec3 vR = reflect(vV,vN);

    
    vDCol = textureCubeLod(CubeMap,vN,10.0);
    vDCol += textureCubeLod(CubeMap,-vN,10.0) * (1.0-vF) * (1.0-Density);
    
    //vDCol = mix(textureCubeLod(CubeMap,-vN,10.0), vDCol, (Density));
    vDCol *= (1.0-Metalness);
    //vDCol *= smoothstep(-.250,.50,vNormal.y);
    
    vDCol2 = textureCubeLod(CubeMap2,vN,10.0);
    vDCol2 += textureCubeLod(CubeMap2,vN,10.0)* (1.0-vF) * (1.0-Density);
    vDCol2 *= (1.0-Metalness);

    vDCol3 = textureCubeLod(CubeMapTop,vN,10.0);
    vDCol3 += textureCubeLod(CubeMapTop,vN,10.0)* (1.0-vF) * (1.0-Density);
    vDCol3 *= (1.0-Metalness);


    //vF = (rHP + (vF * rHP))*rHP*10.0;
    
    vRCol = textureCubeLod(CubeMap,vR,2.0);
    //vRCol *= smoothstep(-.250,.50,vNormal.y);
    vRCol2 = textureCubeLod(CubeMap2,vR,2.0);

    vRCol3 = textureCubeLod(CubeMapTop,vR,2.0);
    #if SJX_LIGHT_SPOTS
         for (int i = 0; i < SJX_LIGHT_SPOTS; ++i)
        {
            vSpotLightDirection[i] = worldPos.xyz - SpotLightPosition[i];
        }
    #endif

    gl_Position = kzProjectionCameraWorldMatrix * vec4(kzPosition.xyz, 1.0);
}

//sjx 25.8.2021