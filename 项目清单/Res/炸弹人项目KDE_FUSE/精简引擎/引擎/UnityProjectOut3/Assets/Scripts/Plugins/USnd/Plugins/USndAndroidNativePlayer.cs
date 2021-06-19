using UnityEngine;
using System.Runtime.InteropServices;

public class USndAndroidNativePlayer
{
    private static USndAndroidNativePlayer player = null;

#if UNITY_ANDROID || !UNITY_EDITOR
	static AndroidJavaObject plugin = null;
#endif

    static public void Initialize(int maxNum)
    {
        if (player == null)
        {
            player = new USndAndroidNativePlayer();

#if UNITY_ANDROID || !UNITY_EDITOR
            if (Application.platform == RuntimePlatform.Android)
            {
                if (plugin == null)
                {
                    plugin = new AndroidJavaObject("com.konami.usndplugin.USndSoundPoolPlayer");
                    plugin.Call("Init", maxNum);
                }
            }
#endif
        }
    }
    
    static public void Terminate()
    {
    	if ( player != null )
    	{
#if UNITY_ANDROID || !UNITY_EDITOR
            if (plugin != null)
            {
                plugin.Call("Release");
                plugin = null;
            }
#endif
    		player = null;
    	}
    }
    
    static public int LoadData(string saveName, string className, string funcName)
    {
#if UNITY_ANDROID || !UNITY_EDITOR
        if (plugin == null) return 0;
        return plugin.Call<int>("LoadSound", className, funcName, saveName);
#else
		return 0;
#endif
    }
    
    //Adia
    static public int Play(int soundId, float volume, float rate)
    {
#if UNITY_ANDROID || !UNITY_EDITOR
        if (plugin == null) return 0;
        return plugin.Call<int>("Play", soundId, volume, rate);
#else
		return 0;
#endif
    }
    
    static public void Stop(int streamId)
    {
#if UNITY_ANDROID || !UNITY_EDITOR
        if (plugin == null) return;
        plugin.Call("Stop", streamId);
#endif
    }
    
    static public void Unload(int soundId)
    {
#if UNITY_ANDROID || !UNITY_EDITOR
        if (plugin == null) return;
        plugin.Call("Unload", soundId);
#endif
    }
    
}


