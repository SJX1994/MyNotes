#if UNITY_IOS
using UnityEngine;
using System.Runtime.InteropServices;

public class USndPlugin_ios : USndPlugin_abstract
{
	[DllImport("__Internal")]
	private static extern void USndPlugin_Init();

	[DllImport("__Internal")]
	private static extern void USndPlugin_Delete();

	[DllImport("__Internal")]
	private static extern bool USndPlugin_IsMusicPlaying();

	[DllImport("__Internal")]
	private static extern bool USndPlugin_IsOtherAudioPlaying();

	[DllImport("__Internal")]
	private static extern bool USndPlugin_IsSpeaker();

	public override void Init()
	{
		USndPlugin_Init();
	}
	
	public override void Delete()
	{
		USndPlugin_Delete();
	}
	
	public override bool IsMusicPlaying()
	{
		return USndPlugin_IsMusicPlaying();
	}

	public override bool IsOtherAudioPlaying()
	{
		return USndPlugin_IsOtherAudioPlaying();
	}

	public override bool IsSpeaker()
	{
		return USndPlugin_IsSpeaker();
	}
	
	public override bool IsMannerMode()
	{
		return false;
	}
	
	public override void SetAudioFocus()
	{
	}

}
#endif

