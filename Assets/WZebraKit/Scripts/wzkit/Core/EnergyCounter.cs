using System;

using UnityEngine;
using UnityEngine.Events;

using UltEvents;

using DG.Tweening;

using wzebra.kit.utils;

namespace wzebra.kit.core
{
    public class EnergyCounter : MonoBehaviour
    {
        [System.Serializable]
        public class Events
        {
            public UltEvent OnLoss;
            public UltEvent OnAppearOne;
            public UltEvent<int> OnCounterUpdate;
            public UltEvent<float> OnCounterUpdateNormalized;
            public UltEvent<float> OnTimerUpdate;
            public UltEvent OnRegeneratedOne;
            public UltEvent OnFull;
        }

        [SerializeField] private LevelSpawner _spawner;

        [SerializeField] private int _count;

        [SerializeField] private int _regenerationDuration;

        [SerializeField] private bool _removeOnLose = true;

        [SerializeField] private Events _events;

        public event UnityAction OnLoss;
        public event UnityAction OnAppearOne;
        public event UnityAction<int> OnCounterUpdate;
        public event UnityAction<float> OnCounterUpdateNormalized;
        public event UnityAction<float> OnTimerUpdate;
        public event UnityAction OnRegeneratedOne;
        public event UnityAction OnStartRegeneration;
        public event UnityAction OnFull;

        private EnergySaver _saver;

        private int _currentCount;

        private float _regenerationState = 0;

        private bool _isRegenerating;

        private Tween _timer;

        private void Start()
        {
            OnLoss += () => { _events.OnLoss?.Invoke(); };
            OnAppearOne += () => { _events.OnAppearOne?.Invoke(); };
            OnCounterUpdate += (int count) => { _events.OnCounterUpdate?.Invoke(count); };
            OnCounterUpdateNormalized += (float value) => { _events.OnCounterUpdateNormalized?.Invoke(value); };
            OnTimerUpdate += (float value) => { _events.OnTimerUpdate?.Invoke(value); };
            OnRegeneratedOne += () => { _events.OnRegeneratedOne?.Invoke(); };
            OnFull += () => { _events.OnFull?.Invoke(); };
            OnCounterUpdate += (int count) => { OnCounterUpdateNormalized?.Invoke(GetNormalizedCount()); };

            _saver = new EnergySaver(this);

            SynchronizeState();

            if (_removeOnLose)
            {
                _spawner.OnLose += Remove;
            }
        }

        private void SynchronizeState()
        {
            _currentCount = _saver.GetCount();

            if (CanAdd())
            {
                float state = _saver.GetRegenerationState(_regenerationDuration);

                int regenerated = (int)state;
                float restState = state - regenerated;

                for (int i = 0; i < regenerated; i++)
                {
                    _currentCount++;

                    if (CanAdd() == false)
                    {
                        break;
                    }
                }

                if (CanAdd())
                {
                    Regeneration(restState);
                }
            }

            OnCountUpdate();
        }

        public void Remove()
        {
            if (CanRemove() && enabled)
            {
                _currentCount--;

                OnCountUpdate();
            }
        }

        public void Add()
        {
            if (CanAdd())
            {
                _currentCount++;

                OnCountUpdate();
            }
        }

        public void AddFull()
        {
            for (int i = 0; i < _count; i++)
            {
                Add();
            }
        }

        private void OnCountUpdate()
        {
            if (_currentCount == 1)
            {
                OnAppearOne?.Invoke();
            }

            if (_currentCount == 0)
            {
                OnLoss?.Invoke();
            }

            if (CanAdd())
            {
                if (_isRegenerating == false)
                {
                    Regeneration();
                }
            }
            else
            {
                StopTimer();

                OnFull?.Invoke();
            }

            OnCounterUpdate?.Invoke(GetCurrentCount());
        }

        private void Regeneration(float state = 0)
        {
            OnStartRegeneration?.Invoke();

            float currentState = state;

            StopTimer();

            _saver.SetRegenerationTime(DateTime.UtcNow.AddSeconds(-currentState * _regenerationDuration));

            _isRegenerating = true;

            _timer = DOTween.To(() => currentState, x => currentState = x, 1, _regenerationDuration * (1 - currentState)).SetEase(Ease.Linear).OnUpdate(() =>
            {
                _regenerationState = currentState;
                OnTimerUpdate?.Invoke(currentState);
            }).OnComplete(OnCompleteRegeneration);
        }

        private void StopTimer()
        {
            if (_timer != null)
            {
                if (_timer.IsPlaying())
                {
                    _timer?.Kill();
                }
            }

            _isRegenerating = false;
        }

        private void OnCompleteRegeneration()
        {
            OnRegeneratedOne?.Invoke();
            _isRegenerating = false;
            _regenerationState = 0;
            Add();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus && _saver != null)
            {
                SynchronizeState();
            }

            if(focus == false && _isRegenerating)
            {
                _timer.Kill();
            }
        }

        #region fields
        public bool CanRemove() => _currentCount > 0;

        public bool CanAdd() => _currentCount < _count;

        public int GetCurrentCount() => _currentCount;

        public float GetNormalizedCount() => (float)_currentCount / (float)_count;

        public int GetMaxCount() => _count;

        public int GetSecondsRest() => Mathf.RoundToInt((1 - _regenerationState) * _regenerationDuration);

        public float GetRegenerationStateNormalized() => _regenerationState;

        public void SetRegenerationValue(int seconds)
        {
            _regenerationDuration = seconds;
        }
        #endregion
    }
}