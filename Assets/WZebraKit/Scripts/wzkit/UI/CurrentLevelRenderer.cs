using UnityEngine;

using TMPro;

using wzebra.kit.core;

namespace wzebra.kit.ui
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/CurrentLevelRenderer")]
    public class CurrentLevelRenderer : MonoBehaviour
    {
        [SerializeField] private LevelSpawner _spawner;

        [SerializeField] private LevelCounter _counter;

        [SerializeField] private TMP_Text _text;

        [SerializeField] private string _pattern;

        [SerializeField] private bool _startFromZero;

        private void Awake()
        {
            _spawner.OnStartLevel += () => 
            {
                _text.text = string.Format(_pattern, _counter.GetShowingLevel() + (_startFromZero ? 0 : 1));
            };
        }
    }
}