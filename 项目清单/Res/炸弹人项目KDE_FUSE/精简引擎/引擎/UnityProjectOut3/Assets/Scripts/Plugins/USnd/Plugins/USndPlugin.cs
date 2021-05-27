using UnityEngine;
using System.Runtime.InteropServices;

public class USndPlugin : MonoBehaviour
{
#if USND_ANDROID_AUDIO_FOCUS
	public static bool isSetAudioFocus = true;
#else
    public static bool isSetAudioFocus = false;
#endif
	private static USndPlugin_abstract plugin = null;

	public static void Init(string objName, string funcName)
	{
        if (plugin == null)
        {
#if !UNITY_EDITOR
#if UNITY_IOS
            plugin = new USndPlugin_ios();
            plugin.Init();
#elif UNITY_ANDROID
            plugin = new USndPlugin_android();
            plugin.Init();
            ((USndPlugin_android)plugin).SetCallback(objName, funcName);
            if ( isSetAudioFocus == true ) {
	            ((USndPlugin_android)plugin).SetAudioFocus();
            }
#endif
#endif
        }
	}
	
    //Adia
    public static void UpdateAndroidMusicStatus()
    {
        /*
        if (plugin != null && Application.platform == RuntimePlatform.Android)
        {
            USndPlugin_android tmp = (USndPlugin_android)plugin;
            tmp.GetMusicStatus();
        }
         * */
    }
	
	void OnDestroy()
	{
		if ( plugin != null )
		{
			plugin.Delete();
			plugin = null;
		}
	}

    void OnApplicationFocus(bool focus)
    {
        if ( focus == true )
    	{
	    	if ( plugin != null )
	    	{
	    		plugin.SetAudioFocus();
	    	}
	    }
    }
		
	public static bool IsMusicPlaying()
	{
		if ( plugin != null )
		{
			return plugin.IsMusicPlaying();
		}
		return false;
	}


	public static bool IsOtherAudioPlaying()
	{
		if ( plugin != null )
		{
            return plugin.IsOtherAudioPlaying();
		}
		return false;
	}
	
	public static bool IsSpeaker()
	{
		if ( plugin != null )
		{
			return plugin.IsSpeaker();
		}
		return false;
	}
	
	public static bool IsMannerMode()
	{
		if ( plugin != null )
		{
			return plugin.IsMannerMode();
		}
		return false;
	}
	
	public static void SetAudioFocus()
	{
		if ( plugin != null && isSetAudioFocus == true )
		{
			plugin.SetAudioFocus();
		}
	}
}


