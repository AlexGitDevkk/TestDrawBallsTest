using System.Collections.Generic;

using UnityEngine;

namespace wzebra.kit.core
{
    [RequireComponent(typeof(Collider))]
    public class RigidbodyWindZone : MonoBehaviour
    {
        [SerializeField] private Transform _direction;
        [SerializeField] private float _force;

        private List<Rigidbody> _activeObjects;

        private void Awake()
        {
            _activeObjects = new List<Rigidbody>();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if(collider.TryGetComponent(out Rigidbody rigidbody))
            {
                _activeObjects.Add(rigidbody);
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
            {
                if (_activeObjects.Contains(rigidbody))
                {
                    _activeObjects.Remove(rigidbody);
                }
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _activeObjects.Count; i++)
            {
                _activeObjects[i].AddForce(_direction.localPosition * _force * Time.fixedDeltaTime);
            }
        }
    }
}