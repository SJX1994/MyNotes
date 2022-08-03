#ifndef SAND_HELPER
#define SAND_HELPER

// Math
                half3 f_objectScale()
                {
                    half3x3 m = (half3x3)UNITY_MATRIX_M;
                    half3 m_objectScale = half3(
                        length( half3( m[0][0], m[1][0], m[2][0] ) ),
                        length( half3( m[0][1], m[1][1], m[2][1] ) ),
                        length( half3( m[0][2], m[1][2], m[2][2] ) )
                    );
                    return m_objectScale;
                }
                inline float unity_noise_randomValue(float2 uv)
                {
                    return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
                }
                inline float unity_noise_interpolate(float a, float b, float t)
                {
                    return (1.0 - t) * a + (t * b);
                }
                inline float unity_valueNoise(float2 uv)
                {
                    float2 i = floor(uv);
                    float2 f = frac(uv);
                    f = f * f * (3.0 - 2.0 * f);

                    uv = abs(frac(uv) - 0.5);
                    float2 c0 = i + float2(0.0, 0.0);
                    float2 c1 = i + float2(1.0, 0.0);
                    float2 c2 = i + float2(0.0, 1.0);
                    float2 c3 = i + float2(1.0, 1.0);
                    float r0 = unity_noise_randomValue(c0);
                    float r1 = unity_noise_randomValue(c1);
                    float r2 = unity_noise_randomValue(c2);
                    float r3 = unity_noise_randomValue(c3);

                    float bottomOfGrid = unity_noise_interpolate(r0, r1, f.x);
                    float topOfGrid = unity_noise_interpolate(r2, r3, f.x);
                    float t = unity_noise_interpolate(bottomOfGrid, topOfGrid, f.y);
                    return t;
                }

                void Unity_SimpleNoise_float(float2 UV, float Scale, out float Out)
                {
                    float t = 0.0;

                    float freq = pow(2.0, float(0));
                    float amp = pow(0.5, float(3-0));
                    t += unity_valueNoise(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;

                    freq = pow(2.0, float(1));
                    amp = pow(0.5, float(3-1));
                    t += unity_valueNoise(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;

                    freq = pow(2.0, float(2));
                    amp = pow(0.5, float(3-2));
                    t += unity_valueNoise(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;

                    Out = t;
                }
#endif