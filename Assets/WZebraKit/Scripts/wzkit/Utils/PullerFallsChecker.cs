using System.Collections;

using UnityEngine;

using wzebra.kit.core;

namespace wzebra.kit.utils
{
    [RequireComponent(typeof(Puller))]
    public class PullerFallsChecker : MonoBehaviour
    {
        [SerializeField] private float _fallDistance;

        private float _checkingInterval = 1f;

        private Puller _puller;

        private void Start()
        {
            _puller = GetComponent<Puller>();

            StartCoroutine(Checker());
        }

        private IEnumerator Checker()
        {
            WaitForSeconds wait = new WaitForSeconds(_checkingInterval);

            while (true)
            {
                yield return wait;

                GameObject[] gos = _puller.GetAllObjects();

                for (int i = 0; i < gos.Length; i++)
                {
                    if (gos[i].activeInHierarchy && gos[i].transform.position.magnitude > _fallDistance)
                    {
                        _puller.ReleaseObject(gos[i]);
                    }
                }
            }
        }
    }
}