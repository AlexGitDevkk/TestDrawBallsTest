using System.Collections.Generic;

using UnityEngine;

namespace wzebra.kit.core
{
    [RequireComponent(typeof(Collider2D))]
    public class RigidbodyWindZone2D : MonoBehaviour /// !!! Переделать, сейчас DRY с 3D аналогом
    {
        [SerializeField] private Transform _direction;
        [SerializeField] private float _force;

        private List<Rigidbody2D> _activeObjects;

        private void Awake()
        {
            _activeObjects = new List<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out Rigidbody2D rigidbody))
            {
                _activeObjects.Add(rigidbody);
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out Rigidbody2D rigidbody))
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