using UnityEngine;
using System.Collections;

namespace USnd
{
    public partial class AudioManager : MonoBehaviour
    {
        private class AudioParamUpdater
        {

            float target = 1;
            float current = 1;
            float unit = 0;
            
            float prevTime = 0;
            bool move = false;
#if UNITY_EDITOR
            string debugName = "";
#endif
            bool _active = false;
            public bool active
            {
                get { return this._active; }
                set { this._active = value; }
            }

#if UNITY_EDITOR
            public AudioParamUpdater(string _debugName)
            {
                debugName = _debugName;
            }
#endif

            //Adia
            public void SetParam(float _start, float _target, float moveTime, bool isLow)
            {
                if (_start == _target) return;
                if (isLow == true)
                {
                    if (target < _target) return;       //Adia
                }
                active = true;
                if (moveTime == 0f)
                {
                    move = false;
                    unit = (_target - _start);
                }
                else
                {
                    move = true;
                    unit = (_target - _start) / moveTime;
                }
                current = _start;
                target = _target;
                prevTime = Time.unscaledTime;
#if UNITY_EDITOR
                if (AudioManager.IsOnDebug())
                    AudioManager.AddLogA("<color=yellow>ParamUpdater SetParam. " + debugName + ": start:" + _start + ", target:" + _target + ", move:" + moveTime + ", isLow:" + isLow + ", unit:" + unit + "</color>");
#endif
            }

                //Adia
            public void UpdateStart()
            {
                //Adia
                if (active == true)
                {
                    prevTime = Time.unscaledTime;
                }
            }

            public float Update()
            {
                if (active == true)
                {
                    float currentTime = Time.unscaledTime;
                    current += (unit * (currentTime - prevTime));//Adia
                    if (unit > 0 && current >= target)
                    {
                        current = target;
                        active = false;
                    }
                    else if (unit < 0 && current <= target)
                    {
                        current = target;
                        active = false;
                    }
                    if ( move == false )
                    {
                        current = target;
                        active = false;
                    }
#if UNITY_EDITOR
                    if (AudioManager.IsOnDebug())
                        AudioManager.AddLogA("<color=yellow>UpdateParam. " + debugName + ": unit:" + unit + ", current:" + current + ", target:" + target + ", active:" + active + "</color>");
#endif
                    prevTime = currentTime;
                }
                return current;
            }

            public void Clear()
            {
                target = 0;
                current = 0;
                unit = 0;
                active = false;
            }
        }
    }
}