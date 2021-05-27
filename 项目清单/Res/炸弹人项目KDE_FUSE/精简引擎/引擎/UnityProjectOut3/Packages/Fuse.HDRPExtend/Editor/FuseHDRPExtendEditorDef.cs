//===========================================================================
//!
//!	@file		FuseHDRPExtendEditorDef.cs
//!	@brief		HDRPエディター用定義
//!
//!	@author		Copyright (C) 2020 HEXADRIVE Inc. All rights reserved.
//!	@author		K.Fujihira
//!
//===========================================================================
#if UNITY_EDITOR
using UnityEditor.Rendering.HighDefinition;

//===========================================================================
//!	@class		FuseHDRPExtendEditorDef
//!	@brief		HDRPエディター用定義
//===========================================================================
public static class FuseHDRPExtendEditorDef
{
	//-----------------------------------------------------------
	//!	@name 定数
	//-----------------------------------------------------------
	//@{

	public static readonly string					G5_SHADER_NAME_LIT									= "Fuse/HDRP/LitWithStd";									//!< G5Litシェーダー名
	public static readonly string					G5_SHADER_NAME_LIT_WITH_STD_ALBEDO_OVERWRITABLE		= "Fuse/HDRP/LitWithStdAlbedoOverwritable";					//!< G5Litシェーダー名（Stdはアルベドカラーの上書きが可能）

	public static readonly string					G5_SHADER_NAME_PARTICLE_ALPHA_BLENDED				= "Shader Graphs/Fuse Unlit Effect Alpha Blended";				//!< G5Unlitエフェクトシェダー名 アルファブレンド
	public static readonly string					G5_SHADER_NAME_PARTICLE_ADDITIVE					= "Shader Graphs/Fuse Unlit Effect Additive";					//!< G5Unlitエフェクトシェダー名 加算ブレンド
	public static readonly string					G5_SHADER_NAME_PARTICLE_PREMULTIPLY_ALPHA_BLEND		= "Shader Graphs/Fuse Unlit Effect Premultiply Alpha Blend";	//!< G5Unlitエフェクトシェダー名 事前アルファ乗算アルファブレンド
	public static readonly string					G5_SHADER_NAME_PARTICLE_MULTIPLY					= "Shader Graphs/Fuse Unlit Effect Multiply";					//!< G5Unlitエフェクトシェダー名 乗算ブレンド
	public static readonly string					G5_SHADER_NAME_DISSOLVE_ALPHA_BLEND					= "Shader Graphs/Fuse Unlit Effect Dissolve Alpha Blend";		//!< G5Unlitエフェクトシェダー名 溶けアルファブレンド
	public static readonly string					G5_SHADER_NAME_DISSOLVE_ADD							= "Shader Graphs/Fuse Unlit Effect Dissolve Add";				//!< G5Unlitエフェクトシェダー名 溶け加算ブレンド

	public static readonly string					G4_SHADER_KEYWORD_POST_FIX		= "_G4";													//!< G4のシェーダーでキーワードとプロパティの重複回避用ポストフィックス
	public const uint								G4_EXPANDABLE_BIT				= (uint)EXPANDABLE_BIT_4;									//!< G4用マテリアルUIブロック展開ビット（アクセシビリティの回避の為uintにキャスト）
	public const uint								G5_EXPANDABLE_BIT				= (uint)EXPANDABLE_BIT_10;									//!< G5用マテリアルUIブロック展開ビット（アクセシビリティの回避の為uintにキャスト）

	private const MaterialUIBlock.Expandable		EXPANDABLE_BIT_4				= (MaterialUIBlock.Expandable)(1 << 4);						//!< マテリアルUIブロック未使用スロット4番（フリースロット）
	private const MaterialUIBlock.Expandable		EXPANDABLE_BIT_10				= (MaterialUIBlock.Expandable)(1 << 10);					//!< マテリアルUIブロック未使用スロット10番

	//! Fuseブレンドモード
	// NOTE: マテリアルに値が設定されているので、値を変更しないでください
	public enum FuseBlendMode {
		Alpha			= BlendMode.Alpha,				//!< 0 アルファブレンド
		Premultiply		= BlendMode.Premultiply,		//!< 4 事前アルファ乗算アルファブレンド
		Additive		= BlendMode.Additive,			//!< 1 加算ブレンド
		Multiply		= 99,							//!< 乗算ブレンド
	}

	//@}
}
#endif // UNITY_EDITOR
//===========================================================================
//	END OF FILE
//===========================================================================