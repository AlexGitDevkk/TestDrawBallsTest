using UnityEngine;
using UltEvents;

namespace wzebra.kit.core
{
    [System.Serializable]
    public struct CustomLevelAction
    {
        [SerializeField] private int _id;
        [SerializeField] private UltEvent _action;

        public int ID => _id;
        public UltEvent Action => _action;

        public void SetID(int id)
        {
            _id = id;
        }
    }
}