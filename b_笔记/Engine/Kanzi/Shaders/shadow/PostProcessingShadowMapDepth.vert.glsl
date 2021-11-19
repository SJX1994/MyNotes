attribute vec3 Position;
uniform highp mat4 ProjectionCameraWorldMatrix;

void main()
{
    precision mediump float;
    vec4 position = ProjectionCameraWorldMatrix * vec4(Position.xyz, 1.0);

    gl_Position = position;
}