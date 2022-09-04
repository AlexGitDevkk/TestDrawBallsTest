using UnityEngine;

namespace wzebra.kit.utils
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;

        [SerializeField] private GameObject[] _prefabs;

        [SerializeField] private Transform _parent;

        [SerializeField] private bool _spawnOnStart;

        private void Start()
        {
            if (_spawnOnStart)
            {
                Spawn();
            }
        }

        public void Spawn()
        {
            SpawnPrefab(_prefab);
        }

        public void Spawn(int index)
        {
            if(index < 0 || index >= _prefabs.Length)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            SpawnPrefab(_prefabs[index]);
        }

        private void SpawnPrefab(GameObject prefab)
        {
            Instantiate(prefab, _parent).SetActive(true);
        }
    }
}