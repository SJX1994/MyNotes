using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ParticleLoader : MonoBehaviour
{
      
      public Vector4 position;
      public Color32 Color;

      public Texture2D s_atlases;


      void OnEnable()
      {
            Load();
      }
      void Load()
      {
            /*ParticleSystem ps = GetComponent<ParticleSystem>();
            var tsa = ps.textureSheetAnimation;
            tsa.enabled = true;
            tsa.numTilesX = NumTilesX;
            tsa.numTilesY = NumTilesY;
            tsa.animation = ParticleSystemAnimationType.WholeSheet;
            tsa.frameOverTime = new ParticleSystem.MinMaxCurve(CurveConstant);
            tsa.cycleCount = 1;*/

            ParticleSystemRenderer renderer = GetComponent<ParticleSystemRenderer>();
            if (renderer && renderer.sharedMaterial)
            {
                  //Adia
           
                  //Adia
                  

                  renderer.sharedMaterial.mainTexture = ScaleTextureCutOut(s_atlases, position[0], position[1], position[2], position[3]);

                  renderer.sharedMaterial.SetColor("_TintColor", Color);
            }
      }
    Texture2D ScaleTextureCutOut(Texture2D originalTexture, float startX, float startY, float originalWidth, float originalHeight)
    {
        originalWidth = Mathf.Clamp(originalWidth, 0, Mathf.Max(originalTexture.width - startX, 0));
        originalHeight = Mathf.Clamp(originalHeight, 0, Mathf.Max(originalTexture.height - startY, 0));
        Texture2D newTexture = new Texture2D(Mathf.CeilToInt(originalWidth), Mathf.CeilToInt(originalHeight));
        int maxX = originalTexture.width - 1;
        int maxY = originalTexture.height - 1;
        for (int y = 0; y < newTexture.height; y++)
        {
            for (int x = 0; x < newTexture.width; x++)
            {
                float targetX = x + startX;
                float targetY = y + startY;
                int x1 = Mathf.Min(maxX, Mathf.FloorToInt(targetX));
                int y1 = Mathf.Min(maxY, Mathf.FloorToInt(targetY));
                int x2 = Mathf.Min(maxX, x1 + 1);
                int y2 = Mathf.Min(maxY, y1 + 1);

                float u = targetX - x1;
                float v = targetY - y1;
                float w1 = (1 - u) * (1 - v);
                float w2 = u * (1 - v);
                float w3 = (1 - u) * v;
                float w4 = u * v;
                Color color1 = originalTexture.GetPixel(x1, y1);
                Color color2 = originalTexture.GetPixel(x2, y1);
                Color color3 = originalTexture.GetPixel(x1, y2);
                Color color4 = originalTexture.GetPixel(x2, y2);
                Color color = new Color(Mathf.Clamp01(color1.r * w1 + color2.r * w2 + color3.r * w3 + color4.r * w4),
                                        Mathf.Clamp01(color1.g * w1 + color2.g * w2 + color3.g * w3 + color4.g * w4),
                                        Mathf.Clamp01(color1.b * w1 + color2.b * w2 + color3.b * w3 + color4.b * w4),
                                        Mathf.Clamp01(color1.a * w1 + color2.a * w2 + color3.a * w3 + color4.a * w4)
                                        );
                newTexture.SetPixel(x, y, color);
            }
        }
        newTexture.anisoLevel = 2;
        newTexture.Apply();
        return newTexture;
    }
    //Adia
    void Start()
      {

      }

      //Adia
      void Update()
      {

      }
}
