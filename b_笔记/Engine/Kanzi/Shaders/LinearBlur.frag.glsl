varying mediump vec2 uv;
uniform sampler2D srcTexture;
uniform sampler2D BGTexture;
uniform mediump float intensity;
uniform lowp float BlendIntensity;
uniform int passes;

precision mediump float;
    
vec4 blur(vec2 uv, sampler2D tex) {
    float disp = 0.;
    vec4 c1 = vec4(0.0);
    disp = intensity*(0.5-distance(0.5, .1));
  
    for (int xi=0; xi<passes; xi++) {
        float x = float(xi) / float(passes) - 0.5;
        for (int yi=0; yi<passes; yi++) {
            float y = float(yi) / float(passes) - 0.5;
            vec2 v = vec2(x, y);
            float d = disp;
            c1 += texture2D(tex, uv + d*v);
        }
    }
    c1 /= float(passes*passes);
    return c1;
}

void main() {
    vec4 bg = blur(uv, BGTexture);
    gl_FragColor = blur(uv, srcTexture) * BlendIntensity;
}