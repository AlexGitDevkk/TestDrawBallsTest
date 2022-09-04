using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

using wzebra.kit.tweener;

namespace wzebra.kit.core
{
    public class Puller : MonoBehaviour
    {
        [SerializeField] private GameObject _object;

        [SerializeField] private int _count;

        [SerializeField] private bool _spawnOnStart;

        public event UnityAction OnSpawn;

        private GameObject[] _spawned;

        private Dictionary<System.Type, Component[]> _cachedComponents;

        private void Start()
        {
            _cachedComponents = new Dictionary<System.Type, Component[]>();

            if (_spawnOnStart)
            {
                Spawn();
            }
        }

        public void SetCount(int count)
        {
            _count = count;
        }

        public void Spawn()
        {
            _spawned = new GameObject[_count];

            for (int i = 0; i < _count; i++)
            {
                GameObject go = Instantiate(_object, transform);
                go.SetActive(false);

                go.name += i;

                _spawned[i] = go;
            }

            OnSpawn?.Invoke();
        }

        public GameObject GetObject()
        {
            for (int i = 0; i < _spawned.Length; i++)
            {
                if (_spawned[i].activeSelf == false)
                {
                    _spawned[i].transform.localPosition = Vector3.zero;
                    _spawned[i].SetActive(true);
                    return _spawned[i];
                }
            }

            throw new System.Exception("Have no free objects in pool.");
        }

        public void ReleaseObject(GameObject go, bool withScaler = false)
        {
            if(_spawned.Contains(go) == false)
            {
                throw new System.Exception("Incorrect object in pull");
            }

            if (withScaler && go.TryGetComponent(out ScaleFader scaler))
            {
                scaler.FadeOut().onComplete += () =>
                {
                    EndReleaseObject(go);
                };
            }
            else
            {
                EndReleaseObject(go);
            }
        }

        private void EndReleaseObject(GameObject go)
        {
            if (go.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.velocity = Vector3.zero;
            }

            if (go.TryGetComponent(out Rigidbody2D rigidbody2D))
            {
                rigidbody2D.velocity = Vector3.zero;
            }

            go.SetActive(false);

            go.transform.SetParent(transform);
        }

        public void ReleaseAll()
        {
            for (int i = 0; i < _spawned.Length; i++)
            {
                ReleaseObject(_spawned[i]);
            }
        }

        public void CacheComponent<T>()
        {
            Component[] components = new Component[_spawned.Length];

            for (int i = 0; i < _spawned.Length; i++)
            {
                components[i] = _spawned[i].GetComponent(typeof(T));
            }

            _cachedComponents.Add(typeof(T), components);
        }

        public T[] GetCachedComponents<T>() where T : Component
        {
            if (_cachedComponents.ContainsKey(typeof(T)) == false)
            {
                throw new System.Exception($"Type {typeof(T)} doesn't cached.");
            }

            return _cachedComponents[typeof(T)].Select(c => (T)c).ToArray();
        }

        public T GetCachedComponent<T>(GameObject go) where T : Component => GetCachedComponents<T>().Where(c => c.gameObject == go).First();

        public GameObject[] GetAllObjects() => _spawned;
    }
}