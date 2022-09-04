using UnityEngine;
using UnityEngine.UI;

using wzebra.kit.core;

namespace wzebra.kit.ui
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/StartLevelButton")]
    public class StartLevelButton : MonoBehaviour
    {
        [SerializeField] private LevelSpawner _spawner;

        [SerializeField] private LevelCounter _counter;

        [SerializeField] private bool _nextLevel;

        [SerializeField] private Button _button;

        private void Start()
        {
            if(_nextLevel && _counter.IncrementOnWin == false)
            {
                _button.onClick.AddListener(_counter.Increment);
            }

            /// Инкремент уровня идёт в LevelCounter, с подпиской на событие LevelSpawner.OnWin
            /// По нажатию на кнопку просто спавнится уровень с нужного индекса
            _button.onClick.AddListener(_spawner.SpawnLevel);
        }
    }
}