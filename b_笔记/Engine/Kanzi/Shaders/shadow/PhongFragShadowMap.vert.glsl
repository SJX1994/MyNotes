uniform highp vec3 ShadowCamPos;
uniform lowp float ShadowCamSize;
uniform lowp float ShadowCamNear;
uniform lowp float ShadowCamFar;

attribute vec3 Position;
attribute vec3 Normal;

uniform highp mat4 ProjectionCameraWorldMatrix;
uniform highp mat4 WorldMatrix;
uniform highp mat4 NormalMatrix;
uniform highp vec3 CameraPosition;

uniform mediump vec3 PointLightPosition[2];
uniform mediump vec3 DirectionalLightDirection[1];

varying mediump vec3 vNormal;
varying mediump vec3 vViewDirection;
varying lowp vec3 vColor;
varying mediump vec3 vDirectionalLightDirection;
varying mediump vec3 vPointLightDirection[2];

uniform lowp vec4 Ambient;
uniform lowp vec4 Emissive;

varying highp vec4 vShadowPosition[1];

// matrix operations
mat4 translate(vec3 t)
{
 	return mat4(
        vec4(1.,0.,0.,0.),
        vec4(0.,1.,0.,0.),
        vec4(0.,0.,1.,0.),
        vec4(t,1.)
        );
}

mat4 scale(vec3 s)
{
 	return mat4(
        vec4(s.x,0.,0.,0.),
        vec4(0.,s.y,0.,0.),
        vec4(0.,0.,s.z,0.),
        vec4(0.,0.,0.,1.)
        );
}

mat4 ortho(float l, float r, float b, float t, float n, float f)
{
// translation and scale
    return scale(vec3(2./(r-l),2./(t-b),2./(f-n))) * 
                 translate(vec3(-(l+r)/2.,-(t+b)/2.,-(f+n)/2.));    
}

mat4 lookAt(vec3 eye, vec3 center, vec3 up)
{
    vec3 z = normalize(eye-center);
    vec3 x = normalize(cross(up,z));
    vec3 y = cross(z,x);
    
    mat4 v = mat4(
        vec4(x.x,y.x,z.x,0.),
        vec4(x.y,y.y,z.y,0.),
        vec4(x.z,y.z,z.z,0.),
        vec4(0.,0.,0.,1.)
        );
    
    return v*translate(-eye);
}

void main()
{
    precision mediump float;
        
    vec4 positionWorld = WorldMatrix * vec4(Position.xyz, 1.0);
    vViewDirection = positionWorld.xyz - CameraPosition;
    
    vPointLightDirection[0] = positionWorld.xyz - PointLightPosition[0];
    vPointLightDirection[1] = positionWorld.xyz - PointLightPosition[1];
    vDirectionalLightDirection = vec3(1.0, 0.0, 0.0);
    if(length(DirectionalLightDirection[0]) > 0.01)
    {
        vDirectionalLightDirection = normalize(-DirectionalLightDirection[0]);
    }
    
    vec4 N = NormalMatrix * vec4(Normal, 1.0);
    vNormal = N.xyz;
    vColor = Ambient.rgb + Emissive.rgb;
    
	highp mat4 BiasMatrix = mat4(
		vec4(0.5, 0.0, 0.0, 0.0),
		vec4(0.0, 0.5, 0.0, 0.0),
		vec4(0.0, 0.0, 0.5, 0.0),
		vec4(0.5, 0.5, 0.5, 1.0)
	);
   	
	mat4 ShadowView = translate(vec3(ShadowCamPos)) * lookAt(-DirectionalLightDirection[0], vec3(0.0), vec3(0.0, 1.0, 0.0));
    
    mat4 ShadowProjection = ortho(-ShadowCamSize, ShadowCamSize, -ShadowCamSize, ShadowCamSize, -ShadowCamNear, -ShadowCamFar);
    
    vec4 shadowPosition = BiasMatrix * ShadowProjection * ShadowView * WorldMatrix * vec4(Position.xyz, 1.0);
	vShadowPosition[0] = shadowPosition;
	
    gl_Position = ProjectionCameraWorldMatrix * vec4(Position.xyz, 1.0);
}