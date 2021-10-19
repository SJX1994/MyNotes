attribute vec3 kzPosition;
attribute vec2 kzTextureCoordinate0;
uniform highp mat4 kzProjectionCameraWorldMatrix;
precision mediump float;

uniform vec3 pos1;
uniform vec3 pos2;
uniform vec3 pos3;
uniform vec3 pos4;
uniform float TestValue;
varying vec3 B2rgb;

vec3 bezier2(vec3 a, vec3 b, float t) {
   return mix(a, b, t);
}
vec3 bezier3(vec3 a, vec3 b, vec3 c, float t) {
   return mix(bezier2(a, b, t), bezier2(b, c, t), t);
}
vec3 bezier4(vec3 a, vec3 b, vec3 c, vec3 d, float t) {
   return mix(bezier3(a, b, c, t), bezier3(b, c, d, t), t);
}

void main()
{
   

    float t  = kzTextureCoordinate0.y;
    //t = TestValue;

    vec3 newPos = kzPosition.xyz + bezier4(pos1,pos2,pos3,pos4,t);

    

    gl_Position = kzProjectionCameraWorldMatrix * vec4(newPos.xyz, 1.0) ;

    B2rgb = vec3(kzTextureCoordinate0.x);

}