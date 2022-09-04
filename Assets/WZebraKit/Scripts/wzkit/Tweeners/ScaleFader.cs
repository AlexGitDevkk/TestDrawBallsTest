using UnityEngine;

using DG.Tweening;

namespace wzebra.kit.tweener
{
    public class ScaleFader : Fader
    {
        [SerializeField] private Vector3Int _axisMask;

        private Vector3 _targetScale;

        protected override void AfterAwake()
        {
            _targetScale = transform.localScale;
        }

        protected override Tween Fade(float value)
        {
            _currentTween = transform.DOScale(GetVector(value), _data.Time).SetEase(_data.Ease);

            if (_data.Loop)
            {
                _currentTween.SetLoops(_data.LoopCount, _data.LoopType);
            }

            return _currentTween;
        }

        public override void ChangeState(float value)
        {
            transform.localScale = GetVector(value);
        }

        private Vector3 GetVector(float value)
        {
            Vector3 endScale = transform.localScale;

            if (_axisMask.x == 1)
            {
                endScale.x = _targetScale.x * value;
            }

            if (_axisMask.y == 1)
            {
                endScale.y = _targetScale.y * value;
            }

            if (_axisMask.z == 1)
            {
                endScale.z = _targetScale.z * value;
            }

            return endScale;
        }
    }
}