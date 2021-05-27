//===========================================================================
//!
//!	@file		FuseShaderGraphDef.cs
//!	@brief		Fuseシェーダーグラフ用定義
//!
//!	@author		Copyright (C) 2020 HEXADRIVE Inc. All rights reserved.
//!	@author		K.Fujihira
//!
//===========================================================================
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.ShaderGraph;
using UnityEditor.ShaderGraph.Internal;

namespace UnityEditor.Rendering.HighDefinition
{
	//===========================================================================
	//!	@class		FuseShaderGraphDef
	//!	@brief		Fuseシェーダーグラフ用定義
	//===========================================================================
	static class FuseShaderGraphDef
	{
		//-----------------------------------------------------------
		//!	@name 定義
		//-----------------------------------------------------------
		//@{

		//===========================================================================
		//!	@struct		G4ParticleInfo
		//!	@brief		G4パーティクル情報
		//===========================================================================
		public struct G4ParticleInfo
		{
			public AbstractShaderProperty[]		properties;		//!< プロパティ
			public Pass							pass;			//!< パス情報
		}

		//@}


		//-----------------------------------------------------------
		//!	@name 定数
		//-----------------------------------------------------------
		//@{

		//! G4パーティクルモード
		public enum G4ParticleMode {
			FuseCustomParticlesAdditive,					//!< 加算ブレンド
			FuseCustomParticlesAlphaBlended,				//!< アルファブレンド
			FuseCustomParticlesMultiply,					//!< 乗算ブレンド
			FuseCustomParticlesAlphaBlendPremultiply,		//!< 事前アルファ乗算アルファブレンド
			FuseCustomDissolveDissolveAdd,					//!< 溶け加算ブレンド
			FuseCustomDissolveDissolveAlphaBlend,			//!< 溶けアルファブレンド
			// Note: シリアライズしているので最後に追加
		}

		private const string	HDRP_TO_ROOT_PATH		= "../../../../../../";											//!< HDRPパッケージからルートへのパス
		private const string	TEMPLATE_PATH			= "Packages/Fuse.HDRPExtend/Editor/ShaderGraph/Template/";		//!< テンプレートフォルダへのパス
		private const string	INCLUDE_PATH			= "Packages/Fuse.HDRPExtend/Editor/ShaderGraph/Include/";		//!< インクルードフォルダへのパス

		//!	プロパティ
		public struct PROPERTY
		{
			private const bool	HIDDEN	= true;	//!< 非表示フラグ

			//! 色合いプロパティ
			public static readonly AbstractShaderProperty		TINT_COLOR				= new ColorShaderProperty {
				displayName = "Tint Color",
				overrideReferenceName = "_TintColor",
				value = new Color(0.5f, 0.5f, 0.5f, 0.5f),
				hidden = HIDDEN,
			};

			//! メインテクスチャプロパティ
			public static readonly AbstractShaderProperty		MAIN_TEX				= new Texture2DTilingOffsetShaderProperty {
				displayName = "Texture",
				overrideReferenceName = "_MainTex",
				defaultType = Texture2DTilingOffsetShaderProperty.DefaultType.White,
				hidden = HIDDEN,
			};

			//! ソフトパーティクルファクタープロパティ
			public static readonly AbstractShaderProperty		SOFT_PARTICLE_FACTOR	= new Vector1ShaderProperty {
				displayName = "Soft Particles Factor",
				overrideReferenceName = "_InvFade",
				value = 1.0f,
				floatType = FloatType.Slider,
				rangeValues = new Vector2(0.01f, 3.0f),
				hidden = HIDDEN,
			};

			//! カラーの強さプロパティ
			public static readonly AbstractShaderProperty		COLOR_STRENGTH			= new Vector1ShaderProperty {
				displayName = "Color Strength",
				overrideReferenceName = "_ColorStrength",
				value = 1.0f,
				hidden = HIDDEN,
			};

			//! テクスチャタイリングプロパティ
			public static readonly AbstractShaderProperty		TEXTURE_TILING_OFFSET	= new Vector4ShaderProperty {
				displayName = "Texture Tiling Offset",
				overrideReferenceName = "_TextureTilingOffset",
				value = new Vector4(1.0f, 1.0f, 0.0f, 0.0f),
				hidden = HIDDEN,
			};

			//! 溶けマップテクスチャプロパティ
			public static readonly AbstractShaderProperty		DISSOLVE_MAP			= new Texture2DTilingOffsetShaderProperty {
				displayName = "Dissolve Map",
				overrideReferenceName = "_DissolveMap",
				defaultType = Texture2DTilingOffsetShaderProperty.DefaultType.White,
				hidden = HIDDEN,
			};

			//! 溶け勾配プロパティ
			public static readonly AbstractShaderProperty		GRADIENT_RANGE	= new Vector1ShaderProperty {
				displayName = "Gradient Range",
				overrideReferenceName = "_GradientRange",
				value = 0.2f,
				floatType = FloatType.Slider,
				rangeValues = new Vector2(0.0f, 1.0f),
				hidden = HIDDEN,
			};

			//! 溶けしきい値プロパティ
			public static readonly AbstractShaderProperty		DISSOLVE_THRESHOLD	= new Vector1ShaderProperty {
				displayName = "Dissolve Threshold",
				overrideReferenceName = "_DissolveThreshold",
				value = 0.01f,
				floatType = FloatType.Slider,
				rangeValues = new Vector2(0.0f, 1.0f),
				hidden = HIDDEN,
			};
		}

		//! G4パーティクル情報一覧
		public static readonly Dictionary<G4ParticleMode, G4ParticleInfo>		G4_PARTICLE_INFOS	= new Dictionary<G4ParticleMode, G4ParticleInfo> {
			//! 加算ブレンド
			[G4ParticleMode.FuseCustomParticlesAdditive]	= new G4ParticleInfo {
				properties = new AbstractShaderProperty[] {
					PROPERTY.TINT_COLOR,
					PROPERTY.MAIN_TEX,
					PROPERTY.SOFT_PARTICLE_FACTOR,
					PROPERTY.COLOR_STRENGTH,
					PROPERTY.TEXTURE_TILING_OFFSET,
				},
				pass = new Pass()
				{
					TemplateName = makeTemplatePath("HxParticleAdditivePass.template"),
					MaterialName = "Unlit",
					ExtraDefines = new List<string>()
					{
					},
					Includes = new List<string>()
					{
						makeIncludeText("HxParticle.cginc"),
					},
					PixelShaderSlots = new List<int>()
					{
					},
					VertexShaderSlots = new List<int>()
					{
					},
					UseInPreview = true,

					OnGeneratePassImpl = (IMasterNode node, ref Pass pass) =>
					{
					}
				},
			},

			//! アルファブレンド
			[G4ParticleMode.FuseCustomParticlesAlphaBlended]	= new G4ParticleInfo {
				properties = new AbstractShaderProperty[] {
					PROPERTY.TINT_COLOR,
					PROPERTY.MAIN_TEX,
					PROPERTY.SOFT_PARTICLE_FACTOR,
					PROPERTY.COLOR_STRENGTH,
					PROPERTY.TEXTURE_TILING_OFFSET,
				},
				pass = new Pass()
				{
					TemplateName = makeTemplatePath("HxParticleAlphaBlendedPass.template"),
					MaterialName = "Unlit",
					ExtraDefines = new List<string>()
					{
					},
					Includes = new List<string>()
					{
						makeIncludeText("HxParticle.cginc"),
					},
					PixelShaderSlots = new List<int>()
					{
					},
					VertexShaderSlots = new List<int>()
					{
					},
					UseInPreview = true,

					OnGeneratePassImpl = (IMasterNode node, ref Pass pass) =>
					{
					}
				},
			},

			//! 乗算ブレンド
			[G4ParticleMode.FuseCustomParticlesMultiply]	= new G4ParticleInfo {
				properties = new AbstractShaderProperty[] {
					PROPERTY.TINT_COLOR,
					PROPERTY.MAIN_TEX,
					PROPERTY.SOFT_PARTICLE_FACTOR,
					PROPERTY.COLOR_STRENGTH,
					PROPERTY.TEXTURE_TILING_OFFSET,
				},
				pass = new Pass()
				{
					TemplateName = makeTemplatePath("HxParticleMultiplyPass.template"),
					MaterialName = "Unlit",
					ExtraDefines = new List<string>()
					{
					},
					Includes = new List<string>()
					{
						makeIncludeText("HxParticle.cginc"),
					},
					PixelShaderSlots = new List<int>()
					{
					},
					VertexShaderSlots = new List<int>()
					{
					},
					UseInPreview = true,

					OnGeneratePassImpl = (IMasterNode node, ref Pass pass) =>
					{
					}
				},
			},

			//! 事前アルファ乗算アルファブレンド
			[G4ParticleMode.FuseCustomParticlesAlphaBlendPremultiply]	= new G4ParticleInfo {
				properties = new AbstractShaderProperty[] {
					PROPERTY.TINT_COLOR,
					PROPERTY.MAIN_TEX,
					PROPERTY.SOFT_PARTICLE_FACTOR,
					PROPERTY.COLOR_STRENGTH,
					PROPERTY.TEXTURE_TILING_OFFSET,
				},
				pass = new Pass()
				{
					TemplateName = makeTemplatePath("HxParticleAlphaBlendPremultiplyPass.template"),
					MaterialName = "Unlit",
					ExtraDefines = new List<string>()
					{
					},
					Includes = new List<string>()
					{
						makeIncludeText("HxParticle.cginc"),
					},
					PixelShaderSlots = new List<int>()
					{
					},
					VertexShaderSlots = new List<int>()
					{
					},
					UseInPreview = true,

					OnGeneratePassImpl = (IMasterNode node, ref Pass pass) =>
					{
					}
				},
			},

			//! 溶け加算ブレンド
			[G4ParticleMode.FuseCustomDissolveDissolveAdd]	= new G4ParticleInfo {
				properties = new AbstractShaderProperty[] {
					PROPERTY.MAIN_TEX,
					PROPERTY.DISSOLVE_MAP,
					PROPERTY.GRADIENT_RANGE,
					PROPERTY.DISSOLVE_THRESHOLD,
					PROPERTY.TINT_COLOR,
					PROPERTY.COLOR_STRENGTH,
					PROPERTY.TEXTURE_TILING_OFFSET,
				},
				pass = new Pass()
				{
					TemplateName = makeTemplatePath("DissolveAddPass.template"),
					MaterialName = "Unlit",
					ExtraDefines = new List<string>()
					{
					},
					Includes = new List<string>()
					{
						makeIncludeText("DissolveShaderPassMain.hlsl"),
					},
					PixelShaderSlots = new List<int>()
					{
					},
					VertexShaderSlots = new List<int>()
					{
					},
					UseInPreview = true,

					OnGeneratePassImpl = (IMasterNode node, ref Pass pass) =>
					{
					}
				},
			},

			//! 溶けアルファブレンド
			[G4ParticleMode.FuseCustomDissolveDissolveAlphaBlend]	= new G4ParticleInfo {
				properties = new AbstractShaderProperty[] {
					PROPERTY.MAIN_TEX,
					PROPERTY.DISSOLVE_MAP,
					PROPERTY.GRADIENT_RANGE,
					PROPERTY.DISSOLVE_THRESHOLD,
					PROPERTY.TINT_COLOR,
					PROPERTY.COLOR_STRENGTH,
					PROPERTY.TEXTURE_TILING_OFFSET,
				},
				pass = new Pass()
				{
					TemplateName = makeTemplatePath("DissolveAlphaBlendPass.template"),
					MaterialName = "Unlit",
					ExtraDefines = new List<string>()
					{
					},
					Includes = new List<string>()
					{
						makeIncludeText("DissolveShaderPassMain.hlsl"),
					},
					PixelShaderSlots = new List<int>()
					{
					},
					VertexShaderSlots = new List<int>()
					{
					},
					UseInPreview = true,

					OnGeneratePassImpl = (IMasterNode node, ref Pass pass) =>
					{
					}
				},
			},
		};

		//@}


		//-----------------------------------------------------------
		//!	@name 静的公開メソッド
		//-----------------------------------------------------------
		//@{

		//-----------------------------------------------------------
		//!	@brief	テンプレートファイルのパスを作成
		//!	@param	[in]	fileName	ファイル名
		//!	@return	パス
		//-----------------------------------------------------------
		public static string makeTemplatePath(string fileName)
		{
			return HDRP_TO_ROOT_PATH + TEMPLATE_PATH + fileName;
		}

		//-----------------------------------------------------------
		//!	@brief	インクルードテキストの作成
		//!	@param	[in]	fileName	ファイル名
		//!	@return	パス
		//-----------------------------------------------------------
		public static string makeIncludeText(string fileName)
		{
			return $@"#include ""{INCLUDE_PATH}{fileName}""";
		}

		//@}
	}
}
//===========================================================================
//	END OF FILE
//===========================================================================
