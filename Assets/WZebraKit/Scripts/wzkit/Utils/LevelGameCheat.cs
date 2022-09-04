using UnityEngine;

using TMPro;

using wzebra.kit.core;

namespace wzebra.kit.utils
{
    public class LevelGameCheat : MonoBehaviour
    {
        [SerializeField] private LevelSpawner _spawner;

        [SerializeField] private LevelCounter _counter;

        [SerializeField] private TMP_InputField _input;

        public void SetLevel()
        {
            if(int.TryParse(_input.text, out int level))
            {
                level++;
                if(level < 1 || level > _spawner.GetLevelCount() - 1)
                {
                    level = 0;
                }

                _counter.SetLevel(level);
                _spawner.SpawnLevel();
            }
        }
    }
}