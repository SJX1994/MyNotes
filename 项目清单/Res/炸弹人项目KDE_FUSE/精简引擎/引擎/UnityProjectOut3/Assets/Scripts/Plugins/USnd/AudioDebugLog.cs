using UnityEngine;
using System.Collections;

namespace USnd
{
    public partial class AudioManager : MonoBehaviour
    {

        private class AudioDebugLog
        {
#if USND_DEBUG_LOG
            static bool isActive = true;
#else
        static bool isActive = false;
#endif
            static public void Break()
            {
                if (IsEnable()) UnityEngine.Debug.Break();
            }

            static public void Log(object message)
            {
                if (IsEnable()) UnityEngine.Debug.Log(message);
            }

            static public void Log(object message, Object context)
            {
                if (IsEnable()) UnityEngine.Debug.Log(message, context);
            }

            static public void LogError(object message)
            {
                if (IsEnable()) UnityEngine.Debug.LogError(message);
            }

            static public void LogError(object message, Object context)
            {
                if (IsEnable()) UnityEngine.Debug.LogError(message, context);
            }

            static public void LogWarning(object message)
            {
                if (IsEnable()) UnityEngine.Debug.LogWarning(message);
            }

            static public void LogWarning(object message, Object context)
            {
                if (IsEnable()) UnityEngine.Debug.LogWarning(message, context);
            }

            static bool IsEnable()
            {
                return (UnityEngine.Debug.isDebugBuild && AudioDebugLog.isActive);
            }

        }
    }
}