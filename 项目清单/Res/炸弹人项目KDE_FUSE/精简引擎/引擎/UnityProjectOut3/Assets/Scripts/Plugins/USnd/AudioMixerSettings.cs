using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;

namespace USnd
{
    public partial class AudioManager : MonoBehaviour
    {
        private class AudioMixerSettings
        {

            public AudioMixer mixer = null;

            public void SetAudioMixer(AudioMixer _mixer)
            {
                mixer = _mixer;
            }

            public AudioMixerGroup[] FindGroup(string groupName)
            {
            	if ( mixer == null ) return null;
                return mixer.FindMatchingGroups(groupName);
            }

            public void SetSnapshot(string snapName, float time)
            {
                if (mixer == null) return;
                AudioMixerSnapshot shot = mixer.FindSnapshot(snapName);
                if (shot != null)
                {
                    shot.TransitionTo(time);
                }
            }

            public void SetFloat(string paramName, float value)
            {
                if (mixer == null) return;
                mixer.SetFloat(paramName, value);
            }
        }
    }
}
