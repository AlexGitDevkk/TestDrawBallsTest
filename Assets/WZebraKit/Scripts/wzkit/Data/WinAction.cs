using UnityEngine;
using UltEvents;

namespace wzebra.kit.data
{
    [System.Serializable]
    public struct WinAction
    {
        [Range(0, 1)]
        [SerializeField] private float _success;

        [SerializeField] private UltEvent _action;

        public float Success => _success;

        public UltEvent Action => _action;
    }
}