using UnityEngine;

namespace wzebra.kit.core
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/LevelCounter")]
    [RequireComponent(typeof(LevelSpawner))]
    public class LevelCounter : MonoBehaviour
    {
        [SerializeField] private int _cyclingIndex;

        [SerializeField] private bool _incrementOnWin;

        private LevelSpawner _spawner;

        private int _maxLevel;

        private int _actualLevel;
        private int _showingLevel;

        private void Awake()
        {
            _actualLevel = LevelSaver.GetActualLevel();
            _showingLevel = LevelSaver.GetShowingLevel();
        }

        private void Start()
        {
            _spawner = GetComponent<LevelSpawner>();

            if (_incrementOnWin)
            {
                _spawner.OnWin += (float success) =>
                {
                    Increment();
                };
            }

            SetMaxLevel(_spawner.GetLevelCount());
        }

        private void SetMaxLevel(int max)
        {
            if (_cyclingIndex >= max)
            {
                throw new System.ArgumentOutOfRangeException("Cycling index bigger than levels count.");
            }

            _maxLevel = max;
        }

        public void Increment()
        {
            _actualLevel++;

            if (_actualLevel >= _maxLevel)
            {
                _actualLevel = _cyclingIndex;
            }

            LevelSaver.SetActualLevel(_actualLevel);

            _showingLevel++;
            LevelSaver.SetShowingLevel(_showingLevel);
        }

        /// <summary>
        /// Не использовать! Только для отладочных целей
        /// </summary>
        public void SetLevel(int level, bool soft = true)
        {
            _actualLevel = level;

            if (soft)
            {
                if (_actualLevel < 0)
                {
                    _actualLevel = 0;
                }

                if (_actualLevel >= _maxLevel)
                {
                    _actualLevel = _cyclingIndex;
                }
            }

            _showingLevel = _actualLevel;

            LevelSaver.SetActualLevel(level);
            LevelSaver.SetShowingLevel(level);
        }

        public int GetActualLevel() => _actualLevel;

        public int GetShowingLevel() => _showingLevel;

        public bool IncrementOnWin => _incrementOnWin;
    }
}