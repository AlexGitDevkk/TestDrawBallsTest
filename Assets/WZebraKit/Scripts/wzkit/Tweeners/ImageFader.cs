using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace wzebra.kit.tweener
{
    public class ImageFader : Fader
    {
        [SerializeField] private Image _image;

        protected override Tween Fade(float value)
        {
            _currentTween = _image.DOFade(value, _data.Time).SetEase(_data.Ease);

            if (_data.Loop)
            {
                _currentTween.SetLoops(_data.LoopCount, _data.LoopType);
            }

            return _currentTween;
        }

        public override void ChangeState(float value)
        {
            Color color = _image.color;
            color.a = value;

            _image.color = color;
        }
    }
}