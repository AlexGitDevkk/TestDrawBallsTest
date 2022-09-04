using DG.Tweening;

using UnityEngine;

namespace wzebra.kit.tweener
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/(Rect)TransformAnimation")]
    public class RectTransformAnimation : ObjectAnimation
    {
        [SerializeField] private RectTransform _rect;
        [SerializeField] private RectTransform _target;

        private Vector3 _startPosition;
        private Vector3 _startRotation;
        private Vector3 _startSize;

        protected override void OnStart()
        {
            _startPosition = _rect.anchoredPosition;
            _startRotation = _rect.eulerAngles;
            _startSize = _rect.sizeDelta;
        }

        protected override Tween OnPlay()
        {
            return Animation(_target.anchoredPosition, _target.eulerAngles, _target.sizeDelta, _data.Time);
        }

        protected override void OnRewind(bool withAnimation)
        {
            Animation(_startPosition, _startRotation, _startSize, withAnimation ? _data.Time : 0);
        }

        private Tween Animation(Vector3 position, Vector3 rotation, Vector3 size, float duration = 0)
        {
            Tween pos = _rect.DOAnchorPos(position, duration).SetEase(_data.Ease);
            Tween rot = _rect.DORotate(rotation, duration).SetEase(_data.Ease);

            if (_data.Loop)
            {
                pos.SetLoops(_data.LoopCount, _data.LoopType);
                rot.SetLoops(_data.LoopCount, _data.LoopType);
            }

            return _rect.DOSizeDelta(size, duration).SetEase(_data.Ease);
        }
    }
}