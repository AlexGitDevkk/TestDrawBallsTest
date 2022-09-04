using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;

using wzebra.kit.data;

namespace wzebra.kit.tweener
{
    public abstract class Tweener : MonoBehaviour
    {
        [SerializeField] protected TweenerData _data;

        public event UnityAction OnStartAnimation, OnEndAnimation;

        protected Tween _currentTween;

        private void Start()
        {
            OnStartAnimation += () => { _data.OnStartAnimation?.Invoke(); };
            OnEndAnimation += () => { _data.OnEndAnimation?.Invoke(); };

            AfterStart();
        }

        protected virtual void AfterStart() { }

        protected void StartAnimationInvoke()
        {
            OnStartAnimation?.Invoke();
        }

        protected void EndAnimationInvoke()
        {
            OnEndAnimation?.Invoke();
        }

        protected float GetDurationBySpeed(float distance) => distance / _data.Time;

        protected float GetDurationBySpeed(Vector3 first, Vector3 second) => GetDurationBySpeed(Vector3.Distance(first, second));

        protected float GetDurationOrSpeed(Vector3 first, Vector3 second) => _data.Type == TweenerData.TweenTimeType.Duration ? _data.Time : GetDurationBySpeed(first, second);

        public void Stop(bool withComplete = false)
        {
            _currentTween?.Kill(withComplete);
        }

        public void Reset()
        {
            _currentTween.Goto(0);
        }
    }
}