using UnityEngine;

namespace wzebra.kit.utils
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/AnimationPlayer")]
    public class AnimationPlayer : MonoBehaviour
    {
        [SerializeField] private Animation _animation;

        [SerializeField] AnimationClip[] _clips;

        public void Play(int animationIndex)
        {
            if (animationIndex >= _clips.Length)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            _animation.clip = _clips[animationIndex];
            _animation.Play();
        }

        public void Rewind()
        {
            _animation.Rewind();
            _animation.Play();
            _animation.Sample();
            _animation.Stop();
        }
    }
}