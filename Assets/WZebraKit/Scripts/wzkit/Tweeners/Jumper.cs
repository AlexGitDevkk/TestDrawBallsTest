using UnityEngine;

using DG.Tweening;

namespace wzebra.kit.tweener
{
    public class Jumper : Tweener
    {
        [SerializeField] private float _height;

        public Tween Jump(Vector3 position)
        {
            StartAnimationInvoke();

            _currentTween = transform.DOJump(position, _height, 1, GetDurationOrSpeed(transform.position, position)).
                SetEase(_data.Ease);

            _currentTween.onComplete += EndAnimationInvoke;

            if (_data.Loop)
            {
                _currentTween.SetLoops(_data.LoopCount, _data.LoopType);
            }

            return _currentTween;
        }

        public void Jump(Transform target) => Jump(target.position);
    }
}