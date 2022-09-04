using UnityEngine;

using TMPro;

using wzebra.drawballs.core;

namespace wzebra.drawballs.ui
{
    public class BallsTriggerCountRenderer : MonoBehaviour
    {
        [SerializeField] private BallsTrigger _trigger;

        [SerializeField] private TMP_Text _text;

        [SerializeField] private string _pattern = "{0}";

        private void Awake()
        {
            _trigger.OnCountUpdate += OnCountUpdate;
        }

        private void Start() { }

        private void OnCountUpdate(int count)
        {
            if (isActiveAndEnabled)
            {
                _text.text = string.Format(_pattern, count);
            }
        }
    }
}