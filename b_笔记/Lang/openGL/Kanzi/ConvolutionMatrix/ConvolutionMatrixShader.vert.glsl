precision mediump float;

attribute vec3 kzPosition;
attribute vec2 kzTextureCoordinate0;
uniform highp mat4 kzProjectionCameraWorldMatrix;

uniform mediump vec2 TextureOffset;
uniform mediump vec2 TextureTiling;

varying mediump vec2 vTexCoord;
//卷积需要的变量：
    uniform mediump float imageWidthFactor; //0.0001 越小越好
    uniform mediump float imageHeightFactor;//0.0001 越小越好
    uniform mediump float sharpness;        // (-3.6)负数为模糊 正数为锐化

    varying vec2 leftTextureCoordinate;
    varying vec2 rightTextureCoordinate;
    varying vec2 topTextureCoordinate;
    varying vec2 bottomTextureCoordinate;

    varying float centerMultiplier; 
    varying float edgeMultiplier; 

void main()
{
    

    vTexCoord = kzTextureCoordinate0*TextureTiling + TextureOffset;
    gl_Position = kzProjectionCameraWorldMatrix * vec4(kzPosition.xyz, 1.0);

    vec2 widthStep = vec2(imageWidthFactor, 0.0);
    vec2 heightStep = vec2(0.0, imageHeightFactor);

    leftTextureCoordinate = kzTextureCoordinate0.xy - widthStep;
    rightTextureCoordinate = kzTextureCoordinate0.xy + widthStep;
    topTextureCoordinate = kzTextureCoordinate0.xy + heightStep;
    bottomTextureCoordinate = kzTextureCoordinate0.xy - heightStep;

    centerMultiplier = 1.0 + 4.0 * sharpness;
    edgeMultiplier = sharpness;
}