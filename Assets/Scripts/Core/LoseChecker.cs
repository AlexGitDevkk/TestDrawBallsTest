using System.Linq;
using System.Collections;

using UnityEngine;

using UltEvents;

using wzebra.kit.core;

namespace wzebra.drawballs.core
{
    public class LoseChecker : MonoBehaviour
    {
        [SerializeField] private BallsDrawer _drawer;

        [SerializeField] private LevelSpawner _spawner;

        [SerializeField] private Puller _puller;

        [SerializeField] private float _stayThreshold = 3f;

        [SerializeField] private UltEvent OnLose;

        private Coroutine _checker;

        private const float _chechInterval = 0.5f;

        private void Start()
        {
            _drawer.OnLostAllBalls += StartChecker;

            _spawner.OnEndLevel += () => 
            {
                StopCoroutine(_checker);
            };
        }

        public void StartChecker()
        {
            _checker = StartCoroutine(CheckCycle());
        }

        private IEnumerator CheckCycle()
        {
            WaitForSeconds wait = new WaitForSeconds(_chechInterval);

            _puller.CacheComponent<Rigidbody>();

            while (true)
            {
                yield return wait;

                if(HaveMovingBalls())
                {
                    continue;
                }

                yield return new WaitForSeconds(_stayThreshold);

                if (HaveMovingBalls())
                {
                    continue;
                }

                OnLose?.Invoke();

                StopCoroutine(_checker);
            }
        }

        private bool HaveMovingBalls() => _puller.GetCachedComponents<Rigidbody>().Where(r => r.gameObject.activeSelf && r.velocity.magnitude > 0.5f).Count() == 0;
    }
}