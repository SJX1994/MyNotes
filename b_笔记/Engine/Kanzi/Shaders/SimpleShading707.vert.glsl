
precision lowp float;
    
attribute vec3 Position;
uniform mat4 ProjectionCameraWorldMatrix;

attribute vec3 Normal;
varying vec3 vNormal;

uniform vec3 CameraPosition;
varying vec3 ViewDirection;
varying vec3 ViewDirectionSkyBox;
varying vec3 ViewDirectionSkyBox2;
varying vec3 ViewDirectionSkyBox3;

uniform mat4 WorldMatrix;
uniform mat4 NormalMatrix;


attribute vec2 TextureCoordinate0;
uniform vec2 TextureTiling;
uniform vec2 TextureOffset;

varying vec2 vTexCoord;

#if TEXTURE_NORMAL

attribute vec3 Tangent;
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
uniform bool CarHead;
uniform float TestValue;


//from frag:
varying float Angle;
varying vec3 m_objectPos;
//from frag function:
float GetAngleBetweenPoints(vec3 a, vec3 b)
{
   return acos((a.x*b.x + a.y*b.y + a.z*b.z)/( sqrt(pow(a.x,2.0)+pow(a.y,2.0)+pow(a.z,2.0)) * sqrt(pow(b.x,2.0)+pow(b.y,2.0)+pow(b.z,2.0))));
}

//math
float Saturate(float target)
    {
        return clamp(target,0.0,1.0);
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
void main()
{
   

    vNormal = ((NormalMatrix * vec4(Normal, 1.0)).xyz);
    
    vTexCoord = (TextureCoordinate0 + TextureOffset) * TextureTiling;
    
    #if TEXTURE_NORMAL
        vTangent = ((NormalMatrix * vec4(Tangent.xyz, 0.0)).xyz);
        vBinormal = (cross(vNormal, vTangent));
        vTBN = mat3((vTangent), 
                    (vBinormal),
                    (vNormal));
    #endif
    objectPos = normalize(Position);

    // objectPos = (
    //     matrixChange(
    //         vec3(0.0,-24.9,0.0),
    //         vec3(1.0,1.0,1.0),
    //         vec3(0.0,0.0,0.0),
    //         WorldMatrix * vec4(Position, 1.0)
    //         )).xyz;
    
     mat4 objSpaceMat  = mat4(
            vec4 ( 0.5,  0.5, 0.5 , 1.0),
              vec4  (-0.5,  0.5, 0.5, 1.0),
              vec4  ( 0.5, -0.5, 0.5, 1.0),
            vec4   (-0.5, -0.5, 0.5, 1.0)
          );

    objectPos = (
        matrixChange(
            vec3(0.0,-24.9,0.0),
            vec3(1.0,1.0,1.0),
            vec3(0.0,0.0,0.0),
            WorldMatrix * vec4(Position, 1.0)
            )).xyz;

    float m_SkyPosit = SkyPosit.y;

    m_SkyPosit += CarHead ?  Saturate(smoothstep(-9.0,9.0,objectPos.z) )*25.0 : SkyPosit.y;

    worldPosSkyBox = (
        matrixChange(
            vec3(SkyPosit.x,m_SkyPosit,SkyPosit.z),
            SkyScale,
            SkyRotat,
            WorldMatrix * vec4(Position, 1.0)
            )).xyz;
    

    worldPosSkyBox2 = (
        matrixChange(
            Sky2Posit,
            Sky2Scale,
            Sky2Rotat,
            WorldMatrix * vec4(Position, 1.0)
            )).xyz;

    worldPosSkyBox3 = (
        matrixChange(
            Sky3Posit,
            Sky3Scale,
            Sky3Rotat,
            WorldMatrix * vec4(Position, 1.0)
            )).xyz;

    worldPos = (WorldMatrix * vec4(Position, 1.0)).xyz;
    
    // worldPos =  (
    //     matrixChange(
    //         Sky2Posit,
    //         Sky2Scale,
    //         Sky2Rotat,
    //         WorldMatrix * vec4(Position, 1.0)
    //         )).xyz;


    ViewDirectionSkyBox = worldPosSkyBox-CameraPosition;
    ViewDirectionSkyBox2 = worldPosSkyBox2-CameraPosition;
    ViewDirectionSkyBox3 = worldPosSkyBox3-CameraPosition;
    
    ViewDirection = vec3((worldPos-CameraPosition)[0],(worldPos-CameraPosition)[1],(worldPos-CameraPosition)[2]);

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


    gl_Position = ProjectionCameraWorldMatrix * vec4(Position.xyz, 1.0);
    
     vec3 vvv = vec3(1.0,0.0,0.0);
     Angle =GetAngleBetweenPoints(vvv,CameraPosition);


     //物体坐标色：
     m_objectPos = vec3(objectPos.x,objectPos.y,objectPos.z+18.0);
}

//sjx 25.8.2021