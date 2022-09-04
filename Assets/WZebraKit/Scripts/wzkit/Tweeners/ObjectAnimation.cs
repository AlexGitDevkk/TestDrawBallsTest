using DG.Tweening;

namespace wzebra.kit.tweener
{
    public abstract class ObjectAnimation : Tweener
    {
        private void Start()
        {
            OnStart();
        }

        protected virtual void OnStart() { }

        public Tween Play()
        {
            StartAnimationInvoke();

            _currentTween = OnPlay();

            _currentTween.onComplete += EndAnimationInvoke;

            if (_data.Loop)
            {
                _currentTween.SetLoops(_data.LoopCount, _data.LoopType);
            }

            return _currentTween;
        }

        protected abstract Tween OnPlay();

        public void Rewind(bool withAnimation)
        {
            OnRewind(withAnimation);
        }

        protected abstract void OnRewind(bool withAnimation);
    }
}