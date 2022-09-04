using UnityEngine;

using DG.Tweening;

using wzebra.kit.data;

namespace wzebra.kit.tweener
{
    public class Shaker : Tweener
    {
        [SerializeField] private TransformMask _mask;

        [SerializeField] private float _strength = 1;
        [SerializeField] private int _vibrato = 10;

        public Tween Shake()
        {
            StartAnimationInvoke();

            if (_mask.HasFlag(TransformMask.PositionX) || _mask.HasFlag(TransformMask.PositionY) || _mask.HasFlag(TransformMask.PositionZ))
            {
                _currentTween = transform.DOShakePosition(_data.Time, strength: GetStrength(TransformMask.PositionX, TransformMask.PositionY, TransformMask.PositionZ), vibrato: _vibrato);
            }

            if (_mask.HasFlag(TransformMask.RotationX) || _mask.HasFlag(TransformMask.RotationY) || _mask.HasFlag(TransformMask.RotationZ))
            {
                _currentTween = transform.DOShakeRotation(_data.Time, strength: GetStrength(TransformMask.RotationX, TransformMask.RotationY, TransformMask.RotationZ), vibrato: _vibrato);
            }

            if (_mask.HasFlag(TransformMask.ScaleX) || _mask.HasFlag(TransformMask.ScaleY) || _mask.HasFlag(TransformMask.ScaleZ))
            {
                _currentTween = transform.DOShakeScale(_data.Time, strength: GetStrength(TransformMask.ScaleX, TransformMask.ScaleY, TransformMask.ScaleZ), vibrato: _vibrato);
            }

            if (_data.Loop)
            {
                _currentTween.SetLoops(_data.LoopCount, _data.LoopType);
            }

            _currentTween.onComplete += EndAnimationInvoke;

            return _currentTween;
        }

        private Vector3 GetStrength(TransformMask maskX, TransformMask maskY, TransformMask maskZ)
        {
            Vector3 strength;

            strength.x = _mask.HasFlag(maskX) ? _strength : 0;
            strength.y = _mask.HasFlag(maskY) ? _strength : 0;
            strength.z = _mask.HasFlag(maskZ) ? _strength : 0;

            return strength;
        }
    }
}