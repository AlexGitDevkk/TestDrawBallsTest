using UnityEngine;

#if ADJUST
using com.adjust.sdk;

namespace wzebra.kit.analytics
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/Аналитика.-Adjust")]
    public class AdjustAnalytic : MonoBehaviour
    {
        [SerializeField] private string _key;

        private void Start()
        {
            var adjustConfig = new AdjustConfig(
                _key,
                AdjustEnvironment.Production,
                true
            );

            adjustConfig.setLogLevel(AdjustLogLevel.Info);
            adjustConfig.setSendInBackground(true);

            new GameObject("Adjust").AddComponent<Adjust>();

            Adjust.start(adjustConfig);
        }
    }
}
#endif