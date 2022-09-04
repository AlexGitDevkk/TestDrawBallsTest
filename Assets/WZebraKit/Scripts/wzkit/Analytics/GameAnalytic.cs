﻿using UnityEngine;

#if GAMEANALYTICS

using GameAnalyticsSDK;

namespace wzebra.kit.analytics
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/Аналитика.-GameAnalytics")]
    public class GameAnalytic : MonoBehaviour, IGameAnalyticsATTListener
    {
        private void Start()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                GameAnalytics.RequestTrackingAuthorization(this);
            }
            else
            {
                GameAnalytics.Initialize();
            }
        }

        public void GameAnalyticsATTListenerNotDetermined()
        {
            GameAnalytics.Initialize();
        }
        public void GameAnalyticsATTListenerRestricted()
        {
            GameAnalytics.Initialize();
        }
        public void GameAnalyticsATTListenerDenied()
        {
            GameAnalytics.Initialize();
        }
        public void GameAnalyticsATTListenerAuthorized()
        {
            GameAnalytics.Initialize();
        }
    }
}
#endif