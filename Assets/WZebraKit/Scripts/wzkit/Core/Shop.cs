using UnityEngine;
using UnityEngine.Events;

using wzebra.kit.data;

namespace wzebra.kit.core
{
    public abstract class Shop<T> : MonoBehaviour
    {
        [SerializeField] protected ShopElement<T>[] _elements;

        public event UnityAction<int> OnSelectedUpdate, OnOpen;

        protected int _selected;

        protected ShopSaver<T> _saver;

        private void Awake()
        {
            _saver = new(_elements);

            _selected = _saver.GetSelected();
            _saver.LoadElements();

            SetOpen(0);

            AfterAwake();
        }

        protected virtual void AfterAwake() { }

        public ShopElement<T>[] GetAllElements() => _elements;

        public void SetOpen(int index)
        {
            _elements[index].SetOpen(true);
            _saver.SaveElement(_elements[index]);

            OnOpen?.Invoke(index);
        }

        public void SetSelected(int index)
        {
            if(index < 0 || index >= _elements.Length)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            _selected = index;

            _saver.SaveSelected(_selected);

            OnSelectedUpdate?.Invoke(_selected);
        }

        public ShopElement<T> GetSelected() => _elements[_selected];

        public int GetSelectedIndex() => _selected;
    }
}