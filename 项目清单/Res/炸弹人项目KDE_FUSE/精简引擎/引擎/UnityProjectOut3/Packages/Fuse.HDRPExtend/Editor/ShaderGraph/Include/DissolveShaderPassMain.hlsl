//===========================================================================
//!
//!	@file		DissolveShaderPassMain.hlsl
//!	@brief		溶け表現 メインシェーダーパス
//!
//!	@author		Copyright (C) 2019 HEXADRIVE Inc. All rights reserved.
//!	@author		K.Yamada
//!
//===========================================================================

#ifndef INCLUDE_GUARD_DISSOLVE_SHADER_PASS_MAIN
#define INCLUDE_GUARD_DISSOLVE_SHADER_PASS_MAIN

//-----------------------------------------------------------
//!	@name インクルード
//-----------------------------------------------------------
//@{

#include "UnityCG.cginc"

//@}


//-----------------------------------------------------------
//!	@name 定義
//-----------------------------------------------------------
//@{

//===========================================================================
//!	@struct		vin
//!	@brief		頂点シェーダー入力データ
//===========================================================================
struct vin
{
	float4	position	: POSITION;		//!< 頂点座標
	float2	uv			: TEXCOORD0;	//!< UV座標
	float4	color		: COLOR;		//!< 色
	UNITY_VERTEX_INPUT_INSTANCE_ID		//!< インスタンスID (インスタンシング描画時)
};

//===========================================================================
//!	@struct		v2p
//!	@brief		頂点シェーダーからピクセルシェーダーへ受け渡すパラメーター
//===========================================================================
struct v2p
{
	float4	position	: SV_Position;	//!< 頂点座標
	float4	uv			: TEXCOORD0;	//!< UV座標
	float4	color		: TEXCOORD1;	//!< 接線
};

//===========================================================================
//!	@struct		pout
//!	@brief		ピクセルシェーダーから出力するデータ
//===========================================================================
struct pout
{
	half4 color		: SV_Target0;		//!< ピクセルカラー
};

//@}


//-----------------------------------------------------------
//!	@name 変数
//-----------------------------------------------------------
//@{

sampler2D	_MainTex;				//!< メインテクスチャ
float4		_MainTex_ST;			//!< メインテクスチャスケールオフセット
			
sampler2D	_DissolveMap;			//!< 溶けマップ
float4		_DissolveMap_ST;		//!< 溶けマップスケールオフセット

float		_GradientRange;			//!< グラデーションする範囲
float		_DissolveThreshold;		//!< 溶け閾値
float		_ColorStrength;			//!< 色の強さ

UNITY_INSTANCING_BUFFER_START(Props)
	UNITY_DEFINE_INSTANCED_PROP(float4, _TintColor)					//!< 色
#if HX_TEXTURE_TILING_OFFSET
	UNITY_DEFINE_INSTANCED_PROP(float4, _TextureTilingOffset)		//!< テクスチャタイリングオフセット
#endif // HX_TEXTURE_TILING_OFFSET
UNITY_INSTANCING_BUFFER_END(Props)


//@}


//-----------------------------------------------------------
//!	@name メソッド
//-----------------------------------------------------------
//@{

//-----------------------------------------------------------
//!	@brief	頂点シェーダーエントリーポイント
//!	@param	[in]	i		入力データ
//!	@param	[out]	o		データ出力先
//!	@return	なし
//-----------------------------------------------------------
void vsMain(in vin i, out v2p o)
{
	UNITY_SETUP_INSTANCE_ID(i);

	float4 pos = i.position;
#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_BILLBOARD)
	// 回転だけ先に適用
	pos = mul(unity_ObjectToWorld, float4(pos.xyz, 0.0f));
	pos.w = i.position.w;

	// カメラの逆行列適用
	pos = mul(UNITY_MATRIX_I_V, float4(pos.xyz, 0.0f));
	pos.w = i.position.w;

	// ワールド上での移動を適用
	pos.xyz += unity_ObjectToWorld._14_24_34;

	// ワールドからスクリーン座標へ変換
	pos = UnityWorldToClipPos(pos);
#else // UNITY_INSTANCING_ENABLED && defined(HX_BILLBOARD)
	pos = UnityObjectToClipPos(pos);
#endif // UNITY_INSTANCING_ENABLED && defined(HX_BILLBOARD)
	o.position = pos;

	o.uv.xy = TRANSFORM_TEX(i.uv, _MainTex);
	o.uv.zw = TRANSFORM_TEX(i.uv, _DissolveMap);

#if defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET)
	float4 texSheetAnimUV = UNITY_ACCESS_INSTANCED_PROP(Props, _TextureTilingOffset);
	o.uv.xy = o.uv.xy * texSheetAnimUV.xy + texSheetAnimUV.zw;
	o.uv.zw = o.uv.zw * texSheetAnimUV.xy + texSheetAnimUV.zw;
#endif // defined(UNITY_INSTANCING_ENABLED) && defined(HX_TEXTURE_TILING_OFFSET)

	o.color = i.color * UNITY_ACCESS_INSTANCED_PROP(Props, _TintColor);
}

//-----------------------------------------------------------
//!	@brief	ピクセルシェーダーエントリーポイント
//!	@param	[in]	i		入力データ
//!	@param	[out]	o		データ出力先
//!	@return	なし
//-----------------------------------------------------------
void psMain(in v2p i, out pout o)
{
	fixed4 texColor = tex2D(_MainTex, i.uv.xy);

	fixed dissolveMask = tex2D(_DissolveMap, i.uv.zw).r;
	fixed rangedDissolveMask = dissolveMask * max(1.0 - _GradientRange, 0) + _GradientRange;
	fixed invAlpha = (1.0f - i.color.a);
	clip(texColor.a * rangedDissolveMask - invAlpha - _DissolveThreshold);

	// NOTE: ParticleSystemの色系モジュールでHDRカラーが扱えないので、色の強さを別途マテリアル側で設定
	fixed4 color = fixed4(_ColorStrength * texColor.rgb * i.color.rgb, smoothstep(invAlpha, invAlpha + _GradientRange, rangedDissolveMask));
	color.a = saturate(color.a); // alpha should not have double-brightness applied to it, but we can't fix that legacy behaior without breaking everyone's effects, so instead clamp the output to get sensible HDR behavior (case 967476)

	o.color = color;
}

//@}

#endif // INCLUDE_GUARD_DISSOLVE_SHADER_PASS_MAIN
//===========================================================================
//	END OF FILE
//===========================================================================
