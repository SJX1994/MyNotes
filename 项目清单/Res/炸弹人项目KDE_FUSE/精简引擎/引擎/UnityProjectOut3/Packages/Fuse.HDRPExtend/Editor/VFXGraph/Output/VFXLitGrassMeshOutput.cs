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
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VFX.Block;
using UnityEngine;

namespace UnityEditor.VFX
{
    [VFXInfo]
	// ↓ Modified by HexaDrive.
    class VFXLitGrassMeshOutput : VFXAbstractParticleHDRPLitOutput
	// ↑ Modified by HexaDrive.
    {
		// ↓ Modified by HexaDrive.
        public override string name { get { return "Output Particle Lit Grass Mesh"; } }
        public override string codeGeneratorTemplate { get { return FuseVFXGraphDef.makeTemplatePath("VFXParticleLitGrassMesh"); } }
		// ↑ Modified by HexaDrive.
        public override VFXTaskType taskType { get { return VFXTaskType.ParticleMeshOutput; } }
        public override bool supportsUV { get { return shaderGraph == null; } }
        public override bool implementsMotionVector { get { return true; } }

        public override CullMode defaultCullMode { get { return CullMode.Back; } }

        public override IEnumerable<VFXAttributeInfo> attributes
        {
            get
            {
                yield return new VFXAttributeInfo(VFXAttribute.Position, VFXAttributeMode.Read);
                if (colorMode != ColorMode.None)
                    yield return new VFXAttributeInfo(VFXAttribute.Color, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.Alpha, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.Alive, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.AxisX, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.AxisY, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.AxisZ, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.AngleX, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.AngleY, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.AngleZ, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.PivotX, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.PivotY, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.PivotZ, VFXAttributeMode.Read);

                yield return new VFXAttributeInfo(VFXAttribute.Size, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.ScaleX, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.ScaleY, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.ScaleZ, VFXAttributeMode.Read);

                if (usesFlipbook)
                    yield return new VFXAttributeInfo(VFXAttribute.TexIndex, VFXAttributeMode.Read);
            }
        }

        public class InputProperties
        {
            [Tooltip("Specifies the mesh used to render the particle.")]
            public Mesh mesh = VFXResources.defaultResources.mesh;
            [Tooltip("Defines a bitmask to control which submeshes are rendered."), BitField]
            public uint subMeshMask = 0xffffffff;
        }

		// ↓ Add by HexaDrive.
		//===========================================================================
		//!	@class		VFXLitGrassMeshOutput.OptionalInputProperties
		//!	@brief		追加インプットプロパティ
		//===========================================================================
		public class OptionalInputProperties
		{
			//-----------------------------------------------------------
			//!	@name メンバ変数
			//-----------------------------------------------------------
			//@{

			public Vector3	grassDirection	= Vector3.zero;					//!< 草の傾いてる方向
			public Vector2	curveRange		= new Vector2(0.0f, 1.0f);		//!< 草の曲がりをブレンドする範囲

			//@}
		}

		protected override IEnumerable<VFXPropertyWithValue> inputProperties
		{
			get
			{
				var properties = base.inputProperties;
				if( shaderGraph == null ) {
					properties = properties.Concat(PropertiesFromType("OptionalInputProperties"));
				}
				return properties;
			}
		}

		protected override IEnumerable<VFXNamedExpression> CollectGPUExpressions(IEnumerable<VFXNamedExpression> slotExpressions)
		{
			foreach( var exp in base.CollectGPUExpressions(slotExpressions) ) yield return exp;

			if( shaderGraph == null ) {
				yield return slotExpressions.First(o => o.name == "grassDirection");
				yield return slotExpressions.First(o => o.name == "curveRange");
			}
		}
		// ↑ Add by HexaDrive.

        public override VFXExpressionMapper GetExpressionMapper(VFXDeviceTarget target)
        {
            var mapper = base.GetExpressionMapper(target);

            switch (target)
            {
                case VFXDeviceTarget.CPU:
                {
                    mapper.AddExpression(inputSlots.First(s => s.name == "mesh").GetExpression(), "mesh", -1);
                    mapper.AddExpression(inputSlots.First(s => s.name == "subMeshMask").GetExpression(), "subMeshMask", -1);
                    break;
                }
                default:
                {
                    break;
                }
            }

            return mapper;
        }
    }
}
