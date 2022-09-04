using UnityEngine;

using wzebra.kit.core;

#if MAXADS
using wzebra.kit.ads;
#endif

namespace wzebra.kit.analytics
{
    public abstract class LevelEventTracker : MonoBehaviour
    {
        [SerializeField] private LevelSpawner _spawner;
        [SerializeField] private LevelCounter _counter;

#if MAXADS
        [SerializeField] private MaxAds _ads;
#endif

        private AttemptCounter _attempt;
        private bool _levelFinished;

        private int _pastLevel;

        protected const string _skipEventName = "LevelSkipped";

#if MAXADS
        protected const string _receiveRewardEventName = "RVWatched";
#endif

        private void Awake()
        {
            _attempt = new AttemptCounter(name);

            Init();

            _levelFinished = true;

            _spawner.OnStartLevel += StartLevel;
            _spawner.OnRestartLevel += RestartLevel;
            _spawner.OnWin += CompleteLevel;
            _spawner.OnLose += FailLevel;

#if MAXADS
            _ads.OnReceiveReward += ReceivedReward;
#endif
        }

        protected abstract void Init();

        private void StartLevel()
        {
            int level = GetCurrentLevel();

            Debug.Log($"<color=green>Start level {level} with {_attempt.GetAttempt()} attempt.</color> — {name}");

            if (_levelFinished == false && _pastLevel != level)
            {
                _attempt.DeleteAttempt();
                SkipLevel();
            }

            _levelFinished = true;

            _pastLevel = level;

            StartLevel(level, _attempt.GetAttempt());
        }

        protected abstract void StartLevel(int level, int attempt);

        private void SkipLevel()
        {
            CustomEvent(_skipEventName, "1");
        }

        private void RestartLevel()
        {
            int level = GetCurrentLevel();

            _attempt.IncrementAttempt();

            Debug.Log($"<color=green>Restart level {level} with {_attempt.GetAttempt()} attempt.</color> — {name}");

            _levelFinished = false;

            RestartLevel(level, _attempt.GetAttempt());
        }

        protected abstract void RestartLevel(int level, int attempt);

        private void CompleteLevel(float score)
        {
            int level = GetCurrentLevel();

            Debug.Log($"<color=green>Complete level {level} with {_attempt.GetAttempt()} attempt.</color> — {name}");
            CompleteLevel(level, _attempt.GetAttempt(), (int)(score * 100));

            _levelFinished = true;

            _attempt.DeleteAttempt();
        }
        protected abstract void CompleteLevel(int level, int attempt, int score);

        private void FailLevel()
        {
            int level = GetCurrentLevel();

            Debug.Log($"<color=green>Fail level {level} with {_attempt.GetAttempt()} attempt.</color> — {name}");

            _levelFinished = true;

            FailLevel(level, _attempt.GetAttempt());
        }

        protected abstract void FailLevel(int level, int attempt);

#if MAXADS
        private void ReceivedReward(int index)
        {
            int level = GetCurrentLevel();

            string eventName = _ads.GetPlacementName(index) + _receiveRewardEventName;

            Debug.Log($"<color=green>Received reward '{eventName}' in {level} level</color> — {name}");

            ReceivedReward(level, eventName);
        }

        protected abstract void ReceivedReward(int level, string placement);
#endif

        public void CustomEvent(string eventName, string value)
        {
            int level = GetCurrentLevel();
            int attempt = _attempt.GetAttempt();

            Debug.Log($"<color=green>Custom event '{eventName}'-'{value}' in level {level} with {_attempt.GetAttempt()} attempt.</color> — {name}");

            CustomEvent(eventName, value, level, attempt);
        }

        protected abstract void CustomEvent(string eventName, string value, int level, int attempt);

        private int GetCurrentLevel() => _counter.GetActualLevel();
    }
}