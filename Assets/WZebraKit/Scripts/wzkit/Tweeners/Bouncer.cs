using UnityEngine;

using DG.Tweening;

namespace wzebra.kit.tweener
{
    public class Bouncer : Tweener
    {
        [Range(0, 1)]
        [SerializeField] private float _startBounceValue;

        public Tween Bounce(bool ignoreStartBounce = false)
        {
            float scale = transform.localScale.x;

            if (ignoreStartBounce)
            {
                transform.localScale *= _startBounceValue;
            }

            _currentTween = transform.DOScale(scale, _data.Time).SetEase(Ease.OutBack);

            if (_data.Loop)
            {
                _currentTween.SetLoops(_data.LoopCount, _data.LoopType);
            }

            _currentTween.onComplete += EndAnimationInvoke;

            return _currentTween;
        }
    }
}