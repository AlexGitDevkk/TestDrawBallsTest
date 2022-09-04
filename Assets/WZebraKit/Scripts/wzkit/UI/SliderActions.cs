using UnityEngine;
using UnityEngine.UI;

using wzebra.kit.data;

namespace wzebra.kit.ui
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/SliderActions")]
    public class SliderActions : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        [SerializeField] private SliderValueAction[] _actions;

        private float _pastValue;

        private void Awake()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            for (int i = 0; i < _actions.Length; i++)
            {
                SliderValueAction action = _actions[i];

                if(action.Value > _pastValue && action.Value <= value)
                {
                    action.UpAction?.Invoke();
                }else if (action.Value <= _pastValue && action.Value > value)
                {
                    action.DownAction?.Invoke();
                }
            }

            _pastValue = value;
        }
    }
}