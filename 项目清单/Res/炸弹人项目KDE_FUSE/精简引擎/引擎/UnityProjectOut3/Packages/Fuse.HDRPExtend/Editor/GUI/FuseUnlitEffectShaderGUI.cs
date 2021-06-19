//===========================================================================
//!
//!	@file		FuseUnlitEffectShaderGUI.cs
//!	@brief		「Fuse Unlit Effect」用インスペクターGUI
//!
//!	@author		Copyright (C) 2020 HEXADRIVE Inc. All rights reserved.
//!	@author		K.Fujihira
//!
//===========================================================================
using UnityEngine;

using PROPERTY = UnityEditor.Rendering.HighDefinition.FuseShaderGraphDef.PROPERTY;

namespace UnityEditor.Rendering.HighDefinition
{
	//===========================================================================
	//!	@class		FuseUnlitEffectShaderGUI
	//!	@brief		「Fuse Unlit Effect」シェーダーGUI
	//===========================================================================
	class FuseUnlitEffectShaderGUI : FuseHDUnlitEffectGUI
	{
		//-----------------------------------------------------------
		//!	@name 公開メソッド
		//-----------------------------------------------------------
		//@{

		//-----------------------------------------------------------
		//!	@brief	GUI表示
		//!	@param	[in]	materialEditor	マテリアルエディター
		//!	@param	[in]	properties		マテリアルのプロパティ
		//!	@return	なし
		//-----------------------------------------------------------
		public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
		{
			base.OnGUI(materialEditor, properties);

			using( var header = new MaterialHeaderScope("G4 Effect", FuseHDRPExtendEditorDef.G4_EXPANDABLE_BIT, materialEditor, colorDot: Color.green) ) {
				if( header.expanded ) {
					if( materialEditor == null ||
						properties == null ) return;

					var material = materialEditor.target as Material;
					if( material == null ) return;
	
					findProperties(properties, material);

					defaultShaderProperty(PROPERTY.TINT_COLOR.referenceName,				properties, materialEditor, material);
					defaultShaderProperty(PROPERTY.MAIN_TEX.referenceName,					properties, materialEditor, material);

					defaultShaderProperty(PROPERTY.DISSOLVE_MAP.referenceName,				properties, materialEditor, material);
					defaultShaderProperty(PROPERTY.GRADIENT_RANGE.referenceName,			properties, materialEditor, material);
					defaultShaderProperty(PROPERTY.DISSOLVE_THRESHOLD.referenceName,		properties, materialEditor, material);

					defaultShaderProperty(PROPERTY.SOFT_PARTICLE_FACTOR.referenceName,		properties, materialEditor, material);
					defaultShaderProperty(PROPERTY.COLOR_STRENGTH.referenceName,			properties, materialEditor, material);
	
					// 独自実装の一括描画時に使用するパラメーター系は、インスペクター上に出さずにデフォルト値を入れる
					if( _textureTilingOffset != null ) {
						_textureTilingOffset.vectorValue = new Vector4(1.0f, 1.0f, 0.0f, 0.0f);
					}
	
					EditorGUILayout.Space();

					EditorGUILayout.LabelField("↓「G5 HD」の設定で変更可能");
					EditorGUI.BeginDisabledGroup(true);
					materialEditor.RenderQueueField();
					EditorGUI.EndDisabledGroup();
					materialEditor.EnableInstancingField();
				}
			}
		}

		//@}


		//-----------------------------------------------------------
		//!	@name 内部メソッド
		//-----------------------------------------------------------
		//@{
	
		//-----------------------------------------------------------
		//!	@brief	プロパティの検索
		//!	@param	[in]	properties		マテリアルのプロパティ
		//!	@param	[in]	material		マテリアル
		//!	@return	なし
		//-----------------------------------------------------------
		private void findProperties(MaterialProperty[] properties, Material material)
		{
			// privateかつ呼び出し元でチェックしているのでnullチェックは省略

			_textureTilingOffset	= findProperty(PROPERTY.TEXTURE_TILING_OFFSET.referenceName,	properties,		material);

		}

		//-----------------------------------------------------------
		//!	@brief	プロパティの検索
		//!	@param	[in]	propertyName	プロパティ名
		//!	@param	[in]	properties		マテリアルのプロパティ
		//!	@param	[in]	material		マテリアル
		//!	@return	プロパティ
		//-----------------------------------------------------------
		private MaterialProperty findProperty(string propertyName, MaterialProperty[] properties, Material material)
		{
			// privateかつ呼び出し元でチェックしているのでnullチェックは省略

			if( !material.HasProperty(propertyName) ) return null;
	
			return FindProperty(propertyName, properties);
		}

		//-----------------------------------------------------------
		//!	@brief	デフォルトのシェーダープロパティ表示
		//!	@param	[in]	propertyName	プロパティ名
		//!	@param	[in]	properties		マテリアルのプロパティ
		//!	@param	[in]	materialEditor	マテリアルエディター
		//!	@param	[in]	material		マテリアル
		//!	@return	なし
		//-----------------------------------------------------------
		private void defaultShaderProperty(string propertyName, MaterialProperty[] properties, MaterialEditor materialEditor, Material material)
		{
			// privateかつ呼び出し元でチェックしているのでnullチェックは省略

			var property = findProperty(propertyName, properties, material);
			if( property == null) return;

			materialEditor.DefaultShaderProperty(property, property.displayName);
		}

		//@}


		//-----------------------------------------------------------
		//!	@name メンバ変数
		//-----------------------------------------------------------
		//@{

		private MaterialProperty	_textureTilingOffset	= null;		//!< 「テクスチャタイリングオフセット」プロパティ

		//@}
	}
}
//===========================================================================
//	END OF FILE
//===========================================================================
