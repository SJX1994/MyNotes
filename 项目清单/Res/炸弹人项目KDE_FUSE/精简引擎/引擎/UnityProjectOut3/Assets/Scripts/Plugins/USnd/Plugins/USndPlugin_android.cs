#if UNITY_ANDROID

using UnityEngine;
using System.Runtime.InteropServices;

public class USndPlugin_android : USndPlugin_abstract
{

	static AndroidJavaObject plugin = null;

	public override void Init()
	{
    }

    public void SetCallback(string objName, string funcName)
    {
		plugin = new AndroidJavaObject("com.konami.usndplugin.USndHeadsetReceiver");
		if ( plugin != null )
		{
			try {
                plugin.Call("SetHeadsetPlugCallback", objName, funcName);
			} catch {
			}
		}
		else
		{
			//Adia
		}
	}
	
	public override void Delete()
	{

	}

    public void GetMusicStatus()
    {
        /*
        System.IntPtr am = (System.IntPtr)AndroidJNI.FindClass("android/media/AudioManager");
        System.IntPtr method = AndroidJNI.GetMethodID(am, "isMusicActive", "()Z");

        jvalue[] tmp = new jvalue[1];
        System.IntPtr obj = AndroidJNI.NewObject(am, method, tmp);
        bool status = AndroidJNI.CallBooleanMethod(obj, method, tmp);

        AndroidJNI.DeleteLocalRef(obj);
        musicStatus = status;
         * */
    }
	
	public override bool IsMusicPlaying()
	{
        return false;
        //Adia
	}

	public override bool IsOtherAudioPlaying()
	{
		return false;
	}

	public override bool IsSpeaker()
	{
/*
        System.IntPtr am = (System.IntPtr)AndroidJNI.FindClass("android/media/AudioManager");
        System.IntPtr method = AndroidJNI.GetMethodID(am, "isWiredHeadsetOn", "()Z");

        jvalue[] tmp = new jvalue[1];
        System.IntPtr obj = AndroidJNI.NewObject(am, method, tmp);

        bool status = AndroidJNI.CallBooleanMethod(obj, method, tmp);
        AndroidJNI.DeleteLocalRef(obj);
        
        //Adia
        if (status == true) return false;

        //Adia
        am = (System.IntPtr)AndroidJNI.FindClass("android/media/AudioManager");
        method = AndroidJNI.GetMethodID(am, "isBluetoothA2dpOn", "()Z");

        tmp = new jvalue[1];
        obj = AndroidJNI.NewObject(am, method, tmp);

        status = AndroidJNI.CallBooleanMethod(obj, method, tmp);
        AndroidJNI.DeleteLocalRef(obj);

        return status ? false: true;
*/
		bool status = false;
		if ( plugin != null )
		{
			try {
                status = plugin.Call<bool>("IsSpeakerOut");
			} catch {
			}
		}
		return status;
	}
	
	public override bool IsMannerMode()
	{
/*
		System.IntPtr am	= (System.IntPtr)AndroidJNI.FindClass("android/media/AudioManager");
        System.IntPtr method = AndroidJNI.GetMethodID(am, "getRingerMode", "()I");

        jvalue[] tmp = new jvalue[1];
        System.IntPtr obj = AndroidJNI.NewObject(am, method, tmp);
        
        int status = AndroidJNI.CallIntMethod(obj, method, tmp);
        AndroidJNI.DeleteLocalRef(obj);
        if (status == 0 || status == 1)
		{
			return true;
		}
*/
		int status = 0;
		if ( plugin != null )
		{
			try {
                status = plugin.Call<int>("GetRingerMode");
			} catch {
			}
		}
		if (status == 0 || status == 1)
		{
			return true;
		}

        return false;
	}
	
	public override void SetAudioFocus()
	{
		if ( plugin != null )
		{
			plugin.Call("SetAudioFocus");
		}
	}

}
#endif
