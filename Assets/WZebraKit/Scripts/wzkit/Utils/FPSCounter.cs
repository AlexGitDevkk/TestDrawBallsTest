using UnityEngine;

using TMPro;

namespace wzebra.kit.utils
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/FPSCounter")]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FPSCounter : MonoBehaviour 
    {
        [SerializeField] private float _updateInterval = 0.3f;

        [SerializeField] private string _pattern;

        private float _accum = .0f;
        private int _frames = 0;
        private float _timeLeft;

        private TextMeshProUGUI _text;

        private void Start()
        {
            _timeLeft = _updateInterval;

            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {           
            _timeLeft -= Time.deltaTime;

            _accum += Time.timeScale / Time.deltaTime;
            ++_frames;

            if (_timeLeft <= 0)
            {
                _text.text = string.Format(_pattern, Mathf.RoundToInt(_accum / _frames));

                _timeLeft = _updateInterval;
                _accum = .0f;
                _frames = 0;
            }
        }
    }
}