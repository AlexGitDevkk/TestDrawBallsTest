using UnityEngine;

using TMPro;

using DG.Tweening;

namespace wzebra.kit.tweener
{
    public class TextFader : Fader
    {
        [SerializeField] private TMP_Text _text;

        protected override Tween Fade(float value)
        {
            _currentTween = _text.DOFade(value, _data.Time).SetEase(_data.Ease);

            if (_data.Loop)
            {
                _currentTween.SetLoops(_data.LoopCount, _data.LoopType);
            }

            return _currentTween;
        }

        public override void ChangeState(float value)
        {
            Color color = _text.color;
            color.a = value;

            _text.color = color;
        }
    }
}