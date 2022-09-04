using UnityEngine;

using UltEvents;

using DG.Tweening;

namespace wzebra.kit.tweener
{
    public class Shifter : Tweener
    {
        [SerializeField] private Vector3 _shiftValue;

        [SerializeField] private UltEvent OnShift, OnReset;

        private Vector3 _startPosition;

        private Tween _tween;

        private void Start()
        {
            SetStartPosition();
        }

        public void SetStartPosition()
        {
            _startPosition = transform.position;
        }

        public Tween ShiftX()
        {
            return ShiftX(_shiftValue.x);
        }

        public Tween ShiftY()
        {
            return ShiftY(_shiftValue.y);
        }

        public Tween ShiftZ()
        {
            return ShiftZ(_shiftValue.z);
        }

        public Tween Shift()
        {
            return Shift(_shiftValue);
        }

        public Tween ShiftX(float x)
        {
            return Shift(Vector3.right * x);
        }

        public Tween ShiftY(float y)
        {
            return Shift(Vector3.up * y);
        }

        public Tween ShiftZ(float z)
        {
            return Shift(Vector3.forward * z);
        }

        private Tween Shift(Vector3 position)
        {
            StartAnimationInvoke();

            OnShift?.Invoke();

            Vector3 endPosition = transform.position + position;

            if (_data.Time == 0)
            {
                transform.position = endPosition;
                return null;
            }
            else
            {
                StopTween();
                StartAnimationInvoke();
                _tween = transform.DOMove(endPosition, GetDurationOrSpeed(transform.position, endPosition)).SetEase(_data.Ease);

                _currentTween = _tween;

                _currentTween.onComplete += EndAnimationInvoke;

                if (_data.Loop)
                {
                    _currentTween.SetLoops(_data.LoopCount, _data.LoopType);
                }

                return _currentTween;
            }
        }

        public Tween ResetPosition(bool fast = true)
        {
            OnReset?.Invoke();

            if (fast)
            {
                transform.position = _startPosition;
                return null;
            }
            else
            {
                StopTween();
                _tween = transform.DOMove(_startPosition, GetDurationOrSpeed(transform.position, _startPosition)).SetEase(_data.Ease);

                _currentTween = _tween;

                _currentTween.onComplete += EndAnimationInvoke;

                if (_data.Loop)
                {
                    _currentTween.SetLoops(_data.LoopCount, _data.LoopType);
                }

                return _currentTween;
            }
        }

        private void StopTween()
        {
            if(_tween.IsActive())
            {
                if (_tween.IsPlaying())
                {
                    _tween.Kill();
                }
            }
        }

        public Vector3 GetShiftValue() => _shiftValue;
    }
}