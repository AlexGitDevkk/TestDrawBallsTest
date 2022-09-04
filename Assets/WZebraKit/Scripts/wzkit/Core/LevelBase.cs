using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

using wzebra.kit.data;
using wzebra.kit.utils;

namespace wzebra.kit.core
{
    public abstract class LevelBase : MonoBehaviour
    {
        [SerializeField, TabGroup("General")] private LevelData _data;

        public event UnityAction<float> OnWinInvoke;
        public event UnityAction OnLoseInvoke;
        public event UnityAction<int> OnLoseVariantInvoke;
#if MAXADS
        public event UnityAction<int> OnReceiveReward;
#endif
        public event UnityAction<int> OnCustomEventInvoke;

        private LevelSpawner _spawner;

        public void SetSpawner(LevelSpawner spawner)
        {
            _spawner = spawner;

            SubscriveEvents();
        }

        private void SubscriveEvents()
        {
            _spawner.OnWin += WinSubscribe;

            _spawner.OnLose += LoseSubscribe;

            _spawner.CustomAction += CustomEventSubscribe;
        }

        private void OnDestroy()
        {
            _spawner.OnWin -= WinSubscribe;

            _spawner.OnLose -= LoseSubscribe;

            _spawner.CustomAction -= CustomEventSubscribe;
        }

        private void WinSubscribe(float success)
        {
            new DelayedAction(this, () =>
            {
                _data.WinAction?.Invoke();
                OnWinInvoke?.Invoke(success);
            }, _data.WinDelay);
        }

        private void LoseSubscribe()
        {
            new DelayedAction(this, () =>
            {
                _data.LoseAction?.Invoke();
                OnLoseInvoke?.Invoke();
            }, _data.LoseDelay);
        }

        private void CustomEventSubscribe(int id)
        {
            foreach (var action in _data.CustomActions)
            {
                if (action.ID == id)
                {
                    action.Action?.Invoke();
                }
            }
            OnCustomEventInvoke?.Invoke(id);
        }


        public void WinInvoke(float success = 1)
        {
            _spawner.WinInvoke(success);
        }

        public void LoseInvoke()
        {
            _spawner.LoseInvoke();
        }

        public void LoseVariantInvoke(int id)
        {
            _spawner.VariantLoseInvoke(id);
            OnLoseVariantInvoke?.Invoke(id);
        }

        public void CustomEventInvoke(int id)
        {
            _spawner.CustomActionInvoke(id);
        }

#if MAXADS
        public void ReceiveRewardActionInvoke(int index)
        {
            OnReceiveReward?.Invoke(index);

            foreach (var action in _data._rewardActions)
            {
                if(action.ID == index)
                {
                    action.Action?.Invoke();
                }
            }
        }
#endif

        public void AddCoins(int count, Transform target)
        {
            CoinCounter.Instance.AddCoinsWithAnimation(count, target);
        }
    }
}