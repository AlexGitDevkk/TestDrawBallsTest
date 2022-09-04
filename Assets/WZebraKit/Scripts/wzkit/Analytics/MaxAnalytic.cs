using UnityEngine;

#if MAX

namespace wzebra.kit.analytics
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/Аналитика.-MaxSDK")]
    public class MaxAnalytic : MonoBehaviour
    {
        [SerializeField] private string _key;

        private void Start()
        {
            MaxSdk.SetSdkKey(_key);
            MaxSdk.SetUserId(SystemInfo.deviceUniqueIdentifier);
            MaxSdk.SetVerboseLogging(true);
            MaxSdk.InitializeSdk();
        }

        public void ShowMediationDebugger()
        {
            MaxSdk.ShowMediationDebugger();
        }
    }
}
#endif