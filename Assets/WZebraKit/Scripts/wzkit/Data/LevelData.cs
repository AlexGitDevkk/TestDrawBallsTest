using UnityEngine;
using UltEvents;

using wzebra.kit.core;

namespace wzebra.kit.data
{
    [System.Serializable]
    public struct LevelData
    {
        [SerializeField] private float _winDelay;
        [SerializeField] private float _loseDelay;

        public UltEvent WinAction;
        public UltEvent LoseAction;
        public CustomLevelAction[] CustomActions;

#if MAXADS
        public CustomLevelAction[] _rewardActions;
#endif
        public float WinDelay => _winDelay;
        public float LoseDelay => _loseDelay;
    }
}