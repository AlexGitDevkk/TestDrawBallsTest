using UnityEngine;
using UnityEngine.UI;

namespace wzebra.kit.utils
{
    public class DeveloperScreen : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private bool _state;

        private void Start()
        {
            _button.onClick.AddListener(() => 
            {
                _state ^= true;
                transform.SetActiveChilds(_state);
            });
        }
    }
}