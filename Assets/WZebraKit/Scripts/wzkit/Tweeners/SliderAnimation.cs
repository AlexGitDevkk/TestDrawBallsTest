using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace wzebra.kit.tweener
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/SliderAnimation")]
    public class SliderAnimation : Tweener
    {
        [SerializeField] private Slider _slider;

        public Tween SetValue(float value)
        {
            if(value < 0 || value > 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            float current = 0;

            StartAnimationInvoke();

            _currentTween = DOTween.To(() => current, x => current = x, value, _data.Time * value).SetEase(_data.Ease).OnUpdate(() =>
            {
                _slider.value = current;
            });

            _currentTween.onComplete += EndAnimationInvoke;

            if (_data.Loop)
            {
                _currentTween.SetLoops(_data.LoopCount, _data.LoopType);
            }

            return _currentTween;
        }
    }
}