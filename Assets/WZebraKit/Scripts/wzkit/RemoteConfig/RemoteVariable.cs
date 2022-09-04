using UnityEngine;

#if REMOTE

namespace wzebra.kit.remote
{
    public class RemoteVariable : MonoBehaviour
    {
        public static int GetInt(string key)
        {
            return MaxRemoteConfig.LoadRemoteData<int>(key);
        }

        public static bool GetBool(string key)
        {
            return MaxRemoteConfig.LoadRemoteData<bool>(key);
        }

        public static float GetFloat(string key)
        {
            return MaxRemoteConfig.LoadRemoteData<float>(key);
        }

        public static string GetString(string key)
        {
            return MaxRemoteConfig.LoadRemoteData<string>(key);
        }
    }
}

#endif