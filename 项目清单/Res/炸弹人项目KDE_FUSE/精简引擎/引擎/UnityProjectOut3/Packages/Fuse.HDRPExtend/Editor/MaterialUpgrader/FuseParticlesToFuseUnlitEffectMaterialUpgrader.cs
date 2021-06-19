//===========================================================================
//!
//!	@file		FuseParticlesToFuseUnlitEffectMaterialUpgrader.cs
//!	@brief		G4パーティクルマテリアルからG5Unlitエフェクトマテリアルのアップグレーダー
//!
//!	@author		Copyright (C) 2020 HEXADRIVE Inc. All rights reserved.
//!	@author		K.Fujihira
//!
//===========================================================================
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

using static UnityEngine.Rendering.HighDefinition.HDMaterialProperties;

namespace UnityEditor.Rendering.HighDefinition
{
	//===========================================================================
	//!	@class		FuseParticlesToFuseUnlitEffectMaterialUpgrader
	//!	@brief		G4パーティクルマテリアルからG5Unlitエフェクトマテリアルのアップグレーダー
	//===========================================================================
	public class FuseParticlesToFuseUnlitEffectMaterialUpgrader : MaterialUpgrader
	{
		//-----------------------------------------------------------
		//!	@name 定数
		//-----------------------------------------------------------
		//@{

		private const int	RENDER_QUEUE_STEP	= 5;	//!< レンダーキューを割る大きさ

		//@}


		//-----------------------------------------------------------
		//!	@name 公開メソッド
		//-----------------------------------------------------------
		//@{

		//-----------------------------------------------------------
		//!	@brief	コンストラクタ
		//!	@param	[in]	sourceShaderName	元のシェーダー名
		//!	@param	[in]	destShaderName		出力のシェーダー名
		//!	@param	[in]	finalizer			ファイナライザ
		//-----------------------------------------------------------
		public FuseParticlesToFuseUnlitEffectMaterialUpgrader(string sourceShaderName, string destShaderName, MaterialFinalizer finalizer = null)
		{
			RenameShader(sourceShaderName, destShaderName, finalizer);

			RenameColor("_TintColor",			"_TintColor_G5");
			RenameTexture("_MainTex",			"_MainTexture_G5");
			RenameColor("_MainTex_ST",			"_MainTextureTilingOffset_G5");
			RenameFloat("_InvFade",				"_SoftParticleFactor_G5");
			RenameFloat("_ColorStrength",		"_ColorStrength_G5");
			RenameTexture("_DissolveMap",		"_DissolveMap_G5");
			RenameColor("_DissolveMap_ST",		"_DissolveMapTilingOffset_G5");
			RenameFloat("_GradientRange",		"_GradientRange_G5");
			RenameFloat("_DissolveThreshold",	"_DissolveThreshold_G5");
		}

		//-----------------------------------------------------------
		//!	@brief	コンバート
		//!	@param	[in]	srcMaterial	元マテリアル
		//!	@param	[in]	dstMaterial	出力マテリアル
		//!	@return	なし
		//-----------------------------------------------------------
		public override void Convert(Material srcMaterial, Material dstMaterial)
		{
			if( srcMaterial == null ||
				dstMaterial == null ) return;

			dstMaterial.hideFlags = HideFlags.DontUnloadUnusedAsset;

			base.Convert(srcMaterial, dstMaterial);

			// レンダラーキュー変換
			{
				const int TRANSPARENT_QUEUE = (int)RenderQueue.Transparent;
				const int RANGE = HDRenderQueue.Priority.TransparentLast - HDRenderQueue.Priority.TransparentFirst;
				const int RANGE_HALF = RANGE / 2;

				var renderQueue = srcMaterial.renderQueue;
				var priority = renderQueue - TRANSPARENT_QUEUE;
				var newPriority = 0;

				string getPriorityRangeText() { return $"-{RANGE_HALF}～{RANGE_HALF}"; }
				// レンダーキューの警告ログ出力
				void warningRenderQueueLog(string text)
				{
					Debug.LogWarning($"renderQueueからSortingPriority({getPriorityRangeText()})の変換で誤差が発生しています。\n{text}\nマテリアル名 : {dstMaterial.name}\n変換したrenderQueue : {renderQueue} → {newPriority}\n\n");
				}

				if( priority < -RANGE_HALF ) {
					newPriority = -RANGE_HALF;
					warningRenderQueueLog($"元のrenderQueueが{TRANSPARENT_QUEUE + -RENDER_QUEUE_STEP}を下回っている為、{getPriorityRangeText()}に変換できませんでした。");
				}
				if( priority <= 0 ) {
					newPriority = priority;
				}
				else {
					var floatPriority = priority / (float)RENDER_QUEUE_STEP;
					newPriority = Mathf.CeilToInt(floatPriority);
					if( newPriority != floatPriority ) {
						warningRenderQueueLog($"元のrenderQueueが{RENDER_QUEUE_STEP}の倍数でない為、繰り上げが発生しました");
					}
					if( newPriority > RANGE_HALF ) {
						newPriority = RANGE_HALF;
						warningRenderQueueLog($"元のrenderQueueが{TRANSPARENT_QUEUE + RENDER_QUEUE_STEP * RANGE_HALF}を上回っている為、{getPriorityRangeText()}に変換できませんでした。");
					}
				}
				dstMaterial.SetInt(kTransparentSortPriority, newPriority);
			}

			if( !HDShaderUtils.ResetMaterialKeywords(dstMaterial) ) {
				CoreEditorUtils.RemoveMaterialKeywords(dstMaterial);
				// We need to reapply ToggleOff/Toggle keyword after reset via ApplyMaterialPropertyDrawers
				MaterialEditor.ApplyMaterialPropertyDrawers(dstMaterial);
				FuseHDUnlitEffectGUI.SetupMaterialKeywordsAndPass(dstMaterial);
				EditorUtility.SetDirty(dstMaterial);
			}
		}

		//@}
	}
}
//===========================================================================
//	END OF FILE
//===========================================================================
