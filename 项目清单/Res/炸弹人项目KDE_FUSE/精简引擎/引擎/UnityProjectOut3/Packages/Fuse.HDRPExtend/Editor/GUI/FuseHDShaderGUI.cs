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
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;

// Include material common properties names
using static UnityEngine.Rendering.HighDefinition.HDMaterialProperties;

namespace UnityEditor.Rendering.HighDefinition
{
    // A Material can be authored from the shader graph or by hand. When written by hand we need to provide an inspector.
    // Such a Material will share some properties between it various variant (shader graph variant or hand authored variant).
    // HDShaderGUI is here to provide a support for setup material keyword and pass function. It will allow the GUI
    // to setup the material properties needed for rendering when switching shaders on a material. For the GUI part
    // of the material you must use Material UI Blocks, examples of doing so can be found in the classes UnlitGUI,
    // LitGUI or LayeredLitGUI.

	// ↓ Modified by HexaDrive.
    abstract class FuseHDShaderGUI : ShaderGUI
	// ↑ Modified by HexaDrive.
    {
		// ↓ Add by HexaDrive.
		private static readonly string	HEADER_TEXT		= "G5 HD";	//!< //!< マテリアル編集ブロックのヘッダーテキスト
		// ↑ Add by HexaDrive.

        protected bool m_FirstFrame = true;

        // The following set of functions are call by the ShaderGraph
        // It will allow to display our common parameters + setup keyword correctly for them
        protected abstract void SetupMaterialKeywordsAndPassInternal(Material material);

        public override void AssignNewShaderToMaterial(Material material, Shader oldShader, Shader newShader)
        {
            base.AssignNewShaderToMaterial(material, oldShader, newShader);

            ResetMaterialCustomRenderQueue(material);

            SetupMaterialKeywordsAndPassInternal(material);
        }

        protected void ApplyKeywordsAndPassesIfNeeded(bool changed, Material[] materials)
        {
            // !!! HACK !!!
            // When a user creates a new Material from the contextual menu, the material is created from the editor code and the appropriate shader is applied to it.
            // This means that we never setup keywords and passes for a newly created material. The material is then in an invalid state.
            // To work around this, as the material is automatically selected when created, we force an update of the keyword at the first "frame" of the editor.

            // Apply material keywords and pass:
            if (changed || m_FirstFrame)
            {
                m_FirstFrame = false;

                foreach (var material in materials)
                    SetupMaterialKeywordsAndPassInternal(material);
            }
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {
			// ↓ Add by HexaDrive.
			using( var header = new MaterialHeaderScope(HEADER_TEXT, FuseHDRPExtendEditorDef.G5_EXPANDABLE_BIT, materialEditor, colorDot: Color.red) ) {
				if( header.expanded ) {
			// ↑ Add by HexaDrive.

            if (!(RenderPipelineManager.currentPipeline is HDRenderPipeline))
            {
                EditorGUILayout.HelpBox("Editing HDRP materials is only supported when an HDRP asset assigned in the graphic settings", MessageType.Warning);
            }
            else
            {
                OnMaterialGUI(materialEditor, props);
            }

			// ↓ Add by HexaDrive.
				}
			}
			// ↑ Add by HexaDrive.
        }

        protected abstract void OnMaterialGUI(MaterialEditor materialEditor, MaterialProperty[] props);

        protected static void ResetMaterialCustomRenderQueue(Material material)
        {
            HDRenderQueue.RenderQueueType targetQueueType;
            switch (material.GetSurfaceType())
            {
                case SurfaceType.Opaque:
                    targetQueueType = HDRenderQueue.GetOpaqueEquivalent(HDRenderQueue.GetTypeByRenderQueueValue(material.renderQueue));
                    break;
                case SurfaceType.Transparent:
                    targetQueueType = HDRenderQueue.GetTransparentEquivalent(HDRenderQueue.GetTypeByRenderQueueValue(material.renderQueue));
                    break;
                default:
                    throw new ArgumentException("Unknown SurfaceType");
            }

            // Decal doesn't have properties to compute the render queue 
            if (material.HasProperty(kTransparentSortPriority) && material.HasProperty(kAlphaCutoffEnabled))
            {
                float sortingPriority = material.GetFloat(kTransparentSortPriority);
                bool alphaTest = material.GetFloat(kAlphaCutoffEnabled) > 0.5f;
                material.renderQueue = HDRenderQueue.ChangeType(targetQueueType, (int)sortingPriority, alphaTest);
            }
        }

        readonly static string[] floatPropertiesToSynchronize = {
            "_UseShadowThreshold", kReceivesSSR, kUseSplitLighting
        };

        protected static void SynchronizeShaderGraphProperties(Material material)
        {
            var defaultProperties = new Material(material.shader);
            foreach (var floatToSync in floatPropertiesToSynchronize)
                if (material.HasProperty(floatToSync))
                    material.SetFloat(floatToSync, defaultProperties.GetFloat(floatToSync));
            defaultProperties = null;
        }
    }
}
