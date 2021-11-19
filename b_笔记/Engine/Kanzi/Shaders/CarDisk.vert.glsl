precision mediump float;

//base
attribute vec3 Position;
attribute vec3 Normal;
attribute vec2 TextureCoordinate0;
uniform highp mat4 ProjectionCameraWorldMatrix;
uniform mat4 WorldMatrix;
uniform mat4 NormalMatrix;
uniform vec3 CameraPosition;
varying vec3 vNormal;

//坐标切分（节省空间）
varying vec2 vTexCoord;
varying vec2 vTexCoord0;
varying vec2 vTexCoord1;
varying vec2 vTexCoord2;

//真实片元光照
#define PI 3.1415926538
uniform mediump vec3 SpotLightPosition[2];
varying mediump vec3 vSpotLightDirection[2];
varying mediump float vSpotCutoff[2];
uniform mediump float SpotLightCutoffAngle[2];
varying mediump vec3 vViewDirection;


//光照流程控制
uniform bool isRipUnderCar;
uniform bool isRipUnderCarLight;
uniform bool isRipUnderCarLightToggle;

void main()
{
    
    //xy图像宽高 zw对象宽高
    vec4 PngSize = vec4(2048,2048,1024,1024);
    
    vec2 ripple = vec2 (0.0,0.0);//disk坐标
    vec2 fackLight = vec2(1024.0,0.0);//贴图光照坐标
    vec2 fackShadow = vec2(0.0,1024.0);//贴图投影坐标
    
    if(isRipUnderCar)
    {
        //贴图光照
        if(isRipUnderCarLightToggle== false)
        {
            vTexCoord0 = TextureCoordinate0;
            vTexCoord0.x = TextureCoordinate0.x*(PngSize.z/PngSize.x)+(ripple.x/PngSize.x);
            vTexCoord0.y = TextureCoordinate0.y*(PngSize.w/PngSize.y)+(((PngSize.y - ripple.y) - PngSize.w) / PngSize.y);
        }
        
        //片元光照
        if(isRipUnderCarLightToggle == true)
        {
            vTexCoord0 = TextureCoordinate0;
            vTexCoord0.x = TextureCoordinate0.x*(PngSize.z/PngSize.x)+(ripple.x/PngSize.x);
            vTexCoord0.y = TextureCoordinate0.y*(PngSize.w/PngSize.y)+(((PngSize.y - ripple.y) - PngSize.w) / PngSize.y);
            
            vTexCoord1 = TextureCoordinate0;
            vTexCoord1.x = TextureCoordinate0.x*(PngSize.z/PngSize.x)+(fackLight.x/PngSize.x);
            vTexCoord1.y = TextureCoordinate0.y*(PngSize.w/PngSize.y)+(((PngSize.y - fackLight.y) - PngSize.w) / PngSize.y);
            
            vec4 positionWorld = WorldMatrix * vec4(Position.xyz, 1.0);
            vViewDirection =  positionWorld.xyz - CameraPosition;
            
            //支持 两盏 spot灯，可拓展
            vSpotLightDirection[0] = positionWorld.xyz - SpotLightPosition[0];
            vSpotLightDirection[1] = positionWorld.xyz - SpotLightPosition[1];
            
            vSpotCutoff[0] = cos(SpotLightCutoffAngle[0] * PI / 180.0);
            vSpotCutoff[1] = cos(SpotLightCutoffAngle[1] * PI / 180.0);
            
            
            
            vec4 N = NormalMatrix * vec4(Normal, 1.0);
            vNormal = N.xyz;
            
            
        }
        //阴影
         vTexCoord2 = TextureCoordinate0;
         vTexCoord2.x = TextureCoordinate0.x*(PngSize.z/PngSize.x)+(fackShadow.x/PngSize.x);
         vTexCoord2.y = TextureCoordinate0.y*(PngSize.w/PngSize.y)+(((PngSize.y - fackShadow.y) - PngSize.w) / PngSize.y);
            
    }
    
    
    gl_Position = ProjectionCameraWorldMatrix * vec4(Position.xyz, 1.0);
}