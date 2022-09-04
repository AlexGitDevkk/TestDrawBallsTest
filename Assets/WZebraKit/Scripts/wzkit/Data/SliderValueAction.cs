using UnityEngine;
using UltEvents;

namespace wzebra.kit.data
{
    [System.Serializable]
    public struct SliderValueAction
    {
        [Range(0, 1)]
        [SerializeField] private float _value;

        [SerializeField] private UltEvent _upAction;
        [SerializeField] private UltEvent _downAction;

        public float Value => _value;

        public UltEvent UpAction => _upAction;

        public UltEvent DownAction => _downAction;
    }
}