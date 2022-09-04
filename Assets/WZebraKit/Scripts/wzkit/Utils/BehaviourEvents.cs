using UnityEngine;
using UnityEngine.Events;

namespace wzebra.kit.utils
{
    public class BehaviourEvents : MonoBehaviour
    {
        public event UnityAction onEnable, onDisable, onStart, onDestroy;

        private void OnEnable()
        {
            onEnable?.Invoke();
        }

        private void OnDisable()
        {
            onDisable?.Invoke();
        }

        private void Start()
        {
            onStart?.Invoke();
        }

        private void OnDestroy()
        {
            onDestroy?.Invoke();
        }
    }
}