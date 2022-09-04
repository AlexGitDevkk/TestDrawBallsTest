using DG.Tweening;

using UnityEngine;

namespace wzebra.kit.tweener
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/(Rect)TransformAnimation")]
    public class TransformAnimation : ObjectAnimation
    {
        [SerializeField] private Transform _target;

        private Vector3 _startPosition;
        private Vector3 _startRotation;
        private Vector3 _startScale;

        protected override void OnStart()
        {
            _startPosition = transform.position;
            _startRotation = transform.eulerAngles;
            _startScale = transform.localScale;
        }

        protected override Tween OnPlay()
        {
            return Animation(_target.position, _target.eulerAngles, _target.localScale, _data.Time);
        }

        protected override void OnRewind(bool withAnimation)
        {
            Animation(_startPosition, _startRotation, _startScale, withAnimation ? _data.Time : 0);
        }

        private Tween Animation(Vector3 position, Vector3 rotation, Vector3 scale, float duration = 0)
        {
            Tween pos = transform.DOMove(position, duration).SetEase(_data.Ease);
            Tween rot = transform.DORotate(rotation, duration).SetEase(_data.Ease);

            if (_data.Loop)
            {
                pos.SetLoops(_data.LoopCount, _data.LoopType);
                rot.SetLoops(_data.LoopCount, _data.LoopType);
            }

            return transform.DOScale(scale, duration).SetEase(_data.Ease);
        }
    }
}