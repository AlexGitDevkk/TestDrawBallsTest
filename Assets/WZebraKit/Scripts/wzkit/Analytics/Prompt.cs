using System.Linq;

using UnityEngine;
using UltEvents;

using wzebra.kit.core;

namespace wzebra.kit.analytics
{
    public class Prompt : LevelEventTracker
    {
        [SerializeField] private CustomLevelAction[] _levels;

        [SerializeField] private UltEvent _generalAction;

        [Range(1, 10)]
        [SerializeField] private int _times;

        protected override void StartLevel(int level, int attempt)
        {
            if(attempt != 0 && attempt >= _times)
            {
                if (_levels.Select(l => l.ID).Contains(level))
                {
                    _levels.Where(l => l.ID == level).First().Action?.Invoke();
                }

                _generalAction?.Invoke();
            }
        }

        protected override void CompleteLevel(int level, int attempt, int score) { }

        protected override void FailLevel(int level, int attempt) { }

        protected override void Init() { }

        protected override void RestartLevel(int level, int attempt) { }

        protected override void CustomEvent(string eventName, string value, int level, int attempt) { }

#if MAXADS
        protected override void ReceivedReward(int level, string placement) { }
#endif
    }
}