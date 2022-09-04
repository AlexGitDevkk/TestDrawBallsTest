using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace wzebra.kit.ui
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/SliderText")]
    public class SliderText : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        [SerializeField] private string _pattern;

        [SerializeField] private TextMeshProUGUI _text;

        private void Awake()
        {
            _slider.onValueChanged.AddListener((float value) =>
            {
                _text.text = string.Format(_pattern, Mathf.Round(value * 100f));
            });
        }
    }
}