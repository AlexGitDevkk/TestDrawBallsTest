using UnityEngine;

using wzebra.kit.data;

namespace wzebra.kit.core
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/Tutorial")]
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private LevelSpawner _spawner;
        [SerializeField] private LevelCounter _counter;

        [SerializeField] private TutorialData[] _data;

        private void Awake()
        {
            _spawner.OnStartLevel += OnStartLevel;
            _spawner.CustomAction += OnCustomAction;
        }

        private void OnCustomAction(int id)
        {
            for (int i = 0; i < _data.Length; i++)
            {
                if (_data[i].TargetLevel == id && _data[i].CustomEvent == id)
                {
                    _data[i].TutorialObject.SetActive(true);
                }
            }
        }

        private void OnStartLevel()
        {
            for (int i = 0; i < _data.Length; i++)
            {
                if(_counter.GetActualLevel() == _data[i].TargetLevel && _data[i].CustomEvent == -1)
                {
                    _data[i].TutorialObject.SetActive(true);
                }
            }
        }
    }
}