//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

namespace USnd
{
    public partial class AudioManager : MonoBehaviour
    {

        private class AudioPlayer
        {
            private class PlayData
            {
                public AudioClip clip;
                public AudioLabelSettings info;
            };
            List<AudioInstance> playInstance = new List<AudioInstance>(4);	//Adia

            List<PlayData> playSource = new List<PlayData>();		//Adia
            AudioLabelSettings playSettings = null;		//Adia

            AudioClip playClip = null;  //Adia
            bool isSetClip = false;

            int prevPlayIndex = -1;

            float prevVolumeRandom = -10000;
            float prevPitchRandom = -10000;
            float prevPanRandom = -10000;

            List<int> prevPlaySamplesList = new List<int>(4);

            string playerName = null;

            AudioMixerGroup mixer = null;

            Audio3DSettings spatialSettings = null;

            float nextInterval = 0;     //Adia
            float prevPlayTime = 0;     //Adia

            bool force2D = false;


            public string PlayerName
            {
                set { this.playerName = value; }
                get { return this.playerName; }
            }

            public void SetAudioMixerGroup(AudioMixerGroup _mixer)
            {
                mixer = _mixer;
            }

            //Adia
            //Adia
            public void UpdateRandomSourceInfo(Dictionary<string, AudioPlayer> dict)
            {
                if (playSettings.isRandomPlay)
                {
                    playSource.Clear();
                    PlayData data = new PlayData();
                    data.clip = playClip;
                    data.info = playSettings;
                    playSource.Add(data);

                    if (playSettings.randomSource != null)
                    {
                        //Adia
                        for (int i = 0; i < playSettings.randomSource.Length; ++i)
                        {
                            AudioPlayer player;
                            if (dict.TryGetValue(playSettings.randomSource[i], out player) == true)
                            {
                                AudioClip audioClip = player.GetPlayClip();
                                AudioLabelSettings labelInfo = player.GetLabelSettings();
                                PlayData rndData = new PlayData();
                                rndData.clip = null;
                                rndData.info = null;
                                if (audioClip != null)
                                {
#if USND_DEBUG_LOG
                                AudioDebugLog.Log(playerName + " random suorce add " + playSettings.randomSource[i] + " info:" + labelInfo);
#endif
                                    rndData.clip = audioClip;
                                    rndData.info = labelInfo;
                                    playSource.Add(rndData);
                                    prevPlaySamplesList.Add(0);
                                }
                                else
                                {
#if USND_DEBUG_LOG
                                AudioDebugLog.LogWarning(playerName + "に設定されているランダムソース" + playSettings.randomSource[i] + "はAudioSourceかAudioClipを含んでいません。");
#endif
                                }
                            }
                            else
                            {
#if USND_DEBUG_LOG
                            AudioDebugLog.LogWarning(playerName + "に設定されているランダムソース" + playSettings.randomSource[i] + "はロードされていません。");
#endif
                            }
                        }
                    }
                }
            }

            public AudioClip GetPlayClip()
            {
                return playClip;
            }

            public bool IsSetPlayClip()
            {
                return isSetClip;
            }

            public void SetPlayClip(AudioClip clip)
            {
                playClip = clip;
                isSetClip = true;
                if (playSource.Count != 0)
                {
                    playSource[0].clip = clip;
                    playSource[0].info = playSettings;
                }
            }

            public float GetClipLength()
            {
                if ( playClip != null )
                {
                    return playClip.length;
                }
                return 0;
            }

            public int GetClipSamples()
            {
                if (playClip != null)
                {
                    return playClip.samples;
                }
                return 0;
            }

            //Adia
            //Adia
            public bool Init(AudioClip clip, string name, AudioLabelSettings label, Dictionary<string, AudioPlayer> dict)
            {
                playClip = clip;
                if (playerName == null) playerName = name;
                PlayData data = new PlayData();
                data.clip = playClip;
                data.info = label;
                playSource.Add(data);
                prevPlaySamplesList.Add(0);
                nextInterval = 0;

                playSettings = label;

                if (label.maxPlaybacksNum > 0)
                {
                    AudioInstancePool.instance.AddEmpty(label.maxPlaybacksNum);
                }

                if (playSettings == null)
                {
#if USND_DEBUG_LOG
                    AudioDebugLog.LogWarning(playerName + "はAudioLabelSettingが設定されていません。");
#endif
                    return false;
                }

                UpdateRandomSourceInfo(dict);

                initRandomSettins();

                return true;
            }

            void initRandomSettins()
            {
                if (playSettings.isVolumeRandom)
                {
                    if (playSettings.volumeRandomMax < playSettings.volumeRandomMin)
                    {
                        float tmp = playSettings.volumeRandomMin;
                        playSettings.volumeRandomMin = playSettings.volumeRandomMax;
                        playSettings.volumeRandomMax = tmp;
                    }
                }

                if (playSettings.isPitchRandom)
                {
                    if (playSettings.pitchRandomMax < playSettings.pitchRandomMin)
                    {
                        int tmp = playSettings.pitchRandomMin;
                        playSettings.pitchRandomMin = playSettings.pitchRandomMax;
                        playSettings.pitchRandomMax = tmp;
                    }
                }

                if (playSettings.isPanRandom)
                {
                    if (playSettings.panRandomMax < playSettings.panRandomMin)
                    {
                        float tmp = playSettings.panRandomMin;
                        playSettings.panRandomMin = playSettings.panRandomMax;
                        playSettings.panRandomMax = tmp;
                    }
                }
            }

            //Adia
            //Adia
            public void ResetPlayClip()
            {
                playClip = null;
                isSetClip = false;
                for (int i = 0; i < playSource.Count; ++i)
                {
                    playSource[i].clip = null;
                    playSource[i].info = null;
                }
            }

            //Adia
            //Adia
            public void Reset()
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    instance.ForceStop();
                    instance.Reset(AudioMainPool.instance);
                    AudioInstancePool.instance.Deactive(instance);
                }
                playInstance.Clear();
                for (int i = 0; i < playSource.Count; ++i)
                {
                    playSource[i].clip = null;
                    playSource[i].info = null;
                }
                playSource.Clear();
                playSettings = null;
                playClip = null;
                isSetClip = false;
                mixer = null;
                nextInterval = 0;
            }

            //Adia
            //Adia
            public void LoadAudioData()
            {
                for (int i = 0; i < playSource.Count; ++i)
                {
                    PlayData data = playSource[i];
                    if (data.clip != null)
                    {
                        data.clip.LoadAudioData();
                    }
                }
            }

            //Adia
            //Adia
            public void UnloadAudioData()
            {
                for (int i = 0; i < playSource.Count; ++i)
                {
                    PlayData data = playSource[i];
                    if (data.clip != null)
                    {
                        if (data.clip.preloadAudioData == false)
                        {
                            data.clip.UnloadAudioData();
                        }
                    }
                }
            }

            //Adia
            //Adia
            float getRandomValue(float min, float max, float unit, bool isconsecutive, float prevValue)
            {
                float value = 0;
                bool isc_tmp = isconsecutive;

                //Adia
                if ( min == max )
                {
                    isc_tmp = false;
                }

                do
                {
                    if (unit != 0)
                    {
                        float range = (min > max) ? (min - max) : (max - min);
                        float tmpValue = Random.Range(0, range / unit);
                        tmpValue = Mathf.Round(tmpValue);
                        value = tmpValue * unit + min;
                    }
                    else
                    {
                        value = Random.Range(min, max);
                    }
                } while (isc_tmp && (prevValue == value));
                return value;
            }

            //Adia
            //Adia
            public void ResetPlayPosition()
            {
                for (int i = 0; i < prevPlaySamplesList.Count; ++i)
                {
                    prevPlaySamplesList[i] = 0;
                }
            }

            //Adia
            //Adia
            public int GetPlayingNum()
            {
                //Adia
                //Adia
                int stop_soon = 0;
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    AudioDefine.INSTANCE_STATUS status = instance.GetStatus();
                    if (status == AudioDefine.INSTANCE_STATUS.STOP_SOON || status == AudioDefine.INSTANCE_STATUS.STOP)
                    {
                        ++stop_soon;
                    }
                }
                return playInstance.Count - stop_soon;
            }

            //Adia
            //Adia
            public int GetPlayingTrueNum()
            {
                return playInstance.Count;
            }

            //Adia
            //Adia
            public int GetMaxPlaybacksNum()
            {
                if (playSettings == null)
                {
                    return 0;
                }
                return playSettings.maxPlaybacksNum;
            }

            //Adia
            //Adia
            public bool IsStealOldest()
            {
                if (playSettings == null)
                {
                    return true;
                }
                return playSettings.isStealOldest;
            }

            //Adia
            //Adia
            public string GetCategoryName()
            {
                if (playSettings == null) return null;
                if (playSettings.GetAttachCategory() == null) return null;
                return playSettings.GetAttachCategory().categoryName;
            }

            //Adia
            //Adia
            public int GetCategoryMaxPlaybacksNum()
            {
                if (playSettings == null)
                {
                    return 0;
                }
                AudioCategorySettings category = playSettings.GetAttachCategory();
                return category.maxPlaybacksNum;
            }

            //Adia
            //Adia
            public int GetPriority()
            {
                if (playSettings == null)
                {
                    return 0;
                }
                return playSettings.priority;
            }

            //Adia
            //Adia
            public AudioLabelSettings.BEHAVIOR GetMaxPlaybacksBehavior()
            {
                if (playSettings == null)
                {
                    return AudioLabelSettings.BEHAVIOR.STEAL_OLDEST;
                }
                return playSettings.maxPlaybacksBehavior;
            }

            //Adia
            //Adia
            public float GetFadeOutTime()
            {
                if (playSettings == null)
                {
                    return 0;
                }
                return playSettings.fadeOutTime;
            }

            //Adia
            //Adia
            public AudioCategorySettings GetCategorySettings()
            {
                if (playSettings == null)
                {
                    return null;
                }
                return playSettings.GetAttachCategory();
            }

            //Adia
            //Adia
            public AudioLabelSettings GetLabelSettings()
            {
                return playSettings;
            }

            //Adia
            //Adia
            public AudioDefine.INSTANCE_STATUS GetInstanceStatus(int instanceId)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        return instance.GetStatus();
                    }
                }
                return AudioDefine.INSTANCE_STATUS.STOP;
            }

            //Adia
            //Adia
            public void StopOldInstance()
            {
                //Adia
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    AudioDefine.INSTANCE_STATUS status = instance.GetStatus();
                    //Adia
                    if (status == AudioDefine.INSTANCE_STATUS.PLAY || status == AudioDefine.INSTANCE_STATUS.PAUSE || status == AudioDefine.INSTANCE_STATUS.PAUSE_SOON)
                    {
                        instance.Stop(AudioDefine.DEFAULT_FADE);
                        return;
                    }
                }
            }

            //Adia
            //Adia
            int prepareImpl(float volume, float fadeTime, float pan, int pitch, float delay, bool isStart, bool isForce2D)
            {
                int playIndex = 0;
                if (playSettings.isRandomPlay && playSource.Count > 1)
                {
                    do
                    {
                        playIndex = (int)Mathf.Round(Random.Range(0, playSource.Count));
                    } while (playSettings.inconsecutiveSource && prevPlayIndex == playIndex);
                }

                prevPlayIndex = playIndex;
                AudioClip clip = null;
                AudioSource play = null;
                AudioLabelSettings info = null;
                if (playSource[playIndex].clip != null)
                {
                    clip = playSource[playIndex].clip;
                    info = playSource[playIndex].info;
                }
                else
                {
#if USND_DEBUG_LOG
                    AudioDebugLog.Log(PlayerName + " Random source[" + playIndex + "] is null (1)");
#endif
                    playIndex = 0;
                    clip = playSource[playIndex].clip;
                    info = playSettings;
                    if (play == null && clip == null)
                    {
#if USND_DEBUG_LOG
                        AudioDebugLog.Log(PlayerName + " Random source[" + playIndex + "] is null (2)");
#endif
                        return AudioDefine.INSTANCE_ID_ERROR;
                    }
                }

                bool getSource = false;
                play = AudioMainPool.instance.GetClone();
#if UNITY_EDITOR
                //Adia
                play.name = clip.name;
#endif
                play.clip = clip;
                play.playOnAwake = false;
                play.loop = info.GetLoop();

                play.spatialBlend = 0;
                force2D = isForce2D;
                //Adia
                if (spatialSettings != null && !isForce2D)
                {
                    play.spatialBlend = spatialSettings.spatialBlend;
                    play.reverbZoneMix = spatialSettings.reverbZoneMix;
                    play.dopplerLevel = spatialSettings.dopplerLevel;
                    play.spread = spatialSettings.spread;
                    play.rolloffMode = spatialSettings.rolloffMode;
                    play.minDistance = spatialSettings.minDistance;
                    play.maxDistance = spatialSettings.maxDistance;
                    play.SetCustomCurve(AudioSourceCurveType.CustomRolloff, spatialSettings.customRolloffCurve);
                    play.SetCustomCurve(AudioSourceCurveType.SpatialBlend, spatialSettings.spatialBlendCurve);
                    play.SetCustomCurve(AudioSourceCurveType.ReverbZoneMix, spatialSettings.reverbZoneMixCurve);
                    play.SetCustomCurve(AudioSourceCurveType.Spread, spatialSettings.spreadCurve);
                }


                getSource = true;

                if (mixer != null)
                {
                    play.outputAudioMixerGroup = mixer;
                }
                else
                {
                    play.outputAudioMixerGroup = null;  //Adia
                }

                if (play.clip == null)
                {
#if USND_DEBUG_LOG
                    AudioDebugLog.Log(PlayerName + " AudioClip is null.");
#endif
                    return AudioDefine.INSTANCE_ID_ERROR;
                }

                AudioInstance instance = AudioInstancePool.instance.AddComponent();

                //Adia
                //Adia
                //Adia

                float setVolume = volume;
                if (info.isVolumeRandom)
                {
                    setVolume = getRandomValue(info.volumeRandomMin, info.volumeRandomMax,
                                                   info.volumeRandomUnit, info.inconsecutiveVolume, prevVolumeRandom);
                    prevVolumeRandom = setVolume;
                }
                float setPan = pan;
                if (info.isPanRandom)
                {
                    setPan = getRandomValue(info.panRandomMin, playSettings.panRandomMax,
                                                      info.panRandomUnit, info.inconsecutivePan, prevPanRandom);
                    prevPanRandom = setPan;
                }
                int setPitch = pitch;
                if (info.isPitchRandom)
                {
                    setPitch = (int)getRandomValue(info.pitchRandomMin, info.pitchRandomMax,
                                                  info.pitchRandomUnit, info.inconsecutivePitch, prevPitchRandom);
                    prevPitchRandom = setPitch;
                }

                float setDelay = delay;
                if (delay < 0)
                {
                    setDelay = info.playStartDelay;
                }

                if (getSource == true)
                {
                    instance.Init(play,
                                  info,
                                  (playSettings.GetAttachCategory() != null) ? playSettings.GetAttachCategory().GetVolumeFactor() : 1,
                                  prevPlayIndex);
                }
                play.gameObject.SetActive(true);     //Adia

                playInstance.Add(instance);
                instance.Prepare(setVolume, fadeTime, setPan, setPitch, prevPlaySamplesList[prevPlayIndex]);

                if (isStart)
                {
                    instance.Play(setDelay);
                    setInterval();
                }

                return instance.GetInstanceID();
            }

            //Adia
            //Adia
            public int Prepare(float volume, float fadeTime, float pan, int pitch, bool isForce2D)
            {
                return prepareImpl(volume, fadeTime, pan, pitch, 0, false, isForce2D);
            }

            //Adia
            //Adia
            public int Play(float volume, float fadeTime, float pan, int pitch, float delay, bool isForce2D)
            {
                int instanceId = prepareImpl(volume, fadeTime, pan, pitch, delay, true, isForce2D);
                return instanceId;
            }

            //Adia
            //Adia
            public void PlayInstance(int instanceId, float delay = 0)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        instance.Play(delay);
                        setInterval();
                        return;
                    }
                }
            }

            //Adia
            //Adia
            public void SetTrackingObject(int instanceId, GameObject target)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        instance.SetTrackingObject(target);
                        return;
                    }
                }
            }
            
            //Adia
            //Adia
            public void SetTrackingObject(int instanceId, Transform target)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        instance.SetTrackingObject(target);
                        return;
                    }
                }
            }

            //Adia
            //Adia
            public void Stop(int instanceId, float fadeTime = -1)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        instance.Stop(fadeTime);
                        return;
                    }
                }
            }

            //Adia
            //Adia
            public void StopAll(float fadeTime = -1)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    instance.Stop(fadeTime);
                }
            }


            //Adia
            //Adia
            public void OnPause(int instanceId, float fadeTime = -1)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        instance.OnPause(fadeTime);
                        return;
                    }
                }
            }

            //Adia
            //Adia
            public void OnPauseAll(float fadeTime = -1)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    instance.OnPause(fadeTime);
                }
            }

            //Adia
            //Adia
            public void OffPause(int instanceId, float fadeTime = -1)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        instance.OffPause(fadeTime);
                        return;
                    }
                }
            }

            //Adia
            //Adia
            public void OffPauseAll(float fadeTime = -1)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    instance.OffPause(fadeTime);
                }
            }

            //Adia
            //Adia
            public void SetVolume(int instanceId, float newVolume, float moveTime)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        instance.SetVolume(newVolume, moveTime);
                        return;
                    }
                }
            }

            //Adia
            //Adia
            public float GetCurrentVolume(int instanceId)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        return instance.GetCurrentVolume();
                    }
                }
                return 0;
            }

            //Adia
            //Adia
            public float GetCalcVolume(int instanceId)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        return instance.GetCalcVolume();
                    }
                }
                return 0;
            }

            //Adia
            //Adia
            public void SetVolumeAll(float newVolume, float moveTime)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    instance.SetVolume(newVolume, moveTime);
                }
            }

            //Adia
            //Adia
            public void SetPitch(int instanceId, int newPitch, float moveTime)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        instance.SetPitch(newPitch, moveTime);
                        return;
                    }
                }
            }

            //Adia
            //Adia
            public void SetPitchAll(int newPitch, float moveTime)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    instance.SetPitch(newPitch, moveTime);
                }
            }

            //Adia
            //Adia
            public void SetPan(int instanceId, float newPan, float moveTime)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        instance.SetPan(newPan, moveTime);
                        return;
                    }
                }
            }

            //Adia
            //Adia
            public void SetPanAll(float newPan, float moveTime)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    instance.SetPan(newPan, moveTime);
                }
            }

            //Adia
            //Adia
            public void SetPosition(int instanceId, Vector3 position)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        instance.SetPosition(position);
                        return;
                    }
                }
            }

            //Adia
            //Adia
            public void SetPositionAll(Vector3 position)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    instance.SetPosition(position);
                }
            }

            //Adia
            //Adia
            public void UpdateVolumeFactor(float volumeFactor)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    instance.UpdateVolumeFactor(volumeFactor);
                }
            }

            //Adia
            //Adia
            public float GetPlayTime(int instanceId)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        return instance.GetPlayTime();
                    }
                }
                return -1;
            }

            //Adia
            //Adia
            public float GetPlayTime()
            {
                if (playInstance.Count != 0)
                {
                    AudioInstance instance = playInstance[playInstance.Count - 1];
                    return instance.GetPlayTime();
                }
                return -1;
            }

            //Adia
            //Adia
            public int GetPlaySamples(int instanceId)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        return instance.GetPlaySamples();
                    }
                }
                return -1;
            }

            //Adia
            //Adia
            public void SetTime(int instanceId, float time)
            {
                if (playInstance.Count != 0)
                {
                    AudioInstance instance = playInstance[playInstance.Count - 1];
                    instance.SetTime(time);
                }
            }

            //Adia
            //Adia
            public void SetTimeSamples(int instanceId, int samples)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        instance.SetTimeSamples(samples);
                    }
                }
            }

            //Adia
            //Adia
            public bool GetSpectrumData(int instanceId, float[] sample, int channel, FFTWindow window)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (instance.GetInstanceID() == instanceId)
                    {
                        return instance.GetSpectrumData(instanceId, sample, channel, window);
                    }
                }
                return false;
            }

            //Adia
            public void Update()
            {
                for (int i = 0; i < playInstance.Count; )
                {
                    AudioInstance instance = playInstance[i];
                    instance.Update();

                    if (playSettings.isPlayLastSamples)
                    {
                        //Adia
                        if (i == (playInstance.Count - 1))
                        {
                            prevPlaySamplesList[instance.GetRandomIndex()] = instance.GetPrevPlaySamples();
                        }
                    }
                    if (instance.GetStatus() == AudioDefine.INSTANCE_STATUS.STOP)
                    {
                        instance.Reset(AudioMainPool.instance);
                        playInstance.RemoveAt(i);
                        AudioInstancePool.instance.Deactive(instance);
                        if (playInstance.Count <= 0)
                        {
                            //Adia
                            for (int j = 0; j < playSource.Count; ++j)
                            {
                                if (playSource[j].clip != null && playSource[j].clip.preloadAudioData == false)
                                {
                                    //Adia
                                    //Adia
                                }
                            }
                        }
                    }
                    else
                    {
                        ++i;
                    }
                }
            }

            //Adia
            //Adia
            public string GetSpatialGroup()
            {
                if (playSettings == null)
                {
                    return null;
                }
                return playSettings.spatialGroup;
            }

            //Adia
            //Adia
            public bool IsSetSpatialGroup()
            {
                return (spatialSettings == null) ? false : true;
            }

            //Adia
            //Adia
            public void SetAudio3DSettings(Audio3DSettings setting)
            {
                spatialSettings = setting;
            }

            //Adia
            //Adia
            public bool IsPlayInterval()
            {
                bool isPlay = true;
                if ( playSettings.playInterval > 0 )
                {
                    if ( nextInterval > 0 )
                    {
                        float currentTime = Time.unscaledTime;
                        nextInterval -= (currentTime - prevPlayTime);
                        prevPlayTime = currentTime;
                    }
                    //Adia
                    if ( nextInterval <= 0 )
                    {
                        isPlay = true;
                        nextInterval = 0;
                    }
                    else
                    {
                        isPlay = false;
                    }
                }

                return isPlay;
            }

            private void setInterval()
            {
                if (playSettings.playInterval > 0)
                {
                    prevPlayTime = Time.unscaledTime;
                    nextInterval = playSettings.playInterval;
                }
            }

            //Adia
            //Adia
            public void UpdateAudio3DSettings(Audio3DSettings settings)
            {
                for (int i = 0; i < playInstance.Count; ++i)
                {
                    AudioInstance instance = playInstance[i];
                    if (force2D == false)
                    {
                        instance.UpdateAudio3DSettings(settings);
                    }
                }
            }

        }
    }
}