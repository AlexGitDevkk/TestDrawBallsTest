using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

using wzebra.kit.tweener;
using wzebra.kit.data;

namespace wzebra.kit.ui
{
    public class ShopElementRenderer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;

        [SerializeField] private Image _icon;

        [SerializeField] private GameObject _select, _lock;

        [SerializeField] private Button _button;

        [SerializeField] private ScaleFader _fader;

        public event UnityAction OnClick;

        private void Start()
        {
            _button.onClick.AddListener(() => { OnClick?.Invoke(); });
        }

        public void SetData<T>(ShopElement<T> element)
        {
            _label.text = element.Name;
            _icon.sprite = element.Icon;
        }

        public void SetSelected(bool state)
        {
            _select.SetActive(state);
        }

        public void SetLocked(bool state)
        {
            _lock.SetActive(state);
        }

        public void ScaleBounce()
        {
            _fader.FadeIn().onComplete += () => { _fader.FadeOut(); };
        }
    }
}