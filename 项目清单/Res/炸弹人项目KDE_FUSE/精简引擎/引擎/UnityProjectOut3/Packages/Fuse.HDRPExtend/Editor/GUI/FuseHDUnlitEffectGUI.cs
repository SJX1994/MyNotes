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
using UnityEngine.Rendering;

// Include material common properties names
using static UnityEngine.Rendering.HighDefinition.HDMaterialProperties;

namespace UnityEditor.Rendering.HighDefinition
{
    /// <summary>
    /// GUI for HDRP Unlit shader graphs
    /// </summary>
	// « Modified by HexaDrive.
    class FuseHDUnlitEffectGUI : FuseHDShaderGUI
	// ª Modified by HexaDrive.
    {
        // For surface option shader graph we only want all unlit features but alpha clip, double sided mode and back then front rendering
		// « Modified by HexaDrive.
        const FuseSurfaceOptionUIBlock.Features   surfaceOptionFeatures = FuseSurfaceOptionUIBlock.Features.Unlit
            ^ FuseSurfaceOptionUIBlock.Features.AlphaCutoffThreshold
            ^ FuseSurfaceOptionUIBlock.Features.DoubleSidedNormalMode
            ^ FuseSurfaceOptionUIBlock.Features.BackThenFrontRendering
			| FuseSurfaceOptionUIBlock.Features.DisableSurface
			| FuseSurfaceOptionUIBlock.Features.DisableBlendMode;
		// ª Modified by HexaDrive.

        MaterialUIBlockList uiBlocks = new MaterialUIBlockList
        {
			// « Modified by HexaDrive.
            new FuseSurfaceOptionUIBlock(MaterialUIBlock.Expandable.Base, features: surfaceOptionFeatures),
			// ª Modified by HexaDrive.
            new ShaderGraphUIBlock(MaterialUIBlock.Expandable.ShaderGraph, ShaderGraphUIBlock.Features.Unlit),
        };

        protected override void OnMaterialGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            using (var changed = new EditorGUI.ChangeCheckScope())
            {
                uiBlocks.OnGUI(materialEditor, props);
                ApplyKeywordsAndPassesIfNeeded(changed.changed, uiBlocks.materials);
            }
        }

        public static void SetupMaterialKeywordsAndPass(Material material)
        {
            SynchronizeShaderGraphProperties(material);
			// « Modified by HexaDrive.
            FuseUnlitGUI.SetupUnlitMaterialKeywordsAndPass(material);
			// ª Modified by HexaDrive.
			// « Add by HexaDrive.
			ResetMaterialCustomRenderQueue(material);
			// ª Add by HexaDrive.
        }

        protected override void SetupMaterialKeywordsAndPassInternal(Material material) => SetupMaterialKeywordsAndPass(material);
    }
}
