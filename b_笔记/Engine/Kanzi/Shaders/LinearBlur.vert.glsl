attribute vec3 Position;
uniform highp mat4 ProjectionCameraWorldMatrix;
attribute vec2 TextureCoordinate0;
varying mediump vec2 uv;
void main()
{
    precision mediump float;
    uv = TextureCoordinate0;
    gl_Position = ProjectionCameraWorldMatrix * vec4(Position.xyz, 1.0);
}