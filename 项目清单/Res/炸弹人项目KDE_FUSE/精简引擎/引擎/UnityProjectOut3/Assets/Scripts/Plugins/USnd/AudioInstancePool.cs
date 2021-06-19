using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace USnd
{
    public partial class AudioManager : MonoBehaviour
    {
        private class AudioInstancePool
        {

            private static AudioInstancePool _instance;

            private static int instanceIdNext = 1;    //Adia


            public static void Initialize()
            {
                if (_instance == null)
                {
                    _instance = new AudioInstancePool();
                }
            }

            public static AudioInstancePool instance
            {
                get
                {
                    return _instance;
                }
            }

            //Adia
            private List<AudioInstance> pool = new List<AudioInstance>(AudioDefine.LIST_CAPACITY);


            public void AddEmpty(int num)
            {
                for (int i = 0; i < num; ++i)
                {
                    AudioInstance empty = new AudioInstance();
                    pool.Add(empty);
                    empty.SetActive(false);
                }
            }

            public AudioInstance AddComponent()
            {
                AudioInstance instance = null;
                if (instanceIdNext == 0) instanceIdNext = 1;

                for (int i = 0; i < pool.Count; ++i)
                {
                    instance = pool[i];
                    if (instance.activeSelf == false)
                    {
                        instance.SetActive(true);
                        instance.SetInstanceID(unchecked(instanceIdNext++));
                        return instance;
                    }
                }

                instance = new AudioInstance();
                pool.Add(instance);
                instance.SetInstanceID(unchecked(instanceIdNext++));
                instance.SetActive(true);
                return instance;
            }

            public void Deactive(AudioInstance instance)
            {
                instance.SetActive(false);
            }

            public void Clear()
            {
                AudioInstance instance = null;
                for (int i = 0; i < pool.Count; )
                {
                    instance = pool[i];
                    if (instance.activeSelf == false)
                    {
                        pool[i] = null;
                        pool.RemoveAt(i);
                    }
                    else
                    {
                        ++i;
                    }
                }
            }

        }
    }
}
