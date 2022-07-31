// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "sjx/sand"

    {
	Properties
	{
		_MainTex ("Main Texture", 2D) = "white" {}

		[Header(Ambient)]
            _Ambient ("Intensity", Range(0., 1.)) = 0.1
            _AmbColor ("Color", color) = (1., 1., 1., 1.)

		[Header(Diffuse)]
            _Diffuse ("Val", Range(0., 1.)) = 1.
            _DifColor ("Color", color) = (1., 1., 1., 1.)

		[Header(Specular)]
            [Toggle] _Spec("Enabled?", Float) = 0.
            _Shininess ("Shininess", Range(0.1, 10)) = 1.
            _SpecColor ("Specular color", color) = (1., 1., 1., 1.)

		[Header(Emission)]
            _EmissionTex ("Emission texture", 2D) = "gray" {}
            _EmiVal ("Intensity", float) = 0.
            [HDR]_EmiColor ("Color", color) = (1., 1., 1., 1.)
        [Header(Transparent)]
            _Cutoff ("AlphaCutoff", Range(0., 1.)) = 0.5
            [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 2 
            [Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend ("Src Blend", Float) = 1
		    [Enum(UnityEngine.Rendering.BlendMode)] _DstBlend ("Dst Blend", Float) = 0
            [Enum(Off, 0, On, 1)] _ZWrite ("Z Write", Float) = 0
        [Header(Sand)]
            _Amount ("Amount", Range(0., 1.)) = 0.25
            _SandColor ("SandColor", color) = (1., 1., 0., 1.)
            _Width ("DropWidth", Range(1., 100.)) = 0.25
	}

	SubShader
	{
        Tags
		{
			"Queue" = "Transparent"
		}
        Pass // MOVE SandBack
        {
            Name "SandBack"
            Cull front  
            Blend [_SrcBlend] [_DstBlend]
            // Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off  

            Tags { 
                "RenderType"="Opaque" 
                "Queue"="Transparent" 
                "LightMode"="ForwardBase" 
                }
            CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            #pragma target 3.0
            #include "UnityCG.cginc"
            #include "sandHelper.cginc"

            struct unity_vertexInput {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                fixed3 objectPos : COLOR0;
                fixed2 viewPos : COLOR1;
                float3 worldNormal : TEXCOORD2;
            
        
            };
            unity_vertexInput vert(appdata_full l_appdata_full)
			{

				unity_vertexInput m_unity_vertexInput;

                m_unity_vertexInput.objectPos = l_appdata_full.vertex;
                m_unity_vertexInput.viewPos = UnityObjectToClipPos (l_appdata_full.vertex);
				// World position
				m_unity_vertexInput.worldPos = mul(unity_ObjectToWorld, l_appdata_full.vertex);
				// Clip position
				m_unity_vertexInput.pos = mul(UNITY_MATRIX_VP, float4(m_unity_vertexInput.worldPos, 1.));
                
                
				// Normal in WorldSpace
				m_unity_vertexInput.worldNormal = normalize(mul(l_appdata_full.normal, (float3x3)unity_WorldToObject));

				m_unity_vertexInput.uv = l_appdata_full.texcoord;

				return m_unity_vertexInput;
			}
            // sand
            fixed _Amount;
            fixed4 _SandColor;
            fixed _Width;
            fixed4 frag(unity_vertexInput l_unity_vertexInput) : SV_Target
            {
                // Camera direction
				float3 m_viewDir = normalize(_WorldSpaceCameraPos.xyz - l_unity_vertexInput.worldPos.xyz);

                // 沙砾逻辑
                fixed4 f_color = fixed4(1.0,1.0,0.0,1.0);
                fixed m_objectY = l_unity_vertexInput.objectPos.yyy;
                m_objectY = step(0.0, m_objectY);
                
                fixed m_worldY = l_unity_vertexInput.worldPos.yyy;
                m_worldY = step(0.0, m_worldY);
                float3 m_ObjectPosition = UNITY_MATRIX_M._m03_m13_m23;
                fixed m_faceWordY = (l_unity_vertexInput.worldPos - m_ObjectPosition).yyy;

                fixed3 m_halfPos = mul(fixed3(0.0,0.5,0.0),unity_ObjectToWorld);
                fixed3 m_objectScale = f_objectScale();
                float m_inputAmount = m_objectScale.yyy * (_Amount-0.5);
                float m_sandWithSacle = m_halfPos.yyy + m_inputAmount.xxx;
                float m_lower =(1.0-m_objectY) * step(m_faceWordY,-m_sandWithSacle); 

                float m_upper = m_objectY * step(m_faceWordY,m_sandWithSacle);
                float m_finalMask = m_upper + m_lower;

                f_color.rgb = m_finalMask.xxx;
                #if defined(_CLIPPING)
                    clip(f_color.a - _Cutoff);
                #endif
                
                f_color.a = m_finalMask;

                // 沙漠材质
                float2 m_viewPos =  l_unity_vertexInput.viewPos.xy;
                float m_simpleNoise = 0.0;
                 Unity_SimpleNoise_float(m_viewPos,1000,m_simpleNoise);
                 m_simpleNoise = saturate(step(0.7,m_simpleNoise)*10.0 + m_simpleNoise*0.2);
                fixed4 m_sandMatColor = pow(saturate(_SandColor + m_simpleNoise),1.0)*1.2;   
                f_color.rgb = m_sandMatColor.rgb; 

                // 降落材质
                    // 沙砾落下材质
                    float m_sinTime = sin(_Time);
                    m_viewPos.y +=  m_sinTime; 
                    Unity_SimpleNoise_float(m_viewPos,500,m_simpleNoise); 
                    // 沙柱
                    float3 m_WorldToObjectDir = l_unity_vertexInput.worldPos - m_ObjectPosition; 
                   float3 m_upDir = m_WorldToObjectDir;
                   m_upDir = saturate(pow(saturate( 1.0-abs(cross(m_upDir,m_viewDir))),_Width));
                   m_upDir *= 1.0- step(0.0,m_WorldToObjectDir.y); 
                    m_upDir *= m_WorldToObjectDir.y+1.0;
                   m_upDir *= m_simpleNoise;  
                   m_upDir = saturate(m_upDir);
                f_color.a += m_upDir.y;
                
                

                return f_color; 
            }
            ENDCG
        }
		Pass // MOVE SandFront
		{
            Name "sandFront"
            Cull back
            Blend [_SrcBlend] [_DstBlend]
            // Blend SrcAlpha OneMinusSrcAlpha
			ZWrite On

			Tags { 
                "RenderType"="Opaque" 
                "Queue"="Transparent" 
                "LightMode"="ForwardBase"}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            #pragma target 3.0
            

			// Change "shader_feature" with "pragma_compile" if you want set this keyword from c# code
			#pragma shader_feature __ _SPEC_ON
            #pragma instancing_options assumeuniformscaling
			#pragma shader_feature _CLIPPING
            
			#include "UnityCG.cginc"
            #include "sandHelper.cginc"
            
            struct unity_vertexInput {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                fixed3 objectPos : COLOR0;
                fixed2 viewPos : COLOR1;
                float3 worldNormal : TEXCOORD2;
            
        
            };

			unity_vertexInput vert(appdata_full l_appdata_full)
			{

				unity_vertexInput m_unity_vertexInput;

                m_unity_vertexInput.objectPos = l_appdata_full.vertex;
                m_unity_vertexInput.viewPos = UnityObjectToClipPos (l_appdata_full.vertex);
				// World position
				m_unity_vertexInput.worldPos = mul(unity_ObjectToWorld, l_appdata_full.vertex);
				// Clip position
				m_unity_vertexInput.pos = mul(UNITY_MATRIX_VP, float4(m_unity_vertexInput.worldPos, 1.));
                
                
				// Normal in WorldSpace
				m_unity_vertexInput.worldNormal = normalize(mul(l_appdata_full.normal, (float3x3)unity_WorldToObject));

				m_unity_vertexInput.uv = l_appdata_full.texcoord;

				return m_unity_vertexInput;
			}

			sampler2D _MainTex;

			fixed4 _LightColor0;

			
			// Diffuse
			fixed _Diffuse;
			fixed4 _DifColor;

			//Specular
			fixed _Shininess;
			fixed4 _SpecColor;
			
			//Ambient
			fixed _Ambient;
			fixed4 _AmbColor;

			// Emission
			sampler2D _EmissionTex;
			fixed4 _EmiColor;
			fixed _EmiVal;
            
            // sand
            fixed _Amount;
            fixed4 _SandColor;
            CBUFFER_START(UnityPerMaterial)
                // Transparent
                float _Cutoff;
            CBUFFER_END
			fixed4 frag(unity_vertexInput l_unity_vertexInput,fixed l_facing : VFACE) : SV_Target
			{
				fixed4 f_color = tex2D(_MainTex, l_unity_vertexInput.uv);
                fixed4 f_lighting = fixed4(1.0,1.0,1.0,1.0);

				// Light direction
				float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

				// Camera direction
				float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - l_unity_vertexInput.worldPos.xyz);

				float3 worldNormal = normalize(l_unity_vertexInput.worldNormal);

				// Compute ambient lighting
				fixed4 amb = _Ambient * _AmbColor;

				// Compute the diffuse lighting
				fixed4 NdotL = max(0., dot(worldNormal, lightDir) * _LightColor0);
				fixed4 dif = NdotL * _Diffuse * _LightColor0 * _DifColor;

				fixed4 light = dif + amb;

				// Compute the specular lighting
				#if _SPEC_ON
				float3 refl = normalize(reflect(-lightDir, worldNormal));
				float RdotV = max(0., dot(refl, viewDir));
				fixed4 spec = pow(RdotV, _Shininess) * _LightColor0 * ceil(NdotL) * _SpecColor;

				light += spec;
				#endif

				f_lighting.rgb *= light.rgb;

				// Compute emission
				fixed4 emi = tex2D(_EmissionTex, l_unity_vertexInput.uv).r * _EmiColor * _EmiVal;
				f_lighting.rgb += emi.rgb;
                // 以上是漫反射


                fixed m_objectY = l_unity_vertexInput.objectPos.y;
                m_objectY = step(0.0, m_objectY);
                
                fixed m_worldY = l_unity_vertexInput.worldPos.y;
                m_worldY = step(0.0, m_worldY);
                float3 m_ObjectPosition = UNITY_MATRIX_M._m03_m13_m23;
                fixed m_faceWordY = (l_unity_vertexInput.worldPos - m_ObjectPosition).y;

                fixed3 m_halfPos = mul(fixed3(0.0,0.5,0.0),unity_ObjectToWorld);
                fixed3 m_objectScale = f_objectScale();
                float m_inputAmount = m_objectScale.yyy * (_Amount-0.5);
                float m_sandWithSacle = m_halfPos.yyy + m_inputAmount.xxx;
                float m_lower =(1.0-m_objectY) * step(m_faceWordY,-m_sandWithSacle); 

                float m_upper = m_objectY * step(m_faceWordY,m_sandWithSacle);
                float m_finalMask = m_upper + m_lower;

                f_color.rgb = m_finalMask.xxx;
                #if defined(_CLIPPING)
                    clip(f_color.a - _Cutoff);
                #endif
                
                f_color.a = m_finalMask;

                // 沙漠材质
                
                float2 m_viewPos =  l_unity_vertexInput.viewPos.xy;
                float m_simpleNoise = 0.0;
                 Unity_SimpleNoise_float(m_viewPos,1000,m_simpleNoise);
                 m_simpleNoise = step(0.7,m_simpleNoise)*10.0 + m_simpleNoise*0.2;
                float m_colorInstance = l_facing > 0 ? 1.0 : 0.5;
                fixed4 m_sandMatColor = m_colorInstance*_SandColor + m_simpleNoise;
                m_sandMatColor = lerp(pow(saturate(m_sandMatColor),0.6 )*0.5,pow(saturate(m_sandMatColor),1.5)*1.5, saturate(f_lighting.r)); 

                f_color.rgb = m_sandMatColor.rgb;
 
				return f_color;

			}

			ENDCG

		}
        
	}
}

