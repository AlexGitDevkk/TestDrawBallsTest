using UnityEngine;
#if FACEBOOK
using Facebook.Unity;

namespace wzebra.kit.analytics
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/Аналитика.-Facebook")]
    public class FacebookAnalytics : MonoBehaviour
    {
        private void Awake()
        {
            if (!FB.IsInitialized)
            {
                FB.Init(InitCallback, OnHideUnity);
            }
            else
            {
                FB.ActivateApp();
            }
        }

        private void InitCallback()
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
        }

        private void OnHideUnity(bool isGameShown)
        {
            if (!isGameShown)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
#endif