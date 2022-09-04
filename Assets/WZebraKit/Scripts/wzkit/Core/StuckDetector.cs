using System.Collections;

using UnityEngine;
using UltEvents;

namespace wzebra.kit.core
{
    public class StuckDetector : MonoBehaviour
    {
        [SerializeField] private float _distanceThreshold;
        [SerializeField] private float _timeThreshold;

        public UltEvent OnStuck;

        private float _checkInterval = 0.3f;

        private Vector3 _pastPosition;

        private Coroutine _detector;

        private void Start()
        {
            StartDetector();
        }

        private void StartDetector()
        {
            _detector = StartCoroutine(Detector());
        }

        private IEnumerator Detector()
        {
            while (true)
            {
                _pastPosition = transform.position;
                yield return new WaitForSeconds(_checkInterval);

                if (ChechDistance())
                {
                    yield return new WaitForSeconds(_timeThreshold);

                    if (ChechDistance())
                    {
                        OnStuck?.Invoke();
                        break;
                    }
                }
            }
        }

        public void Restart()
        {
            StopCoroutine(_detector);
            StartDetector();
        }

        private bool ChechDistance() => Vector3.Distance(_pastPosition, transform.position) < _distanceThreshold;
    }
}