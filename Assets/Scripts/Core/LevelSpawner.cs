using UnityEngine;

using wzebra.kit.core;

namespace wzebra.drawballs.core
{
    public class LevelSpawner : kit.core.LevelSpawner
    {
        [SerializeField] private BallsDrawer _drawer;

        private Level _currentLevel;

        protected override void AfterAwake()
        {
            
        }

        protected override void AfterStart()
        {
            
        }

        protected override void AfterSpawnLevel()
        {
            _currentLevel = GetCurrentLevel<Level>();

            foreach (var trigger in GetCurrentLevelGO().GetComponentsInChildren<BallsTrigger>())
            {
                trigger.SetDrawer(_drawer);
            }
        }

        public Level GetCurrentLevel() => _currentLevel;
    }
}