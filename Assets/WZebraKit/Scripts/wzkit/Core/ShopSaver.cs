using UnityEngine;

using wzebra.kit.data;

namespace wzebra.kit.core
{
    public class ShopSaver<T> : MonoBehaviour
    {
        private const string _key = "Shop";

        private readonly ShopElement<T>[] _elements;

        public ShopSaver(ShopElement<T>[] elements)
        {
            _elements = elements;
        }


        public int GetSelected()
        {
            return PlayerPrefs.GetInt(_key + typeof(T).ToString(), 0);
        }

        public void SaveSelected(int index)
        {
            PlayerPrefs.SetInt(_key + typeof(T).ToString(), index);
            PlayerPrefs.Save();
        }

        public void SaveElements()
        {
            foreach(var element in _elements)
            {
                SaveElement(element);
            }
        }

        public void SaveElement(ShopElement<T> element)
        {
            PlayerPrefs.SetInt(_key + typeof(T).ToString() + (element.GetHashCode()), (element.Open ? 1 : 0));

            PlayerPrefs.Save();
        }

        public void LoadElements()
        {
            foreach (var element in _elements)
            {
                int load = PlayerPrefs.GetInt(_key + typeof(T).ToString() + (element.GetHashCode()), 0);
                element.SetOpen(load == 1 ? true : false);
            }
        }
    }
}