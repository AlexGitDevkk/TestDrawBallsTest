using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using TMPro;

using UltEvents;

using Sirenix.OdinInspector;

using wzebra.kit.core;
using wzebra.kit.data;

namespace wzebra.kit.ui
{
    public abstract class ShopRenderer<T> : MonoBehaviour
    {
        [SerializeField] private UltEvent _show, _hide;

        [SerializeField] private GameObject _element;

        [SerializeField] private Transform _container;

        [SerializeField] private Button _buyButton;

        [SerializeField] private int _price;

        [SerializeField] private Shop<T> _shop;

        [SerializeField, FoldoutGroup("Selecting animation")] private float _selectingDuration = 0.2f;

        [SerializeField, FoldoutGroup("Selecting animation")] private int _selectingCount = 10;

        private ShopElementRenderer[] _renderers;

        private void Start()
        {
            CreateElements();
            UpdateElements();

            _shop.OnOpen += UpdateElement;
            _shop.OnSelectedUpdate += UpdateElement;

            _buyButton.gameObject.GetComponentInChildren<TMP_Text>().text = _price.ToString();
            _buyButton.onClick.AddListener(OnBuyClick);

            BuyButtonUpdate();

            CoinCounter.Instance.OnUpdate += (int count) => 
            {
                BuyButtonUpdate();
            };
        }

        public void Show()
        {
            _show?.Invoke();
        }

        public void Hide()
        {
            _hide?.Invoke();
        }

        private void CreateElements()
        {
            _renderers = new ShopElementRenderer[_shop.GetAllElements().Length];

            for(int i = 0; i < _renderers.Length; i++)
            {
                ShopElement<T> element = _shop.GetAllElements()[i];
                int index = i;

                GameObject go = Instantiate(_element, _container);
                ShopElementRenderer renderer = go.GetComponent<ShopElementRenderer>();
                _renderers[i] = renderer;

                renderer.OnClick += () => 
                {
                    if (element.Open)
                    {
                        _shop.SetSelected(index);
                    }
                };
            }
        }

        private void UpdateElements()
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                UpdateElement(i);
            }
        }

        private void UpdateElement(int index)
        {
            ShopElement<T> element = _shop.GetAllElements()[index];

            _renderers[index].SetData(element);
            _renderers[index].SetLocked(!element.Open);

            UpdateSelected();
        }

        private void UpdateSelected()
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].SetSelected(_shop.GetAllElements()[i] == _shop.GetSelected());
            }
        }

        private void OnBuyClick()
        {
            if (CanBuy())
            {
                List<int> closed = new List<int>();

                for (int i = 0; i < _shop.GetAllElements().Length; i++)
                {
                    if (_shop.GetAllElements()[i].Open == false)
                    {
                        closed.Add(i);
                    }
                }

                int index = Random.Range(0, closed.Count);

                int opened = closed[index];

                StartCoroutine(BuyAnimation(closed, () => 
                {
                    _shop.SetOpen(opened);
                    _shop.SetSelected(opened);
                    _renderers[opened].ScaleBounce();
                }));

                CoinCounter.Instance.RemoveCoins(_price);
            }
        }

        private IEnumerator BuyAnimation(List<int> closed, UnityAction callback)
        {
            WaitForSeconds wait = new WaitForSeconds(_selectingDuration);

            if (closed.Count > 1)
            {
                int pastIndex = _shop.GetSelectedIndex();

                for (int i = 0; i < _selectingCount; i++)
                {
                    yield return wait;

                    int index = Random.Range(0, closed.Count);

                    while(closed[index] == pastIndex)
                    {
                        index = Random.Range(0, closed.Count);
                    }

                    _renderers[pastIndex].SetSelected(false);
                    _renderers[closed[index]].SetSelected(true);

                    pastIndex = closed[index];
                }
            }

            callback?.Invoke();
        }

        private void BuyButtonUpdate()
        {
            _buyButton.interactable = CanBuy();
        }

        private bool CanBuy() => CoinCounter.Instance.GetCount() >= _price;
    }
}