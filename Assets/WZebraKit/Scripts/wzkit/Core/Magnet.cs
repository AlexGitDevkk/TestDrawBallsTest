using System.Collections.Generic;

using UnityEngine;

namespace wzebra.kit.core
{
    [RequireComponent(typeof(TriggerAction))]
    public class Magnet : MonoBehaviour
    {
        [SerializeField] private float _force;

        private TriggerAction _trigger;

        private List<Rigidbody> _affectedObjects;

        private void Start()
        {
            _trigger = GetComponent<TriggerAction>();

            _affectedObjects = new List<Rigidbody>();

            _trigger.onTriggerEnter += onTriggerEnter;
            _trigger.onTriggerExit += onTriggerExit;
        }

        private void onTriggerEnter(GameObject go)
        {
            if(go.TryGetComponent(out Rigidbody rigidbody))
            {
                if(_affectedObjects.Contains(rigidbody) == false)
                {
                    _affectedObjects.Add(rigidbody);
                }
            }
        }

        private void onTriggerExit(GameObject go)
        {
            if (go.TryGetComponent(out Rigidbody rigidbody))
            {
                if (_affectedObjects.Contains(rigidbody))
                {
                    _affectedObjects.Remove(rigidbody);
                }
            }
        }

        private void FixedUpdate()
        {
            foreach (var rigidbody in _affectedObjects)
            {
                rigidbody.AddForce((transform.position - rigidbody.position).normalized * _force);
            }
        }
    }
}