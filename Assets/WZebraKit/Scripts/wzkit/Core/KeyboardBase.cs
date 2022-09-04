using UnityEngine;
using UnityEngine.Events;

namespace wzebra.kit.core
{
    public class KeyboardBase : MonoBehaviour
    {
        [SerializeField] private string _initString;

        public event UnityAction<char> OnKeyPress;
        public event UnityAction OnRemovePress;

        public event UnityAction OnUpdate;

        private string _currentText = "";

        private void Start()
        {
            if (_initString.Length > 0)
            {
                SetText(_initString);
            }
        }

        public void KeyPress(string symbol)
        {
            OnKeyPress?.Invoke(symbol[0]);
            AddSymbol(symbol[0]);
        }

        public void RemovePress()
        {
            OnRemovePress?.Invoke();
            RemoveSymbol();
        }

        public void RemoveAllText()
        {
            _currentText = "";

            OnUpdate?.Invoke();
        }

        public void SetText(string text)
        {
            _currentText = text;

            OnUpdate?.Invoke();
        }

        public void AddSymbol(char symbol)
        {
            SetText(GetCurrentText() + symbol);
        }

        public void RemoveSymbol()
        {
            if(GetCurrentText().Length == 0)
            {
                return;
            }

            SetText(GetCurrentText().Substring(0, GetCurrentText().Length - 1));
        }

        public string GetCurrentText() => _currentText;
    }
}