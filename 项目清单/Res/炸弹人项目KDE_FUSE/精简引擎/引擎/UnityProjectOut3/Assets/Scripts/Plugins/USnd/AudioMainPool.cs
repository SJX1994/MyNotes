using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace USnd
{
    public partial class AudioManager : MonoBehaviour
    {

        private class AudioMainPool : MonoBehaviour
        {
            /*
            private class Pair<T, U>
            {
                public Pair()
                {
                }
                public Pair(T first, U second)
                {
                    this.First = first;
                    this.Second = second;
                }
                public T First { get; set; }
                public U Second { get; set; }
            }

            private List<Pair<int, AudioSource>> pool = new List<Pair<int, AudioSource>>();
            */
            //Adia
            private List<AudioSource> pool = new List<AudioSource>(AudioDefine.LIST_CAPACITY);

            private static AudioMainPool _instance;

            private static GameObject owner;

            private static Transform CacheTransform { get { return (_cacheTransform != null) ? _cacheTransform : (_cacheTransform = owner.transform); } }
            private static Transform _cacheTransform;


            public static void Initialize(GameObject obj)
            {
                if (_instance == null)
                {
                    _instance = obj.AddComponent<AudioMainPool>();
                    owner = obj;
                }
            }

            public static void Terminate()
            {
                if (_instance != null)
                {
                    Destroy(_instance);
                    _instance = null;
                    owner = null;
                    _cacheTransform = null;
                }
            }

            public static AudioMainPool instance
            {
                get
                {
                    return _instance;
                }
            }

            public void AddEmpty(int num)
            {
                for (int i = 0; i < num; ++i)
                {
                    AudioSource empty = new GameObject().AddComponent<AudioSource>();
                    empty.transform.parent = CacheTransform;
#if USND_EDIT_MODE
                    empty.name = "source_empty";
#endif
                    empty.gameObject.SetActive(false);

                    pool.Add(empty);
                }
            }

            public AudioSource GetClone()
            {
                AudioSource cloneSource = null;

                for (int i = 0; i < pool.Count; ++i)
                {
                    cloneSource = pool[i];
                    if (cloneSource.gameObject.activeSelf == false)
                    {
                        cloneSource.gameObject.SetActive(true);
                        return cloneSource;
                    }
                }

                cloneSource = new GameObject().AddComponent<AudioSource>();
                cloneSource.transform.parent = CacheTransform;
#if USND_EDIT_MODE
                cloneSource.name = "source_" + instance.GetInstanceID();
#endif
                pool.Add(cloneSource);

                return cloneSource;
            }

            public void Deactive(AudioSource source)
            {
                source.gameObject.SetActive(false);
                source.clip = null;
            }

            public void Clear()
            {
                AudioSource source = null;
                for (int i = 0; i < pool.Count; )
                {
                    source = pool[i];
                    if (source.gameObject.activeSelf == false)
                    {
                        Object.Destroy(source.gameObject);
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
