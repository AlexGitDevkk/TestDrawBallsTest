using UnityEngine;

using TMPro;

using wzebra.kit.core;

namespace wzebra.kit.ui
{
    public class EnergyTimerRenderer : MonoBehaviour
    {
        [SerializeField] private EnergyCounter _counter;

        [SerializeField] private TMP_Text _text;

        [SerializeField] private string _pattern = "{0:00}:{1:00}";

        [SerializeField] private bool _autoHide = true;

        private void Start()
        {
            _counter.OnTimerUpdate += OnTimerUpdate;

            if (_autoHide)
            {
                _counter.OnFull += () => 
                {
                    _text.enabled = false;
                };

                _counter.OnStartRegeneration += () => 
                {
                    _text.enabled = true;
                };
                _text.enabled = false;
            }
        }

        private void OnTimerUpdate(float value)
        {
            if (_autoHide)
            {
                _text.enabled = true;
            }

            float state = _counter.GetSecondsRest();

            int minutes = (int)(state / 60);
            int seconds = (int)(state % 60);

            _text.text = string.Format(_pattern, minutes, seconds);
        }
    }
}