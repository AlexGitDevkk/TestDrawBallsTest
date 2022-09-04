using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

using wzebra.kit.utils;
using wzebra.kit.ui;

namespace wzebra.kit.core
{
    public class CoinCounter : MonoBehaviour
    {
        [SerializeField] private int _startCount = 0;

        [SerializeField] private CoinsAnimator _animator;

        [SerializeField, FoldoutGroup("Events")] private UltEvents.UltEvent Update, Add, Remove, Out;

        private static CoinCounter _instance;

        public static CoinCounter Instance => _instance;

        public event UnityAction<int> OnUpdate, OnAdd, OnRemove;
        public event UnityAction OnOut;

        private int _currentCount;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            OnUpdate += (int count) => { Update?.Invoke(); };
            OnAdd += (int count) => { Add?.Invoke(); };
            OnRemove += (int count) => { Remove?.Invoke(); };
            OnOut += () => { OnOut?.Invoke(); };

            _currentCount = CoinsSaver.GetCount(_startCount);

            UpdateCount();
        }

        public void AddCoins(int count)
        {
            OnAdd?.Invoke(count);

            _currentCount += count;

            UpdateCount();
        }

        public void RemoveCoins(int count)
        {
            OnRemove?.Invoke(count);

            _currentCount -= count;

            if(_currentCount < 0)
            {
                _currentCount = 0;
                OnOut?.Invoke();
            }

            UpdateCount();
        }

        public void AddCoinsWithAnimation(int count, Transform target)
        {
            _animator.AddCoins(count, target).onComplete += () => { AddCoins(count); };
        }

        private void UpdateCount()
        {
            CoinsSaver.SetCount(_currentCount);
            OnUpdate?.Invoke(_currentCount);
        }

        public int GetCount() => _currentCount;

        [Button("Add 100 coins")]
        public void Add100Coins()
        {
            AddCoins(100);
        }

        [Button("Remove 100 coins")]
        public void Remove100Coins()
        {
            RemoveCoins(100);
        }
    }
}