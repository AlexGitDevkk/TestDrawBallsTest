using UnityEngine;
using UnityEngine.Events;

using UltEvents;

namespace wzebra.kit.core
{
    public class TriggerAction : MonoBehaviour
    {
        [SerializeField] private LayerMask _mask = ~0;

        [SerializeField] private UltEvent CollisionStay;
        [SerializeField] private UltEvent TriggerStay;

        [SerializeField] private UltEvent CollisionEnter;
        [SerializeField] private UltEvent TriggerEnter;

        [SerializeField] private UltEvent CollisionExit;
        [SerializeField] private UltEvent TriggerExit;

        public event UnityAction<GameObject> onCollisionStay;
        public event UnityAction<GameObject> onTriggerStay;

        public event UnityAction<GameObject> onCollisionEnter; 
        public event UnityAction<GameObject> onTriggerEnter;

        public event UnityAction<GameObject> onCollisionExit;
        public event UnityAction<GameObject> onTriggerExit;

        private void Awake()
        {
            onCollisionStay += (GameObject) => { CollisionStay?.Invoke(); };
            onTriggerStay += (GameObject) => { TriggerStay?.Invoke(); };

            onCollisionEnter += (GameObject) => { CollisionEnter?.Invoke(); };
            onTriggerEnter += (GameObject) => { TriggerEnter?.Invoke(); };

            onCollisionExit += (GameObject) => { CollisionExit?.Invoke(); };
            onTriggerExit += (GameObject) => { TriggerExit?.Invoke(); };
        }

        /// <summary>
        /// Не убирать! Нужно, чтобы у компонента отображалась галочка включения.
        /// </summary>
        private void Start() {}

        private void OnCollisionStay(Collision collision)
        {
            OnCollisionStay(collision.gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnter(collision.gameObject);
        }

        private void OnCollisionExit(Collision collision)
        {
            OnCollisionExit(collision.gameObject);
        }

        private void OnTriggerStay(Collider collider)
        {
            OnTriggerStay(collider.gameObject);
        }

        private void OnTriggerEnter(Collider collider)
        {
            OnTriggerEnter(collider.gameObject);
        }

        private void OnTriggerExit(Collider collider)
        {
            OnTriggerExit(collider.gameObject);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            OnCollisionStay(collision.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEnter(collision.gameObject);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            OnCollisionExit(collision.gameObject);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            OnTriggerStay(collision.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnTriggerEnter(collision.gameObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            OnTriggerExit(collision.gameObject);
        }

        private void OnCollisionStay(GameObject go)
        {
            if (Contains(go.layer) && isActiveAndEnabled)
            {
                onCollisionStay?.Invoke(go);
            }
        }

        private void OnCollisionEnter(GameObject go)
        {
            if (Contains(go.layer) && isActiveAndEnabled)
            {
                onCollisionEnter?.Invoke(go);
            }
        }

        private void OnCollisionExit(GameObject go)
        {
            if (Contains(go.layer) && isActiveAndEnabled)
            {
                onCollisionExit?.Invoke(go);
            }
        }

        private void OnTriggerStay(GameObject go)
        {
            if (Contains(go.layer) && isActiveAndEnabled)
            {
                onTriggerStay?.Invoke(go);
            }
        }

        private void OnTriggerEnter(GameObject go)
        {
            if (Contains(go.layer) && isActiveAndEnabled)
            {
                onTriggerEnter?.Invoke(go);
            }
        }

        private void OnTriggerExit(GameObject go)
        {
            if (Contains(go.layer) && isActiveAndEnabled)
            {
                onTriggerExit?.Invoke(go);
            }
        }

        private bool Contains(int layer) => _mask == (_mask | (1 << layer));
    }
}