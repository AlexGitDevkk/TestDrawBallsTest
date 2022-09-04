using UnityEngine;

using TMPro;

using wzebra.kit.core;

namespace wzebra.kit.utils
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/CounterRenderer")]
    public class CounterRenderer : MonoBehaviour
    {
        [SerializeField] private Counter _counter;

        [SerializeField] private TMP_Text _text;

        [SerializeField] private string _pattern;

        private void Awake()
        {
            _counter.OnCountUpdate += OnCountUpdate;
        }

        private void OnCountUpdate(int count)
        {
            _text.text = string.Format(_pattern, count, _counter.GetTargetCount());
        }
    }
}