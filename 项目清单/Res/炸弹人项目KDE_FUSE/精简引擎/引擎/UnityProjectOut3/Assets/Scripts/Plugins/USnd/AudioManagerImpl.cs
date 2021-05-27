using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System;
using UnityEngine.Audio;
using System.Threading;


#pragma warning disable 649 //Adia


namespace USnd
{

    public partial class AudioManager : MonoBehaviour
    {
#if UNITY_EDITOR
#if USND_EDIT_MODE
        bool IsActiveTool = true;       //Adia
#else
        bool IsActiveTool = false;       //Adia
#endif
#endif

        bool IsOnMute = false;

        int AndroidSoundPoolNum = 6;    //Adia

        Dictionary<string, AudioPlayer> sourceDict = new Dictionary<string, AudioPlayer>();

        Dictionary<int, AudioPlayer> playAudioDict = new Dictionary<int, AudioPlayer>();        //Adia
        Dictionary<string, List<int>> playCategoryDict = new Dictionary<string, List<int>>();     //Adia
        Dictionary<string, AudioCategorySettings> categoryDict = new Dictionary<string, AudioCategorySettings>();           //Adia
        Dictionary<string, AudioMasterSettings> masterDict = new Dictionary<string, AudioMasterSettings>();     //Adia
        Dictionary<string, List<string>> playDuckingTrigger = new Dictionary<string, List<string>>();   //Adia

        List<int> playAudioRemoveKey = new List<int>();     //Adia
        HashSet<AudioPlayer> playerHashSet = new HashSet<AudioPlayer>(); //Adia

        Dictionary<string, AudioClip> audioClipDict = new Dictionary<string, AudioClip>();      //Adia

        Dictionary<string, Audio3DSettings> audio3DSettings = new Dictionary<string, Audio3DSettings>();

        enum RESULT
        {
            CONTINUE,       //Adia
            EXECUTE,        //Adia
            FINISH,         //Adia
        };

        //Adia
        //Adia


        AudioMixerSettings mixerSettings = new AudioMixerSettings();

        Transform CacheTransform { get { return (_cacheTransform != null) ? _cacheTransform : (_cacheTransform = this.transform); } }
        Transform _cacheTransform;


#if UNITY_EDITOR

        public struct SoundLabelInfo
        {
            public int instance;
            public string labelName;
        }

        List<SoundLabelInfo> labelInfoList;

        List<string> logs;

        List<string> tableLogs;

        HashSet<string> callLog;    //Adia



        public List<SoundLabelInfo> getLabelInfoList()
        {
            return labelInfoList;
        }

        public List<string> getLog()
        {
            return logs;
        }

        public List<string> getTableLog()
        {
            return tableLogs;
        }


        public HashSet<string> getCallLog()
        {
            return callLog;
        }

        public void clearCallLog()
        {
            callLog.Clear();
        }

        //Adia
        Dictionary<string, Audio3DSettings> audio3DSetShallow = new Dictionary<string, Audio3DSettings>();

        //Adia
        //Adia
        public void soundToolPlayListClear()
        {
            if (labelInfoList != null)
            {
                labelInfoList.Clear();
            }
        }

        //Adia
        //Adia
        public void soundToolLogsClear()
        {
            if (logs != null)
            {
                logs.Clear();
            }
        }

        //Adia
        //Adia
        void AddLog(string str)
        {
            logs.Insert(0, DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00") + "." + DateTime.Now.Millisecond.ToString("000") + ": " + str);
            if (logs.Count > AudioDefine.LOG_MAX)
            {
                logs.RemoveAt(logs.Count - 1);
            }
        }

        //Adia
        //Adia
        void AddTableLog(string str)
        {
            tableLogs.Insert(0, DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00") + "." + DateTime.Now.Millisecond.ToString("000") + ": " + str);
            if (tableLogs.Count > AudioDefine.LOG_MAX)
            {
                tableLogs.RemoveAt(tableLogs.Count - 1);
            }
        }

        //Adia
        //Adia
        void AddCallLog(string labelName)
        {
            callLog.Add(labelName);
        }

        //Adia
        //Adia
        void SetLabelInfoList(string labelName, int instance, float volume)
        {
            SoundLabelInfo info = new SoundLabelInfo();
            info.instance = instance;
            info.labelName = labelName;
            labelInfoList.Insert(0, info);
            if (labelInfoList.Count > AudioDefine.SOUNDINFO_MAX)
            {
                //Adia
                for (int i = labelInfoList.Count - 1; i >= 0; --i)
                {
                    if (getInstanceStatus(labelInfoList[i].instance) == AudioDefine.INSTANCE_STATUS.STOP)
                    {
                        labelInfoList.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public AudioLabelSettings getLabelInfo(string name)
        {
            AudioPlayer player;
            if (sourceDict.TryGetValue(name, out player))
            {
                return player.GetLabelSettings();
            }
            return null;
        }

        public AudioCategorySettings getCategoryInfo(string name)
        {
            AudioCategorySettings value;
            if (categoryDict.TryGetValue(name, out value))
            {
                return value;
            }
            return null;
        }


        public AudioMasterSettings getMasterInfo(string name)
        {
            AudioMasterSettings value;
            if (masterDict.TryGetValue(name, out value))
            {
                return value;
            }
            return null;
        }

        public Audio3DSettings getAudio3DSettingsInfo(string name)
        {
            Audio3DSettings value;
            if(audio3DSettings.TryGetValue(name, out value))
            {
                return value;
            }
            return null;
        }

        public bool saveAudio3DSettingsParam(Audio3DSettings audio3d)
        {
            Audio3DSettings org;
            if(audio3DSetShallow.TryGetValue(audio3d.spatialName, out org))
            {
                org.Copy(audio3d);
                return true;
            }
            return false;
        }

        public bool undoAudio3DSettingsParam(Audio3DSettings audio3d)
        {
            Audio3DSettings org;
            if (audio3DSetShallow.TryGetValue(audio3d.spatialName, out org))
            {
                audio3d.Copy(org);
                return true;
            }
            return false;
        }

        //Adia
        //Adia
        public void updateAudio3DSettings(Audio3DSettings audio3d)
        {
            //Adia
            //Adia
            foreach (KeyValuePair<int, AudioPlayer> playValue in playAudioDict)
            {
                if (playValue.Value != null)
                {
                    playValue.Value.UpdateAudio3DSettings(audio3d);
                }
            }
        }

#endif
        void OnDestroy()
        {
            USndAndroidNativePlayer.Terminate();
        }

        void OnApplicationPause(bool status)
        {

        }

        void OnApplicationFocus(bool status)
        {
            //Adia
            if (Application.platform == RuntimePlatform.Android)
            {
                //Adia
                if (status)
                {
                    USndPlugin.SetAudioFocus();
                    //Adia
                    /*
                    if ( USndPlugin.isSetAudioFocus )
                    {
	                    offPauseAll(0.1f);
	                }*/
                }
                else
                {
                    //Adia
                    /*
                    if ( USndPlugin.isSetAudioFocus )
                    {
	                    onPauseAll(0.1f);
	                }*/
                }
            }
            SetMannerMode();
        }

        void onHeadsetPlugCallback(string status)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (AudioDefine.ANDROID_MANNER_MODE_MUTE)
                {
                    if (status.CompareTo("mute_on") == 0 || status.CompareTo("mute_off") == 0)
                    {
                        SetMannerMode();
                    }
                    else if (USndPlugin.IsMannerMode() == true)
                    {
                        if (status.CompareTo("noisy") == 0)
                        {
                            //Adia
                            SetMannerMode(true);
                        }
                        else if (status.CompareTo("false") == 0)
                        {
                            //Adia
                            //Adia
                            SetMannerMode();
                        }
                        else
                        {
                            //Adia
                            SetMannerMode(false);
                        }
                    }
                    else
                    {
                        SetMannerMode(false);
                    }
                }
            }
        }

        //Adia
        //Adia
        private void SetMannerMode(bool onMute)
        {
            if (AudioDefine.ANDROID_MANNER_MODE_MUTE)
            {
                foreach (KeyValuePair<string, AudioMasterSettings> value in masterDict)
                {
                    AudioMasterSettings master = value.Value;
                    master.SetMannerMode(onMute);
                }
            }
        }

        private void SetMannerMode()
        {
            if (AudioDefine.ANDROID_MANNER_MODE_MUTE)
            {
                if (USndPlugin.IsMannerMode() == true)
                {
                    if (USndPlugin.IsSpeaker())
                    {
                        //Adia
                        SetMannerMode(true);
                    }
                    else
                    {
                        //Adia
                        SetMannerMode(false);
                    }
                }
                else
                {
                    SetMannerMode(false);
                }
            }
        }

        void Awake()
        {
#if UNITY_EDITOR
            labelInfoList = new List<SoundLabelInfo>();
            logs = new List<string>();
            tableLogs = new List<string>();
            callLog = new HashSet<string>();
#endif

            this.name = "USndAudioManager";
            AudioInstancePool.Initialize();
#if USND_DEBUG_LOG
            AudioDebugLog.Log("AudioManager Awake().");
#endif

            USndPlugin.Init(this.name, "onHeadsetPlugCallback");

            //Adia
            USndAndroidNativePlayer.Initialize(AndroidSoundPoolNum);

            SetMannerMode();

            AudioMainPool.Initialize(this.gameObject);
#if USND_DEBUG_LOG
            AudioDebugLog.Log("AudioManager Awake() Finish.");
#endif
        }

        //Adia
        //Adia
        public void setAudioMixer(AudioMixer mixer)
        {
            if (mixer != null)
            {
                mixerSettings.SetAudioMixer(mixer);
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>SetUnityMixerInfo name:" + mixer.name + ".</color>");
#endif
            }
        }

        //Adia
        //Adia
        public void unsetAudioMixer()
        {
            mixerSettings.SetAudioMixer(null);
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>UnsetUnityMixerInfo.</color>");
#endif
        }

        //Adia
        //Adia
        public void setSnapshot(string snapName, float time)
        {
            if (mixerSettings != null)
            {
                mixerSettings.SetSnapshot(snapName, time);
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>SetSnapshot name:" + snapName + " time:" + time + ".</color>");
#endif
            }
        }

        //Adia
        //Adia
        public void setAudioMixerExposedParam(string paramName, float value)
        {
            if (mixerSettings != null)
            {
                mixerSettings.SetFloat(paramName, value);
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>SetAudioMixerExposedParam name:" + paramName + " value:" + value + ".</color>");
#endif
            }
        }

        //Adia
        //Adia
        public void setAudio3DSettingsFromJson(string jsonStr)
        {
            Audio3DSettings settings = D3SettingsFromJson(jsonStr);
            if ( settings.spatialName != "" )
            {
                audio3DSettings.Add(settings.spatialName, settings);
            }
        }

        //Adia
        //Adia
        public void setAudio3DSettings(Audio3DSettings setting)
        {
            if (setting.spatialName != "")
            {
#if USND_EDIT_MODE
                audio3DSettings.Add(setting.spatialName, (Audio3DSettings)setting.Clone());
#if UNITY_EDITOR
                audio3DSetShallow.Add(setting.spatialName, setting);
#endif
#else
                audio3DSettings.Add(setting.spatialName, setting);
#endif
            }
        }

        //Adia
        //Adia
        public void setAudio3DSettings(Audio3DSettings[] settings)
        {
            for (int i = 0; i < settings.Length; ++i)
            {
                if (settings[i].spatialName != "")
                {
                    audio3DSettings.Add(settings[i].spatialName, settings[i]);
                }
            }
        }

        string getChunk(byte[] tableData, int startIndex)
        {
            byte[] chunkByte = new byte[4];
            Buffer.BlockCopy(tableData, startIndex, chunkByte, 0, 4);
            return System.Text.Encoding.UTF8.GetString(chunkByte);
        }

        //Adia
        //Adia
        public bool loadBinaryTable(byte[] tableData, int loadId)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>LoadBinaryTable loadId: " + loadId + ".</color>");
#endif
            int startIndex = 0;
            string chunk = getChunk(tableData, 0);
            startIndex += 4;

            //Adia
            if ( chunk.CompareTo("ver ")  != 0 )
            {
                return false;
            }

            //Adia
            int intValue = BitConverter.ToInt32(tableData, startIndex);
            startIndex += 4;
            int tableVer = intValue;

            //Adia
            if ( intValue < AudioDefine.TABLE_LOWER_VERSION || AudioDefine.TABLE_UPPER_VERSION < intValue)
            {
                //Adia
                return false;
            }

            //Adia
            intValue = BitConverter.ToInt32(tableData, startIndex);
            startIndex += 4;

#if UNITY_EDITOR
            string fileName = getString(tableData, ref startIndex);
#else
            getString(tableData, ref startIndex);
#endif
            //Adia
            chunk = getChunk(tableData, startIndex);
            startIndex += 4;

#if UNITY_EDITOR
            if (IsActiveTool)
            {
                AddLog("<color=cyan>   Table name=" + fileName + " ID=" + intValue + ", Type=" + chunk + ".</color>");
                AddTableLog(fileName + " ID=" + intValue + " Type=" + chunk);
            }
#if USND_DEBUG_LOG
            AudioDebugLog.Log("LoadBinaryTable name: " + fileName + " loadId: " + loadId + " type: " + chunk + " table id:" + intValue);
#endif
#endif


            if ( chunk.CompareTo("mstr") == 0)
            {
                return loadMasterBinary(tableData, ref startIndex);
            }
            else if (chunk.CompareTo("ctgr") == 0)
            {
                return loadCategoryBinary(tableData, ref startIndex);
            }
            else if ( chunk.CompareTo("lbl ") == 0)
            {
                return loadLabelBinary(tableData, ref startIndex, loadId, tableVer);
            }

            return false;
        }

        string getString(byte[] tableData, ref int startIndex)
        {
            int textSize = BitConverter.ToInt32(tableData, startIndex);
            startIndex += 4;
            if (textSize == 0) return null;

            byte[] text = new byte[textSize];
            Buffer.BlockCopy(tableData, startIndex, text, 0, textSize);
            startIndex += textSize;
            return System.Text.Encoding.UTF8.GetString(text);
        }

        //Adia
        //Adia
        public bool loadJson(string tableData, int loadId)
        {
            if ( tableData != "" && loadJsonStatus != AudioDefine.LOAD_JSON_STATUS.LOADING)
            {
                loadJsonStatus = AudioDefine.LOAD_JSON_STATUS.LOADING;

                //Adia
                StartCoroutine(loadJsonImpl(tableData, loadId));

                return true;
            }
            else if ( tableData == "" && loadJsonStatus != AudioDefine.LOAD_JSON_STATUS.LOADING )
            {
                //Adia
                loadJsonStatus = AudioDefine.LOAD_JSON_STATUS.ERROR;
            }

            return false;
        }

        private Thread jsonThread;
        private bool jsonThreadFlag;
        private string jsonStr;

        private AudioMasterSettings[] tmpMaster;
        private AudioCategorySettings[] tmpCategory;
        private AudioLabelSettings[] tmpLabel;

        IEnumerator loadJsonImpl(string tableData, int loadId)
        {
            //Adia
            jsonThreadFlag = true;
            jsonStr = tableData;

            tmpMaster = null;
            tmpCategory = null;
            tmpLabel = null;

            jsonThread = new Thread(jsonParse);
            jsonThread.Start();
            
            //Adia
            while (jsonThreadFlag)
            {
                yield return null;
            }
            
            //Adia
            if ( tmpMaster != null )
            {
                addMasterSettings(tmpMaster);
                tmpMaster = null;
            }
            else if ( tmpCategory != null )
            {
                addCategorySettings(tmpCategory);
                tmpCategory = null;
            }
            else if ( tmpLabel != null )
            {
                //Adia
                loadLabelJson(loadId);
                tmpLabel = null;
            }

            loadJsonStatus = AudioDefine.LOAD_JSON_STATUS.FINISH;
        }

        private void jsonParse()
        {
            //Adia
            if (jsonStr.IndexOf("{\"master\":[") >= 0 )
            {
                tmpMaster = MasterFromJson<AudioMasterSettings>(jsonStr);
            }
            else if (jsonStr.IndexOf("{\"category\":[") >= 0)
            {
                tmpCategory = CategoryFromJson<AudioCategorySettings>(jsonStr);
            }
            else if (jsonStr.IndexOf("{\"label\":[") >= 0)
            {
                tmpLabel = LabelFromJson<AudioLabelSettings>(jsonStr);
            }
            
            //Adia
            jsonThreadFlag = false;
        }

        private T[] MasterFromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.master;
        }
        private T[] CategoryFromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.category;
        }
        private T[] LabelFromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.label;
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] master;
            public T[] category;
            public T[] label;
        }

        private Audio3DSettings D3SettingsFromJson(string json)
        {
            AudioSourceWrapper settings = JsonUtility.FromJson<AudioSourceWrapper>(json);
            Audio3DSettings d3 = ScriptableObject.CreateInstance<Audio3DSettings>();//Adia

            d3.spatialName = settings.spatialName;
            d3.spatialBlend = settings.spatialBlend;
            d3.reverbZoneMix = settings.reverbZoneMix;
            d3.dopplerLevel = settings.dopplerLevel;
            d3.spread = settings.spread;
            d3.rolloffMode = settings.rolloffMode;
            d3.minDistance = settings.minDistance;
            d3.maxDistance = settings.maxDistance;
            d3.customRolloffCurve = settings.customRolloffCurve;
            d3.spatialBlendCurve = settings.spatialBlendCurve;
            d3.reverbZoneMixCurve = settings.reverbZoneMixCurve;
            d3.spreadCurve = settings.spreadCurve;

            return d3;
        }


        [System.Serializable]
        private class AudioSourceWrapper
        {
            public string spatialName = "";

            [Range(0, 1)]
            public float spatialBlend = 1;

            [Range(0, 1.1f)]
            public float reverbZoneMix = 1;

            [Range(0, 5)]
            public float dopplerLevel = 1;

            [Range(0, 360)]
            public int spread = 0;

            public AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;

            public float minDistance = 1;

            public float maxDistance = 500;

            //Adia
            //Adia
            public AnimationCurve customRolloffCurve;

            public AnimationCurve spatialBlendCurve;

            public AnimationCurve reverbZoneMixCurve;

            public AnimationCurve spreadCurve;
        }

        //Adia
        //Adia
        bool loadLabelJson(int loadId)
        {
            //Adia
            int recordNum = tmpLabel.Length;

            for (int i = 0; i < recordNum; ++i)
            {
                string labelName = tmpLabel[i].name;
                AudioPlayer player = null;
                AudioLabelSettings label = null;

                if (sourceDict.TryGetValue(labelName, out player) == true)
                {
                    //Adia
                    player.StopAll(0);
                    removeLabel(labelName);
                }

                player = new AudioPlayer();
                player.PlayerName = labelName;
                label = tmpLabel[i];
                label.loadId = loadId;

                bool error = false;
                AudioClip clip = null;


                //Adia
                AudioCategorySettings findCategory;
                if (categoryDict.TryGetValue(label.GetCategoryName(), out findCategory))
                {
                    label.SetAttachCategoryInstance(categoryDict[label.GetCategoryName()]);
                }
                else
                {
#if USND_DEBUG_LOG
                    AudioDebugLog.Log("カテゴリ[" + label.GetCategoryName() + "]が見つからなかったので" + label.name + "を登録しませんでした。");
#endif
                    error = true;
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=red>    カテゴリ[" + label.GetCategoryName() + "]が見つからなかったので" + label.name + "を登録しませんでした。</color>");
#endif
                }

                //Adia
#if UNITY_EDITOR
                if (label.isVolumeRandom && label.volumeRandomMin == label.volumeRandomMax)
                    AddLog("<color=red>" + label.name + ":ランダムボリュームの最大値と最小値が同じ値です.</color>");
#endif
#if USND_DEBUG_LOG
                if (label.isVolumeRandom && label.volumeRandomMin == label.volumeRandomMax)
                    AudioDebugLog.LogWarning(label.name + ":ランダムボリュームの最大値と最小値が同じ値です.");
#endif


#if UNITY_EDITOR
                if (label.isPitchRandom && label.pitchRandomMin == label.pitchRandomMax)
                    AddLog("<color=red>" + label.name + ":ランダムピッチの最大値と最小値が同じ値です.</color>");
#endif
#if USND_DEBUG_LOG
                if (label.isPitchRandom && label.pitchRandomMin == label.pitchRandomMax)
                    AudioDebugLog.LogWarning(label.name + ":ランダムピッチの最大値と最小値が同じ値です.");
#endif


#if UNITY_EDITOR
                if (label.isPanRandom && label.panRandomMin == label.panRandomMax)
                    AddLog("<color=red>" + label.name + ":ランダムパンの最大値と最小値が同じ値です.</color>");
#endif
#if USND_DEBUG_LOG
                if (label.isPanRandom && label.panRandomMin == label.panRandomMax)
                    AudioDebugLog.LogWarning(label.name + ":ランダムパンの最大値と最小値が同じ値です.");
#endif

                //Adia
                if (error == true)
                {
                    deleteAudioSource(player);
                }
                else
                {
                    sourceDict.Add(player.PlayerName, player);
                    if (player.Init(clip, label.GetClipName(), label, sourceDict) == false)
                    {
#if USND_DEBUG_LOG
                            AudioDebugLog.LogWarning(player.PlayerName + "の初期化に失敗");
#endif
                    }
                    setUnityAudioMixer(player);
                }

            }

            updateRandomSourceInfoAll();

            return true;
        }


        //Adia
        //Adia
        bool loadMasterBinary(byte[] tableData, ref int startIndex)
        {
            //Adia
            int recordNum = BitConverter.ToInt32(tableData, startIndex);
            startIndex += 4;

            AudioMasterSettings[] data = new AudioMasterSettings[recordNum];

            for (int i = 0; i < recordNum; ++i)
            {
                data[i] = new AudioMasterSettings();
                data[i].masterName = getString(tableData, ref startIndex);
                data[i].volume = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
            }

            addMasterSettings(data);

            return true;
        }

        //Adia
        //Adia
        bool loadCategoryBinary(byte[] tableData, ref int startIndex)
        {
            //Adia
            int recordNum = BitConverter.ToInt32(tableData, startIndex);
            startIndex += 4;

            AudioCategorySettings[] data = new AudioCategorySettings[recordNum];

            for (int i = 0; i < recordNum; ++i)
            {
                data[i] = new AudioCategorySettings();
                data[i].categoryName = getString(tableData, ref startIndex);
                data[i].volume = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                data[i].maxPlaybacksNum = BitConverter.ToInt32(tableData, startIndex);
                startIndex += 4;
                data[i].masterName = getString(tableData, ref startIndex);
            }

            addCategorySettings(data);

            return true;
        }

        //Adia
        //Adia
        bool loadLabelBinary(byte[] tableData, ref int startIndex, int loadId, int tableVer)
        {

            //Adia
            //Adia
            //Adia

            //Adia
            int recordNum = BitConverter.ToInt32(tableData, startIndex);
            startIndex += 4;

            for (int i = 0; i < recordNum; ++i)
            {
                string labelName = getString(tableData, ref startIndex);
                AudioPlayer player = null;
                AudioLabelSettings label = null;
                bool newCreate = false;
                if (sourceDict.TryGetValue(labelName, out player) == true)
                {
                    //Adia
                    player.StopAll(0);
                    label = player.GetLabelSettings();
                }
                else
                {
                    //Adia
                    player = new AudioPlayer();
                    player.PlayerName = labelName;

                    label = new AudioLabelSettings();
                    label.name = labelName;

                    newCreate = true;
                }

                label.loadId = loadId;

                bool error = false;
                AudioClip clip = null;


                //Adia

                //Adia
                //Adia
                label.clipName = getString(tableData, ref startIndex);
                audioClipDict.TryGetValue(label.clipName, out clip);

                //Adia
                //Adia
                label.isLoop = BitConverter.ToInt32(tableData, startIndex) == 0 ? false : true;
                startIndex += 4;
                //Adia
                //Adia
                label.volume = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.maxPlaybacksBehavior = (AudioLabelSettings.BEHAVIOR)BitConverter.ToInt32(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.priority = BitConverter.ToInt32(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.categoryName = getString(tableData, ref startIndex);
                AudioCategorySettings findCategory;
                if (categoryDict.TryGetValue(label.GetCategoryName(), out findCategory))
                {
                    label.SetAttachCategoryInstance(categoryDict[label.GetCategoryName()]);
                }
                else
                {
#if USND_DEBUG_LOG
                    AudioDebugLog.Log("カテゴリ[" + label.GetCategoryName() + "]が見つからなかったので" + label.name + "を登録しませんでした。");
#endif
                    error = true;
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=red>    カテゴリ[" + label.GetCategoryName() + "]が見つからなかったので" + label.name + "を登録しませんでした。</color>");
#endif
                }
                //Adia
                //Adia
                label.singleGroup = getString(tableData, ref startIndex);
                //Adia
                //Adia
                label.maxPlaybacksNum = BitConverter.ToInt32(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.isStealOldest = BitConverter.ToInt32(tableData, startIndex) == 0 ? false : true;
                startIndex += 4;
                //Adia
                //Adia
                label.unityMixerName = getString(tableData, ref startIndex);
                //Adia
                //Adia
                label.spatialGroup = getString(tableData, ref startIndex);
                //Adia
                //Adia
                label.playStartDelay = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                if (tableVer >= AudioDefine.TABLE_ADD_INTERVAL_VERSION)
                {
                    //Adia
                    label.playInterval = BitConverter.ToSingle(tableData, startIndex);
                    startIndex += 4;
                }

                //Adia
                //Adia
                label.pan = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.pitchShiftCent = BitConverter.ToInt32(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.isPlayLastSamples = BitConverter.ToInt32(tableData, startIndex) == 0 ? false : true;
                startIndex += 4;
                //Adia
                //Adia
                label.fadeInTime = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.fadeOutTime = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.fadeInTimeOldSamples = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.fadeOutTimeOnPause = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.fadeInTimeOffPause = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.isVolumeRandom = BitConverter.ToInt32(tableData, startIndex) == 0 ? false : true;
                startIndex += 4;
                //Adia
                //Adia
                label.inconsecutiveVolume = BitConverter.ToInt32(tableData, startIndex) == 0 ? false : true;
                startIndex += 4;
                //Adia
                //Adia
                label.volumeRandomMin = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.volumeRandomMax = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
#if UNITY_EDITOR
                if (label.isVolumeRandom && label.volumeRandomMin == label.volumeRandomMax)
                    AddLog("<color=red>" + label.name + ":ランダムボリュームの最大値と最小値が同じ値です.</color>");
#endif
#if USND_DEBUG_LOG
                if (label.isVolumeRandom && label.volumeRandomMin == label.volumeRandomMax)
                    AudioDebugLog.LogWarning(label.name + ":ランダムボリュームの最大値と最小値が同じ値です.");
#endif

                //Adia
                //Adia
                label.volumeRandomUnit = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.isPitchRandom = BitConverter.ToInt32(tableData, startIndex) == 0 ? false : true;
                startIndex += 4;
                //Adia
                //Adia
                label.inconsecutivePitch = BitConverter.ToInt32(tableData, startIndex) == 0 ? false : true;
                startIndex += 4;
                //Adia
                //Adia
                label.pitchRandomMin = BitConverter.ToInt32(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.pitchRandomMax = BitConverter.ToInt32(tableData, startIndex);
                startIndex += 4;
#if UNITY_EDITOR
                if (label.isPitchRandom && label.pitchRandomMin == label.pitchRandomMax)
                    AddLog("<color=red>" + label.name + ":ランダムピッチの最大値と最小値が同じ値です.</color>");
#endif
#if USND_DEBUG_LOG
                if (label.isPitchRandom && label.pitchRandomMin == label.pitchRandomMax)
                    AudioDebugLog.LogWarning(label.name + ":ランダムピッチの最大値と最小値が同じ値です.");
#endif

                //Adia
                //Adia
                label.pitchRandomUnit = BitConverter.ToInt32(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.isPanRandom = BitConverter.ToInt32(tableData, startIndex) == 0 ? false : true;
                startIndex += 4;
                //Adia
                //Adia
                label.inconsecutivePan = BitConverter.ToInt32(tableData, startIndex) == 0 ? false : true;
                startIndex += 4;
                //Adia
                //Adia
                label.panRandomMin = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.panRandomMax = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
#if UNITY_EDITOR
                if (label.isPanRandom && label.panRandomMin == label.panRandomMax)
                    AddLog("<color=red>" + label.name + ":ランダムパンの最大値と最小値が同じ値です.</color>");
#endif
#if USND_DEBUG_LOG
                if (label.isPanRandom && label.panRandomMin == label.panRandomMax)
                    AudioDebugLog.LogWarning(label.name + ":ランダムパンの最大値と最小値が同じ値です.");
#endif

                //Adia
                //Adia
                label.panRandomUnit = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.isRandomPlay = BitConverter.ToInt32(tableData, startIndex) == 0 ? false : true;
                startIndex += 4;
                //Adia
                //Adia
                label.inconsecutiveSource = BitConverter.ToInt32(tableData, startIndex) == 0 ? false : true;
                startIndex += 4;
                //Adia
                //Adia
                int num = BitConverter.ToInt32(tableData, startIndex);
                startIndex += 4;
                if ( num != 0 )
                {
                    label.randomSource = new string[num];
                    for(int j=0; j<num; ++j)
                    {
                        label.randomSource[j] = getString(tableData, ref startIndex);
                        //Adia
                    }
                }
                //Adia
                //Adia
                label.isMovePitch = BitConverter.ToInt32(tableData, startIndex) == 0 ? false : true;
                startIndex += 4;
                //Adia
                //Adia
                label.pitchStart = BitConverter.ToInt32(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.pitchEnd = BitConverter.ToInt32(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.pitchMoveTime = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.isMovePan = BitConverter.ToInt32(tableData, startIndex) == 0 ? false : true;
                startIndex += 4;
                //Adia
                //Adia
                label.panStart = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.panEnd = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.panMoveTime = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                num = BitConverter.ToInt32(tableData, startIndex);
                startIndex += 4;
                if (num != 0)
                {
                    label.duckingCategories = new string[num];
                    for (int j = 0; j < num; ++j)
                    {
                        //Adia
                        label.duckingCategories[j] = getString(tableData, ref startIndex);
                    }
                }
                //Adia
                //Adia
                label.duckingStartTime = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.duckingEndTime = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.duckingVolumeFactor = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                //Adia
                label.autoRestoreDucking = BitConverter.ToInt32(tableData, startIndex) == 0 ? false : true;
                startIndex += 4;
                //Adia
                //Adia
                label.restoreTime = BitConverter.ToSingle(tableData, startIndex);
                startIndex += 4;
                //Adia
                if (tableVer >= AudioDefine.TABLE_ADD_IS_ANDROID_NATIVE_VERSION)
                {
                    //Adia
                    label.isAndroidNative = BitConverter.ToInt32(tableData, startIndex) == 0 ? false : true;
                    startIndex += 4;
                }

                //Adia
                if (error == true)
                {
                    deleteAudioSource(player);
                }
                else
                {
                    if (newCreate == true)
                    {
                        sourceDict.Add(player.PlayerName, player);
                        if (player.Init(clip, label.GetClipName(), label, sourceDict) == false)
                        {
#if USND_DEBUG_LOG
                            AudioDebugLog.LogWarning(player.PlayerName + "の初期化に失敗");
#endif
                        }
                    }
                    setUnityAudioMixer(player);
                }

            }

            updateRandomSourceInfoAll();

            return true;
        }

        //Adia
        //Adia
        public void addCategorySettings(AudioCategorySettings[] list)
        {
            if (list != null)
            {
                for (int i = 0; i < list.Length; ++i)
                {
                    AudioCategorySettings category = list[i];
                    addCategorySettings(category);
                }
            }
        }

        //Adia
        //Adia
        public bool addCategorySettings(AudioCategorySettings category)
        {
            if (category == null)
            {
                return false;
            }
            AudioCategorySettings dest;
            if (categoryDict.TryGetValue(category.categoryName, out dest) == false)
            {
                string categoryName = category.categoryName;
#if USND_DEBUG_LOG
                AudioDebugLog.Log("AudioManager add category " + categoryName);
#endif
                categoryDict.Add(categoryName, category);
                attachMasterSettings(category);

                if (playCategoryDict.ContainsKey(categoryName) == false)
                {
                    List<int> instanceList = new List<int>((category.maxPlaybacksNum > 0) ? category.maxPlaybacksNum : AudioDefine.LIST_CAPACITY);
                    playCategoryDict.Add(categoryName, instanceList);
                }

                if (playDuckingTrigger.ContainsKey(categoryName) == false)
                {
                    List<string> ducking = new List<string>();
                    playDuckingTrigger.Add(categoryName, ducking);
                }
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>AddCategorySettings name:" + category.categoryName + ".</color>");
#endif
                if (category.maxPlaybacksNum > 0)
                {
                    AudioMainPool.instance.AddEmpty(category.maxPlaybacksNum);
                }
                return true;
            }
            else
            {
#if USND_DEBUG_LOG
                AudioDebugLog.Log(category.categoryName + "は既に存在しているのでパラメータを上書きしました。");
#endif
                dest.CopySettings(category);
                attachMasterSettings(dest);
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>AddCategorySettings name:" + category.categoryName + " update.</color>");
#endif
            }
            return false;
        }

        //Adia
        //Adia
        public void addMasterSettings(AudioMasterSettings[] list)
        {
            if (list != null)
            {
                for (int i = 0; i < list.Length; ++i)
                {
                    AudioMasterSettings master = list[i];
                    addMasterSettings(master);
                }
            }
        }

        //Adia
        //Adia
        public bool addMasterSettings(AudioMasterSettings master)
        {
            if (master == null)
            {
                return false;
            }
            AudioMasterSettings dest;
            if (masterDict.TryGetValue(master.masterName, out dest) == false)
            {
#if USND_DEBUG_LOG
                AudioDebugLog.Log("AudioManager add master " + master.masterName);
#endif
                masterDict.Add(master.masterName, master);
                master.SetMute(IsOnMute);
                SetMannerMode();
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>AddMasterSettings name:" + master.masterName + ".</color>");
#endif
                return true;
            }
            else
            {
#if USND_DEBUG_LOG
                AudioDebugLog.Log(master.masterName + "は既に存在しているのでパラメータを上書きしました。");
#endif
                //Adia
                dest.CopySettings(master);
                dest.SetMute(IsOnMute);
                SetMannerMode();
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>AddMasterSettings name:" + master.masterName + " update.</color>");
#endif
            }
            return false;
        }

        //Adia
        //Adia
        public void addAudioClip(AudioClip[] clips)
        {
            for (int i = 0; i < clips.Length; ++i)
            {
                addAudioClip(clips[i]);
            }
        }

        //Adia
        //Adia
        public void addAudioClip(AudioClip clip)
        {
            if (!audioClipDict.ContainsKey(clip.name))
            {
                audioClipDict.Add(clip.name, clip);
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>AddAudioClip name:" + clip.name + ".</color>");
#endif
            }
            else
			{
				//Adia
				audioClipDict[clip.name] = clip;
			}
        }

        //Adia
        //Adia
        public bool isExistAudioClip(string clipName)
        {
            return audioClipDict.ContainsKey(clipName);
        }

        //Adia
        //Adia
        public void removeAudioClip(string clipName)
        {
            if (audioClipDict.ContainsKey(clipName))
            {
                audioClipDict.Remove(clipName);
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>RemoveAudioClip name:" + clipName + ".</color>");
#endif
            }
        }

        //Adia
        //Adia
        public void removeAudioClipAll()
        {
            audioClipDict.Clear();
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>RemoveAudioClipAll.</color>");
#endif
        }

        //Adia
        //Adia
        public bool findLabel(string name)
        {
            return sourceDict.ContainsKey(name);
        }

        //Adia
        //Adia
        public bool findCategory(string name)
        {
            return categoryDict.ContainsKey(name);
        }

        //Adia
        //Adia
        public bool findMaster(string name)
        {
            return masterDict.ContainsKey(name);
        }

        //Adia
        //Adia
        public bool canRemoveLabel(string labelName)
        {
            AudioPlayer player;
            if (sourceDict.TryGetValue(labelName, out player) == true)
            {
                if (player.GetPlayingTrueNum() != 0)
                {
                    return false;
                }
            }
            return true;
        }

        //Adia
        //Adia
        public bool unsetAudioClipToLabel(string labelName)
        {
            AudioPlayer player;
            if (sourceDict.TryGetValue(labelName, out player) == true)
            {
                AudioLabelSettings label = player.GetLabelSettings();
                if (label.isAndroidNative == true && Application.platform == RuntimePlatform.Android)
                {
                    int soundId = label.GetAndroidSoundId();
                    USndAndroidNativePlayer.Unload(soundId);
                }

                if (player.GetPlayingTrueNum() != 0)
                {
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=red>UnsetAudioClipToLabel name:" + labelName + " はまだ再生中なのでUnsetできません.</color>");
#endif
                    return false;
                }

                AudioClip clip = player.GetPlayClip();

                if (clip != null)
                {
                    audioClipDict.Remove(clip.name);
                }
                player.ResetPlayClip();

#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>UnsetAudioClipToLabel name:" + labelName + ".</color>");
#endif
                return true;
            }
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>UnsetAudioClipToLabel name:" + labelName + " error.</color>");
#endif
            //Adia
            return true;
        }

        //Adia
        //Adia
        public void unsetAudioClipToLabelLoadId(int loadId)
        {
            foreach (KeyValuePair<string, AudioPlayer> value in sourceDict)
            {
                AudioLabelSettings label = value.Value.GetLabelSettings();
                if (label.loadId == loadId)
                {
                    if (label.isAndroidNative == true && Application.platform == RuntimePlatform.Android)
                    {
                        int soundId = label.GetAndroidSoundId();
                        USndAndroidNativePlayer.Unload(soundId);
                    }
                    
                    AudioPlayer player = value.Value;

                    if (player.GetPlayingTrueNum() != 0)
                    {
#if UNITY_EDITOR
                        if (IsActiveTool)
                            AddLog("<color=red>UnsetAudioClipToLabelLoadId loadId:" + loadId + " " + player.PlayerName + " はまだ再生中なのでUnsetできません.</color>");
#endif
                    }
                    else
                    {
                        AudioClip clip = player.GetPlayClip();
                        if (clip != null)
                        {
                            audioClipDict.Remove(clip.name);
                        }
                        player.ResetPlayClip();
                    }
                }
            }

#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>UnsetAudioClipToLabelLoadId loadId:" + loadId + ".</color>");
#endif
        }

        //Adia
        //Adia
        public void unsetAudioClipToLabelAll()
        {
            foreach (KeyValuePair<string, AudioPlayer> value in sourceDict)
            {
                AudioPlayer player = value.Value;
                if (player.GetPlayingTrueNum() != 0)
                {
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=red>UnsetAudioClipToLabelAll : " + player.PlayerName + " はまだ再生中なのでUnsetできません.</color>");
#endif
                }
                else
                {
                    AudioClip clip = player.GetPlayClip();
                    if (clip != null)
                    {
                        audioClipDict.Remove(clip.name);
                    }
                    player.ResetPlayClip();
                }
            }

#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>UnsetAudioClipToLabelAll.</color>");
#endif
        }

        //Adia
        //Adia
        public bool removeLabel(string labelName)
        {
            AudioPlayer player;
            if (sourceDict.TryGetValue(labelName, out player) == true)
            {
                if ( player.GetPlayingTrueNum() != 0 )
                {
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=red>RemoveLabel name:" + labelName + " はまだ再生中なのでRemoveできません.</color>");
#endif
                    return false;
                }

                resetDuckingBeforeUpdate(player);
                sourceDict.Remove(labelName);

                AudioClip clip = player.GetPlayClip();

                if (clip != null)
                {
                    audioClipDict.Remove(clip.name);
                }
                deleteAudioSource(player);

#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>RemoveLabel name:" + labelName + ".</color>");
#endif
                return true;
            }
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>RemoveLabel name:" + labelName + " error.</color>");
#endif
            //Adia
            return true;
        }

        //Adia
        //Adia
        public void removeLabelLoadId(int loadId)
        {
            List<string> removeList = new List<string>(sourceDict.Count);
            foreach (KeyValuePair<string, AudioPlayer> value in sourceDict)
            {
                AudioLabelSettings label = value.Value.GetLabelSettings();
                if (label.loadId == loadId)
                {
                    AudioPlayer player = value.Value;

                    if (player.GetPlayingTrueNum() != 0)
                    {
#if UNITY_EDITOR
                        if (IsActiveTool)
                            AddLog("<color=red>RemoveLabelLoadId loadId:" + loadId + " " + player.PlayerName + " はまだ再生中なのでRemoveできません.</color>");
#endif
                    }
                    else
                    {
                        resetDuckingBeforeUpdate(player);
                        removeList.Add(value.Key);
                        AudioClip clip = player.GetPlayClip();
                        if (clip != null)
                        {
                            audioClipDict.Remove(clip.name);
                        }
                        deleteAudioSource(value.Value);
                    }
                }
            }

            for (int i = 0; i < removeList.Count; ++i)
            {
                sourceDict.Remove(removeList[i]);
            }

#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>RemoveLabelLoadId loadId:" + loadId + ".</color>");
#endif
        }

        //Adia
        //Adia
        public void removeLabelAll()
        {
            stopAll();

            List<string> removeList = new List<string>(sourceDict.Count);

            foreach (KeyValuePair<string, AudioPlayer> value in sourceDict)
            {
                AudioPlayer player = value.Value;
                if (player.GetPlayingTrueNum() != 0)
                {
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=red>RemoveLabelAll : " + player.PlayerName + " はまだ再生中なのでRemoveできません.</color>");
#endif
                }
                else
                {
                    resetDuckingBeforeUpdate(player);
                    removeList.Add(value.Key);
                    AudioClip clip = player.GetPlayClip();
                    if (clip != null)
                    {
                        audioClipDict.Remove(clip.name);
                    }
                    deleteAudioSource(value.Value);
                }
            }

            for (int i = 0; i < removeList.Count; ++i)
            {
                sourceDict.Remove(removeList[i]);
            }
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>RemoveLabelAll.</color>");
#endif
        }

        //Adia
        //Adia
        public void removeAll()
        {
            stopAll(0);
            removeLabelAll();
            removeAudioClipAll();

            masterDict.Clear();

            categoryDict.Clear();

            clearObjectPool();
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>RemoveAll.</color>");
#endif
        }


        void deleteAudioSource(AudioPlayer player)
        {
            player.Reset();
        }


        //Adia
        //Adia
        public void updateRandomSourceInfo(string labelName)
        {
            AudioPlayer player;
            if (sourceDict.TryGetValue(labelName, out player))
            {
                player.UpdateRandomSourceInfo(sourceDict);
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>UpdateRandomSourceInfo name: " + labelName + "</color>");
#endif
            }
        }

        //Adia
        //Adia
        public void updateRandomSourceInfoAll()
        {
            foreach (KeyValuePair<string, AudioPlayer> source in sourceDict)
            {
                AudioPlayer player = source.Value;
                player.UpdateRandomSourceInfo(sourceDict);
            }
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>UpdateRandomSourceInfoAll.</color>");
#endif
        }

        //Adia
        //Adia
        public void loadAudioData(string labelName)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>LoadAudioData name:" + labelName + ".</color>");
#endif
            AudioPlayer player;
            if (!sourceDict.TryGetValue(labelName, out player))
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>       " + labelName + " not found.</color>");
#endif
                return;
            }
            else
            {
                player.LoadAudioData();
            }
        }

        //Adia
        //Adia
        public void loadAudioDataLoadId(int loadId)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>LoadAudioDataLoadId loadId: " + loadId + ".</color>");
#endif
            foreach (KeyValuePair<string, AudioPlayer> source in sourceDict)
            {
                AudioPlayer player = source.Value;
                AudioLabelSettings label = player.GetLabelSettings();
                if (label.loadId == loadId)
                {
                    player.LoadAudioData();
                }
            }
        }

        //Adia
        //Adia
        public void unloadAudioData(string labelName)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>UnloadAudioData labelName: " + labelName + ".</color>");
#endif
            AudioPlayer player;
            if (!sourceDict.TryGetValue(labelName, out player))
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>       " + labelName + " not found.</color>");
#endif
                return;
            }
            else
            {
                player.UnloadAudioData();
            }
        }

        //Adia
        //Adia
        public void unloadAudioDataAll()
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>UnlaodAudioDataAll.</color>");
#endif
            foreach (KeyValuePair<string, AudioPlayer> source in sourceDict)
            {
                AudioPlayer player = source.Value;
                player.UnloadAudioData();
            }
        }

        //Adia
        //Adia
        public void unloadAudioDataLoadId(int loadId)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>UnloadAudioDataLoadId loadId: " + loadId + ".</color>");
#endif
            foreach (KeyValuePair<string, AudioPlayer> source in sourceDict)
            {
                AudioPlayer player = source.Value;
                AudioLabelSettings label = player.GetLabelSettings();
                if (label.loadId == loadId)
                {
                    player.UnloadAudioData();
                }
            }
        }

        //Adia
        //Adia
        void setUnityAudioMixer(AudioPlayer player)
        {
            AudioLabelSettings label = player.GetLabelSettings();

            if (mixerSettings != null)
            {
                //Adia

                if (label.unityMixerName != null)
                {
                    AudioMixerGroup[] group = mixerSettings.FindGroup(label.unityMixerName);
                    if ( group != null )
                    {
	                    if (group.Length != 0)
	                    {
	                        player.SetAudioMixerGroup(group[0]);
	                    }
	                }
                }
            }
        }


        AudioDefine.LOAD_XML_STATUS loadXmlStatus = AudioDefine.LOAD_XML_STATUS.STANDBY;
        AudioDefine.LOAD_JSON_STATUS loadJsonStatus = AudioDefine.LOAD_JSON_STATUS.STANDBY;


        //Adia
        //Adia
        public AudioDefine.LOAD_XML_STATUS getLoadXmlStatus()
        {
            return loadXmlStatus;
        }

        //Adia
        //Adia
        public AudioDefine.LOAD_JSON_STATUS getLoadJsonStatus()
        {
            return loadJsonStatus;
        }

        //Adia
        //Adia
        public bool loadMasterXml(Stream xml, Stream xsd = null)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
            {
                AddLog("<color=cyan>LoadMasterXml.</color>");
                AddTableLog("LoadMasterXml");
            }
#endif
            if (loadXmlStatus == AudioDefine.LOAD_XML_STATUS.LOADING)
            {
#if USND_DEBUG_LOG
                AudioDebugLog.LogWarning("別のXMLをロード中なので処理できません。");
#endif
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>       別のXMLをロード中なので処理できません.</color>");
#endif
                return false;
            }

            loadXmlStatus = AudioDefine.LOAD_XML_STATUS.LOADING;
            StartCoroutine(loadMasterXmlCoroutine(xml, xsd));

            xml.Dispose();
            if (xsd != null) xsd.Dispose();

            return true;
        }

        IEnumerator loadMasterXmlCoroutine(Stream xml, Stream xsd)
        {
            XmlDocument xmlDoc = null;
            if (xsd == null)
            {
                xmlDoc = AudioXmlLoad.Load(xml);
            }
            else
            {
                xmlDoc = AudioXmlLoad.Load(xsd, xml);
            }

            if (xmlDoc == null)
            {
#if USND_DEBUG_LOG
                AudioDebugLog.LogWarning("MasterXMLの読み込みに失敗しました。");
#endif
                loadXmlStatus = AudioDefine.LOAD_XML_STATUS.ERROR;
                yield break;
            }

            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("MasterSet");
            if (nodeList == null)
            {
#if USND_DEBUG_LOG
                AudioDebugLog.LogWarning("MasterSetノードがありません。");
#endif
                loadXmlStatus = AudioDefine.LOAD_XML_STATUS.ERROR;
                yield break;
            }
            else
            {
                //Adia
                for (int i = 0; i < nodeList.Count; ++i)
                {
                    //Adia
                    XmlNode node = nodeList[i];

                    if (node.HasChildNodes == false)
                    {
                        continue;
                    }

                    //Adia
                    //Adia
                    XmlNode dataNode = node.ChildNodes[0];
                    if (dataNode.Name.CompareTo("MasterName") != 0)
                    {
                        continue;
                    }

                    XmlNode valueNode = dataNode.ChildNodes[0];
                    AudioMasterSettings master = null;
                    bool add = false;
                    if ( valueNode == null )
                    {
#if USND_DEBUG_LOG
                        AudioDebugLog.LogWarning("MasterName Node Empty!");
#endif
                        continue;
                    }

                    if (masterDict.TryGetValue(valueNode.Value, out master) == false)
                    {
                        //Adia
                        if (dataNode.HasChildNodes == true)
                        {
                            master = new AudioMasterSettings();
                            master.masterName = valueNode.Value;

                            add = true;
                        }
                    }

                    if (master != null && node.ChildNodes.Count > 1)
                    {
                        dataNode = node.ChildNodes[1];
                        if (dataNode.Name.CompareTo("Volume") != 0)
                        {
                            continue;
                        }
                        if (dataNode.HasChildNodes == true)
                        {
                            valueNode = dataNode.ChildNodes[0];
                            master.volume = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                        }
                    }

                    if (add == true)
                    {
                        addMasterSettings(master);
                    }
                    //Adia
                }
            }
            loadXmlStatus = AudioDefine.LOAD_XML_STATUS.FINISH;
        }

        //Adia
        //Adia
        void attachMasterSettings(AudioCategorySettings category)
        {
            AudioMasterSettings master;
            if (masterDict.TryGetValue(category.masterName, out master))
            {
                category.SetAttachMasterInstance(master);
            }
            else
            {
#if USND_DEBUG_LOG
                AudioDebugLog.LogWarning("マスター[" + category.masterName + "]は登録されていません");
#endif
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>マスター[" + category.masterName + "]は登録されていません.</color>");
#endif
            }
        }



        //Adia
        //Adia
        public bool loadCategoryXml(Stream xml, Stream xsd = null)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
            {
                AddLog("<color=cyan>LoadCategoryXml.</color>");
                AddTableLog("LoadCategoryXml");
            }
#endif
            if (loadXmlStatus == AudioDefine.LOAD_XML_STATUS.LOADING)
            {
#if USND_DEBUG_LOG
                AudioDebugLog.LogWarning("別のXMLをロード中なので処理できません。");
#endif
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>       別のXMLをロード中なので処理できません.</color>");
#endif
                return false;
            }

            loadXmlStatus = AudioDefine.LOAD_XML_STATUS.LOADING;
            StartCoroutine(loadCategoryXmlCoroutine(xml, xsd));

            xml.Dispose();
            if (xsd != null) xsd.Dispose();

            return true;
        }

        IEnumerator loadCategoryXmlCoroutine(Stream xml, Stream xsd)
        {
            XmlDocument xmlDoc = null;

            if (xsd == null)
            {
                xmlDoc = AudioXmlLoad.Load(xml);
            }
            else
            {
                xmlDoc = AudioXmlLoad.Load(xsd, xml);
            }

            if (xmlDoc == null)
            {
#if USND_DEBUG_LOG
                AudioDebugLog.LogWarning("CategoryXMLの読み込みに失敗しました。");
#endif
                yield break;
            }

            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("CategorySet");
            if (nodeList == null)
            {
#if USND_DEBUG_LOG
                AudioDebugLog.LogWarning("CategorySetノードがありません。");
#endif
                yield break;
            }
            else
            {
                //Adia
                for (int i = 0; i < nodeList.Count; ++i)
                {
                    //Adia
                    XmlNode node = nodeList[i];

                    if (node.HasChildNodes == false)
                    {
                        continue;
                    }

                    //Adia
                    //Adia
                    XmlNode dataNode = node.ChildNodes[0];
                    if (dataNode.Name.CompareTo("CategoryName") != 0)
                    {
                        continue;
                    }

                    XmlNode valueNode = dataNode.ChildNodes[0];
                    AudioCategorySettings category = null;
                    bool add = false;

                    if (valueNode == null)
                    {
#if USND_DEBUG_LOG
                        AudioDebugLog.Log("CategoryName Node Empty!");
#endif
                        continue;
                    }

                    if (categoryDict.TryGetValue(valueNode.Value, out category) == false)
                    {
                        //Adia
                        if (dataNode.HasChildNodes == true)
                        {
                            category = new AudioCategorySettings();
                            category.categoryName = valueNode.Value;
                            add = true;
                        }
                    }

                    if (category != null && node.ChildNodes.Count > 1)
                    {
                        //Adia
                        for (int j = 1; j < node.ChildNodes.Count; ++j)
                        {
                            dataNode = node.ChildNodes[j];
                            if (dataNode.HasChildNodes == true)
                            {
                                valueNode = dataNode.ChildNodes[0];
                                switch (dataNode.Name)
                                {
                                    case "Volume":
                                        category.volume = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "MaxNum":
                                        category.maxPlaybacksNum = int.Parse(valueNode.Value);
                                        break;
                                    case "MasterName":
                                        category.masterName = valueNode.Value;
                                        attachMasterSettings(category);
                                        break;
                                }
                            }
                        }
                    }

                    if (add == true)
                    {
                        addCategorySettings(category);
                    }
                    //Adia
                }
            }
            loadXmlStatus = AudioDefine.LOAD_XML_STATUS.FINISH;
        }


        //Adia
        //Adia
        public bool loadLabelXml(int loadId, Stream xml, Stream xsd = null)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
            {
                AddLog("<color=cyan>LoadLabelXml.</color>");
                AddTableLog("LoadLabelXml");
            }
#endif
            if (loadXmlStatus == AudioDefine.LOAD_XML_STATUS.LOADING)
            {
#if USND_DEBUG_LOG
                AudioDebugLog.LogWarning("別のXMLをロード中なので処理できません。");
#endif
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>       別のXMLをロード中なので処理できません.</color>");
#endif
                return false;
            }

            loadXmlStatus = AudioDefine.LOAD_XML_STATUS.LOADING;
            StartCoroutine(loadLabelXmlCoroutine(loadId, xml, xsd));

            xml.Dispose();
            if (xsd != null) xsd.Dispose();

            return true;
        }

        IEnumerator loadLabelXmlCoroutine(int loadId, Stream xml, Stream xsd)
        {
            XmlDocument xmlDoc = null;

            if (xsd == null)
            {
                xmlDoc = AudioXmlLoad.Load(xml);
            }
            else
            {
                xmlDoc = AudioXmlLoad.Load(xsd, xml);
            }

            if (xmlDoc == null)
            {
#if USND_DEBUG_LOG
                AudioDebugLog.LogWarning("LabelXMLの読み込みに失敗しました。");
#endif
                yield break;
            }

            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("LabelSet");
            if (nodeList == null)
            {
#if USND_DEBUG_LOG
                AudioDebugLog.LogWarning("LabelSetノードがありません。");
#endif
                yield break;
            }
            else
            {
                //Adia
                for (int i = 0; i < nodeList.Count; ++i)
                {
                    //Adia
                    XmlNode node = nodeList[i];

                    if (node.HasChildNodes == false)
                    {
                        continue;
                    }
                    //Adia
                    //Adia
                    XmlNode dataNode = node.ChildNodes[0];
                    if (dataNode.Name.CompareTo("LabelName") != 0)
                    {
                        continue;
                    }

                    XmlNode valueNode = dataNode.ChildNodes[0];
                    AudioLabelSettings label = null;
                    AudioPlayer player = null;
                    AudioClip clip = null;
                    bool isLoop = false;
                    bool newCreate = false;

                    if (valueNode == null)
                    {
#if USND_DEBUG_LOG
                        AudioDebugLog.LogWarning("LabelName Node Empty!");
#endif
                        continue;
                    }

                    if (sourceDict.TryGetValue(valueNode.Value, out player) == true)
                    {
                        //Adia
                        player.StopAll(0);
                        label = player.GetLabelSettings();
                    }
                    else
                    {
                        //Adia
                        if (dataNode.HasChildNodes == true)
                        {
                            player = new AudioPlayer();
                            player.PlayerName = valueNode.Value;

                            label = new AudioLabelSettings();
                            label.name = valueNode.Value;

                            newCreate = true;
                        }
                    }

                    label.loadId = loadId;

                    bool error = false;
                    bool loadClipName = false;
                    if (label != null && node.ChildNodes.Count > 1)
                    {
                        //Adia
                        for (int j = 1; j < node.ChildNodes.Count; ++j)
                        {
                            dataNode = node.ChildNodes[j];
                            if (dataNode.HasChildNodes == true)
                            {
                                valueNode = dataNode.ChildNodes[0];

                                //Adia

                                switch (dataNode.Name)
                                {
                                    case "FileName":
                                        audioClipDict.TryGetValue(valueNode.Value, out clip);
                                        label.SetClipName(valueNode.Value);
                                        loadClipName = true;
                                        break;
                                    case "Loop":
                                        isLoop = bool.Parse(valueNode.Value);
                                        label.SetLoop(isLoop);
                                        break;
                                    case "Volume":
                                        label.volume = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "CategoryBehavior":
                                        if (AudioLabelSettings.BEHAVIOR.STEAL_OLDEST.ToString().CompareTo(valueNode.Value) == 0)
                                        {
                                            label.maxPlaybacksBehavior = AudioLabelSettings.BEHAVIOR.STEAL_OLDEST;
                                        }
                                        else if (AudioLabelSettings.BEHAVIOR.JUST_FAIL.ToString().CompareTo(valueNode.Value) == 0)
                                        {
                                            label.maxPlaybacksBehavior = AudioLabelSettings.BEHAVIOR.JUST_FAIL;
                                        }
                                        else if (AudioLabelSettings.BEHAVIOR.QUEUE.ToString().CompareTo(valueNode.Value) == 0)
                                        {
                                            label.maxPlaybacksBehavior = AudioLabelSettings.BEHAVIOR.QUEUE;
                                        }
                                        break;
                                    case "Priority":
                                        label.priority = int.Parse(valueNode.Value);
                                        break;
                                    case "CategoryName":
                                        {
                                            label.categoryName = valueNode.Value;
                                            AudioCategorySettings tmpCat;
                                            if (categoryDict.TryGetValue(label.GetCategoryName(), out tmpCat))
                                            {
                                                label.SetAttachCategoryInstance(tmpCat);
                                            }
                                            else
                                            {
#if USND_DEBUG_LOG
                                                AudioDebugLog.LogWarning("カテゴリ[" + label.GetCategoryName() + "]が見つからなかったので" + label.name + "を登録しませんでした。");
#endif
                                                error = true;
#if UNITY_EDITOR
                                                if (IsActiveTool)
                                                    AddLog("<color=red>    カテゴリ[" + label.GetCategoryName() + "]が見つからなかったので" + label.name + "を登録しませんでした。</color>");
#endif
                                                continue;
                                            }
                                        }
                                        break;
                                    case "SingleGroup":
                                        label.singleGroup = valueNode.Value;
                                        break;
                                    case "MaxNum":
                                        label.maxPlaybacksNum = int.Parse(valueNode.Value);
                                        break;
                                    case "IsStealOldest":
                                        label.isStealOldest = bool.Parse(valueNode.Value);
                                        break;
                                    case "UnityMixerName":
                                        label.unityMixerName = valueNode.Value;
                                        break;
                                    case "SpatialGroup":
                                        label.spatialGroup = valueNode.Value;
                                        break;
                                    case "Delay":
                                        label.playStartDelay = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "Interval":
                                        label.playInterval = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "Pan":
                                        label.pan = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "Pitch":
                                        label.pitchShiftCent = int.Parse(valueNode.Value);
                                        break;
                                    case "IsLastSamples":
                                        label.isPlayLastSamples = bool.Parse(valueNode.Value);
                                        break;
                                    case "FadeInTime":
                                        label.fadeInTime = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "FadeOutTime":
                                        label.fadeOutTime = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "FadeInOldSample":
                                        label.fadeInTimeOldSamples = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "FadeOutOnPause":
                                        label.fadeOutTimeOnPause = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "FadeInOffPause":
                                        label.fadeInTimeOffPause = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "IsVolRnd":
                                        label.isVolumeRandom = bool.Parse(valueNode.Value);
                                        break;
                                    case "IncVol":
                                        label.inconsecutiveVolume = bool.Parse(valueNode.Value);
                                        break;
                                    case "VolRndMin":
                                        label.volumeRandomMin = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "VolRndMax":
                                        label.volumeRandomMax = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "VolRndUnit":
                                        label.volumeRandomUnit = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "IsPitchRnd":
                                        label.isPitchRandom = bool.Parse(valueNode.Value);
                                        break;
                                    case "IncPitch":
                                        label.inconsecutivePitch = bool.Parse(valueNode.Value);
                                        break;
                                    case "PitchRndMin":
                                        label.pitchRandomMin = int.Parse(valueNode.Value);
                                        break;
                                    case "PitchRndMax":
                                        label.pitchRandomMax = int.Parse(valueNode.Value);
                                        break;
                                    case "PitchRndUnit":
                                        label.pitchRandomUnit = int.Parse(valueNode.Value);
                                        break;
                                    case "IsPanRnd":
                                        label.isPanRandom = bool.Parse(valueNode.Value);
                                        break;
                                    case "IncPan":
                                        label.inconsecutivePan = bool.Parse(valueNode.Value);
                                        break;
                                    case "PanRndMin":
                                        label.panRandomMin = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "PanRndMax":
                                        label.panRandomMax = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "PanRndUnit":
                                        label.panRandomUnit = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "IsRndSrc":
                                        label.isRandomPlay = bool.Parse(valueNode.Value);
                                        break;
                                    case "IncSrc":
                                        label.inconsecutiveSource = bool.Parse(valueNode.Value);
                                        break;
                                    case "RndSrc":
                                        //Adia
                                        //Adia
                                        if (valueNode.Value != null)
                                        {
                                            string str = valueNode.Value;
                                            str.Replace("\r\n", "\n");
                                            str.Replace("\r", "\n");
                                            string[] str2 = str.Split("\n"[0]);
                                            label.randomSource = new string[str2.Length];
                                            for (int k = 0; k < str2.Length; ++k)
                                            {
                                                label.randomSource[k] = str2[k];
                                                //Adia
                                            }
                                        }
                                        break;
                                    case "IsMovePitch":
                                        label.isMovePitch = bool.Parse(valueNode.Value);
                                        break;
                                    case "PitchStart":
                                        label.pitchStart = int.Parse(valueNode.Value);
                                        break;
                                    case "PitchEnd":
                                        label.pitchEnd = int.Parse(valueNode.Value);
                                        break;
                                    case "PitchMoveTime":
                                        label.pitchMoveTime = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "IsMovePan":
                                        label.isMovePan = bool.Parse(valueNode.Value);
                                        break;
                                    case "PanStart":
                                        label.panStart = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "PanEnd":
                                        label.panEnd = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "PanMoveTime":
                                        label.panMoveTime = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "DuckingCategory":
                                        //Adia
                                        if (valueNode.Value != null)
                                        {
                                            string str = valueNode.Value;
                                            str.Replace("\r\n", "\n");
                                            str.Replace("\r", "\n");
                                            string[] str2 = str.Split("\n"[0]);
                                            label.duckingCategories = new string[str2.Length];
                                            for (int k = 0; k < str2.Length; ++k)
                                            {
                                                label.duckingCategories[k] = str2[k];
                                            }
                                        }
                                        break;
                                    case "DuckStart":
                                        label.duckingStartTime = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "DuckEnd":
                                        label.duckingEndTime = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "DuckVol":
                                        label.duckingVolumeFactor = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "AutoRestore":
                                        label.autoRestoreDucking = bool.Parse(valueNode.Value);
                                        break;
                                    case "RestoreTime":
                                        label.restoreTime = float.Parse(valueNode.Value, System.Globalization.CultureInfo.InvariantCulture);
                                        break;
                                    case "IsAndroidNative":
                                        label.isAndroidNative = bool.Parse(valueNode.Value);
                                        break;
                                }
                            }
                        }
                    }

                    //Adia
                    if (error == true || loadClipName == false)
                    {
#if UNITY_EDITOR
                        if (IsActiveTool && loadClipName == false)
                            AddLog("<color=red>    " + label.name + "のAudioClip名が設定されていないので登録できませんでした。</color>");
#endif
                        deleteAudioSource(player);
                    }
                    else
                    {
#if UNITY_EDITOR
                        if (label.isVolumeRandom && label.volumeRandomMin == label.volumeRandomMax)
                            AddLog("<color=red>" + label.name + ":ランダムボリュームの最大値と最小値が同じ値です.</color>");
#endif
#if USND_DEBUG_LOG
                        if (label.isVolumeRandom && label.volumeRandomMin == label.volumeRandomMax)
                            AudioDebugLog.LogWarning(label.name + ":ランダムボリュームの最大値と最小値が同じ値です.");
#endif
#if UNITY_EDITOR
                        if (label.isPitchRandom && label.pitchRandomMin == label.pitchRandomMax)
                            AddLog("<color=red>" + label.name + ":ランダムピッチの最大値と最小値が同じ値です.</color>");
#endif
#if USND_DEBUG_LOG
                        if (label.isPitchRandom && label.pitchRandomMin == label.pitchRandomMax)
                            AudioDebugLog.LogWarning(label.name + ":ランダムピッチの最大値と最小値が同じ値です.");
#endif
#if UNITY_EDITOR
                        if (label.isPanRandom && label.panRandomMin == label.panRandomMax)
                            AddLog("<color=red>" + label.name + ":ランダムパンの最大値と最小値が同じ値です.</color>");
#endif
#if USND_DEBUG_LOG
                        if (label.isPanRandom && label.panRandomMin == label.panRandomMax)
                            AudioDebugLog.LogWarning(label.name + ":ランダムパンの最大値と最小値が同じ値です.");
#endif

                        if (newCreate == true)
                        {
                            sourceDict.Add(player.PlayerName, player);
                            if (player.Init(clip, label.GetClipName(), label, sourceDict) == false)
                            {
#if USND_DEBUG_LOG
                                AudioDebugLog.LogWarning(player.PlayerName + "の初期化に失敗");
#endif
                            }
                        }
                        setUnityAudioMixer(player);
                    }

                    //Adia
                    if ((i % 30) == 0)
                    {
                        yield return null;
                    }
                }
                updateRandomSourceInfoAll();
            }
            loadXmlStatus = AudioDefine.LOAD_XML_STATUS.FINISH;
        }

        //Adia
        //Adia
        void orderCategoryInstanceList(List<int> playerList)
        {
            for (int i = 0; i < playerList.Count; )
            {
                int instance = playerList[i];
                AudioPlayer playObj;
                if (playAudioDict.TryGetValue(instance, out playObj))
                {
                    if (playObj != null)
                    {
                        AudioDefine.INSTANCE_STATUS status = playObj.GetInstanceStatus(instance);
                        //Adia
                        if (status == AudioDefine.INSTANCE_STATUS.STOP || status == AudioDefine.INSTANCE_STATUS.STOP_SOON)
                        {
                            playerList.RemoveAt(i);
                        }
                        else
                        {
                            ++i;
                        }
                    }
                    else
                    {
                        ++i;
                    }
                }
                else
                {
                    playerList.RemoveAt(i);
                }
            }
        }

        //Adia
        //Adia
        void addPlayInfo(AudioPlayer player, int instanceId)
        {
            playAudioDict.Add(instanceId, player);
			//Adia
			playerHashSet.Add(player);

			string categoryId = player.GetCategoryName();
            List<int> playerList;
            if (playCategoryDict.TryGetValue(categoryId, out playerList))
            {
                playerList.Add(instanceId);
            }
        }

        //Adia
        //Adia
        void stopSameSingleGroup(string singleGroup, string playLabelName)
        {
            if ( singleGroup == null )
            {
                return;
            }

            foreach(KeyValuePair<int, AudioPlayer> pair in playAudioDict)
            {
                AudioPlayer player = pair.Value;
                if (player != null)
                {
                    if (playLabelName.CompareTo(pair.Value.GetLabelSettings().name) != 0)
                    {
                        if (singleGroup.CompareTo(player.GetLabelSettings().singleGroup) == 0)
                        {
                            player.Stop(pair.Key);
#if UNITY_EDITOR
                            if (IsActiveTool)
                                AddLog("<color=magenta>Stop Same SingleGroup instance:" + pair.Key + "(" + player.GetLabelSettings().name + ") group:" + singleGroup + ".</color>");
#endif
                        }
                    }
                }
            }
        }

        //Adia
        //Adia
        RESULT checkLabelPlaybacksNum(AudioPlayer player)
        {
            //Adia
            //Adia
            if (player.GetMaxPlaybacksNum() > 0)
            {
                if (player.GetPlayingNum() + 1 > player.GetMaxPlaybacksNum())
                {
                    //Adia
                    if (player.IsStealOldest())
                    {
#if USND_DEBUG_LOG
                        AudioDebugLog.Log(player.PlayerName + "の古いインスタンスを停止");
#endif
#if UNITY_EDITOR
                        if (IsActiveTool)
                            AddLog("<color=green>Play Error! " + player.PlayerName + "の古いインスタンスを停止.</color>");
#endif
                        player.StopOldInstance();
                        return RESULT.EXECUTE;
                    }
                    else
                    {
                        //Adia
                        return RESULT.FINISH;
                    }
                }
            }
            return RESULT.CONTINUE;
        }

        //Adia
        //Adia
        RESULT checkCategoryPlaybacksNum(AudioPlayer player, ref float time, ref bool queueOn)
        {
            string categoryId = player.GetCategoryName();

            List<int> playerList;
            if (!playCategoryDict.TryGetValue(categoryId, out playerList))
            {
#if USND_DEBUG_LOG
                AudioDebugLog.Log("カテゴリ名:" + categoryId + "が見つかりませんでした。");
#endif
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=green>カテゴリ名:" + categoryId + "が見つかりませんでした。</color>");
#endif
                return RESULT.FINISH;
            }
            //Adia
            AudioCategorySettings category = categoryDict[categoryId];

            //Adia
            orderCategoryInstanceList(playerList);
            int count = playerList.Count;

            queueOn = false;

            //Adia
            if (count + 1 > category.maxPlaybacksNum)
            {
                if (player.GetMaxPlaybacksBehavior() == AudioLabelSettings.BEHAVIOR.JUST_FAIL)
                {
                    //Adia
                    return RESULT.FINISH;
                }
                else
                {
                    //Adia
                    if (player.GetLabelSettings().singleGroup != null)
                    {
                        for (int i = 0; i < playerList.Count; ++i)
                        {
                            int instance = playerList[i];
                            AudioPlayer targetPlayer = playAudioDict[instance];

                            if (targetPlayer != null)
                            {
                                //Adia
                                if (player.GetLabelSettings().singleGroup.CompareTo(targetPlayer.GetLabelSettings().singleGroup) == 0)
                                {
                                    targetPlayer.Stop(instance);
#if UNITY_EDITOR
                                    if (IsActiveTool)
                                        AddLog("<color=green>シングルグループ名:" + player.GetLabelSettings().singleGroup + "の古いインスタンスを停止.</color>");
#endif
                                    //Adia
                                    return RESULT.EXECUTE;
                                }
                            }
                        }
                    }


                    //Adia
                    for (int i = 0; i < playerList.Count; ++i)
                    {
                        int instance = playerList[i];
                        AudioPlayer targetPlayer = playAudioDict[instance];

                        if (targetPlayer != null)
                        {
                            //Adia
                            if (player.GetPriority() <= targetPlayer.GetPriority())
                            {
                                targetPlayer.Stop(instance);
                                if (player.GetMaxPlaybacksBehavior() == AudioLabelSettings.BEHAVIOR.QUEUE)
                                {
                                    time = targetPlayer.GetFadeOutTime();
                                    queueOn = true;
                                }
#if UNITY_EDITOR
                                if (IsActiveTool)
                                {
                                    AddLog("<color=green>プライオリティの低いインスタンスを停止.</color>");
                                }
#endif
                                //Adia
                                return RESULT.EXECUTE;
                            }
                        }
                    }
                }
            }
            else
            {
                //Adia
                return RESULT.EXECUTE;
            }

            //Adia
            return RESULT.FINISH;
        }


        //Adia
        //Adia
        void startDucking(AudioPlayer player, int instanceId)
        {
            AudioLabelSettings labelSetting = player.GetLabelSettings();

            //Adia
            if (labelSetting.duckingCategories != null)
            {
                for (int i = 0; i < labelSetting.duckingCategories.Length; ++i)
                {
                    string categoryName = labelSetting.duckingCategories[i];
                    List<string> triggerList;
                    if (playDuckingTrigger.TryGetValue(categoryName, out triggerList))
                    {
                        if (labelSetting.autoRestoreDucking)
                        {
                            if (!triggerList.Contains(player.PlayerName))
                            {
                                triggerList.Add(player.PlayerName);
                            }
                        }
                        AudioCategorySettings categoryInstance = categoryDict[categoryName];
                        categoryInstance.SetDuckingVolumeUpdater(labelSetting.duckingVolumeFactor, labelSetting.duckingStartTime, true);
#if UNITY_EDITOR
                        if (IsActiveTool)
                            AddLog("<color=cyan>Start Ducking category:" + categoryName + " .</color>");
#endif
                    }
                }
            }
        }

        //Adia
        //Adia
        public void setDucking(string categoryName, float targetVolumeFactor, float fadeTime)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>SetDucking categoryName: " + categoryName + " vol: " + targetVolumeFactor + " time:" + fadeTime + "ms.</color>");
#endif
            AudioCategorySettings category;
            if (categoryDict.TryGetValue(categoryName, out category))
            {
                category.SetDuckingVolumeUpdater(targetVolumeFactor, fadeTime, true);
            }
            else
            {
#if USND_DEBUG_LOG
                AudioDebugLog.Log("not found category;" + categoryName);
#endif
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>       " + categoryName + " not found.</color>");
#endif
            }
        }

        //Adia
        //Adia
        public void resetDucking(string categoryName, float fadeTime)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>ResetDucking categoryName: " + categoryName + " time:" + fadeTime + "ms.</color>");
#endif
            AudioCategorySettings category;
            if (categoryDict.TryGetValue(categoryName, out category))
            {
                category.SetDuckingVolumeUpdater(1, fadeTime, false);
            }
            else
            {
#if USND_DEBUG_LOG
                AudioDebugLog.Log("not found category;" + categoryName);
#endif
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>       " + categoryName + " not found.</color>");
#endif
            }
        }

        //Adia
        //Adia
        public void resetDuckingAll(float fadeTime)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>ResetDuckingAll time:" + fadeTime + "ms.</color>");
#endif
            foreach (KeyValuePair<string, AudioCategorySettings> categoryValue in categoryDict)
            {
                categoryValue.Value.SetDuckingVolumeUpdater(1, fadeTime, false);
            }
        }


        //Adia
        //Adia
        public void forceResetDucking(string categoryName, float fadeTime)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>ResetDucking categoryName: " + categoryName + " time:" + fadeTime + "ms.</color>");
#endif
            AudioCategorySettings category;
            if (categoryDict.TryGetValue(categoryName, out category))
            {
                category.SetDuckingVolumeUpdater(1, fadeTime, false);
            }
            else
            {
#if USND_DEBUG_LOG
                AudioDebugLog.Log("not found category;" + categoryName);
#endif
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>       " + categoryName + " not found.</color>");
#endif
            }

            //Adia
            List<string> triggerList;
            if (playDuckingTrigger.TryGetValue(categoryName, out triggerList))
            {
                triggerList.Clear();
            }
        }

        //Adia
        //Adia
        public void forceResetDuckingAll(float fadeTime)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>ResetDuckingAll time:" + fadeTime + "ms.</color>");
#endif
            foreach (KeyValuePair<string, AudioCategorySettings> categoryValue in categoryDict)
            {
                categoryValue.Value.SetDuckingVolumeUpdater(1, fadeTime, false);

                //Adia
                List<string> triggerList;
                if (playDuckingTrigger.TryGetValue(categoryValue.Value.categoryName, out triggerList))
                {
                    triggerList.Clear();
                }
            }
        }

        //Adia
        //Adia
        public int play(string labelName, float delay = -1)
        {
#if USND_OUTPUT_CALL_LOG
            AddCallLog(labelName);
#endif
            return playOption(labelName, AudioDefine.DEFAULT_VOLUME, AudioDefine.DEFAULT_FADE, AudioDefine.DEFAULT_PAN, AudioDefine.DEFAULT_PITCH, delay);
        }


        int prepareInstance(string labelName, float volume, float fadeTime, float pan, int pitch, float delay, ref AudioPlayer player, ref float time, ref bool queueOn, bool isForce2D)
        {
            int instanceId = AudioDefine.INSTANCE_ID_ERROR;

            if (!sourceDict.TryGetValue(labelName, out player))
            {
#if USND_DEBUG_LOG
                AudioDebugLog.LogWarning(labelName + "は登録されていません。");
#endif
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>Play Error! " + labelName + "は登録されていません.</color>");
#endif
                return AudioDefine.INSTANCE_ID_ERROR;
            }
            if (player == null)
            {
#if USND_DEBUG_LOG
                AudioDebugLog.LogWarning(labelName + "のプレイヤー情報がありません。");
#endif
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>Play Error! " + labelName + "のプレイヤー情報がありません.</color>");
#endif
                return AudioDefine.INSTANCE_ID_ERROR;
            }

            //Adia
            if ( !player.IsPlayInterval() )
            {
#if USND_DEBUG_LOG
                AudioDebugLog.Log(labelName + "の再生インターバルにより再生できません。");
#endif
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>Play Error! " + labelName + "の再生インターバルにより再生できません.</color>");
#endif
                return AudioDefine.INSTANCE_ID_ERROR;
            }

            //Adia
            if (!player.IsSetPlayClip())
            {
                AudioLabelSettings label = player.GetLabelSettings();
                AudioClip tmpClip;
                if (audioClipDict.TryGetValue(label.GetClipName(), out tmpClip))
                {
                    player.SetPlayClip(audioClipDict[label.GetClipName()]);
                    updateRandomSourceInfo(labelName);
                }
            }

            //Adia
            if (!player.IsSetSpatialGroup())
            {
                string name = player.GetSpatialGroup();
                if (name != null)
                {
                    Audio3DSettings d3set;
                    if (audio3DSettings.TryGetValue(name, out d3set))
                    {
                        player.SetAudio3DSettings(d3set);
                    }
                }
            }

            RESULT ret;
            //Adia
            //Adia
            //Adia
            if (player.GetMaxPlaybacksNum() > 0)
            {
                ret = checkLabelPlaybacksNum(player);
                if (ret == RESULT.EXECUTE)
                {
                    instanceId = player.Prepare(volume, fadeTime, pan, pitch, isForce2D);
                    if (instanceId != AudioDefine.INSTANCE_ID_ERROR)
                    {
                        //Adia
                        //Adia
                    }
                    return instanceId;
                }
                else if (ret == RESULT.FINISH)
                {
#if USND_DEBUG_LOG
                    //Adia
                    AudioDebugLog.Log(labelName + "はラベル優先制御により再生できません。");
#endif
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=red>Play Error! " + labelName + "はラベル優先制御により再生できません.</color>");
#endif
                    return AudioDefine.INSTANCE_ID_ERROR;
                }
            }
            //Adia


            //Adia
            //Adia
            if (player.GetCategoryMaxPlaybacksNum() > 0)
            {
                ret = checkCategoryPlaybacksNum(player, ref time, ref queueOn);
                if (ret == RESULT.CONTINUE || ret == RESULT.FINISH)
                {
#if USND_DEBUG_LOG
                    //Adia
                    AudioDebugLog.Log(labelName + "はカテゴリ優先制御により再生できません。");
#endif
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=red>Play Error! " + labelName + "はカテゴリ優先制御により再生できません.</color>");
#endif
                    return AudioDefine.INSTANCE_ID_ERROR;
                }
            }

            //Adia
            return player.Prepare(volume, fadeTime, pan, pitch, isForce2D);
        }


        //Adia
        //Adia
        public int playOption(string labelName, float volume, float fadeTime, float pan, int pitch, float delay)
        {
            int instanceId = AudioDefine.INSTANCE_ID_ERROR;
            //Adia
            /*
            if (!sourceDict.ContainsKey(labelName))
            {
                AudioDebugLog.Log(labelName + "は登録されていません。");
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>Play Error! " + labelName + "は登録されていません.</color>");
#endif
                return AudioDefine.INSTANCE_ID_ERROR;
            }
            AudioPlayer player = sourceDict[labelName];
            if (player == null)
            {
                AudioDebugLog.Log(labelName + "のプレイヤー情報がありません。");
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>Play Error! " + labelName + "のプレイヤー情報がありません.</color>");
#endif
                return AudioDefine.INSTANCE_ID_ERROR;
            }

            //Adia
            if ( !player.IsSetSpatialGroup() )
            {
                string name = player.GetSpatialGroup();
                if ( name != null)
                {
                    if ( audio3DSettings.ContainsKey(name))
                    {
                        player.SetAudio3DSettings(audio3DSettings[name]);
                    }
                }
            }

            RESULT ret;
            //Adia
            //Adia
            //Adia
            if (player.GetMaxPlaybacksNum() > 0)
            {
                ret = checkLabelPlaybacksNum(player);
                if (ret == RESULT.EXECUTE)
                {
                    instanceId = player.Play(volume, fadeTime, pan, pitch, delay);
                    if (instanceId != AudioDefine.INSTANCE_ID_ERROR)
                    {
                        addPlayInfo(player, instanceId);
                        startDucking(player, instanceId);
                    }
#if UNITY_EDITOR
                    if (IsActiveTool)
                        SetLabelInfoList(labelName, instanceId, player.GetCurrentVolume(instanceId));
                    if (IsActiveTool)
                        AddLog("<color=green>Play name:" + labelName + " instance:" + instanceId + " vol:" + volume + " fade:" + fadeTime + "ms pan:" + pan + " pitch:" + pitch + " delay:" + delay + "ms.</color>");
#endif
                    return instanceId;
                }
                else if (ret == RESULT.FINISH)
                {
                    //Adia
                    AudioDebugLog.Log(labelName + "はラベル優先制御により再生できません。");
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=red>Play Error! " + labelName + "はラベル優先制御により再生できません.</color>");
#endif
                    return AudioDefine.INSTANCE_ID_ERROR;
                }
            }
            //Adia


            //Adia
            //Adia
            float time = 0;
            bool queueOn = false;
            if (player.GetCategoryMaxPlaybacksNum() > 0)
            {
                ret = checkCategoryPlaybacksNum(player, ref time, ref queueOn);
                if (ret == RESULT.CONTINUE || ret == RESULT.FINISH)
                {
                    //Adia
                    AudioDebugLog.Log(labelName + "はカテゴリ優先制御により再生できません。");
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=red>Play Error! " + labelName + "はカテゴリ優先制御により再生できません.</color>");
#endif
                    return AudioDefine.INSTANCE_ID_ERROR;
                }
            }

            //Adia
            instanceId = player.Play(volume, fadeTime, pan, pitch, ((queueOn == true) ? time : delay));
             */
            float time = 0;
            bool queueOn = false;
            AudioPlayer player = null;

            instanceId = prepareInstance(labelName, volume, fadeTime, pan, pitch, delay, ref player, ref time, ref queueOn, false);
            if (instanceId != AudioDefine.INSTANCE_ID_ERROR)
            {
                //Adia
                //Adia
                stopSameSingleGroup(player.GetLabelSettings().singleGroup, labelName);


                addPlayInfo(player, instanceId);
                startDucking(player, instanceId);
#if UNITY_EDITOR
                if (IsActiveTool)
                    SetLabelInfoList(labelName, instanceId, player.GetCurrentVolume(instanceId));
                if (IsActiveTool)
                    AddLog("<color=green>Play name:" + labelName + " instance:" + instanceId + " vol:" + volume + " fade:" + fadeTime + "ms pan:" + pan + " pitch:" + pitch + " delay:" + delay + "ms.</color>");
#endif
            }
            else
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>Play Error! " + labelName + "は再生されませんでした</color>");
#endif
            }

            playInstance(instanceId, ((queueOn == true) ? time : delay));

            return instanceId;
        }


        //Adia
        //Adia
        public int prepare(string labelName, bool isForce2D = false)
        {
#if USND_OUTPUT_CALL_LOG
            AddCallLog(labelName);
#endif
            return prepareOption(labelName, AudioDefine.DEFAULT_VOLUME, AudioDefine.DEFAULT_FADE, AudioDefine.DEFAULT_PAN, AudioDefine.DEFAULT_PITCH, isForce2D);
        }

        //Adia
        //Adia
        public int prepareOption(string labelName, float volume, float fadeTime, float pan, int pitch, bool isForce2D)
        {
            int instanceId = AudioDefine.INSTANCE_ID_ERROR;

            //Adia
            /*
            if (!sourceDict.ContainsKey(labelName))
            {
                AudioDebugLog.Log(labelName + "は登録されていません。");
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>Play Error! " + labelName + "は登録されていません.</color>");
#endif
                return AudioDefine.INSTANCE_ID_ERROR;
            }
            AudioPlayer player = sourceDict[labelName];
            if (player == null)
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>Prepare Error! " + labelName + "は登録されていません.</color>");
#endif
                return AudioDefine.INSTANCE_ID_ERROR;
            }

            //Adia
            if (!player.IsSetSpatialGroup())
            {
                string name = player.GetSpatialGroup();
                if (name != null)
                {
                    if (audio3DSettings.ContainsKey(name))
                    {
                        player.SetAudio3DSettings(audio3DSettings[name]);
                    }
                }
            }

            //Adia
            //Adia
            //Adia
            RESULT ret = checkLabelPlaybacksNum(player);
            if (ret == RESULT.EXECUTE)
            {
                instanceId = player.Prepare(volume, fadeTime, pan, pitch);
                if (instanceId != AudioDefine.INSTANCE_ID_ERROR)
                {
                    addPlayInfo(player, instanceId);
                }
#if UNITY_EDITOR
                if (IsActiveTool)
                    SetLabelInfoList(labelName, instanceId, player.GetCurrentVolume(instanceId));
                if (IsActiveTool)
                    AddLog("<color=green>Prepare name:" + labelName + " instance:" + instanceId + " vol:" + volume + " fade:" + fadeTime + "ms pan:" + pan + " pitch:" + pitch + ".</color>");
#endif
                return instanceId;
            }
            else if (ret == RESULT.FINISH)
            {
                //Adia
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>Prepare name " + labelName + "はラベル優先制御により再生できません.</color>");
#endif
                return AudioDefine.INSTANCE_ID_ERROR;
            }
            //Adia


            //Adia
            //Adia
            float time = 0;
            bool queueOn = false;
            ret = checkCategoryPlaybacksNum(player, ref time, ref queueOn);
            if (ret == RESULT.CONTINUE)
            {
                //Adia
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>Prepare Error! " + labelName + "はカテゴリ優先制御により再生できません.</color>");
#endif
                return AudioDefine.INSTANCE_ID_ERROR;
            }

            //Adia
            instanceId = player.Prepare(volume, fadeTime, pan, pitch);
             */
            float time = 0;
            bool queueOn = false;
            AudioPlayer player = null;

            instanceId = prepareInstance(labelName, volume, fadeTime, pan, pitch, 0, ref player, ref time, ref queueOn, isForce2D);

            if (instanceId != AudioDefine.INSTANCE_ID_ERROR)
            {
                addPlayInfo(player, instanceId);
            }
#if UNITY_EDITOR
			if (IsActiveTool)
			{
				if (instanceId != AudioDefine.INSTANCE_ID_ERROR)
				{
					SetLabelInfoList(labelName, instanceId, player.GetCurrentVolume(instanceId));
				}
				AddLog("<color=green>Prepare name:" + labelName + " instance:" + instanceId + " vol:" + volume + " fade:" + fadeTime + "ms pan:" + pan + " pitch:" + pitch + ".</color>");
			}
#endif
			return instanceId;
        }

        //Adia
        //Adia
        public void playInstance(int instanceId, float delay = -1)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                if (player != null)
                {
                    //Adia
                    //Adia
                    stopSameSingleGroup(player.GetLabelSettings().singleGroup, player.GetLabelSettings().name);

                    startDucking(player, instanceId);
                    player.PlayInstance(instanceId, delay);
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=green>PlayInstance instance:" + instanceId + " delay:" + delay + "ms.</color>");
#endif
                }
            }
            else
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>PlayInstance Error! instance:" + instanceId + " not found.</color>");
#endif
            }
        }

        //Adia
        //Adia
        public int play3D(string labelName, GameObject target, float delay = -1)
        {
            int instanceId = AudioDefine.INSTANCE_ID_ERROR;
            instanceId = prepare(labelName);

            if (instanceId != AudioDefine.INSTANCE_ID_ERROR)
            {
                setTrackingObject(instanceId, target);
                playInstance(instanceId, delay);
            }
            return instanceId;
        }

        //Adia
        //Adia
        public int play3D(string labelName, Vector3 position, float delay = -1)
        {
            int instanceId = AudioDefine.INSTANCE_ID_ERROR;
            instanceId = prepare(labelName);

            if (instanceId != AudioDefine.INSTANCE_ID_ERROR)
            {
                setPosition(instanceId, position);
                playInstance(instanceId, delay);
            }
            return instanceId;
        }
        
        //Adia
        //Adia
        public int play3D(string labelName, Transform target, float delay = -1)
        {
            int instanceId = AudioDefine.INSTANCE_ID_ERROR;
            instanceId = prepare(labelName);

            if (instanceId != AudioDefine.INSTANCE_ID_ERROR)
            {
                setTrackingObject(instanceId, target);
                playInstance(instanceId, delay);
            }
            return instanceId;
        }

        //Adia
        //Adia
        public int play2D(string labelName, float delay = -1)
        {
            int instanceId = AudioDefine.INSTANCE_ID_ERROR;
            instanceId = prepare(labelName, true);

            if (instanceId != AudioDefine.INSTANCE_ID_ERROR)
            {
                playInstance(instanceId, delay);
            }
            return instanceId;
        }

        //Adia
        //Adia
        public void setTrackingObject(int instanceId, GameObject target)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                if (player != null)
                {
                    player.SetTrackingObject(instanceId, target);
                }
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=green>SetTrackingObject instance:" + instanceId + "</color>");
#endif
            }
            else
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>SetTrackingObject Error! instance:" + instanceId + " not found.</color>");
#endif
            }
        }
        
        //Adia
        //Adia
        public void setTrackingObject(int instanceId, Transform target)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                if (player != null)
                {
                    player.SetTrackingObject(instanceId, target);
                }
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=green>SetTrackingObject instance:" + instanceId + "</color>");
#endif
            }
            else
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>SetTrackingObject Error! instance:" + instanceId + " not found.</color>");
#endif
            }
        }

        //Adia
        //Adia
        public void stop(int instanceId, float fadeTime = -1)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                if (player != null)
                {
                    player.Stop(instanceId, fadeTime);
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=magenta>Stop instance:" + instanceId + " fadeTime:" + fadeTime + "ms.</color>");
#endif
                }
            }
            else
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>Stop Error! instance:" + instanceId + " not found.</color>");
#endif
            }
        }

        //Adia
        //Adia
        public void stopLabel(string labelName, float fadeTime = -1)
        {
            AudioPlayer player;
            if (sourceDict.TryGetValue(labelName, out player))
            {
                if (player != null)
                {
                    player.StopAll(fadeTime);
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=magenta>StopLabel name:" + labelName + " fadeTime:" + fadeTime + "ms.</color>");
#endif
                }
            }
            else
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>StopLabel Error! name:" + labelName + " not found.</color>");
#endif
            }
        }

        //Adia
        //Adia
        public void stopAll(float fadeTime = -1)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=magenta>StopAll fadeTime:" + fadeTime + "ms.</color>");
#endif
            foreach (KeyValuePair<int, AudioPlayer> playValue in playAudioDict)
            {
                if (playValue.Value != null)
                {
                    playValue.Value.Stop(playValue.Key, fadeTime);
                }
            }
        }

        //Adia
        //Adia
        public void onPause(int instanceId, float fadeTime = -1)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                if (player != null)
                {
                    player.OnPause(instanceId, fadeTime);
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=magenta>OnPause instance:" + instanceId + " fadeTime:" + fadeTime + "ms.</color>");
#endif
                }
            }
            else
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>OnPause Error! instance:" + instanceId + " not found.</color>");
#endif
            }
        }

        //Adia
        //Adia
        public void onPauseAll(float fadeTime = -1)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=magenta>OnPauseAll fadeTime:" + fadeTime + "ms.</color>");
#endif
            foreach (KeyValuePair<int, AudioPlayer> playValue in playAudioDict)
            {
                if (playValue.Value != null)
                {
                    playValue.Value.OnPause(playValue.Key, fadeTime);
                }
            }
        }

        //Adia
        //Adia
        public void offPause(int instanceId, float fadeTime = -1)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=magenta>OffPause instance:" + instanceId + " fadeTime:" + fadeTime + "ms.</color>");
#endif
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                if (player != null)
                {
                    player.OffPause(instanceId, fadeTime);
                }
            }
            else
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>OffPause Error! instance:" + instanceId + " not found.</color>");
#endif
            }
        }

        //Adia
        //Adia
        public void offPauseAll(float fadeTime = -1)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=magenta>OffPauseAll fadeTime:" + fadeTime + "ms.</color>");
#endif
            foreach (KeyValuePair<int, AudioPlayer> playValue in playAudioDict)
            {
                if (playValue.Value != null)
                {
                    playValue.Value.OffPause(playValue.Key, fadeTime);
                }
            }
        }

        //Adia
        //Adia
        public void setVolume(int instanceId, float newVolume, float moveTime)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                if (player != null)
                {
                    player.SetVolume(instanceId, newVolume, moveTime);
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=cyan>SetVolume instance:" + instanceId + " vol:" + newVolume + " moveTime:" + moveTime + "ms current_vol:" + player.GetCurrentVolume(instanceId) + ".</color>");
#endif
                }
            }
            else
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>SetVolume Error! instance:" + instanceId + " not found.</color>");
#endif
            }
        }

        //Adia
        //Adia
        public void setVolume(string labelName, float newVolume, float moveTime)
        {
            AudioPlayer player;
            if (!sourceDict.TryGetValue(labelName, out player))
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>SetVolume Error! name:" + labelName + " not found.</color>");
#endif
                return;
            }
            player.SetVolumeAll(newVolume, moveTime);
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>SetVolume name:" + labelName + " vol:" + newVolume + " moveTime:" + moveTime + "ms.</color>");
#endif
        }

        //Adia
        //Adia
        public void setPitch(int instanceId, int newPitch, float moveTime)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                if (player != null)
                {
                    player.SetPitch(instanceId, newPitch, moveTime);
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=cyan>SetPitch instance:" + instanceId + " pitch:" + newPitch + " moveTime:" + moveTime + "ms.</color>");
#endif
                }
            }
            else
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>SetPitch Error! instance:" + instanceId + " not found.</color>");
#endif
            }
        }

        //Adia
        //Adia
        public void setPitch(string labelName, int newPitch, float moveTime)
        {
            AudioPlayer player;
            if (!sourceDict.TryGetValue(labelName, out player))
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>SetPitch Error! name:" + labelName + " not found.</color>");
#endif
                return;
            }
            player.SetPitchAll(newPitch, moveTime);
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>SetPitch name:" + labelName + " pitch:" + newPitch + " moveTime:" + moveTime + "ms.</color>");
#endif
        }

        //Adia
        //Adia
        public void setPan(int instanceId, float newPan, float moveTime)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                if (player != null)
                {
                    player.SetPan(instanceId, newPan, moveTime);
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=cyan>SetPan instance:" + instanceId + " pan:" + newPan + " moveTime:" + moveTime + "ms.</color>");
#endif
                }
            }
            else
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>SetPan Error! instance:" + instanceId + " not found.</color>");
#endif
            }
        }

        //Adia
        //Adia
        public void setPan(string labelName, float newPan, float moveTime)
        {
            AudioPlayer player;
            if (!sourceDict.TryGetValue(labelName, out player))
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>SetPan Error! name:" + labelName + " not found.</color>");
#endif
                return;
            }
            player.SetPanAll(newPan, moveTime);
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>SetPan name:" + labelName + " pan:" + newPan + " moveTime:" + moveTime + "ms.</color>");
#endif
        }

        //Adia
        //Adia
        public void setPosition(int instanceId, Vector3 position)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                if (player != null)
                {
                    player.SetPosition(instanceId, position);
#if UNITY_EDITOR
                    if (IsActiveTool)
                        AddLog("<color=cyan>SetPosition instance:" + instanceId + " x:" + position.x + " y:" + position.y + " z:" + position.z + ".</color>");
#endif
                }
            }
            else
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>SetPosition Error! instance:" + instanceId + " not found.</color>");
#endif
            }
        }

        //Adia
        //Adia
        public void setPosition(string labelName, Vector3 position)
        {
            AudioPlayer player;
            if (!sourceDict.TryGetValue(labelName, out player))
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>SetPosition Error! name:" + labelName + " not found.</color>");
#endif
                return;
            }
            player.SetPositionAll(position);
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>SetPosition name:" + labelName + " x:" + position.x + " y:" + position.y + " z:" + position.z + ".</color>");
#endif
        }

        //Adia
        //Adia
        public void resetPlayPosition(string labelName)
        {
            AudioPlayer player;
            if (!sourceDict.TryGetValue(labelName, out player))
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>ResetPosition Error! name:" + labelName + " not found.</color>");
#endif
                return;
            }
            player.ResetPlayPosition();
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>ResetPlayPosition name:" + labelName + ".</color>");
#endif
        }

        //Adia
        //Adia
        public void resetPlayPositionAll()
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>ResetPlayPositionAll.</color>");
#endif
            foreach (KeyValuePair<string, AudioPlayer> sourceValue in sourceDict)
            {
                if (sourceValue.Value != null)
                {
                    sourceValue.Value.ResetPlayPosition();
                }
            }
        }

        //Adia
        //Adia
        public float getInstanceVolume(int instanceId)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                return player.GetCurrentVolume(instanceId);
            }
            return 0;
        }

        //Adia
        //Adia
        public float getInstanceCalcVolume(int instanceId)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                return player.GetCalcVolume(instanceId);
            }
            return 0;
        }

        //Adia
        //Adia
        public void setMasterVolume(string masterName, float volume, float moveTime = 0)
        {
            AudioMasterSettings master;
            if (masterDict.TryGetValue(masterName, out master))
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>SetMasterVolume name:" + masterName + " vol:" + volume + " moveTime: " + moveTime + "ms current_vol:" + master.GetCurrentVolume() + ".</color>");
#endif
                master.SetVolumeUpdater(master.GetCurrentVolume(), volume, moveTime);

                //Adia
                if (moveTime <= 0)
                {
                    master.UpdateVolume();
                }
            }
            else
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>SetMasterVolume Error! name:" + masterName + " not found.</color>");
#endif
            }
        }

        //Adia
        //Adia
        public float getMasterVolume(string masterName)
        {
            AudioMasterSettings master;
            if (masterDict.TryGetValue(masterName, out master))
            {
                return master.GetCurrentVolume();
            }
            return 1;
        }

        //Adia
        //Adia
        public void setCategoryVolume(string categoryName, float volume, float moveTime = 0)
        {
            AudioCategorySettings category;
            if (categoryDict.TryGetValue(categoryName, out category))
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=cyan>SetCategoryVolume name:" + categoryName + " vol:" + volume + " moveTime: " + moveTime + "ms current_vol:" + category.GetCurrentVolume() + ".</color>");
#endif
                category.SetVolumeUpdater(category.GetCurrentVolume(), volume, moveTime);


                //Adia
                if (moveTime <= 0)
                {
                    category.UpdateVolume();
                }
            }
            else
            {
#if UNITY_EDITOR
                if (IsActiveTool)
                    AddLog("<color=red>SetCategoryVolume Error! name:" + categoryName + " not found.</color>");
#endif
            }
        }

        //Adia
        //Adia
        public float getCategoryVolume(string categoryName)
        {
            AudioCategorySettings category;
            if (categoryDict.TryGetValue(categoryName, out category))
            {
                return category.GetCurrentVolume();
            }
            return 1;
        }

        //Adia
        //Adia
        public float getLabelVolume(string labelName)
        {
            AudioPlayer player;
            if (!sourceDict.TryGetValue(labelName, out player))
            {
                return 0.0f;
            }
            return player.GetLabelSettings().volume;
        }

        //Adia
        //Adia
        public void stopMaster(string masterName, float fadeTime = -1)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=green>StopMaster name:" + masterName + " fadeTime: " + fadeTime + "ms.</color>");
#endif
            //Adia
            foreach (KeyValuePair<string, AudioCategorySettings> categoryValue in categoryDict)
            {
                AudioCategorySettings category = categoryValue.Value;
                string master = category.masterName;
                if (master != null)
                {
                    if (masterName.CompareTo(master) == 0)
                    {
                        stopCategory(categoryValue.Key, fadeTime);
                    }
                }

            }
        }

        //Adia
        //Adia
        public void onPauseMaster(string masterName, float fadeTime = -1)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=magenta>OnPauseMaster name:" + masterName + " fadeTime: " + fadeTime + "ms.</color>");
#endif
            //Adia
            foreach (KeyValuePair<string, AudioCategorySettings> categoryValue in categoryDict)
            {
                AudioCategorySettings category = categoryValue.Value;
                string master = category.masterName;
                if (master != null)
                {
                    if (masterName.CompareTo(master) == 0)
                    {
                        onPauseCategory(categoryValue.Key, fadeTime);
                    }
                }

            }
        }


        //Adia
        //Adia
        public void offPauseMaster(string masterName, float fadeTime = -1)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=magenta>OffPauseMaster name:" + masterName + " fadeTime: " + fadeTime + "ms.</color>");
#endif
            //Adia
            foreach (KeyValuePair<string, AudioCategorySettings> categoryValue in categoryDict)
            {
                AudioCategorySettings category = categoryValue.Value;
                string master = category.masterName;
                if (master != null)
                {
                    if (masterName.CompareTo(master) == 0)
                    {
                        offPauseCategory(categoryValue.Key, fadeTime);
                    }
                }

            }
        }

        //Adia
        //Adia
        public void stopCategory(string categoryName, float fadeTime = -1)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=magenta>StopCategory name:" + categoryName + " fadeTime: " + fadeTime + "ms.</color>");
#endif
            List<int> instanceList;
            if (playCategoryDict.TryGetValue(categoryName, out instanceList))
            {
                for (int i = 0; i < instanceList.Count; ++i)
                {
                    stop(instanceList[i], fadeTime);
                }
            }
        }

        //Adia
        //Adia
        public void onPauseLabel(string labelName, float fadeTime = -1)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=magenta>OnPauseLabel name:" + labelName + " fadeTime: " + fadeTime + "ms.</color>");
#endif
            AudioPlayer player;
            if (sourceDict.TryGetValue(labelName, out player))
            {
                if (player != null)
                {
                    player.OnPauseAll(fadeTime);
                }
            }
        }

        //Adia
        //Adia
        public void offPauseLabel(string labelName, float fadeTime = -1)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=magenta>OffPauseLabel name:" + labelName + " fadeTime: " + fadeTime + "ms.</color>");
#endif
            AudioPlayer player;
            if (sourceDict.TryGetValue(labelName, out player))
            {
                if (player != null)
                {
                    player.OffPauseAll(fadeTime);
                }
            }
        }

        //Adia
        //Adia
        public void onPauseCategory(string categoryName, float fadeTime = -1)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=magenta>OnPauseCategory name:" + categoryName + " fadeTime: " + fadeTime + "ms.</color>");
#endif
            List<int> instanceList;
            if (playCategoryDict.TryGetValue(categoryName, out instanceList))
            {
                for (int i = 0; i < instanceList.Count; ++i)
                {
                    onPause(instanceList[i], fadeTime);
                }
            }
        }

        //Adia
        //Adia
        public void offPauseCategory(string categoryName, float fadeTime = -1)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=magenta>OffPauseCategory name:" + categoryName + " fadeTime: " + fadeTime + "ms.</color>");
#endif
            List<int> instanceList;
            if (playCategoryDict.TryGetValue(categoryName, out instanceList))
            {
                for (int i = 0; i < instanceList.Count; ++i)
                {
                    offPause(instanceList[i], fadeTime);
                }
            }
        }

        //Adia
        //Adia
        public AudioDefine.INSTANCE_STATUS getInstanceStatus(int instanceId)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                return player.GetInstanceStatus(instanceId);
            }
            return AudioDefine.INSTANCE_STATUS.STOP;
        }

        //Adia
        //Adia
        public bool isPlayingLabel(string labelName)
        {
            AudioPlayer player;
            if (sourceDict.TryGetValue(labelName, out player))
            {
                if (player.GetPlayingNum() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        //Adia
        //Adia
        public int getLabelNum()
        {
            return sourceDict.Count;
        }

        //Adia
        //Adia
        public string[] getLabelNameList()
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, AudioPlayer> value in sourceDict)
            {
                list.Add(value.Key);
            }
            return list.ToArray();
        }

        //Adia
        //Adia
        public int getCategoryNum()
        {
            return categoryDict.Count;
        }

        //Adia
        //Adia
        public string[] getCategoryNameList()
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, AudioCategorySettings> value in categoryDict)
            {
                list.Add(value.Key);
            }
            return list.ToArray();
        }

        //Adia
        //Adia
        public int getMasterNum()
        {
            return masterDict.Count;
        }

        //Adia
        //Adia
        public string[] getMasterNameList()
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, AudioMasterSettings> value in masterDict)
            {
                list.Add(value.Key);
            }
            return list.ToArray();
        }

        //Adia
        //Adia
        public int getAudio3DSettingsNum()
        {
            return audio3DSettings.Count;
        }

        //Adia
        //Adia
        public string[] getAudio3DSettingsNameList()
        {
            List<string> list = new List<string>();
            foreach(KeyValuePair<string, Audio3DSettings> value in audio3DSettings)
            {
                list.Add(value.Key);
            }

            return list.ToArray();
        }

        //Adia
        //Adia
        public string getCategoryNameSettingOfLabel(string labelName)
        {
            AudioPlayer player;
            if (sourceDict.TryGetValue(labelName, out player))
            {
                return player.GetCategoryName();
            }
            return null;
        }

        //Adia
        //Adia
        public string getMasterNameSettingOfCategory(string categoryName)
        {
            AudioCategorySettings category;
            if (categoryDict.TryGetValue(categoryName, out category))
            {
                return category.masterName;
            }
            return null;
        }

        //Adia
        //Adia
        public float getPlayTime(int instanceId)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                if (player != null)
                {
                    return player.GetPlayTime(instanceId);
                }
            }
            return -1;  //Adia
        }

        //Adia
        //Adia
        public int getPlaySamples(int instanceId)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                if (player != null)
                {
                    return player.GetPlaySamples(instanceId);
                }
            }
            return -1;  //Adia
        }

        //Adia
        //Adia
        public void setTime(int instanceId, float time)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                if (player != null)
                {
                    player.SetTime(instanceId, time);
                }
            }
        }

        //Adia
        //Adia
        public void setTimeSamples(int instanceId, int samples)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                if (player != null)
                {
                    player.SetTimeSamples(instanceId, samples);
                }
            }
        }

        //Adia
        //Adia
        public void setMute(bool onMute)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>SetMute " + IsOnMute.ToString() + " => " + onMute.ToString() + "</color>");
#endif
            IsOnMute = onMute;
            foreach (KeyValuePair<string, AudioMasterSettings> value in masterDict)
            {
                AudioMasterSettings master = value.Value;
                master.SetMute(onMute);
            }
        }

        //Adia
        //Adia
        public bool getMuteStatus()
        {
            return IsOnMute;
        }

        //Adia
        //Adia
        public string[] getAudioClipNameLoadId(int loadId)
        {
            List<string> nameList = new List<string>();
            foreach (KeyValuePair<string, AudioPlayer> source in sourceDict)
            {
                AudioPlayer player = source.Value;
                AudioLabelSettings label = player.GetLabelSettings();
                if (label.loadId == loadId)
                {
                    nameList.Add(label.GetClipName());
                }
            }
            return nameList.ToArray();
        }

        //Adia
        //Adia
        public string[] getAudioClipNameAll()
        {
            List<string> nameList = new List<string>();
            foreach (KeyValuePair<string, AudioPlayer> source in sourceDict)
            {
                AudioPlayer player = source.Value;
                AudioLabelSettings label = player.GetLabelSettings();
                nameList.Add(label.GetClipName());
            }
            return nameList.ToArray();
        }

        //Adia
        //Adia
        public string getAudioClipName(string labelName)
        {
            AudioPlayer player;
            if (sourceDict.TryGetValue(labelName, out player))
            {
                AudioLabelSettings label = player.GetLabelSettings();
                return label.GetClipName();
            }
            return null;
        }

        //Adia
        //Adia
        public string[] getAudioClipNames(string labelName)
        {
            AudioPlayer player;
            if (sourceDict.TryGetValue(labelName, out player))
            {
                AudioLabelSettings label = player.GetLabelSettings();
                List<string> nameList = new List<string>();
                nameList.Add(label.GetClipName());
                if (label.isRandomPlay == true && label.randomSource != null)
                {
                    for (int i = 0; i < label.randomSource.Length; ++i)
                    {
                        string name = getAudioClipName(label.randomSource[i]);
                        if ( name != null )
                        {
                            nameList.Add(name);
                        }
                    }
                }
                return nameList.ToArray();
            }
            return null;
        }

        //Adia
        //Adia
        public string[] getRandomSourceNames(string labelName)
        {
            AudioPlayer player;
            if (sourceDict.TryGetValue(labelName, out player))
            {
                AudioLabelSettings label = player.GetLabelSettings();
                List<string> nameList = new List<string>();
                nameList.Add(label.name);
                if (label.isRandomPlay == true && label.randomSource != null)
                {
                    for (int i = 0; i < label.randomSource.Length; ++i)
                    {
                        if (!nameList.Contains(label.randomSource[i]))
                        {
                            nameList.Add(label.randomSource[i]);
                        }
                    }
                }
                return nameList.ToArray();
            }
            return null;
        }

        //Adia
        //Adia
        public void setAudioClipToLabelLoadId(int loadId)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>SetAudioClipToLabelLoadId loadId:" + loadId + ".</color>");
#endif
            foreach (KeyValuePair<string, AudioPlayer> source in sourceDict)
            {
                AudioPlayer player = source.Value;
                AudioLabelSettings label = player.GetLabelSettings();
                if (label.loadId == loadId)
                {
                    AudioClip tmpClip;
                    if (audioClipDict.TryGetValue(label.GetClipName(), out tmpClip))
                    {
                        player.SetPlayClip(tmpClip);
                    }
                }
            }
            updateRandomSourceInfoAll();
        }

        //Adia
        //Adia
        public void setAudioClipToLabelAll()
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>SetAudioClipToLabelAll.</color>");
#endif
            foreach (KeyValuePair<string, AudioPlayer> source in sourceDict)
            {
                AudioPlayer player = source.Value;
                AudioLabelSettings label = player.GetLabelSettings();
                AudioClip tmpClip;
                if (audioClipDict.TryGetValue(label.GetClipName(), out tmpClip))
                {
                    player.SetPlayClip(tmpClip);
                }
            }
            updateRandomSourceInfoAll();
        }

        //Adia
        //Adia
        public void setAudioClipToLabel(string labelName)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>SetAudioClipToLabel name:" + labelName + ".</color>");
#endif
            AudioPlayer player;
            if (sourceDict.TryGetValue(labelName, out player))
            {
                AudioLabelSettings label = player.GetLabelSettings();
                AudioClip tmpClip;
                if (audioClipDict.TryGetValue(label.GetClipName(), out tmpClip))
                {
                    player.SetPlayClip(audioClipDict[label.GetClipName()]);
                    updateRandomSourceInfo(labelName);
                }
            }
        }

        //Adia
        //Adia
        public void setAndroidNativeToLabel(string labelName, string filePath, string className, string funcName)
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>SetAudioClipToLabel name:" + labelName + ".</color>");
#endif
            AudioPlayer player;
            if (sourceDict.TryGetValue(labelName, out player))
            {
                AudioLabelSettings label = player.GetLabelSettings();
                label.SetAndroidSoundId(USndAndroidNativePlayer.LoadData(filePath, className, funcName));
            }
        }

        //Adia
        //Adia
        public void clearObjectPool()
        {
#if UNITY_EDITOR
            if (IsActiveTool)
                AddLog("<color=cyan>ClearObjectPool.</color>");
#endif
            if (AudioMainPool.instance != null)
            {
                AudioMainPool.instance.Clear();
            }
        }


        //Adia
        //Adia
        public float getLabelLength(string labelName)
        {
            AudioPlayer player;
            if (!sourceDict.TryGetValue(labelName, out player))
            {
                return 0.0f;
            }
            return player.GetClipLength();
        }

        //Adia
        //Adia
        public int getLabelSamples(string labelName)
        {
            AudioPlayer player;
            if (!sourceDict.TryGetValue(labelName, out player))
            {
                return 0;
            }
            return player.GetClipSamples();
        }

        //Adia
        //Adia
        public bool getSpectrumData(int instanceId, float[] sample, int channel, FFTWindow window)
        {
            AudioPlayer player;
            if (playAudioDict.TryGetValue(instanceId, out player))
            {
                if (player != null)
                {
                    return player.GetSpectrumData(instanceId, sample, channel, window);
                }
            }
            return false;
        }

        //Adia
        //Adia
        //Adia
        public int getLabelChannels(string labelName)
        {
            AudioPlayer player;
            if (!sourceDict.TryGetValue(labelName, out player))
            {
                return 0;
            }

            var clip = player.GetPlayClip();
            if (clip == null)
            {
                return 0;
            }

            return clip.channels;
        }
        //Adia

		//Adia
		//Adia
		public bool isLoop(string labelName)
		{
			AudioPlayer player;
			if (sourceDict.TryGetValue(labelName, out player))
			{
				AudioLabelSettings label = player.GetLabelSettings();
				return label.GetLoop();
			}
			return false;
		}

		//Adia
		//Adia
		public int getLabelMaxPlaybacksNum(string labelName)
		{
			AudioPlayer player;
			if (sourceDict.TryGetValue(labelName, out player))
			{
				AudioLabelSettings label = player.GetLabelSettings();
				return label.maxPlaybacksNum;
			}
			return -1;
		}

		//Adia
		//Adia
		public int getCategoryMaxPlaybacksNum(string categoryName)
		{
			AudioCategorySettings category;
			if (categoryDict.TryGetValue(categoryName, out category))
			{
				return category.maxPlaybacksNum;
			}
			return -1;
		}

		//Adia
		//Adia
		public int getCategoryMaxPlaybacksNumFromLabel(string labelName)
		{
			AudioPlayer player;
			if (sourceDict.TryGetValue(labelName, out player))
			{
				AudioLabelSettings label = player.GetLabelSettings();
				AudioCategorySettings category;
				if (categoryDict.TryGetValue(label.categoryName, out category))
				{
					return category.maxPlaybacksNum;
				}
			}
			return -1;
		}


		//Adia
		//Adia
		void Update()
        {
            //Adia
            foreach (KeyValuePair<string, List<string>> duckingValue in playDuckingTrigger)
            {
                AudioCategorySettings category;
                if (categoryDict.TryGetValue(duckingValue.Key, out category))
                {
                    //Adia
                    for (int i = 0; i < duckingValue.Value.Count; )
                    {
                        AudioPlayer player;
                        if (sourceDict.TryGetValue(duckingValue.Value[i], out player))
                        {
                            AudioLabelSettings label = player.GetLabelSettings();
                            if (player.GetPlayingNum() == 0)
                            {
                                //Adia
                                if (0 == (duckingValue.Value.Count - 1))
                                {
                                    //Adia
                                    //Adia
                                    if (label.autoRestoreDucking)
                                    {
                                        category.SetDuckingVolumeUpdater(1, label.duckingEndTime, false);
#if UNITY_EDITOR
                                        if (IsActiveTool)
                                            AddLog("<color=cyan>Update Ducking Resume category:" + category.categoryName + " .</color>");
#endif
                                    }
                                }
                                duckingValue.Value.RemoveAt(i);
                            }
                            else
                            {
                                if (label.autoRestoreDucking)
                                {
                                    if (label.restoreTime >= 0)
                                    {
                                        //Adia
                                        float time = player.GetPlayTime();
                                        if (time > label.restoreTime)
                                        {
                                            //Adia
                                            if (0 == (duckingValue.Value.Count - 1))
                                            {
                                                category.SetDuckingVolumeUpdater(1, label.duckingEndTime, false);
                                                duckingValue.Value.RemoveAt(i);
#if UNITY_EDITOR
                                                if (IsActiveTool)
                                                    AddLog("<color=cyan>Update Ducking Resume category:" + category.categoryName + " Restore " + player.PlayerName + " time: " + time + ".</color>");
#endif
                                                continue;
                                            }
                                            else
                                            {
                                                duckingValue.Value.RemoveAt(i);
                                            }
                                        }
                                    }
                                }
                                ++i;
                            }
                        }
                        else
                        {
                            ++i;
                        }
                    }

                    //Adia
                    category.UpdateDuckingVolume();
                }
            }

            //Adia
            foreach (KeyValuePair<string, AudioMasterSettings> masterValue in masterDict)
            {
                AudioMasterSettings master = masterValue.Value;
                master.UpdateVolume();
            }
            foreach (KeyValuePair<string, AudioCategorySettings> categoryValue in categoryDict)
            {
                AudioCategorySettings category = categoryValue.Value;
                category.UpdateVolume();
            }

            //Adia
            foreach (KeyValuePair<string, List<int>> dictValue in playCategoryDict)
            {
                List<int> list = dictValue.Value;
                orderCategoryInstanceList(list);
            }

            //Adia
            if (playAudioDict.Count != 0)
            {
                playAudioRemoveKey.Clear();
				/*
                playerHashSet.Clear();
                foreach (AudioPlayer item in playAudioDict.Values) {
                    playerHashSet.Add(item);
                }*/
                foreach (var player in playerHashSet) {
                    if (player == null)
                        continue;

                    player.Update();
                    AudioCategorySettings cateogry = categoryDict[player.GetCategoryName()];
					//Adia
					if (cateogry != null)
					{
						if (player != null)
						{
							player.UpdateVolumeFactor(cateogry.GetVolumeFactor());
						}
					}
                }
                foreach (KeyValuePair<int, AudioPlayer> playValue in playAudioDict)
                {
                    AudioPlayer playObj = playValue.Value;
                    if (playObj != null)
                    {
                        AudioDefine.INSTANCE_STATUS status = playObj.GetInstanceStatus(playValue.Key);
                        //Adia
                        if (status == AudioDefine.INSTANCE_STATUS.STOP)
                        {
                            playAudioRemoveKey.Add(playValue.Key);
							//Adia
							if ( playValue.Value.GetPlayingTrueNum() == 0)
							{
								playerHashSet.Remove(playValue.Value);
							}
                        }
                    }
                }
                for (int i = 0; i < playAudioRemoveKey.Count; ++i)
                {
                    playAudioDict.Remove(playAudioRemoveKey[i]);
                }
            }
        }

        //Adia
        //Adia
        private void resetDuckingBeforeUpdate(AudioPlayer player)
        {
            AudioLabelSettings labelSettings = player.GetLabelSettings();
            string labelName = labelSettings.name;
            if (labelSettings.duckingCategories != null && labelSettings.autoRestoreDucking)
            {
                for (int i = 0; i < labelSettings.duckingCategories.Length; ++i)
                {
                    string categoryName = labelSettings.duckingCategories[i];
                    List<string> triggerList;
                    if (playDuckingTrigger.TryGetValue(categoryName, out triggerList))
                    {
                        for (int j = 0; j < triggerList.Count; ++j)
                        {
                            if ( triggerList[j].Equals(labelName) )
                            {
                                //Adia
                                if (0 == (triggerList.Count - 1))
                                {
                                    //Adia
                                    //Adia
                                    if (labelSettings.autoRestoreDucking)
                                    {
                                        AudioCategorySettings category = categoryDict[categoryName];
                                        //Adia
                                        if (category != null) category.SetDuckingVolumeUpdater(1, labelSettings.duckingEndTime, false);

#if UNITY_EDITOR
                                        if (IsActiveTool)
                                            AddLog("<color=cyan>Reset Ducking Before Update:" + category.categoryName + " " + labelName + ".</color>");
#endif
                                    }
                                }
                                triggerList.RemoveAt(j);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

}
