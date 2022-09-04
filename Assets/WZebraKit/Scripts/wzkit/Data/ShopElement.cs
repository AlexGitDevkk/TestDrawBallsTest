using UnityEngine;

namespace wzebra.kit.data
{
    [System.Serializable]
    public class ShopElement<T> : ScriptableObject
    {
        [SerializeField] private string _name = "New Skin";

        [SerializeField] private Sprite _icon;

        [SerializeField] private T _element;

        private bool _open;

        public bool Open => _open;

        public T Element => _element;

        public string Name => _name;

        public Sprite Icon => _icon;

        public void SetOpen(bool state)
        {
            _open = state;
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }
    }
}