//===========================================================================
//!
//!	@file		ShaderGraphUtils.cginc
//!	@brief		シェーダーグラフ用ユーティリティ
//!
//!	@author		Copyright (C) 2020 HEXADRIVE Inc. All rights reserved.
//!	@author		K.Fujihira
//!
//===========================================================================

//-----------------------------------------------------------
//!	@brief	ブレンドモードの適用
//!	@param	[in]	texuterColor	テクスチャカラー
//!	@param	[in]	color			ブレンドカラー
//!	@param	[out]	result			出力カラー
//!	@return	なし
//-----------------------------------------------------------
inline void applyBlendMode_float(float4 texuterColor, float4 color, out float4 result)
{
#if defined(_BLENDMODE_PRE_MULTIPLY)
	result = texuterColor * color;
	result.rgb *= color.a;
#else
	result = texuterColor * color;
#endif
}

//-----------------------------------------------------------
//!	@brief	ブレンドモードの適用
//!	@param	[in]	texuterColor	テクスチャカラー
//!	@param	[in]	color			ブレンドカラー
//!	@param	[out]	result			出力カラー
//!	@return	なし
//-----------------------------------------------------------
inline void applyBlendMode_half(half4 texuterColor, half4 color, out half4 result)
{
#if defined(_BLENDMODE_PRE_MULTIPLY)
	result = texuterColor * color;
	result.rgb *= color.a;
#else
	result = texuterColor * color;
#endif
}

//===========================================================================
//	END OF FILE
//===========================================================================