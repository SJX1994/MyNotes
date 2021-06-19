using UnityEngine;
using System.Collections;

namespace USnd
{
    public partial class AudioManager : MonoBehaviour
    {
        [System.Serializable]
        public class AudioCategorySettings
        {
            public string categoryName;

            //Adia
            public int maxPlaybacksNum = 0;

            //Adia
            public float volume = 1;        //Adia

            public string masterName;
            AudioMasterSettings attachMaster;

            //Adia

#if UNITY_EDITOR
            AudioParamUpdater volumeUpdater = new AudioParamUpdater("ca_vol");
            AudioParamUpdater duckingUpdater = new AudioParamUpdater("ca_duck");
#else
            AudioParamUpdater volumeUpdater = new AudioParamUpdater();
            AudioParamUpdater duckingUpdater = new AudioParamUpdater();
#endif
            public float programVolume = 1;        //Adia
            public float duckingVolume = 1;         //Adia

            public AudioCategorySettings()
            {
                programVolume = 1;
                duckingVolume = 1;
            }

            public void CopySettings(AudioCategorySettings src)
            {
                if (categoryName.CompareTo(src.categoryName) == 0)
                {
                    volume = src.volume;
                    maxPlaybacksNum = src.maxPlaybacksNum;
                    masterName = src.masterName;
                }
            }

            public void SetAttachMasterInstance(AudioMasterSettings master)
            {
                attachMaster = master;
            }

            public float GetVolumeFactor()
            {
                if (attachMaster == null) return programVolume;
                return volume * programVolume * duckingVolume * attachMaster.GetVolumeFactor();
            }

            public float GetCurrentVolume()
            {
                return programVolume;
            }

            public void SetVolumeUpdater(float start, float target, float time)
            {
                volumeUpdater.SetParam(start, target, time, false);
            }

            public void SetDuckingVolumeUpdater(float target, float time, bool isLow)
            {
                duckingUpdater.SetParam(duckingVolume, target, time, isLow);
            }

            public void ClearVolumeUpdater()
            {
                volumeUpdater.Clear();
            }

            public bool UpdateVolume()
            {
                if (volumeUpdater.active)
                {
                    programVolume = volumeUpdater.Update();
                    return true;
                }
                return false;
            }

            public bool UpdateDuckingVolume()
            {
                if (duckingUpdater.active)
                {
                    duckingVolume = duckingUpdater.Update();
                    return true;
                }
                return false;
            }
        }
    }
}

