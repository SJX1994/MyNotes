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
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

// Include material common properties names
using static UnityEngine.Rendering.HighDefinition.HDMaterialProperties;

namespace UnityEditor.Rendering.HighDefinition
{
    /// <summary>
    /// GUI for HDRP unlit shaders (does not include shader graphs)
    /// </summary>
	// Å´ Modified by HexaDrive.
    class FuseUnlitGUI : FuseHDShaderGUI
	// Å™ Modified by HexaDrive.
    {
        MaterialUIBlockList uiBlocks = new MaterialUIBlockList
        {
			// Å´ Modified by HexaDrive.
            new FuseSurfaceOptionUIBlock(MaterialUIBlock.Expandable.Base, features: FuseSurfaceOptionUIBlock.Features.Unlit),
			// Å™ Modified by HexaDrive.
            new UnlitSurfaceInputsUIBlock(MaterialUIBlock.Expandable.Input),
            new TransparencyUIBlock(MaterialUIBlock.Expandable.Transparency),
			// Å´ Modified by HexaDrive.
            new FuseEmissionUIBlock(MaterialUIBlock.Expandable.Emissive),
			// Å™ Modified by HexaDrive.
            new AdvancedOptionsUIBlock(MaterialUIBlock.Expandable.Advance, AdvancedOptionsUIBlock.Features.Instancing | AdvancedOptionsUIBlock.Features.AddPrecomputedVelocity)
        };

        protected override void OnMaterialGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            using (var changed = new EditorGUI.ChangeCheckScope())
            {
                uiBlocks.OnGUI(materialEditor, props);
                ApplyKeywordsAndPassesIfNeeded(changed.changed, uiBlocks.materials);
            }
        }

        protected override void SetupMaterialKeywordsAndPassInternal(Material material) => SetupUnlitMaterialKeywordsAndPass(material);

        // All Setup Keyword functions must be static. It allow to create script to automatically update the shaders with a script if code change
        public static void SetupUnlitMaterialKeywordsAndPass(Material material)
        {
			// Å´ Modified by HexaDrive.
            material.FuseSetupBaseUnlitKeywords();
            material.FuseSetupBaseUnlitPass();
			// Å™ Modified by HexaDrive.

            if (material.HasProperty(kEmissiveColorMap))
                CoreUtils.SetKeyword(material, "_EMISSIVE_COLOR_MAP", material.GetTexture(kEmissiveColorMap));

            // All the bits exclusively related to lit are ignored inside the BaseLitGUI function.
			// Å´ Modified by HexaDrive.
            FuseBaseLitGUI.SetupStencil(material, receivesSSR: false, useSplitLighting: false);
			// Å™ Modified by HexaDrive.

            if (material.HasProperty(kAddPrecomputedVelocity))
            {
                CoreUtils.SetKeyword(material, "_ADD_PRECOMPUTED_VELOCITY", material.GetInt(kAddPrecomputedVelocity) != 0);
            }

        }
    }
} // namespace UnityEditor
