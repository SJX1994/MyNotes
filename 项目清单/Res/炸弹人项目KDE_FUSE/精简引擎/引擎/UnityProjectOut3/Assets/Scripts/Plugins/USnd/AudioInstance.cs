//Adia
//Adia
//Adia
//Adia

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace USnd
{

    public partial class AudioManager : MonoBehaviour
    {
        private class AudioInstance
        {

            bool isAudioDebug = false;

            AudioLabelSettings setting = null;

            AudioSource source = null;
            Transform sourceTransform = null;

            float defaultVolume = 0;
            float defaultPan = 0;
            int defaultPitch = 0;

            int currentPitch = 0;
            float currentVolume = 0;            //Adia

            float volumeFactor = 1.0f;          //Adia
            float ctrlVolumeFactor = 1.0f;      //Adia


#if UNITY_EDITOR
            AudioParamUpdater volumeUpdater = new AudioParamUpdater("volume");
            AudioParamUpdater panUpdater = new AudioParamUpdater("pan");
            AudioParamUpdater pitchUpdater = new AudioParamUpdater("pitch");
            AudioParamUpdater controlUpdater = new AudioParamUpdater("control"); //Adia
#else
            AudioParamUpdater volumeUpdater = new AudioParamUpdater();
            AudioParamUpdater panUpdater = new AudioParamUpdater();
            AudioParamUpdater pitchUpdater = new AudioParamUpdater();
            AudioParamUpdater controlUpdater = new AudioParamUpdater(); //Adia
#endif
            int prevPlaySamples = 0;

            bool onPause = false;

            int randomIndex = 0;        //Adia


            int instanceId = 0;

            public bool activeSelf = false;

            public void SetActive(bool active)
            {
                activeSelf = active;
            }

            enum FADE_END_STATE
            {
                UNSET,
                PAUSE,
                STOP,
            }

            FADE_END_STATE fadeStatus = FADE_END_STATE.UNSET;

            AudioDefine.INSTANCE_STATUS status = AudioDefine.INSTANCE_STATUS.PREPARE;


            Transform targetTransform = null;

            Vector3 defaultPos = new Vector3(0, 0, 0);

            bool isUpdateStart = true;


            public void SetInstanceID(int id)
            {
                instanceId = id;
            }

            public int GetInstanceID()
            {
                return instanceId;
            }

            public void Reset(AudioMainPool pool)
            {
                if (pool != null)
                {
                    pool.Deactive(source);
                }
                source = null;
                sourceTransform = null;
                setting = null;
                volumeUpdater.Clear();
                panUpdater.Clear();
                pitchUpdater.Clear();
                controlUpdater.Clear();
                defaultVolume = 0;
                defaultPan = 0;
                defaultPitch = 0;
                currentPitch = 0;
                currentVolume = 0;
                volumeFactor = 1.0f;
                ctrlVolumeFactor = 1.0f;
                prevPlaySamples = 0;
                onPause = false;
                randomIndex = 0;
                instanceId = 0;
                isUpdateStart = true;
            }

            public int GetRandomIndex()
            {
                return randomIndex;
            }

            //Adia
            //Adia
            public void Init(AudioSource playSource, float factor, int index)
            {
                randomIndex = index;

                source = playSource;
                sourceTransform = playSource.transform;
                setting = source.GetComponent<AudioLabelSettings>();

                volumeFactor = factor;
                defaultPan = source.panStereo;
                defaultPitch = setting.pitchShiftCent;
                isUpdateStart = true;

                sourceTransform.position = defaultPos;
            }

            public void Init(AudioSource playSource, AudioLabelSettings labelInfo, float factor, int index)
            {
                randomIndex = index;

                source = playSource;
                sourceTransform = playSource.transform;
                setting = labelInfo;

                volumeFactor = factor;
                defaultPan = setting.pan;
                defaultPitch = setting.pitchShiftCent;
                isUpdateStart = true;

                sourceTransform.position = defaultPos;
            }

            //Adia
            //Adia
            public void UpdateVolumeFactor(float factor)
            {
                volumeFactor = factor;
                if (source != null && source.isPlaying == true && status == AudioDefine.INSTANCE_STATUS.PLAY)
                {
                    source.volume = setting.volume * currentVolume * volumeFactor * ctrlVolumeFactor;
                }
            }

            //Adia
            //Adia
            public void Update()
            {
                if (source != null)
                {
                    if (targetTransform != null)
                    {
                        sourceTransform.position = targetTransform.position;
                    }

                    //Adia
                    //Adia
                    if (source.isPlaying && source.timeSamples != prevPlaySamples && onPause == false)
                    {
                        if (isUpdateStart == false)
                        {
                            isUpdateStart = true;
                            volumeUpdater.UpdateStart();
                            controlUpdater.UpdateStart();
                            panUpdater.UpdateStart();
                            pitchUpdater.UpdateStart();
                        }
                        if (volumeUpdater.active)
                        {
                            currentVolume = volumeUpdater.Update();
                            source.volume = setting.volume * currentVolume * volumeFactor * ctrlVolumeFactor;
                            if (isAudioDebug)
                            {
#if USND_DEBUG_LOG
                                AudioDebugLog.Log(source.name + " volume: " + source.volume);
#endif
                                if (volumeUpdater.active == false)
                                {
#if USND_DEBUG_LOG
                                    AudioDebugLog.Log(source.name + ".volume update finish." + source.volume);
#endif
                                }
                            }
                        }
                        if (controlUpdater.active)
                        {
                            ctrlVolumeFactor = controlUpdater.Update();
                            source.volume = setting.volume * currentVolume * volumeFactor * ctrlVolumeFactor;
                            if (isAudioDebug)
                            {
#if USND_DEBUG_LOG
                                AudioDebugLog.Log(source.name + " volume: " + source.volume);
#endif
                            }
                            if (controlUpdater.active == false)
                            {
                                if (isAudioDebug)
                                {
#if USND_DEBUG_LOG
                                    AudioDebugLog.Log(source.name + ".volume control finish." + source.volume);
#endif
                                }
                                if (fadeStatus == FADE_END_STATE.STOP)
                                {
                                    if (isAudioDebug)
                                    {
#if USND_DEBUG_LOG
                                        AudioDebugLog.Log("fade out stop: " + source.name);
#endif
                                    }
                                    prevPlaySamples = source.timeSamples;
                                    source.Stop();
                                    fadeStatus = FADE_END_STATE.UNSET;
                                    status = AudioDefine.INSTANCE_STATUS.STOP;
#if UNITY_EDITOR
                                    if (AudioManager.IsOnDebug())
                                        AudioManager.AddLogA("<color=yellow>InstanceUpdate: FADE_END_STOP: " + source.name + "</color>");
#endif
                                }
                                else if (fadeStatus == FADE_END_STATE.PAUSE)
                                {
                                    if (isAudioDebug)
                                    {
#if USND_DEBUG_LOG
                                        AudioDebugLog.Log("fade out pause: " + source.name);
#endif
                                    }
                                    prevPlaySamples = source.timeSamples;
                                    source.Pause();
                                    fadeStatus = FADE_END_STATE.UNSET;
                                    status = AudioDefine.INSTANCE_STATUS.PAUSE;
                                    onPause = true;
#if UNITY_EDITOR
                                    if (AudioManager.IsOnDebug())
                                        AudioManager.AddLogA("<color=yellow>InstanceUpdate: FADE_END_PAUSE: " + source.name + "</color>");
#endif
                                }
                            }
                        }
                        if (panUpdater.active)
                        {
                            source.panStereo = panUpdater.Update();
                            if (isAudioDebug)
                            {
                                if (panUpdater.active == false)
                                {
#if USND_DEBUG_LOG
                                    AudioDebugLog.Log(source.name + ".pan update finish.");
#endif
                                }
                            }
                        }
                        if (pitchUpdater.active)
                        {
                            currentPitch = (int)pitchUpdater.Update();
                            if (isAudioDebug)
                            {
                                //Adia
                            }
                            if (isAudioDebug)
                            {
                                if (pitchUpdater.active == false)
                                {
#if USND_DEBUG_LOG
                                    AudioDebugLog.Log(source.name + ".pitch update finish.");
#endif
                                }
                            }
                            //Adia
                            source.pitch = calcPitchRatio(currentPitch);
                        }
                    }
                    else if (!source.isPlaying && (status == AudioDefine.INSTANCE_STATUS.PLAY || status == AudioDefine.INSTANCE_STATUS.PAUSE_SOON || status == AudioDefine.INSTANCE_STATUS.STOP_SOON))
                    {
                        status = AudioDefine.INSTANCE_STATUS.STOP;
                    }
                    if (source.isPlaying)
                    {
                        prevPlaySamples = source.timeSamples;
                    }
                }
            }

            //Adia
            //Adia
            public void ResetPlayPosition()
            {
                prevPlaySamples = 0;
                source.timeSamples = 0;
            }

            //Adia
            //Adia
            public void Prepare(float volume, float fadeTime, float pan, int pitch, int playStartSample)
            {

                if (source == null) return;

                if (onPause == true)
                {
                    OffPause(AudioDefine.DEFAULT_FADE);
                    return;
                }
                if (source.isPlaying)
                {
                    source.Stop();
                }

                fadeStatus = FADE_END_STATE.UNSET;
                pitchUpdater.Clear();
                panUpdater.Clear();
                setupPitch((pitch < AudioDefine.PITCH_MIN || AudioDefine.PITCH_MAX < pitch) ? defaultPitch : pitch);
                setupStereoPan((pan < AudioDefine.PAN_LEFT || AudioDefine.PAN_RIGHT < pan) ? defaultPan : pan);

                volumeUpdater.Clear();
                source.timeSamples = 0;
                if (setting.isPlayLastSamples && source.loop)
                {
                    if (isAudioDebug)
                    {
#if USND_DEBUG_LOG
                        AudioDebugLog.Log(source.name + " start position: " + playStartSample);
#endif
                    }
                    if (playStartSample < 0 || source.clip.samples < playStartSample)
                    {
                        source.timeSamples = 0;
                    }
                    else
                    {
                        source.timeSamples = playStartSample;
                    }
                    prevPlaySamples = source.timeSamples;
                }
                defaultVolume = (volume < AudioDefine.VOLUME_MIN || AudioDefine.VOLUME_MAX < volume) ? 1 : volume;
                setupVolume(defaultVolume,
                    fadeTime,
                    setting.isPlayLastSamples);

                if (isAudioDebug)
                {
#if USND_DEBUG_LOG
                    AudioDebugLog.Log(source.name + " play: " + Time.renderedFrameCount + " frame");
#endif
                }

                status = AudioDefine.INSTANCE_STATUS.PREPARE;
            }

            //Adia
            //Adia
            public void Play(float delay)
            {
                status = AudioDefine.INSTANCE_STATUS.PLAY;

                //Adia
                volumeUpdater.UpdateStart();
                panUpdater.UpdateStart();
                pitchUpdater.UpdateStart();

#if UNITY_ANDROID || !UNITY_EDITOR
                if (setting.isAndroidNative == true && Application.platform == RuntimePlatform.Android)
                {
                    USndAndroidNativePlayer.Play(setting.GetAndroidSoundId(), source.volume, source.pitch);
                }
                else
                {
                    if (delay == 0)
                    {
                        source.Play();
                    }
                    else
                    {
                        isUpdateStart = false;
                        source.PlayDelayed((delay < 0) ? setting.playStartDelay : delay);
                    }
                }
#else
                if (delay == 0)
                {
                    source.Play();
                }
                else
                {
                    isUpdateStart = false;
                    source.PlayDelayed((delay < 0) ? setting.playStartDelay : delay);
                }
#endif
            }

            //Adia
            //Adia
            public void Stop(float fadeTime)
            {
                /*if (fadeStatus == FADE_END_STATE.STOP)
                {
                    if (fadeTime == 0)
                    {
                        source.Stop();
                        status = AudioDefine.INSTANCE_STATUS.STOP;
                    }
                    return;
                }*/
                if (source != null)
                {
                    if (source.isPlaying)
                    {
                        float time = fadeTime;
                        if (time < 0)
                        {
                            time = setting.fadeOutTime;
                        }
                        if (time != 0)
                        {
                            if (isAudioDebug)
                            {
#if USND_DEBUG_LOG
                                AudioDebugLog.Log(source.name + " set fadeout stop:" + time);
#endif
                            }
                            if (source.timeSamples != 0)
                            {
                                controlUpdater.Clear();
                                controlUpdater.SetParam(ctrlVolumeFactor, 0, time, false);
                                fadeStatus = FADE_END_STATE.STOP;
                                status = AudioDefine.INSTANCE_STATUS.STOP_SOON;
                            }
                            else
                            {
                                //Adia
                                prevPlaySamples = source.timeSamples;
                                source.Stop();
                                status = AudioDefine.INSTANCE_STATUS.STOP;
                            }
                        }
                        else
                        {
                            if (isAudioDebug)
                            {
                                AudioDebugLog.Log("stop " + source.name);
                            }
                            prevPlaySamples = source.timeSamples;
                            source.Stop();
                            status = AudioDefine.INSTANCE_STATUS.STOP;
                        }
                    }
                    else if (status == AudioDefine.INSTANCE_STATUS.PAUSE)
                    {
                        source.Stop();
                        status = AudioDefine.INSTANCE_STATUS.STOP;
                    }
                }
            }

            //Adia
            //Adia
            public void ForceStop()
            {
                volumeUpdater.Clear();
                pitchUpdater.Clear();
                panUpdater.Clear();
                controlUpdater.Clear();
                source.Stop();
                status = AudioDefine.INSTANCE_STATUS.STOP;
            }

            //Adia
            //Adia
            public void OnPause(float fadeTime)
            {
                if (fadeStatus != FADE_END_STATE.UNSET)
                {
                    return;
                }
                if (onPause == false && source.isPlaying)
                {
                    float time = fadeTime;
                    if (time < 0)
                    {
                        time = setting.fadeOutTimeOnPause;
                    }
                    if (time != 0)
                    {
                        if (isAudioDebug)
                        {
#if USND_DEBUG_LOG
                            AudioDebugLog.Log(source.name + " set fadeout pause:" + time);
#endif
                        }
                        controlUpdater.Clear();
                        controlUpdater.SetParam(ctrlVolumeFactor, 0, time, false);
                        fadeStatus = FADE_END_STATE.PAUSE;
                        status = AudioDefine.INSTANCE_STATUS.PAUSE_SOON;
                    }
                    else
                    {
                        if (isAudioDebug)
                        {
#if USND_DEBUG_LOG
                            AudioDebugLog.Log("pause " + source.name);
#endif
                        }
                        prevPlaySamples = source.timeSamples;
                        source.Pause();
                        ctrlVolumeFactor = 0;
                        onPause = true;
                        status = AudioDefine.INSTANCE_STATUS.PAUSE;
                    }
                }
            }

            //Adia
            //Adia
            public void OffPause(float fadeTime)
            {
                if (fadeStatus == FADE_END_STATE.PAUSE)
                {
                    fadeStatus = FADE_END_STATE.UNSET;
                    controlUpdater.Clear();
                    controlUpdater.SetParam(ctrlVolumeFactor, 1, fadeTime, false);
                }
                else if (onPause == true)
                {
                    fadeStatus = FADE_END_STATE.UNSET;
                    onPause = false;
                    float time = fadeTime;
                    if (time < 0)
                    {
                        time = setting.fadeInTimeOffPause;
                    }

                    if (time != 0)
                    {
                        ctrlVolumeFactor = 0;
                        controlUpdater.Clear();
                        controlUpdater.SetParam(ctrlVolumeFactor, 1, time, false);
                        source.volume = 0;
                    }
                    else
                    {
                        ctrlVolumeFactor = 1;
                        source.volume = setting.volume * currentVolume * volumeFactor * ctrlVolumeFactor;
                    }
                    onPause = false;
                    source.Play();
                }
                status = AudioDefine.INSTANCE_STATUS.PLAY;
            }

            //Adia
            //Adia
            public void SetVolume(float newVolume, float moveTime)
            {
                float setVol = newVolume;
                if (newVolume < 0 || 1 < newVolume)
                {
                    setVol = defaultVolume;
                }
                volumeUpdater.Clear();
                if (moveTime == 0)
                {
                    currentVolume = setVol;
                    source.volume = setting.volume * setVol * volumeFactor * ctrlVolumeFactor;
                    if (isAudioDebug)
                    {
#if USND_DEBUG_LOG
                        AudioDebugLog.Log(source.name + " volume update:" + setVol);
#endif
                    }
                }
                else
                {
                    volumeUpdater.SetParam(currentVolume, setVol, moveTime, false);
                    if (isAudioDebug)
                    {
#if USND_DEBUG_LOG
                        AudioDebugLog.Log(source.name + " set volume: " + source.volume + " to " + setVol + " time:" + moveTime);
#endif
                    }
                }
            }

            //Adia
            //Adia
            public float GetCurrentVolume()
            {
                return currentVolume;
            }

            //Adia
            //Adia
            public float GetCalcVolume()
            {
                return source.volume;
            }

            //Adia
            //Adia
            public void SetPitch(int newPitch, float moveTime)
            {
                int setPitch = newPitch;
                if (newPitch < -1200 || 1200 < newPitch)
                {
                    setPitch = defaultPitch;
                }
                pitchUpdater.Clear();
                if (moveTime == 0)
                {
                    currentPitch = setPitch;
                    //Adia
                    source.pitch = calcPitchRatio(currentPitch);
                    if (isAudioDebug)
                    {
#if USND_DEBUG_LOG
                        AudioDebugLog.Log(source.name + " pitch update:" + setPitch);
#endif
                    }
                }
                else
                {
                    pitchUpdater.SetParam(currentPitch, setPitch, moveTime, false);
                    if (isAudioDebug)
                    {
#if USND_DEBUG_LOG
                        AudioDebugLog.Log(source.name + " set pitch: " + currentPitch + " to " + setPitch + " time:" + moveTime);
#endif
                    }
                }
            }

            //Adia
            //Adia
            public void SetPan(float newPan, float moveTime)
            {

                float setPan = newPan;
                if (newPan < -1 || 1 < newPan)
                {
                    setPan = defaultPan;
                }
                pitchUpdater.Clear();
                if (moveTime == 0)
                {
                    source.panStereo = setPan;
                    if (isAudioDebug)
                    {
#if USND_DEBUG_LOG
                        AudioDebugLog.Log(source.name + " pan update:" + setPan);
#endif
                    }
                }
                else
                {
                    panUpdater.SetParam(source.panStereo, setPan, moveTime, false);
                    if (isAudioDebug)
                    {
#if USND_DEBUG_LOG
                        AudioDebugLog.Log(source.name + " set pan: " + source.panStereo + " to " + setPan + " time:" + moveTime);
#endif
                    }
                }
            }

            //Adia
            //Adia
            public void SetTrackingObject(GameObject target)
            {
				if (target != null)
				{
					SetTrackingObject(target.transform);
				}
            }

            //Adia
            //Adia
            public void SetTrackingObject(Transform target)
            {
                targetTransform = target;
                if (source != null)
                {
                    if (targetTransform != null)
                    {
                        sourceTransform.position = targetTransform.position;
                    }
                }
            }

            //Adia
            //Adia
            public void SetPosition(Vector3 position)
            {
                sourceTransform.position = position;
            }

            //Adia
            //Adia
            public bool IsPlaying()
            {
                if (source != null)
                {
                    return source.isPlaying;
                }
                return false;
            }

            //Adia
            //Adia
            public AudioDefine.INSTANCE_STATUS GetStatus()
            {
                return status;
            }

            //Adia
            //Adia
            public int GetPrevPlaySamples()
            {
                return prevPlaySamples;
            }

            //Adia
            //Adia
            public float GetPlayTime()
            {
                if (source != null)
                {
                    return source.time;
                }
                return -1;
            }

            //Adia
            //Adia
            public int GetPlaySamples()
            {
                if (source != null)
                {
                    return source.timeSamples;
                }
                return -1;
            }

            //Adia
            //Adia
            public void SetTime(float time)
            {
                if (source != null)
                {
                    if (source.clip.length >= time)
                    {
                        source.time = time;
                    }
                }
            }

            //Adia
            //Adia
            public void SetTimeSamples(int samples)
            {
                if (source != null)
                {
                    if (source.clip.samples >= samples)
                    {
                        source.timeSamples = samples;
                    }
                }
            }

            //Adia
            //Adia
            public bool GetSpectrumData(int instanceId, float[] sample, int channel, FFTWindow window)
            {
                if (source != null)
                {
                    source.GetSpectrumData(sample, channel, window);
                    return true;
                }
                return false;
            }

            //Adia
            //Adia
            void setupVolume(float volume, float fadeTime, bool isPlayLastSamples)
            {
                if (source != null)
                {
                    currentVolume = volume;

                    float setFade = fadeTime;
                    if (fadeTime < 0)
                    {
                        if (setting.isPlayLastSamples == true && prevPlaySamples != 0)
                        {
                            setFade = setting.fadeInTimeOldSamples;
                        }
                        else
                        {
                            setFade = setting.fadeInTime;
                        }
                    }

                    if (setting.isPlayLastSamples == true && setFade != 0 && prevPlaySamples != 0)
                    {
                        //Adia
                        volumeUpdater.SetParam(0, volume, setFade, false);
                        source.volume = 0;
                        currentVolume = 0;
                    }
                    else if (setFade != 0)
                    {
                        //Adia
                        volumeUpdater.SetParam(0, volume, setFade, false);
                        source.volume = 0;
                        currentVolume = 0;
                    }
                    else
                    {
                        source.volume = setting.volume * volume * volumeFactor * ctrlVolumeFactor;
                    }

                    if (isAudioDebug)
                    {
#if USND_DEBUG_LOG
                        AudioDebugLog.Log(source.name + ".volume = " + source.volume);
#endif
                    }
                }
            }

            //Adia
            //Adia
            void setupPitch(int pitch)
            {
                if (source != null)
                {
                    //Adia
                    currentPitch = pitch;
                    if (isAudioDebug)
                    {
#if USND_DEBUG_LOG
                        AudioDebugLog.Log(source.name + ".pitch = " + currentPitch);
#endif
                    }
                    //Adia
                    if (setting.isMovePitch && setting.pitchMoveTime != 0)
                    {
                        pitchUpdater.SetParam(setting.pitchStart, setting.pitchEnd, setting.pitchMoveTime, false);
                        currentPitch = setting.pitchStart;
                    }

                    //Adia
                    source.pitch = calcPitchRatio(currentPitch);
                }
            }

            //Adia
            //Adia
            void setupStereoPan(float pan)
            {
                if (source != null)
                {
                    //Adia
                    source.panStereo = pan;
                    if (isAudioDebug)
                    {
#if USND_DEBUG_LOG
                        AudioDebugLog.Log(source.name + ".panStereo = " + source.panStereo);
#endif
                    }
                    //Adia
                    if (setting.isMovePan && setting.panMoveTime != 0)
                    {
                        panUpdater.SetParam(setting.panStart, setting.panEnd, setting.panMoveTime, false);
                        source.panStereo = setting.panStart;
                    }
                }
            }

            //Adia
            //Adia
            float getRandomValue(float min, float max, float unit, bool isconsecutive, float prevValue)
            {
                float value = 0;
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
                } while (isconsecutive && (prevValue == value));
                return value;
            }

            //Adia
            //Adia
            float calcPitchRatio(int cent)
            {
                float ratio = 1.0f;
                if (cent < -1200 || 1200 < cent)
                {
#if USND_DEBUG_LOG
                    AudioDebugLog.LogWarning("cent range over. " + cent);
#endif
                    cent = 0;
                }
                /*
                if (cent == 0)
                {
                    ratio = 1.0f;
                }
                else
                {
                    float step = cent / 100.0f;
                    //Adia
                    ratio = Mathf.Pow(1.059463f, step);
                }*/
                ratio = AudioDefine.PITCH_VALUES[cent + 1200];
                return ratio;
            }


            //Adia
            //Adia
            public void UpdateAudio3DSettings(Audio3DSettings audio3d)
            {
                if (setting.spatialGroup.Equals(audio3d.spatialName))
                {
                    if (source != null)
                    {
                        source.spatialBlend = audio3d.spatialBlend;
                        source.reverbZoneMix = audio3d.reverbZoneMix;
                        source.dopplerLevel = audio3d.dopplerLevel;
                        source.spread = audio3d.spread;
                        source.rolloffMode = audio3d.rolloffMode;
                        source.minDistance = audio3d.minDistance;
                        source.maxDistance = audio3d.maxDistance;
                        source.SetCustomCurve(AudioSourceCurveType.CustomRolloff, audio3d.customRolloffCurve);
                        source.SetCustomCurve(AudioSourceCurveType.SpatialBlend, audio3d.spatialBlendCurve);
                        source.SetCustomCurve(AudioSourceCurveType.ReverbZoneMix, audio3d.reverbZoneMixCurve);
                        source.SetCustomCurve(AudioSourceCurveType.Spread, audio3d.spreadCurve);
                    }
                }
            }
        }
    }
}
