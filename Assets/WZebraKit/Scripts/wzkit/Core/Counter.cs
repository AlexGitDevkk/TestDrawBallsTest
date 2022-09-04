using UnityEngine;
using UnityEngine.Events;

using UltEvents;

namespace wzebra.kit.core
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/Counter")]
    public class Counter : MonoBehaviour
    {
        [SerializeField] private int _targetCount;

        [Tooltip("Выполнять Action только один раз при достижении значения (false - если каждый последующий инкремент)")]
        [SerializeField] private bool _accuracy = true;

        [SerializeField] private bool _toZero;

        public UltEvent Action;

        public event UnityAction<int> OnCountUpdate;

        private int _currentCount;

        private void Start()
        {
            if (_toZero)
            {
                _currentCount = _targetCount;
            }

            OnCountUpdate?.Invoke(_currentCount);
        }

        public void Increment()
        {
            _currentCount++;

            Check();
        }

        public void Decrement()
        {
            _currentCount--;

            Check();
        }

        public void Tick()
        {
            if (_toZero)
            {
                Decrement();
            }
            else
            {
                Increment();
            }
        }

        public void Tick(int count)
        {
            if (_toZero)
            {
                Remove(count);
            }
            else
            {
                Add(count);
            }
        }

        public void Add(int count)
        {
            _currentCount += count;

            Check();
        }

        public void Remove(int count)
        {
            _currentCount -= count;

            Check();
        }

        public void Reset()
        {
            if (_toZero) 
            {
                Set(_targetCount);
            }
            else
            {
                Set(0);
            }
        }

        public void Set(int count)
        {
            _currentCount = count;

            Check();
        }

        private void Check()
        {
            OnCountUpdate?.Invoke(_currentCount);

            if (_accuracy)
            {
                if ((_currentCount == _targetCount && _toZero == false) || (_currentCount == 0 && _toZero))
                {
                    Action?.Invoke();
                }
            }
            else
            {
                if ((_toZero && _currentCount <= 0) || (_toZero == false && _currentCount >= _targetCount))
                {
                    Action?.Invoke();
                }
            }
        }

        public int GetInitCount() => _targetCount;

        public int GetTargetCount()
        {
            if (_toZero)
            {
                return 0;
            }
            else
            {
                return _targetCount;
            }
        }

        public int GetCurrentCount() => _currentCount;
    }
}