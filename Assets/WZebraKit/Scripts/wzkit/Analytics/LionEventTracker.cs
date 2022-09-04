using System.Collections.Generic;
using UnityEngine;
#if LION
using LionStudios.Suite.Core;


using LionStudios.Suite.Analytics;

namespace wzebra.kit.analytics
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/Аналитика.-Lion-Analytics")]
    public class LionEventTracker : LevelEventTracker
    {
        protected override void Init()
        {
            LionCore.OnInitialized += () => 
            {
                LionAnalytics.GameStart();
            };
        }

        protected override void StartLevel(int level, int attempt)
        {
            LionAnalytics.LevelStart(level, attempt);
        }

        protected override void RestartLevel(int level, int attempt)
        {
            LionAnalytics.LevelRestart(level, attempt);
        }

        protected override void CompleteLevel(int level, int attempt, int score)
        {
            LionAnalytics.LevelComplete(level, attempt, score);
        }

        protected override void FailLevel(int level, int attempt)
        {
            LionAnalytics.LevelFail(level, attempt);
        }

#if MAXADS
        protected override void ReceivedReward(int level, string placement)
        {
            CustomEvent(placement);
        }
#endif

        protected override void CustomEvent(string eventName, string value, int level, int attempt)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Value", value);
            data.Add("Level", level);
            data.Add("Attempt", attempt);

            LionAnalytics.LogEvent(eventName, data);
        }
    }
}
#endif