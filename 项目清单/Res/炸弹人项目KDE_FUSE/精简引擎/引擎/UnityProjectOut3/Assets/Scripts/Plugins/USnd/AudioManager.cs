//Adia
//Adia

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System;
using UnityEngine.Audio;

namespace USnd
{

    public partial class AudioManager : MonoBehaviour
    {

        private static AudioManager manager = null;

        static public bool IsInitialized()
        {
            return manager ? true : false;
        }

        static public void Initialize(int defaultSampleRate = 0)
        {
            if ( manager == null )
            {
                if (defaultSampleRate >= 0)
                {
                    AudioConfiguration config = AudioSettings.GetConfiguration();
                    config.sampleRate = (defaultSampleRate == 0) ? AudioDefine.DEFAULT_SAMPLE_RATE : defaultSampleRate;
                    AudioSettings.Reset(config);
                }
                GameObject obj = new GameObject();
                manager = obj.AddComponent<AudioManager>();
                DontDestroyOnLoad(manager);
            }
        }

        static public void Terminate()
        {
            if ( manager != null )
            {
                manager.removeAll();
                AudioMainPool.Terminate();
                Destroy(manager.gameObject);
                Destroy(manager);
                manager = null;
            }
        }

        //Adia
        //Adia
        static public void SetAudioMixer(AudioMixer mixer)
        {
            if ( manager != null )
            {
                manager.setAudioMixer(mixer);
            }
        }

        //Adia
        //Adia
        static public void UnsetAudioMixer()
        {
            if (manager != null)
            {
                manager.unsetAudioMixer();
            }
        }

        //Adia
        //Adia
        static public void SetSnapshot(string snapName, float time)
        {
            if (manager != null)
            {
                manager.setSnapshot(snapName, time);
            }
        }

        //Adia
        //Adia
        static public void SetAudioMixerExposedParam(string paramName, float value)
        {
            if (manager != null)
            {
                manager.setAudioMixerExposedParam(paramName, value);
            }
        }

        //Adia
        //Adia
        static public void SetAudio3DSettingsFromJson(string jsonStr)
        {
            if (manager != null)
            {
                manager.setAudio3DSettingsFromJson(jsonStr);
            }
        }

        //Adia
        //Adia
        static public void SetAudio3DSettings(Audio3DSettings setting)
        {
            if (manager != null)
            {
                manager.setAudio3DSettings(setting);
            }
        }

        //Adia
        //Adia
        static public void SetAudio3DSettings(Audio3DSettings[] settings)
        {
            if (manager != null)
            {
                manager.setAudio3DSettings(settings);
            }
        }

        //Adia
        //Adia
        static public bool LoadBinaryTable(byte[] tableData, int loadId = 0)
        {
            if ( manager != null )
            {
                return manager.loadBinaryTable(tableData, loadId);
            }
            return false;
        }

        //Adia
        //Adia
        static public bool LoadJson(string tableData, int loadId = 0)
        {
            if (manager != null)
            {
                return manager.loadJson(tableData, loadId);
            }
            return false;
        }

        //Adia
        //Adia
        static public void AddAudioClip(AudioClip[] clips)
        {
            if (manager != null)
            {
                manager.addAudioClip(clips);
            }
        }

        //Adia
        //Adia
        static public void AddAudioClip(AudioClip clip)
        {
            if (manager != null)
            {
                manager.addAudioClip(clip);
            }
        }

        //Adia
        //Adia
        static public bool IsExistAudioClip(string clipName)
        {
            if ( manager != null )
            {
                return manager.isExistAudioClip(clipName);
            }
            return false;
        }

        //Adia
        //Adia
        static public void RemoveAudioClip(string clipName)
        {
            if (manager != null)
            {
                manager.removeAudioClip(clipName);
            }
        }

        //Adia
        //Adia
        static public void RemoveAudioClipAll()
        {
            if (manager != null)
            {
                manager.removeAudioClipAll();
            }
        }

        //Adia
        //Adia
        static public bool FindLabel(string name)
        {
            if (manager != null)
            {
                return manager.findLabel(name);
            }
            return false;
        }

        //Adia
        //Adia
        static public bool FindCategory(string name)
        {
            if (manager != null)
            {
                return manager.findCategory(name);
            }
            return false;
        }

        //Adia
        //Adia
        static public bool FindMaster(string name)
        {
            if (manager != null)
            {
                return manager.findMaster(name);
            }
            return false;
        }

        //Adia
        //Adia
        static public bool CanRemoveLabel(string labelName)
        {
            if (manager != null)
            {
                return manager.canRemoveLabel(labelName);
            }
            return false;
        }

        //Adia
        //Adia
        static public bool UnsetAudioClipToLabel(string labelName)
        {
            if (manager != null)
            {
                return manager.unsetAudioClipToLabel(labelName);
            }
            return false;
        }

        //Adia
        //Adia
        static public void UnsetAudioClipToLabelLoadId(int loadId)
        {
            if (manager != null)
            {
                manager.unsetAudioClipToLabelLoadId(loadId);
            }
        }

        //Adia
        //Adia
        static public void UnsetAudioClipToLabelAll()
        {
            if (manager != null)
            {
                manager.unsetAudioClipToLabelAll();
            }
        }

        //Adia
        //Adia
        static public bool RemoveLabel(string labelName)
        {
            if (manager != null)
            {
                return manager.removeLabel(labelName);
            }
            return false;
        }

        //Adia
        //Adia
        static public void RemoveLabelLoadId(int loadId)
        {
            if (manager != null)
            {
                manager.removeLabelLoadId(loadId);
            }
        }

        //Adia
        //Adia
        static public void RemoveLabelAll()
        {
            if (manager != null)
            {
                manager.removeLabelAll();
            }
        }

        //Adia
        //Adia
        static public void RemoveAll()
        {
            if (manager != null)
            {
                manager.removeAll();
            }
        }


        //Adia
        //Adia
        static public void UpdateRandomSourceInfo(string labelName)
        {
            if (manager != null)
            {
                manager.updateRandomSourceInfo(labelName);
            }
        }

        //Adia
        //Adia
        static public void UpdateRandomSourceInfoAll()
        {
            if ( manager != null )
            {
                manager.updateRandomSourceInfoAll();
            }
        }

        //Adia
        //Adia
        static public void LoadAudioData(string labelName)
        {
            if (manager != null)
            {
                manager.loadAudioData(labelName);
            }
        }

        //Adia
        //Adia
        static public void LoadAudioDataLoadId(int loadId)
        {
            if (manager != null)
            {
                manager.loadAudioDataLoadId(loadId);
            }
        }

        //Adia
        //Adia
        static public void UnloadAudioData(string labelName)
        {
            if (manager != null)
            {
                manager.unloadAudioData(labelName);
            }
        }

        //Adia
        //Adia
        static public void UnloadAudioDataAll()
        {
            if (manager != null)
            {
                manager.unloadAudioDataAll();
            }
        }

        //Adia
        //Adia
        static public void UnloadAudioDataLoadId(int loadId)
        {
            if (manager != null)
            {
                manager.unloadAudioDataLoadId(loadId);
            }
        }

        //Adia
        //Adia
        static public AudioDefine.LOAD_XML_STATUS GetLoadXmlStatus()
        {
            if (manager != null)
            {
                return manager.getLoadXmlStatus();
            }
            return AudioDefine.LOAD_XML_STATUS.ERROR;
        }

        //Adia
        //Adia
        static public AudioDefine.LOAD_JSON_STATUS GetLoadJsonStatus()
        {
            if (manager != null)
            {
                return manager.getLoadJsonStatus();
            }
            return AudioDefine.LOAD_JSON_STATUS.ERROR;
        }

        //Adia
        //Adia
        static public bool LoadMasterXml(Stream xml, Stream xsd = null)
        {
            if (manager != null)
            {
                return manager.loadMasterXml(xml, xsd);
            }
            return false;
        }

        //Adia
        //Adia
        static public bool LoadCategoryXml(Stream xml, Stream xsd = null)
        {
            if (manager != null)
            {
                return manager.loadCategoryXml(xml, xsd);
            }
            return false;
        }

        //Adia
        //Adia
        static public bool LoadLabelXml(int loadId, Stream xml, Stream xsd = null)
        {
            if (manager != null)
            {
                return manager.loadLabelXml(loadId, xml, xsd);
            }
            return false;
        }

        //Adia
        //Adia
        static public void SetDucking(string categoryName, float targetVolumeFactor, float fadeTime)
        {
            if (manager != null)
            {
                manager.setDucking(categoryName, targetVolumeFactor, fadeTime);
            }
        }

        //Adia
        //Adia
        static public void ResetDucking(string categoryName, float fadeTime)
        {
            if (manager != null)
            {
                manager.resetDucking(categoryName, fadeTime);
            }
        }

        //Adia
        //Adia
        static public void ResetDuckingAll(float fadeTime)
        {
            if (manager != null)
            {
                manager.resetDuckingAll(fadeTime);
            }
        }

        //Adia
        //Adia
        static public void ForceResetDucking(string categoryName, float fadeTime)
        {
            if (manager != null)
            {
                manager.forceResetDucking(categoryName, fadeTime);
            }
        }

        //Adia
        //Adia
        static public void ForceResetDuckingAll(float fadeTime)
        {
            if (manager != null)
            {
                manager.forceResetDuckingAll(fadeTime);
            }
        }

        //Adia
        //Adia
        static public int Play(string labelName, float delay = -1)
        {
            if (manager != null)
            {
                return manager.play(labelName, delay);
            }
            return AudioDefine.INSTANCE_ID_ERROR;
        }

        //Adia
        //Adia
        static public int PlayOption(string labelName, float volume, float fadeTime, float pan, int pitch, float delay = -1)
        {
            if (manager != null)
            {
                return manager.playOption(labelName, volume, fadeTime, pan, pitch, delay);
            }
            return AudioDefine.INSTANCE_ID_ERROR;
        }


        //Adia
        //Adia
        static public int Prepare(string labelName)
        {
            if (manager != null)
            {
                return manager.prepare(labelName);
            }
            return AudioDefine.INSTANCE_ID_ERROR;
        }

        //Adia
        //Adia
        static public int PrepareOption(string labelName, float volume, float fadeTime, float pan, int pitch)
        {
            if (manager != null)
            {
                return manager.prepareOption(labelName, volume, fadeTime, pan, pitch, false);
            }
            return AudioDefine.INSTANCE_ID_ERROR;
        }

        //Adia
        //Adia
        static public void PlayInstance(int instanceId, float delay = -1)
        {
            if (manager != null)
            {
                manager.playInstance(instanceId, delay);
            }
        }

        //Adia
        //Adia
        static public int Play3D(string labelName, GameObject target, float delay = -1)
        {
            if (manager != null)
            {
                return manager.play3D(labelName, target, delay);
            }
            return AudioDefine.INSTANCE_ID_ERROR;
        }

        //Adia
        //Adia
        static public int Play3D(string labelName, Vector3 position, float delay = -1)
        {
            if (manager != null)
            {
                return manager.play3D(labelName, position, delay);
            }
            return AudioDefine.INSTANCE_ID_ERROR;
        }

        //Adia
        //Adia
        static public int Play3D(string labelName, Transform target, float delay = -1)
        {
            if (manager != null)
            {
                return manager.play3D(labelName, target, delay);
            }
            return AudioDefine.INSTANCE_ID_ERROR;
        }

        //Adia
        //Adia
        static public int Play2D(string labelName, float delay = -1)
        {
            if (manager != null)
            {
                return manager.play2D(labelName, delay);
            }
            return AudioDefine.INSTANCE_ID_ERROR;
        }

        //Adia
        //Adia
        static public void SetTrackingObject(int instanceId, GameObject target)
        {
            if (manager != null)
            {
                manager.setTrackingObject(instanceId, target);
            }
        }


        //Adia
        //Adia
        static public void SetTrackingObject(int instanceId, Transform target)
        {
            if (manager != null)
            {
                manager.setTrackingObject(instanceId, target);
            }
        }

        //Adia
        //Adia
        static public void Stop(int instanceId, float fadeTime = -1)
        {
            if (manager != null)
            {
                manager.stop(instanceId, fadeTime);
            }
        }

        //Adia
        //Adia
        static public void StopLabel(string labelName, float fadeTime = -1)
        {
            if (manager != null)
            {
                manager.stopLabel(labelName, fadeTime);
            }
        }

        //Adia
        //Adia
        static public void StopAll(float fadeTime = -1)
        {
            if (manager != null)
            {
                manager.stopAll(fadeTime);
            }
        }
        
        //Adia
        //Adia
        static public void OnPause(int instanceId, float fadeTime = -1)
        {
            if (manager != null)
            {
                manager.onPause(instanceId, fadeTime);
            }
        }

        //Adia
        //Adia
        static public void OnPauseAll(float fadeTime = -1)
        {
            if (manager != null)
            {
                manager.onPauseAll(fadeTime);
            }
        }

        //Adia
        //Adia
        static public void OffPause(int instanceId, float fadeTime = -1)
        {
            if (manager != null)
            {
                manager.offPause(instanceId, fadeTime);
            }
        }

        //Adia
        //Adia
        static public void OffPauseAll(float fadeTime = -1)
        {
            if (manager != null)
            {
                manager.offPauseAll(fadeTime);
            }
        }

        //Adia
        //Adia
        static public void SetVolume(int instanceId, float newVolume, float moveTime)
        {
            if (manager != null)
            {
                manager.setVolume(instanceId, newVolume, moveTime);
            }
        }

        //Adia
        //Adia
        static public void SetVolume(string labelName, float newVolume, float moveTime)
        {
            if (manager != null)
            {
                manager.setVolume(labelName, newVolume, moveTime);
            }
        }

        //Adia
        //Adia
        static public void SetPitch(int instanceId, int newPitch, float moveTime)
        {
            if (manager != null)
            {
                manager.setPitch(instanceId, newPitch, moveTime);
            }
        }

        //Adia
        //Adia
        static public void SetPitch(string labelName, int newPitch, float moveTime)
        {
            if (manager != null)
            {
                manager.setPitch(labelName, newPitch, moveTime);
            }
        }

        //Adia
        //Adia
        static public void SetPan(int instanceId, float newPan, float moveTime)
        {
            if (manager != null)
            {
                manager.setPan(instanceId, newPan, moveTime);
            }
        }

        //Adia
        //Adia
        static public void SetPan(string labelName, float newPan, float moveTime)
        {
            if (manager != null)
            {
                manager.setPan(labelName, newPan, moveTime);
            }
        }

        //Adia
        //Adia
        static public void SetPosition(int instanceId, Vector3 position)
        {
            if (manager != null)
            {
                manager.setPosition(instanceId, position);
            }
        }

        //Adia
        //Adia
        static public void SetPosition(string labelName, Vector3 position)
        {
            if (manager != null)
            {
                manager.setPosition(labelName, position);
            }
        }

        //Adia
        //Adia
        static public void ResetPlayPosition(string labelName)
        {
            if (manager != null)
            {
                manager.resetPlayPosition(labelName);
            }
        }

        //Adia
        //Adia
        static public void ResetPlayPositionAll()
        {
            if (manager != null)
            {
                manager.resetPlayPositionAll();
            }
        }

        //Adia
        //Adia
        static public float GetInstanceVolume(int instanceId)
        {
            if (manager != null)
            {
                return manager.getInstanceVolume(instanceId);
            }
            return 0;
        }

        //Adia
        //Adia
        static public float GetInstanceCalcVolume(int instanceId)
        {
            if (manager != null)
            {
                return manager.getInstanceCalcVolume(instanceId);
            }
            return 0;
        }

        //Adia
        //Adia
        static public void SetMasterVolume(string masterName, float volume, float moveTime = 0)
        {
            if (manager != null)
            {
                manager.setMasterVolume(masterName, volume, moveTime);
            }
        }

        //Adia
        //Adia
        static public float GetMasterVolume(string masterName)
        {
            if (manager != null)
            {
                return manager.getMasterVolume(masterName);
            }
            return 1;
        }

        //Adia
        //Adia
        static public void SetCategoryVolume(string categoryName, float volume, float moveTime = 0)
        {
            if (manager != null)
            {
                manager.setCategoryVolume(categoryName, volume, moveTime);
            }
        }

        //Adia
        //Adia
        static public float GetCategoryVolume(string categoryName)
        {
            if (manager != null)
            {
                return manager.getCategoryVolume(categoryName);
            }
            return 0;
        }

        //Adia
        //Adia
        static public float GetLabelVolume(string labelName)
        {
            if (manager != null)
            {
                return manager.getCategoryVolume(labelName);
            }
            return 0;
        }

        //Adia
        //Adia
        static public void StopMaster(string masterName, float fadeTime = -1)
        {
            if (manager != null)
            {
                manager.stopMaster(masterName, fadeTime);
            }
        }

        //Adia
        //Adia
        static public void OnPauseMaster(string masterName, float fadeTime = -1)
        {
            if (manager != null)
            {
                manager.onPauseMaster(masterName, fadeTime);
            }
        }


        //Adia
        //Adia
        static public void OffPauseMaster(string masterName, float fadeTime = -1)
        {
            if (manager != null)
            {
                manager.offPauseMaster(masterName, fadeTime);
            }
        }

        //Adia
        //Adia
        static public void StopCategory(string categoryName, float fadeTime = -1)
        {
            if (manager != null)
            {
                manager.stopCategory(categoryName, fadeTime);
            }
        }

        //Adia
        //Adia
        static public void OnPauseLabel(string labelName, float fadeTime = -1)
        {
            if (manager != null)
            {
                manager.onPauseLabel(labelName, fadeTime);
            }
        }

        //Adia
        //Adia
        static public void OffPauseLabel(string labelName, float fadeTime = -1)
        {
            if (manager != null)
            {
                manager.offPauseLabel(labelName, fadeTime);
            }
        }

        //Adia
        //Adia
        static public void OnPauseCategory(string categoryName, float fadeTime = -1)
        {
            if (manager != null)
            {
                manager.onPauseCategory(categoryName, fadeTime);
            }
        }

        //Adia
        //Adia
        static public void OffPauseCategory(string categoryName, float fadeTime = -1)
        {
            if (manager != null)
            {
                manager.offPauseCategory(categoryName, fadeTime);
            }
        }

        //Adia
        //Adia
        static public AudioDefine.INSTANCE_STATUS GetInstanceStatus(int instanceId)
        {
            if (manager != null)
            {
                return manager.getInstanceStatus(instanceId);
            }
            return AudioDefine.INSTANCE_STATUS.STOP;
        }

        //Adia
        //Adia
        static public bool IsPlayingLabel(string labelName)
        {
            if (manager != null)
            {
                return manager.isPlayingLabel(labelName);
            }
            return false;
        }

        //Adia
        //Adia
        static public int GetLabelNum()
        {
            if (manager != null)
            {
                return manager.getLabelNum();
            }
            return 0;
        }

        //Adia
        //Adia
        static public string[] GetLabelNameList()
        {
            if (manager != null)
            {
                return manager.getLabelNameList();
            }
            return null;
        }

        //Adia
        //Adia
        static public int GetCategoryNum()
        {
            if (manager != null)
            {
                return manager.getCategoryNum();
            }
            return 0;
        }

        //Adia
        //Adia
        static public string[] GetCategoryNameList()
        {
            if (manager != null)
            {
                return manager.getCategoryNameList();
            }
            return null;
        }

        //Adia
        //Adia
        static public int GetMasterNum()
        {
            if (manager != null)
            {
                return manager.getMasterNum();
            }
            return 0;
        }

        //Adia
        //Adia
        static public string[] GetMasterNameList()
        {
            if (manager != null)
            {
                return manager.getMasterNameList();
            }
            return null;
        }

        //Adia
        //Adia
        static public string GetCategoryNameSettingOfLabel(string labelName)
        {
            if (manager != null)
            {
                return manager.getCategoryNameSettingOfLabel(labelName);
            }
            return null;
        }

        //Adia
        //Adia
        static public string GetMasterNameSettingOfCategory(string categoryName)
        {
            if (manager != null)
            {
                return manager.getMasterNameSettingOfCategory(categoryName);
            }
            return null;
        }

        //Adia
        //Adia
        static public float GetPlayTime(int instanceId)
        {
            if (manager != null)
            {
                return manager.getPlayTime(instanceId);
            }
            return -1;  //Adia
        }

        //Adia
        //Adia
        static public int GetPlaySamples(int instanceId)
        {
            if (manager != null)
            {
                return manager.getPlaySamples(instanceId);
            }
            return -1;  //Adia
        }

        //Adia
        //Adia
        static public void SetTime(int instanceId, float time)
        {
            if (manager != null)
            {
                manager.setTime(instanceId, time);
            }
        }

        //Adia
        //Adia
        static public void SetTimeSamples(int instanceId, int samples)
        {
            if (manager != null)
            {
                manager.setTimeSamples(instanceId, samples);
            }
        }

        //Adia
        //Adia
        static public void SetMute(bool onMute)
        {
            if (manager != null)
            {
                manager.setMute(onMute);
            }
        }

        //Adia
        //Adia
        static public bool GetMuteStatus()
        {
            if (manager != null)
            {
                return manager.getMuteStatus();
            }
            return false;
        }

        //Adia
        //Adia
        static public string[] GetAudioClipNameLoadId(int loadId)
        {
            if (manager != null)
            {
                return manager.getAudioClipNameLoadId(loadId);
            }
            return null;
        }

        //Adia
        //Adia
        static public string[] GetAudioClipNameAll()
        {
            if (manager != null)
            {
                return manager.getAudioClipNameAll();
            }
            return null;
        }

        //Adia
        //Adia
        static public string GetAudioClipName(string labelName)
        {
            if (manager != null)
            {
                return manager.getAudioClipName(labelName);
            }
            return null;
        }

        //Adia
        //Adia
        static public string[] GetAudioClipNames(string labelName)
        {
            if (manager != null)
            {
                return manager.getAudioClipNames(labelName);
            }
            return null;
        }

        //Adia
        //Adia
        static public string[] GetRandomSourceNames(string labelName)
        {
            if (manager != null)
            {
                return manager.getRandomSourceNames(labelName);
            }
            return null;
        }

        //Adia
        //Adia
        static public void SetAudioClipToLabelLoadId(int loadId)
        {
            if (manager != null)
            {
                manager.setAudioClipToLabelLoadId(loadId);
            }
        }

        //Adia
        //Adia
        static public void SetAudioClipToLabelAll()
        {
            if (manager != null)
            {
                manager.setAudioClipToLabelAll();
            }
        }

        //Adia
        //Adia
        static public void SetAudioClipToLabel(string labelName)
        {
            if (manager != null)
            {
                manager.setAudioClipToLabel(labelName);
            }
        }

        //Adia
        //Adia
        static public void SetAndroidNativeToLabel(string labelName, string filePath, string className, string funcName)
        {
            if (manager != null)
            {
                manager.setAndroidNativeToLabel(labelName, filePath, className, funcName);
            }
        }

        //Adia
        //Adia
        static public void ClearObjectPool()
        {
            if (manager != null)
            {
                manager.clearObjectPool();
            }
        }

        //Adia
        //Adia
        static public float GetLabelLength(string labelName)
        {
            if (manager != null)
            {
                return manager.getLabelLength(labelName);
            }
            return 0;
        }

        //Adia
        //Adia
        static public int GetLabelSamples(string labelName)
        {
            if (manager != null)
            {
                return manager.getLabelSamples(labelName);
            }
            return 0;
        }

        //Adia
        //Adia
        static public bool GetSpectrumData(int instanceId, float[] sample, int channel, FFTWindow window)
        {
            if ( manager != null )
            {
                return manager.getSpectrumData(instanceId, sample, channel, window);
            }
            return false;
        }

        //Adia
        //Adia
        //Adia
        static public int GetLabelChannels(string labelName)
        {
            if (manager != null)
            {
                return manager.getLabelChannels(labelName);
            }
            return 0;
        }
        //Adia

		//Adia
		//Adia
		static public bool IsLoop(string labelName)
		{
			if ( manager != null )
			{
				return manager.isLoop(labelName);
			}
			return false;
		}

		//Adia
		//Adia
		static public int GetLabelMaxPlaybacksNum(string labelName)
		{
			if ( manager != null )
			{
				return manager.getLabelMaxPlaybacksNum(labelName);
			}
			return -1;
		}

		//Adia
		//Adia
		static public int GetCategoryMaxPlaybacksNum(string categoryName)
		{
			if (manager != null)
			{
				return manager.getCategoryMaxPlaybacksNum(categoryName);
			}
			return -1;
		}

		//Adia
		//Adia
		static public int GetCategoryMaxPlaybacksNumFromLabel(string labelName)
		{
			if (manager != null)
			{
				return manager.getCategoryMaxPlaybacksNumFromLabel(labelName);
			}
			return -1;
		}
		

#if UNITY_EDITOR
		static public AudioLabelSettings GetLabelInfo(string name)
        {
            if (manager != null)
            {
                return manager.getLabelInfo(name);
            }
            return null;
        }

        static public AudioCategorySettings GetCategoryInfo(string name)
        {
            if (manager != null)
            {
                return manager.getCategoryInfo(name);
            }
            return null;
        }

        static public AudioMasterSettings GetMasterInfo(string name)
        {
            if (manager != null)
            {
                return manager.getMasterInfo(name);
            }
            return null;
        }

        static public List<SoundLabelInfo> GetLabelInfoList()
        {
            if (manager != null)
            {
                return manager.getLabelInfoList();
            }
            return null;
        }

        static public Audio3DSettings GetAudio3DSettingsInfo(string name)
        {
            if(manager != null)
            {
                return manager.getAudio3DSettingsInfo(name);
            }
            return null;
        }

        //Adia
        //Adia
        static public bool SaveAudio3DSettingsParam(Audio3DSettings audio3d)
        {
            if (manager != null)
            {
                return manager.saveAudio3DSettingsParam(audio3d);
            }
            return false;
        }

        //Adia
        //Adia
        static public bool UndoAudio3DSettingsParam(Audio3DSettings audio3d)
        {
            if (manager != null)
            {
                return manager.undoAudio3DSettingsParam(audio3d);
            }
            return false;
        }

        //Adia
        //Adia
        static public void UpdateAudio3DSettings(Audio3DSettings audio3d)
        {
            if (manager != null)
            {
                manager.updateAudio3DSettings(audio3d);
            }
        }


        static public List<string> GetLog()
        {
            if (manager != null)
            {
                return manager.getLog();
            }
            return null;
        }

        static public List<string> GetTableLog()
        {
            if (manager != null)
            {
                return manager.getTableLog();
            }
            return null;
        }

        static public HashSet<string> GetCallLog()
        {
            if (manager != null)
            {
                return manager.getCallLog();
            }
            return null;
        }

        static public void ClearCallLog()
        {
            if (manager != null)
            {
                manager.clearCallLog();
            }
        }

        //Adia
        //Adia
        static public void SoundToolPlayListClear()
        {
            if (manager != null)
            {
                manager.soundToolPlayListClear();
            }
        }

        //Adia
        //Adia
        static public void SoundToolLogsClear()
        {
            if (manager != null)
            {
                manager.soundToolLogsClear();
            }
        }

        //Adia
        //Adia
        static public void AddLogA(string str)
        {
            if (manager != null)
            {
                manager.AddLog(str);
            }
        }

        //Adia
        //Adia
        static public bool IsOnDebug()
        {
            if ( manager != null )
            {
                return manager.IsActiveTool;
            }
            return false;
        }

        //Adia
        //Adia
        static public int GetAudio3DSettingsNum()
        {
            if (manager != null)
            {
                return manager.getAudio3DSettingsNum();
            }
            return 0;
        }

        //Adia
        //Adia
        static public string[] GetAudio3DSettingsNameList()
        {
            if (manager != null)
            {
                return manager.getAudio3DSettingsNameList();
            }
            return null;
        }

#endif
    }

}