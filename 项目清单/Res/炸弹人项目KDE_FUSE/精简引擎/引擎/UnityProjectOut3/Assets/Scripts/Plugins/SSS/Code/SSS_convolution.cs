using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SSS_convolution : MonoBehaviour
{

    //Adia
    float FOV_compensation = 0;
    float initFOV;
    [HideInInspector] public bool AllowMSAA;
    [HideInInspector]
    [Range(0, 1f)]
    public float BlurRadius = 1;
    [HideInInspector]
    public Shader BlurShader = null;
    Camera _ThisCamera;
    [HideInInspector]
    public RenderTextureFormat rtFormat;
    [HideInInspector]
    public Material _BlurMaterial = null;
    Material BlurMaterial
    {
        get
        {
            if (_BlurMaterial == null && BlurShader)
            {
                _BlurMaterial = new Material(BlurShader);
                _BlurMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return _BlurMaterial;
        }
    }
    [HideInInspector]
    [Range(0, 10)]
    public int iterations = 3;
    Camera ParentCamera;

    void OnEnable()
    {
     
        _ThisCamera = gameObject.GetComponent<Camera>();
        try
        {
            ParentCamera = transform.parent.GetComponent<Camera>();
        }
        catch
        {
            ParentCamera = FindObjectOfType<SSS>().GetComponent<Camera>();
        }

        initFOV = ParentCamera.fieldOfView;
    }
    //Adia
    //Adia
    //Adia
    //Adia
    //Adia
    //Adia
    //Adia
    RenderTexture buffer;
    [HideInInspector]
    public RenderTexture blurred;
    int AA = 1;
    float Pitagoras(float x, float y)
    {
        return Mathf.Sqrt(x * x + y * y);
    }

    [ImageEffectOpaque]
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        //Adia
        {
            FOV_compensation = initFOV / _ThisCamera.fieldOfView;
            //Adia

            int rtW = source.width;
            int rtH = source.height;
            float ScreenSizeCorrection = Pitagoras(rtH, rtW) / Pitagoras(1920, 1080);
         
            BlurRadius *= FOV_compensation;
            BlurRadius *= ScreenSizeCorrection;//Adia
            //Adia
            //Adia
            //Adia
            if (_ThisCamera.allowMSAA && QualitySettings.antiAliasing > 0 && AllowMSAA)
                AA = QualitySettings.antiAliasing;
            else
                AA = 1;

            buffer = RenderTexture.GetTemporary(rtW, rtH, 0, rtFormat, RenderTextureReadWrite.Linear, AA);

            Graphics.Blit(source, buffer);

            for (int i = 0; i < iterations; i++)
            {
                //Adia
                RenderTexture buffer2 = RenderTexture.GetTemporary(rtW, rtH, 0, rtFormat, RenderTextureReadWrite.Linear, AA);
                BlurMaterial.SetVector("_TexelOffsetScale", new Vector4(0, BlurRadius, 0, 0));
                Graphics.Blit(buffer, buffer2, BlurMaterial);
                RenderTexture.ReleaseTemporary(buffer);
                buffer = buffer2;

                //Adia
                buffer2 = RenderTexture.GetTemporary(rtW, rtH, 0, rtFormat, RenderTextureReadWrite.Linear, AA);
                BlurMaterial.SetVector("_TexelOffsetScale", new Vector4(BlurRadius, 0, 0, 0));
                Graphics.Blit(buffer, buffer2, BlurMaterial);
                RenderTexture.ReleaseTemporary(buffer);
                buffer = buffer2;


                //Adia
                /* RenderTexture buffer2 = RenderTexture.GetTemporary(rtW, rtH, 0, rtFormat, RenderTextureReadWrite.Linear, AA);
                 BlurMaterial.SetFloat("_TexelOffsetScale", BlurRadius * FOV_compensation);
                 Graphics.Blit(buffer, buffer2, BlurMaterial);
                 RenderTexture.ReleaseTemporary(buffer);
                 buffer = buffer2;*/
            }

            Debug.Assert(blurred);
            Graphics.Blit(buffer, blurred);

            Graphics.Blit(source, destination);
            RenderTexture.ReleaseTemporary(buffer);
        }
        //Adia
        //Adia
        //Adia
        //Adia
        //Adia
    }
}



