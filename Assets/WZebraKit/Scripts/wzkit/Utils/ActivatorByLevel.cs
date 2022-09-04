using UnityEngine;

using wzebra.kit.core;

namespace wzebra.kit.utils
{
    public class ActivatorByLevel : MonoBehaviour
    {
        [SerializeField] private LevelSpawner _spawner;

        [SerializeField] private LevelCounter _counter;

        [SerializeField] private int _levelThreshold;

        [SerializeField] private GameObject _object;

        private void Start()
        {
            _spawner.OnStartLevel += OnStartLevel;
        }

        private void OnStartLevel()
        {
            _object.SetActive(_counter.GetActualLevel() >= _levelThreshold);
        }
    }
}