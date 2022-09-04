using UnityEngine;

using DG.Tweening;

namespace wzebra.kit.tweener
{
    public class MaterialPropertyFader : Fader
    {
        [SerializeField] private string _propertyName;

        [SerializeField] private Renderer _renderer;

        protected override Tween Fade(float value)
        {
            float property = _renderer.material.GetFloat(_propertyName);

            _currentTween = DOTween.To(() => property, x => property = x, value, _data.Time).SetEase(_data.Ease).OnUpdate(() =>
            {
                _renderer.material.SetFloat(_propertyName, property);
            });

            if (_data.Loop)
            {
                _currentTween.SetLoops(_data.LoopCount, _data.LoopType);
            }

            return _currentTween;
        }

        public override void ChangeState(float value)
        {
            _renderer.material.SetFloat(_propertyName, value);
        }
    }
}