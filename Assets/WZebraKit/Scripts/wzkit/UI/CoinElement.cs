using UnityEngine;

using UltEvents;

using TMPro;

namespace wzebra.kit.ui
{
    public class CoinElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        [SerializeField] private UltEvent OnStart, OnStartJump, OnEndAnimation;

        private void Start()
        {
            OnStart?.Invoke();
        }

        public void StartJump()
        {
            OnStartJump?.Invoke();
        }

        public void EndJump()
        {
            OnEndAnimation?.Invoke();
        }

        public void SetCount(int count)
        {
            _text.text = count.ToString();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}