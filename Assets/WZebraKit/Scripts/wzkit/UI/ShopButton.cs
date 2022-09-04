using UnityEngine;
using UnityEngine.UI;

using TMPro;

using wzebra.kit.core;

namespace wzebra.kit.ui
{
    public class ShopButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [SerializeField] private int _price;

        private TMP_Text _text;

        private void Start()
        {
            _text = _button.GetComponentInChildren<TMP_Text>();

            CoinCounter.Instance.OnUpdate += OnUpdate;

            _button.onClick.AddListener(() => { CoinCounter.Instance.RemoveCoins(_price); });

            SetPrice(_price);
        }

        private void OnUpdate(int count)
        {
            _button.interactable = _price <= count;
        }

        public void SetPrice(int price)
        {
            _price = price;
            _text.text = _price.ToString();
        }
    }
}