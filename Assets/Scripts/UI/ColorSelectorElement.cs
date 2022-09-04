using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

namespace wzebra.drawballs.ui
{
    public class ColorSelectorElement : MonoBehaviour
    {
        [SerializeField] private Image _body;
        [SerializeField] private Image _outline;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        public event UnityAction OnSelect;

        private bool _selected;

        private void Start()
        {
            if (_selected == false)
            {
                Select(false);
            }
            _button.onClick.AddListener(OnSelectInvoke);
        }

        public void SetColor(Color color)
        {
            _body.color = color;
        }

        public void Select(bool state)
        {
            _selected = state;
            _outline.gameObject.SetActive(state);
        }

        public void OnSelectInvoke()
        {
            OnSelect?.Invoke();
        }

        public void SetCount(int count)
        {
            _text.text = count.ToString();
        }
    }
}