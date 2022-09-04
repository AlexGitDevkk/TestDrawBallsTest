using UnityEngine;
using UltEvents;

namespace wzebra.kit.core
{
    [RequireComponent(typeof(Collider2D))]
    public class SpriteButton : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;

        /// Можно через UI.ColorBlock, но там появляется несколько неиспользуемых полей
        [SerializeField] private Color _normal = Color.white, _highlighted = new Color(0.9f, 0.9f, 0.9f, 1), _pressed = new Color(0.7f, 0.7f, 0.7f, 1), _disabled = new Color(0.6f, 0.6f, 0.6f, 0.6f);

        [SerializeField] private bool _interactable = true;

        public UltEvent OnClick;

        private void Start()
        {
            _sprite.color = _normal;
        }

        private void OnMouseEnter()
        {
            ChangeColor(_highlighted);
        }

        private void OnMouseExit()
        {
            ChangeColor(_normal);
        }

        private void OnMouseDown()
        {
            ChangeColor(_pressed);

            if (_interactable)
            {
                OnClick?.Invoke();
            }
        }

        private void OnMouseUp()
        {
            ChangeColor(_normal);
        }

        private void ChangeColor(Color color)
        {
            if (_interactable == false)
            {
                return;
            }

            _sprite.color = color;
        }

        public void SetInteractable(bool state)
        {
            _interactable = state;
            _sprite.color = state ? _normal : _disabled;
        }
    }
}