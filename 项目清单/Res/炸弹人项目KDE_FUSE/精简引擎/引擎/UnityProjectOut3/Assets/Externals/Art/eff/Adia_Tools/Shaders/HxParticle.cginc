//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

UNITY_INSTANCING_BUFFER_START(Props)
	UNITY_DEFINE_INSTANCED_PROP(float4, _TintColor)				//Adia
#if HX_TEXTURE_TILING_OFFSET
	UNITY_DEFINE_INSTANCED_PROP(float4, _TextureTilingOffset)	//Adia
#endif //Adia
UNITY_INSTANCING_BUFFER_END(Props)

float _ColorStrength;	//Adia

//Adia
//Adia
//Adia
//Adia
//Adia
inline float4 calcPosition(float4 position)
{
	float4 pos = position;
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_BILLBOARD)
	//Adia
	pos = mul(unity_ObjectToWorld, float4(pos.xyz, 0.0f));
	pos.w = position.w;

	//Adia
	pos = mul(UNITY_MATRIX_I_V, float4(pos.xyz, 0.0f));
	pos.w = position.w;

	//Adia
	pos.xyz += unity_ObjectToWorld._14_24_34;

	//Adia
	pos = UnityWorldToClipPos(pos);
#else //Adia
	pos = UnityObjectToClipPos(pos);
#endif //Adia
	return pos;
}

//Adia
//Adia
//Adia
//Adia
//Adia
inline float2 calcTexCoord(float2 uv)
{
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET)
	float4 texTilingOffset = UNITY_ACCESS_INSTANCED_PROP(Props, _TextureTilingOffset);
	return uv * texTilingOffset.xy + texTilingOffset.zw;
#else
	return uv;
#endif //Adia
}

//Adia
//Adia
//Adia
