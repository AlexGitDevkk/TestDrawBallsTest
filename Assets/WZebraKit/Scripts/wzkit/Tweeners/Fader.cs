using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;

using wzebra.kit.data;

namespace wzebra.kit.tweener
{
    public abstract class Fader : Tweener
    {
        [SerializeField] protected float _inValue;
        [SerializeField] protected float _outValue;

        [SerializeField] protected bool _repeat;

        [SerializeField] private FaderEvents _events;

        public event UnityAction OnStartInFading, OnEndInFading, OnStartOutFading, OnEndOutFading;

        private void Awake()
        {
            OnStartInFading += () => { _events.OnStartInFading?.Invoke(); };
            OnEndInFading += () => { _events.OnEndInFading?.Invoke(); };
            OnStartOutFading += () => { _events.OnStartOutFading?.Invoke(); };
            OnEndOutFading += () => { _events.OnEndOutFading?.Invoke(); };

            OnStartInFading += StartAnimationInvoke;
            OnEndInFading += EndAnimationInvoke;
            OnStartOutFading += StartAnimationInvoke;
            OnEndOutFading += EndAnimationInvoke;

            AfterAwake();
        }

        protected virtual void AfterAwake() { }

        public Tween FadeIn()
        {
            if (_repeat)
            {
                ChangeState(_outValue);
            }

            OnStartInFading?.Invoke();

            Fade(_inValue);

            _currentTween.onComplete += () => { OnEndInFading?.Invoke(); };

            return _currentTween;
        }

        public Tween FadeOut()
        {
            if (_repeat)
            {
                ChangeState(_inValue);
            }

            OnStartOutFading?.Invoke();

            Fade(_outValue);

            _currentTween.onComplete += () => { OnEndOutFading?.Invoke(); };

            return _currentTween;
        }

        public void SetInValue(float value)
        {
            _inValue = value;
        }

        public void SetOutValue(float value)
        {
            _outValue = value;
        }

        protected abstract Tween Fade(float value);

        public abstract void ChangeState(float value);
    }
}