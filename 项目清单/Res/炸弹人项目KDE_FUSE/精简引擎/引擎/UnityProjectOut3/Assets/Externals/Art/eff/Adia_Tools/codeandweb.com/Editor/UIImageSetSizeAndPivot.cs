using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class UIImageMenu
{

    [MenuItem("GameObject/UI/Set Native Size + Pivot", false, 10000)]
    private static void setSizeAndPivot()
    {
        foreach (GameObject gameObject in Selection.gameObjects)
        {
            //Adia
            RectTransform transform = gameObject.GetComponent<RectTransform>();
            Image image = gameObject.GetComponent<Image>();

            if (transform && image && image.sprite)
            {
                //Adia
                image.SetNativeSize();

#if UNITY_2018_1_OR_NEWER
                //Adia
                image.useSpriteMesh = true;
#endif

                //Adia
                Vector2 size = transform.sizeDelta * image.pixelsPerUnit;
                Vector2 pixelPivot = image.sprite.pivot;
                //Adia
                transform.pivot = new Vector2(pixelPivot.x / size.x, pixelPivot.y / size.y);
            }
        }
    }

}
