void main()
{
    gl_FragColor.rgb = vec3(gl_FragCoord.z);
   
    gl_FragColor.a = 1.0;
}