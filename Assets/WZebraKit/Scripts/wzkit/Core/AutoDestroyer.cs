using System.Collections;

using UnityEngine;

namespace wzebra.kit.core
{
    public class AutoDestroyer : MonoBehaviour
    {
        [SerializeField] private float _delay;

        [SerializeField] private bool _softMode;

        private Coroutine _timer;

        private void Start()
        {
            StartTimer();
        }

        public void UpdateTimer(float value)
        {
            if(value < 0)
            {
                throw new System.ArgumentOutOfRangeException("Delay can't be less than zero.");
            }

            _delay = value;

            StopCoroutine(_timer);
            StartTimer();
        }

        private void StartTimer()
        {
            _timer = StartCoroutine(Timer());
        }

        public float GetDelay() => _delay;

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(_delay);

            if (_softMode)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}