using System.Collections;

using UnityEngine;
using UltEvents;

namespace wzebra.kit.utils
{
    public class DelayedEvent : MonoBehaviour
    {
        [SerializeField] private bool _playOnStart;

        [SerializeField] private float _delay;

        [SerializeField] private bool _softMode;

        [SerializeField] private UltEvent _action;

        private Coroutine _coroutine;

        private void Start()
        {
            if(_playOnStart && _softMode == false)
            {
                Play();
            }
        }

        private void OnEnable()
        {
            if (_playOnStart && _softMode)
            {
                Play();
            }
        }

        public void Play()
        {
            if (isActiveAndEnabled)
            {
                if (_delay == 0)
                {
                    _action?.Invoke();
                }
                else
                {
                    _coroutine = StartCoroutine(Delay());
                }
            }
        }

        public void Stop()
        {
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(_delay);
            _action?.Invoke();
        }
    }
}