// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// ----- license.txt
// Copyright (c) 2016 Unity Technologies

// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// -----
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace UnityEditor.Rendering.HighDefinition
{
	// ↓ Modified by HexaDrive.
    public class FuseStandardsToFuseLitWithStdMaterialUpgrader : MaterialUpgrader
	// ↑ Modified by HexaDrive.
    {
		// ↓ Add by HexaDrive.
		//-----------------------------------------------------------
		//!	@brief	同様の動作をするシェーダーか
		//!	@param	[in]	name	シェーダー名
		//!	@retval	true	同様である
		//!	@retval	false	同様ではない
		//-----------------------------------------------------------
		public delegate bool isSameBehaviorShaderName(string name);
		// ↑ Add by HexaDrive.

		// ↓ Modified by HexaDrive.
        static readonly string Standard_Spec = "none";
        static readonly string Standard_Rough = "none";
		// ↑ Modified by HexaDrive.

		// ↓ Modified by HexaDrive.
        public FuseStandardsToFuseLitWithStdMaterialUpgrader(string sourceShaderName, string destShaderName, MaterialFinalizer finalizer = null, isSameBehaviorShaderName isStandardShader = null)
		// ↑ Modified by HexaDrive.
        {
			// ↓ Add by HexaDrive.
			_isStandardShader = isStandardShader;
			// ↑ Add by HexaDrive.

            RenameShader(sourceShaderName, destShaderName, finalizer);

            RenameTexture("_MainTex", "_BaseColorMap");
            RenameColor("_Color", "_BaseColor");
            RenameFloat("_Glossiness", "_Smoothness");
            RenameTexture("_BumpMap", "_NormalMap");
            RenameFloat("_BumpScale", "_NormalScale");
            RenameTexture("_ParallaxMap", "_HeightMap");
            RenameTexture("_EmissionMap", "_EmissiveColorMap");
            RenameTexture("_DetailAlbedoMap", "_DetailMap");
            RenameFloat("_UVSec", "_UVDetail");
            SetFloat("_LinkDetailsWithBase", 0);
            RenameFloat("_DetailNormalMapScale", "_DetailNormalScale");
            RenameFloat("_Cutoff", "_AlphaCutoff");
            RenameKeywordToFloat("_ALPHATEST_ON", "_AlphaCutoffEnable", 1f, 0f);

			// ↓ Add by HexaDrive.
            RenameFloat("_Metallic", "_Metallic" + FuseHDRPExtendEditorDef.G4_SHADER_KEYWORD_POST_FIX);
			// ↑ Add by HexaDrive.

			// ↓ Modified by HexaDrive.
            if (isStandardShader(sourceShaderName))
			// ↑ Modified by HexaDrive.
            {
                SetFloat("_MaterialID", 1f);
            }

            if (sourceShaderName == Standard_Spec)
            {
                SetFloat("_MaterialID", 4f);

                RenameColor("_SpecColor", "_SpecularColor");
                RenameTexture("_SpecGlossMap", "_SpecularColorMap");
            }
        }

        public override void Convert(Material srcMaterial, Material dstMaterial)
        {
            dstMaterial.hideFlags = HideFlags.DontUnloadUnusedAsset;

            base.Convert(srcMaterial, dstMaterial);

            // ---------- Mask Map ----------

            // Metallic
            bool hasMetallic = false;
            Texture metallicMap = TextureCombiner.TextureFromColor(Color.black);
			// ↓ Modified by HexaDrive.
            if (isStandardShader(srcMaterial.shader.name) || (srcMaterial.shader.name == Standard_Rough))
			// ↑ Modified by HexaDrive.
            {
                hasMetallic = srcMaterial.GetTexture("_MetallicGlossMap") != null;
                if (hasMetallic)
                {
                    metallicMap = TextureCombiner.GetTextureSafe(srcMaterial, "_MetallicGlossMap", Color.white);
                }
                else
                {
                    metallicMap = TextureCombiner.TextureFromColor(Color.white);
                }

                // Convert _Metallic value from Gamma to Linear, or set to 1 if a map is used
                float metallicValue = Mathf.Pow(srcMaterial.GetFloat("_Metallic"), 2.2f);
                dstMaterial.SetFloat("_Metallic", hasMetallic? 1f : metallicValue);
            }

            // Occlusion
            bool hasOcclusion = srcMaterial.GetTexture("_OcclusionMap") != null;
            Texture occlusionMap = Texture2D.whiteTexture;
            if (hasOcclusion) occlusionMap = TextureCombiner.GetTextureSafe(srcMaterial, "_OcclusionMap", Color.white);

            dstMaterial.SetFloat("_AORemapMin", 1f - srcMaterial.GetFloat("_OcclusionStrength"));

            // Detail Mask
            bool hasDetailMask = srcMaterial.GetTexture("_DetailMask") != null;
            Texture detailMaskMap = Texture2D.whiteTexture;
            if (hasDetailMask) detailMaskMap = TextureCombiner.GetTextureSafe(srcMaterial, "_DetailMask", Color.white);

            // Smoothness
            bool hasSmoothness = false;
            Texture2D smoothnessMap = TextureCombiner.TextureFromColor(Color.white);

            dstMaterial.SetFloat("_SmoothnessRemapMax", srcMaterial.GetFloat("_Glossiness"));

            if (srcMaterial.shader.name == Standard_Rough)
            {
                hasSmoothness = srcMaterial.GetTexture("_SpecGlossMap") != null;

                if (hasSmoothness)
                    smoothnessMap = (Texture2D)TextureCombiner.GetTextureSafe(srcMaterial, "_SpecGlossMap", Color.grey);
            }
            else
            {
                string smoothnessTextureChannel = "_MainTex";

                if (srcMaterial.GetFloat("_SmoothnessTextureChannel") == 0)
                {
					// ↓ Modified by HexaDrive.
                    if (isStandardShader(srcMaterial.shader.name)) smoothnessTextureChannel = "_MetallicGlossMap";
					// ↑ Modified by HexaDrive.
                    if (srcMaterial.shader.name == Standard_Spec) smoothnessTextureChannel = "_SpecGlossMap";
                }

                smoothnessMap = (Texture2D)srcMaterial.GetTexture(smoothnessTextureChannel);
                if (smoothnessMap != null)
                {
                    hasSmoothness = true;

                    dstMaterial.SetFloat("_SmoothnessRemapMax", srcMaterial.GetFloat("_GlossMapScale"));

                    if (!TextureCombiner.TextureHasAlpha(smoothnessMap))
                    {
                        smoothnessMap = TextureCombiner.TextureFromColor(Color.white);
                    }
                }
                else
                {
                    smoothnessMap = TextureCombiner.TextureFromColor(Color.white);
                }
            }


            // Build the mask map
            if (hasMetallic || hasOcclusion || hasDetailMask || hasSmoothness)
            {
                Texture2D maskMap;

                TextureCombiner maskMapCombiner = new TextureCombiner(
                        metallicMap, 0,                                                     // R: Metallic from red
                        occlusionMap, 1,                                                    // G: Occlusion from green
                        detailMaskMap, 3,                                                   // B: Detail Mask from alpha
                        smoothnessMap, (srcMaterial.shader.name == Standard_Rough) ? -4 : 3 // A: Smoothness Texture from inverse greyscale for roughness setup, or alpha
                        );

                string maskMapPath = AssetDatabase.GetAssetPath(srcMaterial);
                maskMapPath = maskMapPath.Remove(maskMapPath.Length - 4) + "_MaskMap.png";
                maskMap = maskMapCombiner.Combine(maskMapPath);
                dstMaterial.SetTexture("_MaskMap", maskMap);
            }

            // Specular Setup Specific
            if (srcMaterial.shader.name == Standard_Spec)
            {
                // if there is a specular map, change the specular color to white
                if (srcMaterial.GetTexture("_SpecGlossMap") != null) dstMaterial.SetColor("_SpecularColor", Color.white);
            }

            // ---------- Height Map ----------
            bool hasHeightMap = srcMaterial.GetTexture("_ParallaxMap") != null;
            if (hasHeightMap) // Enable Parallax Occlusion Mapping
            {
                dstMaterial.SetFloat("_DisplacementMode", 2);
                dstMaterial.SetFloat("_HeightPoMAmplitude", srcMaterial.GetFloat("_Parallax") * 2f);
            }

            // ---------- Detail Map ----------
            bool hasDetailAlbedo = srcMaterial.GetTexture("_DetailAlbedoMap") != null;
            bool hasDetailNormal = srcMaterial.GetTexture("_DetailNormalMap") != null;
            if (hasDetailAlbedo || hasDetailNormal)
            {
                Texture2D detailMap;
                TextureCombiner detailCombiner = new TextureCombiner(
                        TextureCombiner.GetTextureSafe(srcMaterial, "_DetailAlbedoMap", Color.grey), 4, // Albedo (overlay)
                        TextureCombiner.GetTextureSafe(srcMaterial, "_DetailNormalMap", Color.grey), 1, // Normal Y
                        TextureCombiner.midGrey, 1,                                                     // Smoothness
                        TextureCombiner.GetTextureSafe(srcMaterial, "_DetailNormalMap", Color.grey), 0  // Normal X
                        );
                string detailMapPath = AssetDatabase.GetAssetPath(srcMaterial);
                detailMapPath = detailMapPath.Remove(detailMapPath.Length - 4) + "_DetailMap.png";
                detailMap = detailCombiner.Combine(detailMapPath);
                dstMaterial.SetTexture("_DetailMap", detailMap);
            }


            // Blend Mode
            int previousBlendMode = srcMaterial.GetInt("_Mode");
            switch (previousBlendMode)
            {
                case 0: // Opaque
                    dstMaterial.SetFloat("_SurfaceType", 0);
                    dstMaterial.SetFloat("_BlendMode", 0);
                    dstMaterial.SetFloat("_AlphaCutoffEnable", 0);
                    dstMaterial.SetFloat("_EnableBlendModePreserveSpecularLighting", 1);
                    dstMaterial.renderQueue = HDRenderQueue.ChangeType(HDRenderQueue.RenderQueueType.Opaque, 0, false);
                    break;
                case 1: // Cutout
                    dstMaterial.SetFloat("_SurfaceType", 0);
                    dstMaterial.SetFloat("_BlendMode", 0);
                    dstMaterial.SetFloat("_AlphaCutoffEnable", 1);
                    dstMaterial.SetFloat("_EnableBlendModePreserveSpecularLighting", 1);
                    dstMaterial.renderQueue = HDRenderQueue.ChangeType(HDRenderQueue.RenderQueueType.Opaque, 0, true);
                    break;
                case 2: // Fade -> Alpha with depth prepass + Disable preserve specular
                    dstMaterial.SetFloat("_SurfaceType", 1);
                    dstMaterial.SetFloat("_BlendMode", 0);
                    dstMaterial.SetFloat("_AlphaCutoffEnable", 0);
                    dstMaterial.SetFloat("_EnableBlendModePreserveSpecularLighting", 0);
                    dstMaterial.SetFloat("_TransparentDepthPrepassEnable", 1);
                    dstMaterial.renderQueue = HDRenderQueue.ChangeType(HDRenderQueue.RenderQueueType.Transparent, 0, false);
                    break;
                case 3: // Transparent -> Alpha
                    dstMaterial.SetFloat("_SurfaceType", 1);
                    dstMaterial.SetFloat("_BlendMode", 0);
                    dstMaterial.SetFloat("_AlphaCutoffEnable", 0);
                    dstMaterial.SetFloat("_EnableBlendModePreserveSpecularLighting", 1);
                    dstMaterial.renderQueue = HDRenderQueue.ChangeType(HDRenderQueue.RenderQueueType.Transparent, 0, false);
                    break;
            }

            Color hdrEmission = srcMaterial.GetColor("_EmissionColor");

            // Get the _EMISSION keyword of the Standard shader
            if ( !srcMaterial.IsKeywordEnabled("_EMISSION") )
                hdrEmission = Color.black;

            // Emission toggle of Particle Standard Surface
            if( srcMaterial.HasProperty("_EmissionEnabled") )
                if (srcMaterial.GetFloat("_EmissionEnabled") == 0)
                    hdrEmission = Color.black;

            dstMaterial.SetColor("_EmissiveColor", hdrEmission);

			// ↓ Modified by HexaDrive.
			if( !HDShaderUtils.ResetMaterialKeywords(dstMaterial) ) {
				CoreEditorUtils.RemoveMaterialKeywords(dstMaterial);
				// We need to reapply ToggleOff/Toggle keyword after reset via ApplyMaterialPropertyDrawers
				MaterialEditor.ApplyMaterialPropertyDrawers(dstMaterial);
				FuseStandardShaderWithLitGUI.setupMaterialKeywordsAndPass(dstMaterial);
				EditorUtility.SetDirty(dstMaterial);
			}
			// ↑ Modified by HexaDrive.
        }

		// ↓ Add by HexaDrive.
		//-----------------------------------------------------------
		//!	@brief	スタンダードシェーダーと同様の動作をするシェーダーか
		//!	@param	[in]	name	シェーダー名
		//!	@retval	true	同様である
		//!	@retval	false	同様ではない
		//-----------------------------------------------------------
		private bool isStandardShader(string name)
		{
			return _isStandardShader?.Invoke(name) ?? false;
		}
		// ↑ Add by HexaDrive.

		// ↓ Add by HexaDrive.
		private readonly isSameBehaviorShaderName	_isStandardShader	= null;	//!< スタンダードシェーダーと同様か判定
		// ↑ Add by HexaDrive.
    }
}
