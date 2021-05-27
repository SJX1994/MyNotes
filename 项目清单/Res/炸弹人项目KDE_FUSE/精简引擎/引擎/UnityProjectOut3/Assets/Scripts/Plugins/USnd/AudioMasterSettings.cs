using UnityEngine;
using System.Collections;

namespace USnd
{
    public partial class AudioManager : MonoBehaviour
    {
        [System.Serializable]
        public class AudioMasterSettings
        {

            public string masterName;

            //Adia
            public float volume = 1;        //Adia


            //Adia
#if UNITY_EDITOR
            AudioParamUpdater volumeUpdater = new AudioParamUpdater("ma_vol");
#else
            AudioParamUpdater volumeUpdater = new AudioParamUpdater();
#endif
            public float programVolume = 1;        //Adia

            public float mute = 1;

            float manner = 1;


            public AudioMasterSettings()
            {
                programVolume = 1;
            }

            public void CopySettings(AudioMasterSettings src)
            {
                if (masterName.CompareTo(src.masterName) == 0)
                {
                    volume = src.volume;
                }
            }

            public float GetCurrentVolume()
            {
                return programVolume;
            }

            public float GetVolumeFactor()
            {
                return programVolume * volume * mute * manner;
            }

            public void SetVolumeUpdater(float start, float target, float time)
            {
                volumeUpdater.SetParam(start, target, time, false);
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

            public void SetMute(bool onMute)
            {
                mute = (onMute) ? 0 : 1;
            }

            public void SetMannerMode(bool onMute)
            {
                manner = (onMute) ? 0 : 1;
            }
        }
    }
}
