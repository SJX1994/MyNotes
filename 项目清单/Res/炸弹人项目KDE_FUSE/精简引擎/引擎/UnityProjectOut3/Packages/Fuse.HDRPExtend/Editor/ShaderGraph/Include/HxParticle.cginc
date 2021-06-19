//===========================================================================
//!
//!	@file		HxParticle.cginc
//!	@brief		パーティクル用 共通定義
//!
//!	@author		Copyright (C) 2018 HEXADRIVE Inc. All rights reserved.
//!	@author		K.Yamada
//!
//===========================================================================

UNITY_INSTANCING_BUFFER_START(Props)
	UNITY_DEFINE_INSTANCED_PROP(float4, _TintColor)				//! 色
#if HX_TEXTURE_TILING_OFFSET
	UNITY_DEFINE_INSTANCED_PROP(float4, _TextureTilingOffset)	//!< テクスチャタイリングオフセット値
#endif // HX_TEXTURE_TILING_OFFSET
UNITY_INSTANCING_BUFFER_END(Props)

float _ColorStrength;	//!< 色の強さ (インスタンスごとではなく、マテリアル単位で設定)

//-----------------------------------------------------------
//!	@brief	頂点座標の計算
//!	@param	[in]	position	頂点座標
//!	@return	頂点座標
//-----------------------------------------------------------
inline float4 calcPosition(float4 position)
{
	float4 pos = position;
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_BILLBOARD)
	// 回転だけ先に適用
	pos = mul(unity_ObjectToWorld, float4(pos.xyz, 0.0f));
	pos.w = position.w;

	// カメラの逆行列適用
	pos = mul(UNITY_MATRIX_I_V, float4(pos.xyz, 0.0f));
	pos.w = position.w;

	// ワールド上での移動を適用
	pos.xyz += unity_ObjectToWorld._14_24_34;

	// ワールドからスクリーン座標へ変換
	pos = UnityWorldToClipPos(pos);
#else // UNITY_INSTANCING_ENABLED && defined(HX_BILLBOARD)
	pos = UnityObjectToClipPos(pos);
#endif // UNITY_INSTANCING_ENABLED && defined(HX_BILLBOARD)
	return pos;
}

//-----------------------------------------------------------
//!	@brief	テクスチャ座標の計算
//!	@param	[in]	uv		テクスチャ座標
//!	@return	テクスチャ座標
//-----------------------------------------------------------
inline float2 calcTexCoord(float2 uv)
{
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET)
	float4 texTilingOffset = UNITY_ACCESS_INSTANCED_PROP(Props, _TextureTilingOffset);
	return uv * texTilingOffset.xy + texTilingOffset.zw;
#else
	return uv;
#endif // defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET)
}

//===========================================================================
//	END OF FILE
//===========================================================================
