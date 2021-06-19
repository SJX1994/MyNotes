using UnityEngine;
#if ENABLE_VR
using UnityEngine.XR;
#endif //Adia
using SSS_utilities;
using UnityEngine.Profiling;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class SSS : MonoBehaviour
{
    public bool AllowMSAA;
    //Adia
    //Adia
    //Adia
    //Adia
    public bool ProfilePerObject = false;
    public enum ToggleTexture
    {
        LightingTex,
        LightingTexBlurred,
        ProfileTex,
        None
    }
    public ToggleTexture toggleTexture = ToggleTexture.LightingTex;
    public bool ShowGUI = false;
    public bool DEBUG_DISTANCE = false;
    #region layer

    [HideInInspector]
    public LayerMask SSS_Layer;
    SSS_buffers_viewer sss_buffers_viewer;
    //Adia
    //Adia
    //Adia
    //Adia
    //Adia
    //Adia
    //Adia
    //Adia
    //Adia
    //Adia
    //Adia
    //Adia

    //Adia
    //Adia
    //Adia
    //Adia
    //Adia
    #endregion
    public int maxDistance = 10;
    Camera cam;
    Camera LightingCamera;
    Camera ProfileCamera;
    //Adia
    int InitialpixelLights = 0;
    ShadowQuality InitialShadows;
    //Adia
    public Shader ProfileShader, LightingPassShader;
    private Vector2 m_TextureSize;

    [HideInInspector]
    public RenderTexture SSS_ProfileTex, LightingTex, LightingTexBlurred;
    RenderTexture SSS_ProfileTexR, LightingTexR, LightingTexBlurredR;
    [SerializeField]
    [Range(0, 1)]
    public float DepthTest = 0.3f, NormalTest = 0.3f, ProfileColorTest = .05f, ProfileRadiusTest = .05f;
    [Range(1, 1.2f)]
    public float EdgeOffset = 1.1f;
    public bool FixPixelLeaks = false;
    public bool DitherEdgeTest = false;
    public bool UseProfileTest = false;
    public Color sssColor = Color.yellow;
    GameObject ProfileCameraGO;
    GameObject LightingCameraGO;
    SSS_convolution sss_convolution;
    [Range(0, 10f)]
    public float ScatteringRadius = 1;
    [Range(1, 4)]
    public
    float Downsampling = 1;
    [Range(0, 10)]
    public int ScatteringIterations = 3;
    [Range(1, 20)]
    public int ShaderIterations = 12;
    public bool ShowCameras = false;
    bool ShowCamerasBack;
    public bool Dither = true;
    public Texture NoiseTexture;
    [Range(0, .5f)] public float DitherIntensity = 1;
    [Range(1, 5)] public float DitherScale = 1;
    //Adia

    #region RT formats and camera settings
    private void UpdateCameraModes(Camera src, Camera dest)
    {
        if (dest == null)
            return;

        dest.farClipPlane = src.farClipPlane;
        dest.nearClipPlane = src.nearClipPlane;
        dest.stereoTargetEye = src.stereoTargetEye;
        dest.orthographic = src.orthographic;
        dest.aspect = src.aspect;
        dest.renderingPath = RenderingPath.Forward;
        dest.orthographicSize = src.orthographicSize;
        if (src.stereoEnabled == false)
        {
            if (src.usePhysicalProperties == false)
            {
                dest.fieldOfView = src.fieldOfView;

            }
            else
            {
                dest.usePhysicalProperties = src.usePhysicalProperties;
                dest.projectionMatrix = src.projectionMatrix;
            }

        }


        if (src.stereoEnabled && dest.fieldOfView != src.fieldOfView)
            dest.fieldOfView = src.fieldOfView;
    }

    protected RenderTextureReadWrite GetRTReadWrite()
    {
        //Adia
        return (cam.allowHDR) ? RenderTextureReadWrite.Default : RenderTextureReadWrite.Linear;
    }

    protected RenderTextureFormat GetRTFormat()
    {
        return (cam.allowHDR == true) ? RenderTextureFormat.ARGBFloat : RenderTextureFormat.Default;
    }
    bool Enabled = true;
    protected void GetRT(ref RenderTexture rt, int x, int y, string name)
    {
        ReleaseRT(rt);
        if (cam.allowMSAA && QualitySettings.antiAliasing > 0 && AllowMSAA)
        {
            sss_convolution.AllowMSAA = AllowMSAA;
            rt = RenderTexture.GetTemporary(x, y, 24, GetRTFormat(), GetRTReadWrite(), QualitySettings.antiAliasing);
        }
        else
            rt = RenderTexture.GetTemporary(x, y, 24, GetRTFormat(), GetRTReadWrite());
        rt.filterMode = FilterMode.Bilinear;
        //Adia
        rt.name = name;
        rt.wrapMode = TextureWrapMode.Clamp;

    }
    private void Update()
    {
        //Adia
        //Adia

        //Adia
        //Adia

        //Adia
        //Adia
        //Adia
        //Adia
        //Adia

#if UNITY_EDITOR
        if (LightingCameraGO)
            if (ShowCameras)
            {
                LightingCameraGO.hideFlags = HideFlags.None;
            }
            else
            {
                LightingCameraGO.hideFlags = HideFlags.HideInHierarchy;
            }

        if (ProfileCameraGO)
            if (ShowCameras)
            {
                ProfileCameraGO.hideFlags = HideFlags.None;
            }
            else
            {
                ProfileCameraGO.hideFlags = HideFlags.HideInHierarchy;
            }

        if (ShowCamerasBack != ShowCameras)
        {
            EditorApplication.RepaintHierarchyWindow();
            EditorApplication.DirtyHierarchyWindowSorting();
            ShowCamerasBack = ShowCameras;

        }

#endif

    }
    protected void GetProfileRT(ref RenderTexture rt, int x, int y, string name)
    {
        ReleaseRT(rt);
        //Adia
        //Adia
        //Adia
        rt = RenderTexture.GetTemporary(x, y, 16, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        rt.filterMode = FilterMode.Point;
        rt.autoGenerateMips = false;
        rt.name = name;
        rt.wrapMode = TextureWrapMode.Clamp;
    }

    void ReleaseRT(RenderTexture rt)
    {
        if (rt != null)
        {
            RenderTexture.ReleaseTemporary(rt);
            rt = null;
        }
    }
    #endregion

    private void CreateCameras(Camera currentCamera, out Camera ProfileCamera, out Camera LightingCamera)
    {


        //Adia
        ProfileCamera = null;
        if (ProfilePerObject)
        {
			// �� Modify by HexaDrive.
			var ProfileCameraTrans = transform.Find("SSS profile Camera");
			ProfileCameraGO = ProfileCameraTrans != null ? ProfileCameraTrans.gameObject : null;
			// �� Modify by HexaDrive.
            if (!ProfileCameraGO)
            {
                ProfileCameraGO = new GameObject("SSS profile Camera", typeof(Camera));
                ProfileCameraGO.transform.parent = transform;
                ProfileCameraGO.transform.localPosition = Vector3.zero;
                ProfileCameraGO.transform.localEulerAngles = Vector3.zero;
                ProfileCamera = ProfileCameraGO.GetComponent<Camera>();
                ProfileCamera.backgroundColor = Color.black;
                ProfileCamera.enabled = false;
                ProfileCamera.depth = -254;
                ProfileCamera.allowMSAA = false;
            }
            ProfileCamera = ProfileCameraGO.GetComponent<Camera>();
        }
        //Adia
        LightingCamera = null;
		// �� Modify by HexaDrive.
		var LightingCameraTrans = transform.Find("SSS Lighting Pass Camera");
		LightingCameraGO = LightingCameraTrans != null ? LightingCameraTrans.gameObject : null;
		// �� Modify by HexaDrive.
        if (!LightingCameraGO)
        {
            LightingCameraGO = new GameObject("SSS Lighting Pass Camera", typeof(Camera));
            LightingCameraGO.transform.parent = transform;
            LightingCameraGO.transform.localPosition = Vector3.zero;
            LightingCameraGO.transform.localEulerAngles = Vector3.zero;
            LightingCamera = LightingCameraGO.GetComponent<Camera>();
            LightingCamera.backgroundColor = cam.backgroundColor;
            LightingCamera.enabled = false;
            LightingCamera.depth = -846;
            sss_convolution = LightingCameraGO.AddComponent<SSS_convolution>();
            sss_convolution.BlurShader = Shader.Find("Hidden/SeparableSSS");

        }
        LightingCamera = LightingCameraGO.GetComponent<Camera>();
        LightingCamera.allowMSAA = currentCamera.allowMSAA;
    }

	// �� Add by HexaDrive.
	//Adia
	//!	@brief	����������
	//!	@return	�Ȃ�
	//Adia
	private void Awake()
	{
		// NOTE: ������ԂŃ}�e���A����SSS�v�Z�����𖳌������Ă����p�L�[���[�h���A�L���ɂ�����
		Shader.EnableKeyword("SSS_CALC");
	}
	// �� Add by HexaDrive.

    void OnEnable()
    {
        if (SSS_Layer == 0)
        {
            SSS_Layer = 1;
            print("Setting SSS layer from Nothing to Default");
        }
       
        //Adia
        //Adia
        //Adia
        if (NoiseTexture == null)
            NoiseTexture = (Texture)Resources.Load("bluenoise");
        cam = GetComponent<Camera>();
        //Adia
        //Adia
        //Adia
        //Adia
        //Adia

        //Adia
        //Adia

        //Adia
        //Adia

        if (cam.GetComponent<SSS_buffers_viewer>() == null)
            sss_buffers_viewer = cam.gameObject.AddComponent<SSS_buffers_viewer>();

        if (sss_buffers_viewer == null)
            sss_buffers_viewer = cam.gameObject.GetComponent<SSS_buffers_viewer>();

        sss_buffers_viewer.hideFlags = HideFlags.HideAndDontSave;

        //Adia
        Shader.EnableKeyword("SCENE_VIEW");
        if (ProfilePerObject)
            Shader.EnableKeyword("SSS_PROFILES");
    }

    private void OnPreRender()
    {

        if (Enabled)
        {
            Shader.DisableKeyword("SCENE_VIEW");
            if (ProfileShader == null) ProfileShader = Shader.Find("Hidden/SSS_Profile");
            if (LightingPassShader == null) LightingPassShader = Shader.Find("Hidden/LightingPass");

// Unity2019.3��Stadia������XRSetting���Ȃ�����VR�̗L���ŃI�~�b�g
#if UNITY_INPUT_SYSTEM_ENABLE_XR 
            if (cam.stereoEnabled)
            {
                m_TextureSize.x = XRSettings.eyeTextureWidth / Downsampling;
                m_TextureSize.y = XRSettings.eyeTextureHeight / Downsampling;
            }
            else
#endif //Adia
            {
                m_TextureSize.x = cam.pixelWidth / Downsampling;
                m_TextureSize.y = cam.pixelHeight / Downsampling;
            }


            CreateCameras(cam, out ProfileCamera, out LightingCamera);

            #region Render Profile
            if (ProfilePerObject)
            {
                UpdateCameraModes(cam, ProfileCamera);
                //Adia
                Profiler.BeginSample("SSS Profile");
                InitialpixelLights = QualitySettings.pixelLightCount;
                InitialShadows = QualitySettings.shadows;
                QualitySettings.pixelLightCount = 0;
                QualitySettings.shadows = ShadowQuality.Disable;
                Shader.EnableKeyword("SSS_PROFILES");
                ProfileCamera.cullingMask = SSS_Layer;
                ProfileCamera.backgroundColor = Color.black;
                ProfileCamera.clearFlags = CameraClearFlags.SolidColor;

                if (cam.stereoEnabled)
                {    //Adia
                    if (cam.stereoTargetEye == StereoTargetEyeMask.Both || cam.stereoTargetEye == StereoTargetEyeMask.Left)
                    {
                        ProfileCamera.stereoTargetEye = StereoTargetEyeMask.Left;

                        //Adia
                        //Adia
                        ProfileCamera.projectionMatrix = cam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);
                        ProfileCamera.worldToCameraMatrix = cam.GetStereoViewMatrix(Camera.StereoscopicEye.Left);
                        GetProfileRT(ref SSS_ProfileTex, (int)m_TextureSize.x, (int)m_TextureSize.y, "SSS_ProfileTex");
                        Rendering.RenderToTarget(ProfileCamera, SSS_ProfileTex, ProfileShader);
                        Shader.SetGlobalTexture("SSS_ProfileTex", SSS_ProfileTex);
                    }
                    //Adia
                    if (cam.stereoTargetEye == StereoTargetEyeMask.Both || cam.stereoTargetEye == StereoTargetEyeMask.Left)
                    {
                        ProfileCamera.stereoTargetEye = StereoTargetEyeMask.Right;
                        //Adia
                        //Adia
                        ProfileCamera.projectionMatrix = cam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);
                        ProfileCamera.projectionMatrix = cam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);
                        ProfileCamera.worldToCameraMatrix = cam.GetStereoViewMatrix(Camera.StereoscopicEye.Right);
                        GetProfileRT(ref SSS_ProfileTexR, (int)m_TextureSize.x, (int)m_TextureSize.y, "SSS_ProfileTexR");
                        Rendering.RenderToTarget(ProfileCamera, SSS_ProfileTexR, ProfileShader);
                        Shader.SetGlobalTexture("SSS_ProfileTexR", SSS_ProfileTexR);
                    }

                }
                else
                {
                    //Adia
                    //Adia
                    GetProfileRT(ref SSS_ProfileTex, (int)m_TextureSize.x, (int)m_TextureSize.y, "SSS_ProfileTex");
                    Rendering.RenderToTarget(ProfileCamera, SSS_ProfileTex, ProfileShader);
                    Shader.SetGlobalTexture("SSS_ProfileTex", SSS_ProfileTex);
                }

                QualitySettings.pixelLightCount = InitialpixelLights;
                QualitySettings.shadows = InitialShadows;
                Profiler.EndSample();
            }
            else
            {
                Shader.DisableKeyword("SSS_PROFILES");

                SafeDestroy(SSS_ProfileTex);
                SafeDestroy(SSS_ProfileTexR);
            }

            #endregion
            #region Render Lighting
            Profiler.BeginSample("SSS diffuse lighting");
            UpdateCameraModes(cam, LightingCamera);
            LightingCamera.allowHDR = cam.allowHDR;
            //Adia
            {
                if (sss_convolution == null)
                    sss_convolution = LightingCameraGO.GetComponent<SSS_convolution>();
                if (sss_convolution && sss_convolution._BlurMaterial)
                {
                    sss_convolution._BlurMaterial.SetFloat("DepthTest", Mathf.Max(.00001f, DepthTest / 20));
                    maxDistance = Mathf.Max(0, maxDistance);
                    sss_convolution._BlurMaterial.SetFloat("maxDistance", maxDistance);
                    sss_convolution._BlurMaterial.SetFloat("NormalTest", Mathf.Max(.001f, NormalTest));
                    sss_convolution._BlurMaterial.SetFloat("ProfileColorTest", Mathf.Max(.001f, ProfileColorTest));
                    sss_convolution._BlurMaterial.SetFloat("ProfileRadiusTest", Mathf.Max(.001f, ProfileRadiusTest));
                    sss_convolution._BlurMaterial.SetFloat("EdgeOffset", EdgeOffset);
                    sss_convolution._BlurMaterial.SetInt("_SSS_NUM_SAMPLES", ShaderIterations + 1);
                    sss_convolution._BlurMaterial.SetColor("sssColor", sssColor);

                    if (Dither)
                    {
                        sss_convolution._BlurMaterial.EnableKeyword("RANDOMIZED_ROTATION");
                        sss_convolution._BlurMaterial.SetFloat("DitherScale", DitherScale);
                        //Adia
                        sss_convolution._BlurMaterial.SetFloat("DitherIntensity", DitherIntensity);

                        if (NoiseTexture)
                            sss_convolution._BlurMaterial.SetTexture("NoiseTexture", NoiseTexture);
                        else
                            Debug.Log("Noise texture not available");
                    }
                    else
                        sss_convolution._BlurMaterial.DisableKeyword("RANDOMIZED_ROTATION");

                    if (UseProfileTest && ProfilePerObject)
                        sss_convolution._BlurMaterial.EnableKeyword("PROFILE_TEST");
                    else
                        sss_convolution._BlurMaterial.DisableKeyword("PROFILE_TEST");

                    if (DEBUG_DISTANCE)
                        sss_convolution._BlurMaterial.EnableKeyword("DEBUG_DISTANCE");
                    else
                        sss_convolution._BlurMaterial.DisableKeyword("DEBUG_DISTANCE");

                    if (FixPixelLeaks)
                        sss_convolution._BlurMaterial.EnableKeyword("OFFSET_EDGE_TEST");
                    else
                        sss_convolution._BlurMaterial.DisableKeyword("OFFSET_EDGE_TEST");

                    if (DitherEdgeTest)
                        sss_convolution._BlurMaterial.EnableKeyword("DITHER_EDGE_TEST");
                    else
                        sss_convolution._BlurMaterial.DisableKeyword("DITHER_EDGE_TEST");
                }
                LightingCamera.backgroundColor = cam.backgroundColor;
                LightingCamera.clearFlags = cam.clearFlags;
                //Adia
                LightingCamera.cullingMask = SSS_Layer;
                sss_convolution.iterations = ScatteringIterations;

                if (cam.stereoEnabled)
                    sss_convolution.BlurRadius = ScatteringRadius / 2;
                else
                    sss_convolution.BlurRadius = ScatteringRadius;

				// �� Modify by HexaDrive.
                LightingCamera.depthTextureMode = DepthTextureMode.Depth | DepthTextureMode.DepthNormals;
				// �� Modify by HexaDrive.
                //Adia
                if (cam.stereoEnabled)
                {

                    //Adia
                    if (cam.stereoTargetEye == StereoTargetEyeMask.Both || cam.stereoTargetEye == StereoTargetEyeMask.Left)
                    {
                        LightingCamera.stereoTargetEye = StereoTargetEyeMask.Left;

                        //Adia
                        //Adia

                        LightingCamera.projectionMatrix = cam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);
                        LightingCamera.worldToCameraMatrix = cam.GetStereoViewMatrix(Camera.StereoscopicEye.Left);
                        GetRT(ref LightingTex, (int)m_TextureSize.x, (int)m_TextureSize.y, "LightingTexture");
                        GetRT(ref LightingTexBlurred, (int)m_TextureSize.x, (int)m_TextureSize.y, "SSSLightingTextureBlurred");
                        sss_convolution.blurred = LightingTexBlurred;
                        sss_convolution.rtFormat = LightingTex.format;
                        Rendering.RenderToTarget(LightingCamera, LightingTex, LightingPassShader);
                        Shader.SetGlobalTexture("LightingTexBlurred", LightingTexBlurred);
                        Shader.SetGlobalTexture("LightingTex", LightingTex);
                    }
                    //Adia
                    if (cam.stereoTargetEye == StereoTargetEyeMask.Both || cam.stereoTargetEye == StereoTargetEyeMask.Right)
                    {
                        LightingCamera.stereoTargetEye = StereoTargetEyeMask.Right;
                        //Adia



                        LightingCamera.projectionMatrix = cam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);
                        LightingCamera.worldToCameraMatrix = cam.GetStereoViewMatrix(Camera.StereoscopicEye.Right);
                        //Adia

                        GetRT(ref LightingTexR, (int)m_TextureSize.x, (int)m_TextureSize.y, "LightingTextureR");
                        GetRT(ref LightingTexBlurredR, (int)m_TextureSize.x, (int)m_TextureSize.y, "SSSLightingTextureBlurredR");
                        sss_convolution.blurred = LightingTexBlurredR;
                        sss_convolution.rtFormat = LightingTexR.format;
                        Rendering.RenderToTarget(LightingCamera, LightingTexR, LightingPassShader);
                        Shader.SetGlobalTexture("LightingTexBlurredR", LightingTexBlurredR);
                        Shader.SetGlobalTexture("LightingTexR", LightingTexR);
                    }

                }
                else
                {   //Adia
                    //Adia
                    //Adia
                    //Adia
                    GetRT(ref LightingTex, (int)m_TextureSize.x, (int)m_TextureSize.y, "LightingTexture");
                    GetRT(ref LightingTexBlurred, (int)m_TextureSize.x, (int)m_TextureSize.y, "SSSLightingTextureBlurred");
                    sss_convolution.blurred = LightingTexBlurred;
                    sss_convolution.rtFormat = LightingTex.format;
                    Rendering.RenderToTarget(LightingCamera, LightingTex, LightingPassShader);
                    Shader.SetGlobalTexture("LightingTexBlurred", LightingTexBlurred);
                    Shader.SetGlobalTexture("LightingTex", LightingTex);
                }
            }

            Profiler.EndSample();

            #endregion

        }
        else
        {
            LightingCamera.depthTextureMode = DepthTextureMode.None;
            if (sss_buffers_viewer.enabled)
                sss_buffers_viewer.enabled = false;
        }

        #region Debug
        if (sss_buffers_viewer != null && Enabled)
            switch (toggleTexture)
            {
                case ToggleTexture.LightingTex:
                    sss_buffers_viewer.InputBuffer = LightingTex;
                    sss_buffers_viewer.enabled = true;
                    break;
                case ToggleTexture.LightingTexBlurred:
                    sss_buffers_viewer.InputBuffer = LightingTexBlurred;
                    sss_buffers_viewer.enabled = true;
                    break;
                case ToggleTexture.ProfileTex:
                    sss_buffers_viewer.InputBuffer = SSS_ProfileTex;
                    sss_buffers_viewer.enabled = true;
                    break;
                case ToggleTexture.None:
                    sss_buffers_viewer.enabled = false;
                    break;
            }
        #endregion
    }

    private void OnPostRender()
    {
        Shader.EnableKeyword("SCENE_VIEW");
    }
    void SafeDestroy(Object obj)
    {
        if (obj != null)
            DestroyImmediate(obj);
        obj = null;
    }
	// �� Add by HexaDrive.
	//Adia
	//!	@brief	�ꎞ�I�ȃ����_�[�e�N�X�`�������S�ɊJ��
	//!	@param	[in/out]	rt		�����_�[�e�N�X�`��
	//!	@return	�Ȃ�
	//Adia
	private void safeReleaseTemporaryRenderTexture(ref RenderTexture rt)
	{
		if( rt == null ) return;

		RenderTexture.ReleaseTemporary(rt);
		rt = null;
	}
	// �� Add by HexaDrive.
    void Cleanup()
    {
        if (ProfilePerObject)
        {
            SafeDestroy(ProfileCameraGO);
            safeReleaseTemporaryRenderTexture(ref SSS_ProfileTex);
            safeReleaseTemporaryRenderTexture(ref SSS_ProfileTexR);
        }
        SafeDestroy(LightingCameraGO);
        safeReleaseTemporaryRenderTexture(ref LightingTex);
        safeReleaseTemporaryRenderTexture(ref LightingTexR);

        safeReleaseTemporaryRenderTexture(ref LightingTexBlurred);
        safeReleaseTemporaryRenderTexture(ref LightingTexBlurredR);
        //Adia


    }

    //Adia
    void OnDisable()
    {
        Shader.EnableKeyword("SCENE_VIEW");
        Cleanup();
    }

    private void OnGUI()
    {
        if (ShowGUI)
        {
            float verticalDisplacement = 30;
            GUI.color = Color.white;
            Enabled = GUI.Toggle(new Rect(20, verticalDisplacement, 200, 20), Enabled, "Enabled");
            if (LightingTex)
                GUI.Label(new Rect(20, 20 + verticalDisplacement, 200, 20), "Buffer size: " + LightingTex.width + " x " + LightingTex.height);
            GUI.Label(new Rect(20, 40 + verticalDisplacement, 200, 20), "Screen size: " + Screen.width + " x " + Screen.height);
            GUI.Label(new Rect(20, 60 + verticalDisplacement, 200, 20), "Downscale factor: " + Downsampling.ToString("0.0"));
            Downsampling = GUI.HorizontalSlider(new Rect(20, 80 + verticalDisplacement, 200, 20), Downsampling, 1, 4);
            GUI.Label(new Rect(20, 100 + verticalDisplacement, 200, 20), "Blur size: " + ScatteringRadius.ToString("0.0"));
            ScatteringRadius = GUI.HorizontalSlider(new Rect(20, 120 + verticalDisplacement, 200, 20), ScatteringRadius, 0, 10);
            GUI.Label(new Rect(20, 140 + verticalDisplacement, 200, 20), "Postprocess iterations: " + ScatteringIterations);
            ScatteringIterations = (int)GUI.HorizontalSlider(new Rect(20, 160 + verticalDisplacement, 200, 20), ScatteringIterations, 0, 10);
            GUI.Label(new Rect(20, 180 + verticalDisplacement, 200, 20), "Shader iterations per pass: " + ShaderIterations);

            ShaderIterations = (int)GUI.HorizontalSlider(new Rect(20, 200 + verticalDisplacement, 200, 20), ShaderIterations, 0, 20);
            //Adia
            Dither = GUI.Toggle(new Rect(20, 220 + verticalDisplacement, 200, 20), Dither, "Dither");
            if (Dither)
            {
                GUI.Label(new Rect(20, 240 + verticalDisplacement, 200, 20), "Dither scale: " + DitherScale);
                DitherScale = GUI.HorizontalSlider(new Rect(20, 260 + verticalDisplacement, 200, 20), DitherScale, 0, 5);
                GUI.Label(new Rect(20, 280 + verticalDisplacement, 200, 20), "Dither intensity: " + DitherIntensity);
                DitherIntensity = GUI.HorizontalSlider(new Rect(20, 300 + verticalDisplacement, 200, 20), DitherIntensity, 0, .5f);

            }
        }

    }
}
