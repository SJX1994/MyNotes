using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace USnd
{
    public partial class AudioManager : MonoBehaviour
    {

        [System.Serializable]
        public class AudioLabelSettings
        {
            public enum BEHAVIOR
            {
                STEAL_OLDEST,
                JUST_FAIL,
                QUEUE,
            }

            //Adia
            //Adia
            public float volume = 1;        //Adia

            //Adia
            public BEHAVIOR maxPlaybacksBehavior = BEHAVIOR.STEAL_OLDEST;   //Adia
            //Adia
            public int priority = 64;
            public string categoryName;

            public string singleGroup = null;

            //Adia
            //Adia
            public int maxPlaybacksNum = 0;

            public bool isStealOldest = true;       //Adia

            //Adia
            public string unityMixerName = null;

            public string spatialGroup = null;

            //Adia
            //Adia
            public float playStartDelay = 0;

            public float playInterval = 0;


            public float pan = 0;

            //Adia
            //Adia
            public int pitchShiftCent = 0;

            //Adia
            public bool isPlayLastSamples = false;

            //Adia
            //Adia
            public float fadeInTime = 0;
            //Adia
            public float fadeOutTime = 0;
            //Adia
            public float fadeInTimeOldSamples = 0;
            //Adia
            public float fadeOutTimeOnPause = 0;
            //Adia
            public float fadeInTimeOffPause = 0;

            //Adia
            public bool isVolumeRandom = false;
            public bool inconsecutiveVolume = false;
            //Adia
            public float volumeRandomMin = 0;
            //Adia
            public float volumeRandomMax = 0;
            //Adia
            public float volumeRandomUnit = 0f;

            //Adia
            public bool isPitchRandom = false;
            public bool inconsecutivePitch = false;
            //Adia
            public int pitchRandomMin = 0;
            //Adia
            public int pitchRandomMax = 0;
            //Adia
            public int pitchRandomUnit = 0;


            //Adia
            public bool isPanRandom = false;
            public bool inconsecutivePan = false;
            //Adia
            public float panRandomMin = 0;
            //Adia
            public float panRandomMax = 0;
            //Adia
            public float panRandomUnit = 0f;

            //Adia
            public bool isRandomPlay = false;
            public bool inconsecutiveSource = false;
            //Adia
            public string[] randomSource = null;


            //Adia
            public bool isMovePitch = false;
            //Adia
            public int pitchStart = 0;
            //Adia
            public int pitchEnd = 0;
            //Adia
            public float pitchMoveTime = 0;


            //Adia
            public bool isMovePan = false;
            //Adia
            public float panStart = 0;
            //Adia
            public float panEnd = 0;
            //Adia
            public float panMoveTime = 0;

            //Adia
            //Adia
            public string[] duckingCategories = null;
            //Adia
            public float duckingStartTime = 0;
            //Adia
            public float duckingEndTime = 0;
            //Adia
            public float duckingVolumeFactor = 0;
            public bool autoRestoreDucking = true;
            public float restoreTime = -1;      //Adia


            public bool isAndroidNative = false;
            int androidSoundId = 0;
            public void SetAndroidSoundId(int soundId)
            {
                androidSoundId = soundId;
            }
            public int GetAndroidSoundId()
            {
                return androidSoundId;
            }

            //Adia
            public int loadId = 0;


            public string name = null;

            AudioCategorySettings attachCategory = null;


            public string clipName = null;

            public bool isLoop = false;


            public void SetLoop(bool loop)
            {
                isLoop = loop;
            }

            public bool GetLoop()
            {
                return isLoop;
            }

            public void SetClipName(string name)
            {
                clipName = name;
            }

            public string GetClipName()
            {
                return clipName;
            }

            public void SetAttachCategoryInstance(AudioCategorySettings category)
            {
                attachCategory = category;
            }

            public AudioCategorySettings GetAttachCategory()
            {
                return attachCategory;
            }

            public string GetCategoryName()
            {
                return categoryName;
            }

        }
    }
}