using UnityEngine;

#if BYTEBREW

using ByteBrewSDK;

#if REMOTE
using wzebra.kit.remote;
#endif

namespace wzebra.kit.analytics
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/Аналитика.-Byte-Brew")]
    public class ByteBrewAnalytic : LevelEventTracker
    {
        [SerializeField] private string _enviroment = "Level";
        [SerializeField] private string _stagePattern = "{0:000}";

#if REMOTE
        [SerializeField] private string[] _remoteVariablesLog;
#endif

        protected override void Init()
        {
            ByteBrew.InitializeByteBrew();
        }

        protected override void StartLevel(int level, int attempt)
        {
            ByteBrew.NewProgressionEvent(ByteBrewProgressionTypes.Started, _enviroment, string.Format(_stagePattern, level));

#if REMOTE
            foreach (var item in _remoteVariablesLog)
            {
                SetCustomAttribute(item, RemoteVariable.GetString(item));
            }
#endif
        }

        protected override void CompleteLevel(int level, int attempt, int score)
        {
            ByteBrew.NewProgressionEvent(ByteBrewProgressionTypes.Completed, _enviroment, string.Format(_stagePattern, level));
        }

        protected override void FailLevel(int level, int attempt)
        {
            ByteBrew.NewProgressionEvent(ByteBrewProgressionTypes.Failed, _enviroment, string.Format(_stagePattern, level));
        }

        protected override void RestartLevel(int level, int attempt) {}

#if MAXADS
        protected override void ReceivedReward(int level, string placement)
        {
            SetCustomAttribute(placement, $"Level: {level}");
        }
#endif

        public void SetCustomAttribute(string attrName, string value)
        {
            ByteBrew.SetCustomUserDataAttribute(attrName, value);
        }

        protected override void CustomEvent(string eventName, string value, int level, int attempt)
        {
            ByteBrew.NewCustomEvent(eventName, $"Value: {value}, Level: {level}, Attempt: {attempt}");
        }
    }
}

#endif