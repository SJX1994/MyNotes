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
//===========================================================================
//!
//!	@file		RawDecalProjector.cs
//!	@brief		デカールプロジェクター（コンポーネントではなく直接利用）
//!
//!	@author		Copyright (C) 2021 HEXADRIVE Inc. All rights reserved.
//!	@author		K.Fujihira
//!
//===========================================================================

namespace UnityEngine.Rendering.HighDefinition
{
	//===========================================================================
	//!	@class		RawDecalProjector
	//!	@brief		デカールプロジェクター（コンポーネントではなく直接利用）
	//!	@note		HDRPのDecalProjectorを元にカスタム
	//===========================================================================
	public class RawDecalProjector
	{
		//-----------------------------------------------------------
		//!	@name 定義
		//-----------------------------------------------------------
		//@{

		//===========================================================================
		//!	@struct		RawDecalProjector.Handle
		//!	@brief		ハンドル
		//===========================================================================
		public struct Handle
		{
			//! 空
			public static readonly Handle	EMPTY	= new Handle {
				_handle = null,
			};

			//-----------------------------------------------------------
			//!	@brief	ハンドルを持っているか
			//!	@retval	true	持っている
			//!	@retval	false	持っていない
			//-----------------------------------------------------------
			public bool hasHandle()
			{
				return _handle != null;
			}

			internal DecalSystem.DecalHandle _handle;	//!< ハンドル
		}

		//@}


		//-----------------------------------------------------------
		//!	@name 定数
		//-----------------------------------------------------------
		//@{

		private static readonly Quaternion		MINUS_Y_TO_Z_ROTATION	= Quaternion.Euler(-90, 0, 0);						//!< -Yを+Zにする回転
		private static readonly Vector3			DISABLE_POSITION		= new Vector3(-10000.0f, -10000.0f, -10000.0f);		//!< 無効時の位置

		//@}


		//-----------------------------------------------------------
		//!	@name プロパティ
		//-----------------------------------------------------------
		//@{

		public int			layer					{ get { return _layer; } set { _layer = value; } }
		public float		drawDistance			{ get { return _drawDistance; } set { _drawDistance = Mathf.Max(0f, value); } }
		public float		fadeScale				{ get { return _fadeScale; } set { _fadeScale = Mathf.Clamp01(value); } }
		public Vector2		uvScale					{ get { return _uvScale; } set { _uvScale = value; } }
		public Vector2		uvBias					{ get { return _uvBias; } set { _uvBias = value; } }
		public bool			affectsTransparency		{ get { return _affectsTransparency; } set { _affectsTransparency = value; } }
		public Vector3		size					{ get { return _size; } set { _size = value; } }
		public float		fadeFactor				{ get { return _fadeFactor; } set { _fadeFactor = Mathf.Clamp01(value); } }
		public Quaternion	rotation				{ get { return _rotation; } set { _rotation = value; } }
		public Vector3		position				{ get { return _position; } set { _position = value; } }
		public bool			isEnable				{ get { return _isEnable; } }
		private Vector3		decalSize				{ get { return new Vector3(_size.x, _size.z, _size.y); } }
		private Vector3		decalOffset				{ get { return new Vector3(_offset.x, -_offset.z, _offset.y); } }
		private Vector4		uvScaleBias				{ get { return new Vector4(_uvScale.x, _uvScale.y, _uvBias.x, _uvBias.y); } }

		//@}


		//-----------------------------------------------------------
		//!	@name 公開メソッド
		//-----------------------------------------------------------
		//@{

		//-----------------------------------------------------------
		//!	@brief	作成
		//!	@return	なし
		//-----------------------------------------------------------
		public void create()
		{
			destroy();

			_handle._handle = DecalSystem.instance.AddDecal(DISABLE_POSITION, Quaternion.identity, Vector3.zero, Matrix4x4.zero, 0.0f, 0.0f, Vector4.zero, false, _material, 0, 0.0f);
		}

		//-----------------------------------------------------------
		//!	@brief	破棄
		//!	@return	なし
		//-----------------------------------------------------------
		public void destroy()
		{
			_isEnable = false;

			if( _handle.hasHandle() ) {
				DecalSystem.instance.RemoveDecal(_handle._handle);
				_handle = Handle.EMPTY;
			}
#if HX_DEBUG
			if( _goDecalProjector != null ) {
				GameObject.Destroy(_goDecalProjector.gameObject);
			}
			_goDecalProjector = null;
#endif // HX_DEBUG
		}

		//-----------------------------------------------------------
		//!	@brief	キャッシュを更新
		//!	@return	なし
		//-----------------------------------------------------------
		public void updateCache()
		{
#if HX_DEBUG
			if( _goDecalProjector != null ) {
				_goDecalProjector.enabled				= _isEnable;
				_goDecalProjector.transform.position	= _position;
				_goDecalProjector.transform.rotation	= _rotation;
				_goDecalProjector.gameObject.layer		= _layer;
				_goDecalProjector.material				= _material;
				_goDecalProjector.affectsTransparency	= _affectsTransparency;
				_goDecalProjector.uvBias				= _uvBias;
				_goDecalProjector.uvScale				= _uvScale;
				_goDecalProjector.size					= _size;
				_goDecalProjector.drawDistance			= _drawDistance;
				_goDecalProjector.fadeScale				= _fadeScale;
				_goDecalProjector.fadeFactor			= _fadeFactor;
			}
			else
#endif // HX_DEBUG
			{
				if( !_handle.hasHandle() ) return;

				if( _isEnable ) {
					Matrix4x4 sizeOffset = Matrix4x4.Translate(decalOffset) * Matrix4x4.Scale(decalSize);
					DecalSystem.instance.UpdateCachedData(_position, _rotation * MINUS_Y_TO_Z_ROTATION, sizeOffset, _drawDistance, _fadeScale, uvScaleBias, _affectsTransparency, _handle._handle, _layer, _fadeFactor);
				}
				else {
					DecalSystem.instance.UpdateCachedData(DISABLE_POSITION, Quaternion.identity, Matrix4x4.zero, 0.0f, 0.0f, Vector4.zero, false, _handle._handle, 0, 0.0f);
				}
			}
		}

		//-----------------------------------------------------------
		//!	@brief	マテリアルの設定
		//!	@param	[in]	material	マテリアル
		//!	@return	なし
		//-----------------------------------------------------------
		public void setMaterial(Material material)
		{
			var created = _handle.hasHandle();
			destroy();
			_material = material;

			if( created &&
				_material != null ) {
				if( !DecalSystem.IsHDRenderPipelineDecal(_material.shader) ) // non HDRP/decal shaders such as shader graph decal do not affect transparency
				{
					_affectsTransparency = false;
				}
				create();
			}
		}

		//-----------------------------------------------------------
		//!	@brief	有効化の設定
		//!	@param	[in]	isEnable	有効化するか
		//!	@return	なし
		//-----------------------------------------------------------
		public void setEnable(bool isEnable)
		{
			_isEnable = isEnable;
		}

#if HX_DEBUG
		//-----------------------------------------------------------
		//!	@brief	ゲームオブジェクトを利用するに設定
		//!	@param	[in]	name	名前
		//!	@param	[in]	parent	親
		//!	@return	ゲームオブジェクト
		//-----------------------------------------------------------
		public GameObject setUseGameObject(string name, Transform parent)
		{
			if( _goDecalProjector == null ) {
				DecalSystem.instance.UpdateCachedData(DISABLE_POSITION, Quaternion.identity, Matrix4x4.zero, 0.0f, 0.0f, Vector4.zero, false, _handle._handle, 0, 0.0f);

				var go = new GameObject(name);
				_goDecalProjector = go.AddComponent<DecalProjector>();
			}

			_goDecalProjector.transform.SetParent(parent);

			updateCache();

			return _goDecalProjector.gameObject;
		}

		//-----------------------------------------------------------
		//!	@brief	ゲームオブジェクトを利用しないように設定
		//!	@return	なし
		//-----------------------------------------------------------
		public void setNotUseGameObject()
		{
			if( _goDecalProjector != null ) {
				GameObject.Destroy(_goDecalProjector.gameObject);
			}
			_goDecalProjector = null;
			if( _isEnable ) {
				updateCache();
			}
		}
#endif // HX_DEBUG

		//@}


		//-----------------------------------------------------------
		//!	@name 内部メソッド
		//-----------------------------------------------------------
		//@{

		//-----------------------------------------------------------
		//!	@brief	デストラクタ
		//-----------------------------------------------------------
		~RawDecalProjector()
		{
#if HX_DEBUG
			if( _handle.hasHandle() ) {
				var matName = _material != null ? _material.name : "null";
				Debug.LogWarning($"RawDecalProjector no destroyed. materialName: {matName}");
			}
#endif // HX_DEBUG

			// 解放漏れがあるとHDRPのデカールシステムにゴミが残りそうなので念の為デストラクタで開放する
			destroy();
		}

		//@}


		//-----------------------------------------------------------
		//!	@name メンバ変数
		//-----------------------------------------------------------
		//@{

		private Material		_material				= null;						//!< マテリアル
		private int				_layer					= 1;						//!< レイヤー
		private float			_drawDistance			= 1000.0f;					//!< 描画距離
		private float			_fadeScale				= 0.9f;						//!< 描画距離によるフェードの開始割合
		private Vector2			_uvScale				= new Vector2(1, 1);		//!< UVスケール（タイリング）
		private Vector2			_uvBias					= new Vector2(0, 0);		//!< UVバイアス（オフセット）
		private bool			_affectsTransparency	= false;					//!< 透明度の方法を変更（HDRP/Decalのシェーダーでのみ有効）
		private Vector3			_offset					= new Vector3(0, 0, 0.5f);	//!< 位置のオフセット
		private Vector3			_size					= new Vector3(1, 1, 1);		//!< サイズ
		private float			_fadeFactor				= 1.0f;						//!< 透明度
		private Handle			_handle					= Handle.EMPTY;				//!< デカールのハンドル
		private Quaternion		_rotation				= Quaternion.identity;		//!< 回転
		private Vector3			_position				= Vector3.zero;				//!< 位置
		private bool			_isEnable				= false;					//!< 有効か

#if HX_DEBUG
		private DecalProjector	_goDecalProjector		= null;						//!< デバッグ用のゲームオブジェクト
#endif // HX_DEBUG

		//@}
	}
}
//===========================================================================
//	END OF FILE
//===========================================================================