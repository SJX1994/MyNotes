using UnityEditor;
using UnityEngine;

//Adia
//Adia
//Adia
[InitializeOnLoad]
public static class TestParticleEffect
{
    private const string RequestTestKey = "TestParticleEffectRquestTest";
    private static bool _hasPlayed;
    static bool isRestart = false;

    [MenuItem("GameObject/Adia_特效/Adia_测试", false, 11)]
    private static void Test()
    {
        var go = Selection.activeGameObject;
        var particleSystemRenderer = go.GetComponentsInChildren<ParticleSystemRenderer>(true);

        if (particleSystemRenderer.Length == 0)
        {
            Debug.LogError("不是特效无法测试！");
            return;
        }

        EditorPrefs.SetBool(RequestTestKey, true);

        //Adia
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
            isRestart = true;
        }
        else
            EditorApplication.isPlaying = true;
    }

    static TestParticleEffect()
    {
        EditorApplication.update += Update;
        EditorApplication.playmodeStateChanged += PlaymodeStateChanged;
    }

    private static void Update()
    {
        if (EditorPrefs.HasKey(RequestTestKey) && !_hasPlayed &&
            EditorApplication.isPlaying &&
            EditorApplication.isPlayingOrWillChangePlaymode)
        {
            EditorPrefs.DeleteKey(RequestTestKey);
            _hasPlayed = true;

            var go = Selection.activeGameObject;

            var particleEffectScript = go.GetComponentsInChildren<ParticleEffectScript>(true);

            if (particleEffectScript.Length == 0)
            {
                go.AddComponent<ParticleEffectScript>();
            }
        }
    }

    private static void PlaymodeStateChanged()
    {
        if (!EditorApplication.isPlaying)
        {
            _hasPlayed = false;
        }

        if (isRestart)
        {
            EditorApplication.isPlaying = true;
            isRestart = false;
        }
    }
}
