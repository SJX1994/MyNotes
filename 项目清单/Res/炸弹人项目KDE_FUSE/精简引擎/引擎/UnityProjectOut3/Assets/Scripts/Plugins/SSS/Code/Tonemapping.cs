using System.Collections;
using UnityEngine.Rendering;
using UnityEngine;

[ExecuteInEditMode]
[ImageEffectAllowedInSceneView]
public class Tonemapping : MonoBehaviour
{
    [Range(1, 3)] [SerializeField] float Exposure = 1.2f;
    [Range(1, 3)] [SerializeField] float Contrast = 1.2f;
    Material TonemappingMaterial;


    private void OnEnable()
    {

        if (TonemappingMaterial == null)
            TonemappingMaterial = new Material(Shader.Find("Hidden/Tonemapping"));
        TonemappingMaterial.hideFlags = HideFlags.HideAndDontSave;
    }
    private void Update()
    {
        if (TonemappingMaterial)
        {
            TonemappingMaterial.SetFloat("Exposure", Exposure);
            TonemappingMaterial.SetFloat("Contrast", Contrast);
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, TonemappingMaterial);
    }

}
