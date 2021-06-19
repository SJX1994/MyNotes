//===========================================================================
//!
//! @file       AlbedoOverwritableStandardShaderWithLitGUI.cs
//! @brief      アルベドカラーの上書きが可能なシェーダーGUI表示
//!
//! @author     Copyright (C) 2020 HEXADRIVE Inc. All rights reserved.
//! @author     K.Fujihira
//!
//===========================================================================
using UnityEngine;

namespace UnityEditor.Rendering.HighDefinition
{
	//===========================================================================
	//!	@class		AlbedoOverwritableStandardShaderWithLitGUI
	//!	@brief		アルベドカラーの上書きが可能なシェーダーGUI表示
	//===========================================================================
	internal class FuseAlbedoOverwritableStandardShaderWithLitGUI : FuseStandardShaderWithLitGUI
	{
		//-----------------------------------------------------------
		//!	@name 公開メソッド
		//-----------------------------------------------------------
		//@{
		
		//-----------------------------------------------------------
		//!	@brief	GUI表示 内部処理
		//!	@param	[in]	materialEditor	マテリアルエディター
		//!	@param	[in]	props			マテリアルプロパティ
		//!	@return	なし
		//-----------------------------------------------------------
		public override void onGUIInner(MaterialEditor materialEditor, MaterialProperty[] props)
		{
			base.onGUIInner(materialEditor, props);

			findProperties(props);
			EditorGUI.BeginChangeCheck();
			GUILayout.Space(10);
			GUILayout.Label("AlbedoOverwrite");
			materialEditor.ShaderProperty(_AlbedoOverwrite, _AlbedoOverwrite.displayName);
			materialEditor.TexturePropertySingleLine(new GUIContent(_AlbedoOverwritableMap.displayName), _AlbedoOverwritableMap);
			materialEditor.ShaderProperty(_AlbedoOverwriteColor, _AlbedoOverwriteColor.displayName);
		}

		//@}


		//-----------------------------------------------------------
		//!	@name 公開メソッド
		//-----------------------------------------------------------
		//@{

		//-----------------------------------------------------------
		//!	@brief	プロパティの検索
		//!	@param	[in]	properties	プロパティ
		//!	@return	なし
		//-----------------------------------------------------------
		private void findProperties(MaterialProperty[] properties)
		{
			_AlbedoOverwrite		= FindProperty("_AlbedoOverwrite",			properties);
			_AlbedoOverwritableMap	= FindProperty("_AlbedoOverwritableMap",	properties);
			_AlbedoOverwriteColor	= FindProperty("_AlbedoOverwriteColor",		properties);
		}

		//@}


		//-----------------------------------------------------------
		//!	@name メンバ変数
		//-----------------------------------------------------------
		//@{

		private MaterialProperty	_AlbedoOverwrite		= null;		//!< アルベドカラーを上書きするか
		private MaterialProperty	_AlbedoOverwritableMap	= null;		//!< アルベドカラー上書きマスクテクスチャ
		private MaterialProperty	_AlbedoOverwriteColor	= null;		//!< 上書くアルベドカラー

		//@}
	}
}
//===========================================================================
//	END OF FILE
//===========================================================================
