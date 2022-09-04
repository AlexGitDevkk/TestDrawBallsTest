using UnityEngine;

using wzebra.kit.core;

namespace wzebra.kit.utils
{
    public class LevelNavigator : MonoBehaviour
    {
        [SerializeField] private LevelSpawner _spawner;
        [SerializeField] private LevelCounter _counter;

        public void Previous()
        {
            _counter.SetLevel(_counter.GetActualLevel()-1);
            _spawner.SpawnLevel();
        }

        public void Win()
        {
            _spawner.WinInvokeForTesters();
        }

        public void Next()
        {
            _counter.SetLevel(_counter.GetActualLevel() + 1);
            _spawner.SpawnLevel();
        }
    }
}