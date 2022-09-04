using UnityEngine;

using UltEvents;

using DG.Tweening;

using Sirenix.OdinInspector;

namespace wzebra.kit.data
{
    [System.Serializable]
    public class TweenerData
    {
        [SerializeField] private float _time;

        [SerializeField] private TweenTimeType _timeType = TweenTimeType.Duration;

        [SerializeField] private Ease _ease = Ease.Linear;

        [SerializeField] private UltEvent _onStartAnimation, _onEndAnimation;

        [SerializeField] private bool _loop;

        [SerializeField, ShowIf(nameof(_loop))] private LoopType _loopType;

        [SerializeField, ShowIf(nameof(_loop))] private int _loopCount;

        public float Time => _time;

        public TweenTimeType Type => _timeType;

        public Ease Ease => _ease;

        public UltEvent OnStartAnimation => _onStartAnimation;

        public UltEvent OnEndAnimation => _onEndAnimation;

        public bool Loop => _loop;

        public LoopType LoopType => _loopType;

        public int LoopCount => _loopCount;

        [System.Serializable]
        public enum TweenTimeType
        {
            Duration,
            Speed
        }
    }
}