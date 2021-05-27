//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_SHIFT_SHADOW_SHADER_PASS_JUDGE_OUTLINE
#define INCLUDE_GUARD_SHIFT_SHADOW_SHADER_PASS_JUDGE_OUTLINE

//Adia
//Adia
//Adia
//Adia

#include "UnityCG.cginc"

//Adia


//Adia
//Adia
//Adia
//Adia

//Adia
//Adia
//Adia
//Adia
struct vin
{
	float4	position	: POSITION;		//Adia
	float2	uv			: TEXCOORD0;	//Adia
};

//Adia
//Adia
//Adia
//Adia
struct v2p
{
	float4	position	: SV_Position;	//Adia
	float2	uv			: TEXCOORD0;	//Adia
};

//Adia
//Adia
//Adia
//Adia
struct pout
{
	half4 color		: SV_Target0;		//Adia
};

//Adia


//Adia
//Adia
//Adia
//Adia

#define OUTLINE_PARAM_BASE_SIZE					(float2(1920.0, 1080.0))		//Adia
#define FOV_AXIS_OUTLINE_PARAM_BASE_SIZE		(OUTLINE_PARAM_BASE_SIZE.yy)	//Adia

#define NEAR_AT_MAXIMUM_THICKNESS	(0.05)		//Adia
#define FAR_AT_MINIMUM_THICKNESS	(0.3)		//Adia
#define MIN_THICKNESS				(4)			//Adia
#define MAX_THICKNESS				(16)		//Adia

#define SAMPLE						(40)		//Adia

#define DELTA						(360.0 / SAMPLE)											//Adia
#define TO_RADIAN(angle)			((angle) * (UNITY_PI / 180.0))								//Adia
#define ANGLE_VEC2(angle)			(float2(cos(TO_RADIAN((angle))), sin(TO_RADIAN((angle)))))	//Adia
#define ANGLE_VEC2_1(i)				ANGLE_VEC2(DELTA * (i))										//Adia
#define SAMPLE_X					(SAMPLE - (SAMPLE / 10) * 10)								//Adia
#define SAMPLE_X0					(SAMPLE / 10)												//Adia


//Adia
#define ANGLE_VEC2_10(i)	\
ANGLE_VEC2_1((i) * 10 + 0),\
ANGLE_VEC2_1((i) * 10 + 1),\
ANGLE_VEC2_1((i) * 10 + 2),\
ANGLE_VEC2_1((i) * 10 + 3),\
ANGLE_VEC2_1((i) * 10 + 4),\
ANGLE_VEC2_1((i) * 10 + 5),\
ANGLE_VEC2_1((i) * 10 + 6),\
ANGLE_VEC2_1((i) * 10 + 7),\
ANGLE_VEC2_1((i) * 10 + 8),\
ANGLE_VEC2_1((i) * 10 + 9)


//Adia
#if SAMPLE_X == 0
#define ANGLE_VEC2_X(i)
#elif SAMPLE_X == 1
#define ANGLE_VEC2_X(i)	\
ANGLE_VEC2_1((i) * 10 + 0)
#elif SAMPLE_X == 2
#define ANGLE_VEC2_X(i)	\
ANGLE_VEC2_1((i) * 10 + 0),\
ANGLE_VEC2_1((i) * 10 + 1)
#elif SAMPLE_X == 3
#define ANGLE_VEC2_X(i)	\
ANGLE_VEC2_1((i) * 10 + 0),\
ANGLE_VEC2_1((i) * 10 + 1),\
ANGLE_VEC2_1((i) * 10 + 2)
#elif SAMPLE_X == 4
#define ANGLE_VEC2_X(i)	\
ANGLE_VEC2_1((i) * 10 + 0),\
ANGLE_VEC2_1((i) * 10 + 1),\
ANGLE_VEC2_1((i) * 10 + 2),\
ANGLE_VEC2_1((i) * 10 + 3)
#elif SAMPLE_X == 5
#define ANGLE_VEC2_X(i)	\
ANGLE_VEC2_1((i) * 10 + 0),\
ANGLE_VEC2_1((i) * 10 + 1),\
ANGLE_VEC2_1((i) * 10 + 2),\
ANGLE_VEC2_1((i) * 10 + 3),\
ANGLE_VEC2_1((i) * 10 + 4)
#elif SAMPLE_X == 6
#define ANGLE_VEC2_X(i)	\
ANGLE_VEC2_1((i) * 10 + 0),\
ANGLE_VEC2_1((i) * 10 + 1),\
ANGLE_VEC2_1((i) * 10 + 2),\
ANGLE_VEC2_1((i) * 10 + 3),\
ANGLE_VEC2_1((i) * 10 + 4),\
ANGLE_VEC2_1((i) * 10 + 5)
#elif SAMPLE_X == 7
#define ANGLE_VEC2_X(i)	\
ANGLE_VEC2_1((i) * 10 + 0),\
ANGLE_VEC2_1((i) * 10 + 1),\
ANGLE_VEC2_1((i) * 10 + 2),\
ANGLE_VEC2_1((i) * 10 + 3),\
ANGLE_VEC2_1((i) * 10 + 4),\
ANGLE_VEC2_1((i) * 10 + 5),\
ANGLE_VEC2_1((i) * 10 + 6)
#elif SAMPLE_X == 8
#define ANGLE_VEC2_X(i)	\
ANGLE_VEC2_1((i) * 10 + 0),\
ANGLE_VEC2_1((i) * 10 + 1),\
ANGLE_VEC2_1((i) * 10 + 2),\
ANGLE_VEC2_1((i) * 10 + 3),\
ANGLE_VEC2_1((i) * 10 + 4),\
ANGLE_VEC2_1((i) * 10 + 5),\
ANGLE_VEC2_1((i) * 10 + 6),\
ANGLE_VEC2_1((i) * 10 + 7)
#elif SAMPLE_X == 9
#define ANGLE_VEC2_X(i)	\
ANGLE_VEC2_1((i) * 10 + 0),\
ANGLE_VEC2_1((i) * 10 + 1),\
ANGLE_VEC2_1((i) * 10 + 2),\
ANGLE_VEC2_1((i) * 10 + 3),\
ANGLE_VEC2_1((i) * 10 + 4),\
ANGLE_VEC2_1((i) * 10 + 5),\
ANGLE_VEC2_1((i) * 10 + 6),\
ANGLE_VEC2_1((i) * 10 + 7),\
ANGLE_VEC2_1((i) * 10 + 8)
#endif

//Adia
static const float2 SAMPLING_VECS[SAMPLE] = {
#if SAMPLE_X0 > 0
	ANGLE_VEC2_10(0),
#endif
#if SAMPLE_X0 > 1
	ANGLE_VEC2_10(1),
#endif
#if SAMPLE_X0 > 2
	ANGLE_VEC2_10(2),
#endif
#if SAMPLE_X0 > 3
	ANGLE_VEC2_10(3),
#endif
#if SAMPLE_X0 > 4
	ANGLE_VEC2_10(4),
#endif
#if SAMPLE_X0 > 5
	ANGLE_VEC2_10(5),
#endif
#if SAMPLE_X0 > 6
	ANGLE_VEC2_10(6),
#endif
#if SAMPLE_X0 > 7
	ANGLE_VEC2_10(7),
#endif
#if SAMPLE_X0 > 8
	ANGLE_VEC2_10(8),
#endif
#if SAMPLE_X0 > 9
	ANGLE_VEC2_10(9),
#endif
#if SAMPLE_X0 > 10
	ANGLE_VEC2_10(10),
#endif
#if SAMPLE_X0 > 11
	ANGLE_VEC2_10(11),
#endif
#if SAMPLE_X0 > 12
	ANGLE_VEC2_10(12),
#endif
#if SAMPLE_X0 > 13
	ANGLE_VEC2_10(13),
#endif
#if SAMPLE_X0 > 14
	ANGLE_VEC2_10(14),
#endif
#if SAMPLE_X0 > 15
	ANGLE_VEC2_10(15),
#endif
#if SAMPLE_X0 > 16
	ANGLE_VEC2_10(16),
#endif
#if SAMPLE_X0 > 17
	ANGLE_VEC2_10(17),
#endif

	//Adia
#if SAMPLE_X != 0
	ANGLE_VEC2_X(SAMPLE_X0),
#endif
};

//Adia


//Adia
//Adia
//Adia
//Adia

sampler2D	_MainTex;				//Adia
float4		_MainTex_ST;			//Adia
float4		_MainTex_TexelSize;		//Adia
sampler2D	_CameraDepthTexture;	//Adia

sampler2D	_silhouetteTex;			//Adia

//Adia


//Adia
//Adia
//Adia
//Adia

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
void vsMain(in vin i, out v2p o)
{
	o.position = UnityObjectToClipPos(i.position);
	o.uv = TRANSFORM_TEX(i.uv, _MainTex);
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
void psMain(in v2p i, out pout o)
{
	fixed outlineSilhouette = 0;
	float2 scale = (_MainTex_TexelSize.xy * (_MainTex_TexelSize.zw / FOV_AXIS_OUTLINE_PARAM_BASE_SIZE)) * float2(_MainTex_TexelSize.w / _MainTex_TexelSize.z, 1);
	[unroll]
	for( int j = 0; j < SAMPLE; ++j ) {
		half2 uvOffset = SAMPLING_VECS[j] * scale;
		half2 maxThicknessUV = i.uv + uvOffset * MAX_THICKNESS;
		//Adia
		if( tex2D(_silhouetteTex, maxThicknessUV).r > 0 ) {
			//Adia
			half depth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, maxThicknessUV));
			depth = clamp(depth, NEAR_AT_MAXIMUM_THICKNESS, FAR_AT_MINIMUM_THICKNESS);
			depth = (depth - NEAR_AT_MAXIMUM_THICKNESS) / (FAR_AT_MINIMUM_THICKNESS - NEAR_AT_MAXIMUM_THICKNESS);
			half thickness = lerp(MIN_THICKNESS, MAX_THICKNESS, depth);

			//Adia
			outlineSilhouette += tex2D(_silhouetteTex, i.uv + uvOffset * thickness).r;
		}
	}

	outlineSilhouette = outlineSilhouette > 0 ? 1 : 0;
	o.color = fixed2(outlineSilhouette, 1).xxxy;
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
