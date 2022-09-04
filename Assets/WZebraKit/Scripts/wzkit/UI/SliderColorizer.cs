using UnityEngine;
using UnityEngine.UI;

namespace wzebra.kit.ui
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/SliderColorizer")]
    public class SliderColorizer : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        [SerializeField] private Gradient _gradient;

        [SerializeField] private Image _fill;

        private void Awake()
        {
            _slider.onValueChanged.AddListener((float value) => 
            {
                _fill.color = _gradient.Evaluate(value);
            });
        }
    }
}