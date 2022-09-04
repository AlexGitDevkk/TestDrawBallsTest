using UnityEngine;

using TMPro;

namespace wzebra.kit.core
{
    public class KeyboardRenderer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        [SerializeField] private KeyboardBase _keyboard;

        [SerializeField] Transform _caret;

        private bool _needUpdateCaret;

        private void Awake()
        {
            if (_keyboard != null)
            {
                SetKeyboard(_keyboard);
            }
        }

        private void OnUpdate()
        {
            _text.text = _keyboard.GetCurrentText();

            _needUpdateCaret = true;
        }

        private void FixedUpdate()
        {
            if(_needUpdateCaret && _caret != null && _text.textInfo.characterCount > 0)
            {
                Vector3 bottomRight = _text.textInfo.characterInfo[_text.textInfo.characterCount - 1].bottomRight;
                Vector3 worldBottomRight = _text.transform.TransformPoint(bottomRight);

                Vector3 buttonSpacePos = _caret.transform.parent.InverseTransformPoint(worldBottomRight);

                buttonSpacePos.y = _caret.localPosition.y;
                buttonSpacePos.z = _caret.localPosition.z;

                _caret.transform.localPosition = buttonSpacePos;

                _needUpdateCaret = false;
            }
        }

        public void SetKeyboard(KeyboardBase keyboard)
        {
            _keyboard = keyboard;
            _keyboard.OnUpdate += OnUpdate;
        }
    }
}