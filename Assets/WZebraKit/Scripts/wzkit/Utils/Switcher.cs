using UnityEngine;

using Sirenix.OdinInspector;

using UltEvents;

namespace wzebra.kit.core
{
    public class Switcher : MonoBehaviour
    {
        public UltEvent OnFirstState, OnSecondState;

        [SerializeField] private bool _saveState;

        [SerializeField, ShowIf(nameof(_saveState))] private string _uniqueID;

        private bool _state;

        private void Awake()
        {
            if (_saveState)
            {
                if (string.IsNullOrEmpty(_uniqueID))
                {
                    throw new System.ArgumentException($"Not setted unique id in switcher {gameObject.name}.");
                }

                LoadState();

                Invoke();
            }
        }

        private void LoadState()
        {
            _state = PlayerPrefs.GetInt(_uniqueID, 0) == 1 ? true : false;
        }

        private void SaveState()
        {
            PlayerPrefs.SetInt(_uniqueID, _state ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void Switch()
        {
            if(isActiveAndEnabled == false)
            {
                return;
            }

            _state ^= true;

            if (_saveState)
            {
                SaveState();
            }

            Invoke();
        }

        private void Invoke()
        {
            (_state ? OnSecondState : OnFirstState).Invoke();
        }
    }
}