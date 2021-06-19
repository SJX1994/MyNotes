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
using System.Text;
using UnityEditor.Graphing;
using UnityEngine;

namespace UnityEditor.ShaderGraph.Internal
{
    [Serializable]
	// Å´ Modified by HexaDrive.
    public sealed class Texture2DTilingOffsetShaderProperty : AbstractShaderProperty<SerializableTexture>
	// Å™ Modified by HexaDrive.
    {
        public enum DefaultType { White, Black, Grey, Bump }

        internal Texture2DTilingOffsetShaderProperty()
        {
            displayName = "Texture2D";
            value = new SerializableTexture();
        }

        public override PropertyType propertyType => PropertyType.Texture2D;

        internal override bool isBatchable => false;
        internal override bool isExposable => true;
        internal override bool isRenamable => true;

        internal string modifiableTagString => modifiable ? "" : "[NonModifiableTextureData]";

        internal override string GetPropertyBlockString()
        {
			// Å´ Modified by HexaDrive.
            return $"{hideTagString}{modifiableTagString}{referenceName}(\"{displayName}\", 2D) = \"{defaultType.ToString().ToLower()}\" {{}}";
			// Å™ Modified by HexaDrive.
        }

        internal override string GetPropertyDeclarationString(string delimiter = ";")
        {
            return $"TEXTURE2D({referenceName}){delimiter} SAMPLER(sampler{referenceName}){delimiter} {concretePrecision.ToShaderString()}4 {referenceName}_TexelSize{delimiter}";
        }

        internal override string GetPropertyAsArgumentString()
        {
            return $"TEXTURE2D_PARAM({referenceName}, sampler{referenceName}), {concretePrecision.ToShaderString()}4 {referenceName}_TexelSize";
        }

        [SerializeField]
        bool m_Modifiable = true;

        internal bool modifiable
        {
            get => m_Modifiable;
            set => m_Modifiable = value;
        }

        [SerializeField]
        DefaultType m_DefaultType = DefaultType.White;

        public DefaultType defaultType
        {
            get { return m_DefaultType; }
            set { m_DefaultType = value; }
        }

        internal override AbstractMaterialNode ToConcreteNode()
        {
            return new Texture2DAssetNode { texture = value.texture };
        }

        internal override PreviewProperty GetPreviewMaterialProperty()
        {
            return new PreviewProperty(propertyType)
            {
                name = referenceName,
                textureValue = value.texture
            };
        }

        internal override ShaderInput Copy()
        {
            return new Texture2DShaderProperty()
            {
                displayName = displayName,
                hidden = hidden,
                value = value
            };
        }
    }
}
