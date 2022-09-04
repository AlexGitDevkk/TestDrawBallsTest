using UnityEngine;

using TMPro;

using wzebra.kit.core;

namespace wzebra.kit.utils
{
    public class NavigatorLogRenderer : MonoBehaviour
    {
        [SerializeField] private LevelSpawner _spawner;

        [SerializeField] private TMP_Text _text;

        private void Start()
        {
            _spawner.OnStartLevel += OnStartLevel;
            OnStartLevel();
        }

        private void OnStartLevel()
        {
            _text.text = $"Level name: <i>{_spawner.GetCurrentLevelGO().name}</i>";
        }
    }
}