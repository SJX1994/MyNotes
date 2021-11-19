precision mediump float;

uniform sampler2D ShadowMapTexture;

uniform mediump float ShadowBias;
uniform lowp float ShadowIntensity;

uniform lowp vec4 DirectionalLightColor[1];
uniform lowp vec4 PointLightColor[2];
uniform mediump vec3 PointLightAttenuation[2];

uniform lowp vec4 Diffuse;
uniform lowp vec4 SpecularColor;
uniform mediump float SpecularExponent;

uniform lowp float BlendIntensity;

varying mediump vec3 vNormal;
varying mediump vec3 vViewDirection;
varying lowp vec3 vColor;
varying mediump vec3 vDirectionalLightDirection;
varying mediump vec3 vPointLightDirection[2];

varying highp vec4 vShadowPosition[1];

void main()
{
	vec3 L[3];
    vec3 H[3];
    float LdotN, NdotH;
    float specular;
    lowp vec3 lightColor;
    vec3 c;
    float d, attenuation;
    
    lowp vec3 color = vec3(0.0);
    vec3 N = normalize(vNormal);
    vec3 V = normalize(vViewDirection);    
    
    L[0] = vDirectionalLightDirection;
    H[0] = normalize(-V + L[0]);
    
    L[1] = normalize(-vPointLightDirection[0]);
    H[1] = normalize(-V + L[1]);
    
    L[2] = normalize(-vPointLightDirection[1]);
    H[2] = normalize(-V + L[2]);
    
    // Apply directional light 0.
    {
        LdotN = max(0.0, dot(L[0], N));
        NdotH = max(0.0, dot(N, H[0]));
        specular = pow(NdotH, SpecularExponent);
        lightColor = (LdotN * Diffuse.rgb) + SpecularColor.rgb * specular;
        lightColor *= DirectionalLightColor[0].rgb;
        color += lightColor;
    }
    
    // Apply point light 0.
    {
        LdotN = max(0.0, dot(L[1], N));
        NdotH = max(0.0, dot(N, H[1]));
        specular = pow(NdotH, SpecularExponent);
        c = PointLightAttenuation[0];
        d = length(vPointLightDirection[0]);
        attenuation = 1.0 / (0.01 + c.x + c.y * d + c.z * d * d);
        lightColor = (LdotN * Diffuse.rgb) + SpecularColor.rgb * specular;
        lightColor *= attenuation;
        lightColor *= PointLightColor[0].rgb;
        color += lightColor;
    }

    // Apply point light 1.
    {
        LdotN = max(0.0, dot(L[2], N));
        NdotH = max(0.0, dot(N, H[2]));
        specular = pow(NdotH, SpecularExponent);
        c = PointLightAttenuation[1];
        d = length(vPointLightDirection[1]);
        attenuation = 1.0 / (0.01 + c.x + c.y * d + c.z * d * d);
        lightColor = (LdotN * Diffuse.rgb) + SpecularColor.rgb * specular;
        lightColor *= attenuation;
        lightColor *= PointLightColor[1].rgb;
        color += lightColor;
    }

    vec2 coord = vShadowPosition[0].xy / vShadowPosition[0].w;

	float offsetMod = 1.0 - clamp(dot(N, L[0]), 0., 1.);
	float offset = ShadowBias + 0.001 *  offsetMod;
    float fragDistance = vShadowPosition[0].z - offset;
    	
    if( abs(coord.x) > 1.0 || abs(coord.y) > 1.0 || abs(vShadowPosition[0].z) > 1.0)
    {
		fragDistance = 1.0;
	}
		
    float visibility = texture2D(ShadowMapTexture, coord).r < fragDistance ?  1.0 - ShadowIntensity : 1.0;
    visibility *= dot(N, L[0]) < 0. ? 0. : 1.;
    color *= visibility;
	vec3 abc = mix(color,vec3(0.0,1.0,0.0),1.0-visibility);
	gl_FragColor.rgb = vColor + color * BlendIntensity + abc;
    //gl_FragColor.rgb = abc;
    gl_FragColor.a = BlendIntensity;
}