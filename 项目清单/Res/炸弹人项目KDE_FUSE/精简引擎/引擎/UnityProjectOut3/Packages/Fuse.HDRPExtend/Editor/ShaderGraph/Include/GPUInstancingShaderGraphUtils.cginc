//===========================================================================
//!
//!	@file		GPUInstancingShaderGraphUtils.cginc
//!	@brief		GPUインスタンシングを利用するシェーダーグラフ用ユーティリティ
//!
//!	@author		Copyright (C) 2020 HEXADRIVE Inc. All rights reserved.
//!	@author		K.Fujihira
//!
//===========================================================================

#if defined(UNITY_INSTANCING_ENABLED)
UNITY_INSTANCING_BUFFER_START(Props)

#if defined(HX_TINT_COLOR)
	UNITY_DEFINE_INSTANCED_PROP(float4, _TintColor)				//! 色
#endif // defined(HX_TINT_COLOR)

#if defined(HX_TEXTURE_TILING_OFFSET)
	UNITY_DEFINE_INSTANCED_PROP(float4, _TextureTilingOffset)	//!< テクスチャタイリングオフセット値
#endif // defined(HX_TEXTURE_TILING_OFFSET)

#if defined(HX_TEXCOORD_VALUE_0)
	UNITY_DEFINE_INSTANCED_PROP(float4, _TexcoordValue0)		//!< テクスコード0値
#endif // defined(HX_TEXCOORD_VALUE_0)

#if defined(HX_TEXCOORD_VALUE_1)
	UNITY_DEFINE_INSTANCED_PROP(float4, _TexcoordValue1)		//!< テクスコード1値
#endif // defined(HX_TEXCOORD_VALUE_1)

#if defined(HX_TEXCOORD_VALUE_2)
	UNITY_DEFINE_INSTANCED_PROP(float4, _TexcoordValue2)		//!< テクスコード2値
#endif // defined(HX_TEXCOORD_VALUE_2)

#if defined(HX_TEXCOORD_VALUE_3)
	UNITY_DEFINE_INSTANCED_PROP(float4, _TexcoordValue3)		//!< テクスコード3値
#endif // defined(HX_TEXCOORD_VALUE_3)

UNITY_INSTANCING_BUFFER_END(Props)
#endif // defined(UNITY_INSTANCING_ENABLED)

//-----------------------------------------------------------
//!	@brief	ビルボードの適用
//!	@param	[in]	inPosition	入力頂点座標
//!	@param	[out]	outPosition	出力頂点座標
//!	@return	なし
//-----------------------------------------------------------
inline void applyBillboard_float(float4 inPosition, out float4 outPosition)
{
	outPosition = inPosition;

#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_BILLBOARD)
	// 回転だけ先に適用
	outPosition = mul(UNITY_MATRIX_M, float4(outPosition.xyz, 0.0f));
	outPosition.w = inPosition.w;
 
	// カメラの逆行列適用
	outPosition = mul(UNITY_MATRIX_I_V, float4(outPosition.xyz, 0.0f));
	outPosition.w = inPosition.w;

	outPosition = mul(UNITY_MATRIX_I_M, float4(outPosition.xyz, 0.0f));
	outPosition.w = inPosition.w;
#endif // defined(UNITY_INSTANCING_ENABLED) && defined(HX_BILLBOARD)
}

//-----------------------------------------------------------
//!	@brief	ビルボードの適用
//!	@param	[in]	inPosition	入力頂点座標
//!	@param	[out]	outPosition	出力頂点座標
//!	@return	なし
//-----------------------------------------------------------
inline void applyBillboard_half(half4 inPosition, out half4 outPosition)
{
	outPosition = inPosition;

#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_BILLBOARD)
	// 回転だけ先に適用
	outPosition = mul(UNITY_MATRIX_M, half4(outPosition.xyz, 0.0f));
	outPosition.w = inPosition.w;
 
	// カメラの逆行列適用
	outPosition = mul(UNITY_MATRIX_I_V, half4(outPosition.xyz, 0.0f));
	outPosition.w = inPosition.w;

	outPosition = mul(UNITY_MATRIX_I_M, half4(outPosition.xyz, 0.0f));
	outPosition.w = inPosition.w;
#endif // defined(UNITY_INSTANCING_ENABLED) && defined(HX_BILLBOARD)
}

//-----------------------------------------------------------
//!	@brief	テクスチャ座標の計算
//!	@param	[in]	uv							テクスチャ座標
//!	@param	[in]	tilingOffset				タイリングとオフセット
//!	@param	[in]	tilingOffsetInstancing		タイリングとオフセット（インスタンシング時の値）
//!	@param	[out]	outUV						出力テクスチャ座標
//!	@return	なし
//-----------------------------------------------------------
inline void calcTexCoord_float(float2 uv, float4 tilingOffset, float4 tilingOffsetInstancing, out float2 outUV)
{
	outUV = uv * tilingOffset.xy + tilingOffset.zw;

#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET)
	// オフセットずらした後にタイリング計算しているが、G4パーティクルでの計算に準拠
	outUV *= tilingOffsetInstancing.xy + tilingOffsetInstancing.zw;
#endif // defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET)
}

//-----------------------------------------------------------
//!	@brief	テクスチャ座標の計算
//!	@param	[in]	uv							テクスチャ座標
//!	@param	[in]	tilingOffset				タイリングとオフセット
//!	@param	[in]	tilingOffsetInstancing		タイリングとオフセット（インスタンシング時の値）
//!	@param	[out]	outUV						出力テクスチャ座標
//!	@return	なし
//-----------------------------------------------------------
inline void calcTexCoord_half(half2 uv, half4 tilingOffset, half4 tilingOffsetInstancing, out half2 outUV)
{
	outUV = uv * tilingOffset.xy + tilingOffset.zw;

#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET)
	// オフセットずらした後にタイリング計算しているが、G4パーティクルでの計算に準拠
	outUV *= tilingOffsetInstancing.xy + tilingOffsetInstancing.zw;
#endif // defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET)
}

//-----------------------------------------------------------
//!	@brief	TintColorの取得（インスタンシングに対応）
//!	@param	[in]	tintColor		元のTintColor
//!	@param	[out]	outTintColor	出力TintColor
//!	@return	なし
//-----------------------------------------------------------
inline void getTintColor_float(float4 tintColor, out float4 outTintColor)
{
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TINT_COLOR)
	outTintColor = UNITY_ACCESS_INSTANCED_PROP(Props, _TintColor);
#else // defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET)
	outTintColor = tintColor;
#endif // !(defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET))
}

//-----------------------------------------------------------
//!	@brief	TintColorの取得（インスタンシングに対応）
//!	@param	[in]	tintColor		元のTintColor
//!	@param	[out]	outTintColor	出力TintColor
//!	@return	なし
//-----------------------------------------------------------
inline void getTintColor_half(half4 tintColor, out half4 outTintColor)
{
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TINT_COLOR)
	outTintColor = UNITY_ACCESS_INSTANCED_PROP(Props, _TintColor);
#else // defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET)
	outTintColor = tintColor;
#endif // !(defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET))
}

//-----------------------------------------------------------
//!	@brief	TilingOffsetの取得（インスタンシングに対応）
//!	@param	[out]	outTilingOffset		出力TilingOffset
//!	@return	なし
//-----------------------------------------------------------
inline void getTilingOffset_float(out float4 outTilingOffset)
{
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET)
	outTilingOffset = UNITY_ACCESS_INSTANCED_PROP(Props, _TextureTilingOffset);
#else // defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET)
	outTilingOffset = float4(1.0, 1.0, 0.0, 0.0);
#endif // !(defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET))
}

//-----------------------------------------------------------
//!	@brief	TilingOffsetの取得（インスタンシングに対応）
//!	@param	[out]	outTilingOffset		出力TilingOffset
//!	@return	なし
//-----------------------------------------------------------
inline void getTilingOffset_half(out half4 outTilingOffset)
{
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET)
	outTilingOffset = UNITY_ACCESS_INSTANCED_PROP(Props, _TextureTilingOffset);
#else // defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET)
	outTilingOffset = half4(1.0, 1.0, 0.0, 0.0);
#endif // !(defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET))
}

//-----------------------------------------------------------
//!	@brief	テクスコード0の取得（インスタンシングに対応）
//!	@param	[in]	value		インスタシング無効時の値
//!	@param	[out]	outValue	出力テクスコード
//!	@return	なし
//-----------------------------------------------------------
inline void getTexcoordValue0_float(float4 value, out float4 outValue)
{
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_0)
	outValue = UNITY_ACCESS_INSTANCED_PROP(Props, _TexcoordValue0);
#else // defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_0)
	outValue = value;
#endif // !(defined(UNITY_INSTANCING_ENABLED)  && defined(HX_TEXCOORD_VALUE_0))
}

//-----------------------------------------------------------
//!	@brief	テクスコード0の取得（インスタンシングに対応）
//!	@param	[in]	value		インスタシング無効時の値
//!	@param	[out]	outValue	出力テクスコード
//!	@return	なし
//-----------------------------------------------------------
inline void getTexcoordValue0_half(float4 value, out half4 outValue)
{
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_0)
	outValue = UNITY_ACCESS_INSTANCED_PROP(Props, _TexcoordValue0);
#else // defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_0)
	outValue = value;
#endif // !(defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_0))
}

//-----------------------------------------------------------
//!	@brief	テクスコード1の取得（インスタンシングに対応）
//!	@param	[in]	value		インスタシング無効時の値
//!	@param	[out]	outValue	出力テクスコード
//!	@return	なし
//-----------------------------------------------------------
inline void getTexcoordValue1_float(float4 value, out float4 outValue)
{
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_1)
	outValue = UNITY_ACCESS_INSTANCED_PROP(Props, _TexcoordValue1);
#else // defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_1)
	outValue = value;
#endif // !(defined(UNITY_INSTANCING_ENABLED)  && defined(HX_TEXCOORD_VALUE_1))
}

//-----------------------------------------------------------
//!	@brief	テクスコード1の取得（インスタンシングに対応）
//!	@param	[in]	value		インスタシング無効時の値
//!	@param	[out]	outValue	出力テクスコード
//!	@return	なし
//-----------------------------------------------------------
inline void getTexcoordValue1_half(float4 value, out half4 outValue)
{
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_1)
	outValue = UNITY_ACCESS_INSTANCED_PROP(Props, _TexcoordValue1);
#else // defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_1)
	outValue = value;
#endif // !(defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_1))
}

//-----------------------------------------------------------
//!	@brief	テクスコード2の取得（インスタンシングに対応）
//!	@param	[in]	value		インスタシング無効時の値
//!	@param	[out]	outValue	出力テクスコード
//!	@return	なし
//-----------------------------------------------------------
inline void getTexcoordValue2_float(float4 value, out float4 outValue)
{
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_2)
	outValue = UNITY_ACCESS_INSTANCED_PROP(Props, _TexcoordValue2);
#else // defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_2)
	outValue = value;
#endif // !(defined(UNITY_INSTANCING_ENABLED)  && defined(HX_TEXCOORD_VALUE_2))
}

//-----------------------------------------------------------
//!	@brief	テクスコード2の取得（インスタンシングに対応）
//!	@param	[in]	value		インスタシング無効時の値
//!	@param	[out]	outValue	出力テクスコード
//!	@return	なし
//-----------------------------------------------------------
inline void getTexcoordValue2_half(float4 value, out half4 outValue)
{
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_2)
	outValue = UNITY_ACCESS_INSTANCED_PROP(Props, _TexcoordValue2);
#else // defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_2)
	outValue = value;
#endif // !(defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_2))
}

//-----------------------------------------------------------
//!	@brief	テクスコード3の取得（インスタンシングに対応）
//!	@param	[in]	value		インスタシング無効時の値
//!	@param	[out]	outValue	出力テクスコード
//!	@return	なし
//-----------------------------------------------------------
inline void getTexcoordValue3_float(float4 value, out float4 outValue)
{
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_3)
	outValue = UNITY_ACCESS_INSTANCED_PROP(Props, _TexcoordValue3);
#else // defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_3)
	outValue = value;
#endif // !(defined(UNITY_INSTANCING_ENABLED)  && defined(HX_TEXCOORD_VALUE_3))
}

//-----------------------------------------------------------
//!	@brief	テクスコード3の取得（インスタンシングに対応）
//!	@param	[in]	value		インスタシング無効時の値
//!	@param	[out]	outValue	出力テクスコード
//!	@return	なし
//-----------------------------------------------------------
inline void getTexcoordValue3_half(float4 value, out half4 outValue)
{
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_3)
	outValue = UNITY_ACCESS_INSTANCED_PROP(Props, _TexcoordValue3);
#else // defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_3)
	outValue = value;
#endif // !(defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXCOORD_VALUE_3))
}

//===========================================================================
//	END OF FILE
//===========================================================================