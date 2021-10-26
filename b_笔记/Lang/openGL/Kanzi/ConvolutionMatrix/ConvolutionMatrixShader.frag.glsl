precision mediump float;
uniform sampler2D Texture0;
varying mediump vec2 vTexCoord;

varying  vec2 leftTextureCoordinate;
varying  vec2 rightTextureCoordinate;
varying  vec2 topTextureCoordinate;
varying  vec2 bottomTextureCoordinate;
varying  float centerMultiplier;
varying  float edgeMultiplier;

void main()
{
    //思路：将色差大的像素平均一下
    //把所有像素转变为矩阵

    vec3 color = texture2D(Texture0, vTexCoord).rgb;

     
     vec3 leftTextureColor = texture2D(Texture0, leftTextureCoordinate).rgb;
     vec3 rightTextureColor = texture2D(Texture0, rightTextureCoordinate).rgb;
     vec3 topTextureColor = texture2D(Texture0, topTextureCoordinate).rgb;
     vec3 bottomTextureColor = texture2D(Texture0, bottomTextureCoordinate).rgb;

    
    gl_FragColor = vec4(

        color * centerMultiplier - 
        (

            leftTextureColor * edgeMultiplier +
            rightTextureColor * edgeMultiplier +
            topTextureColor * edgeMultiplier +
            bottomTextureColor * edgeMultiplier
        )
        ,
        texture2D(Texture0, vTexCoord).a
        );

    // gl_FragColor = vec4(
    //     leftTextureColor
    //     ,
    //      texture2D(Texture0, vTexCoord).a);

}