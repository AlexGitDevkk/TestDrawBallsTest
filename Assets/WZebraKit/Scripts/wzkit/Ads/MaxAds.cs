using System.Collections;

using UnityEngine;
using UnityEngine.Events;

using UltEvents;

using wzebra.kit.core;

#if MAXADS
using LionStudios.Suite.Ads;
using LionStudios.Suite.Debugging;

namespace wzebra.kit.ads
{
    public class MaxAds : MonoBehaviour
    {
        [SerializeField] private string[] _placements;

        [SerializeField] private LevelSpawner _spawner;

        [SerializeField] private LevelCounter _counter;

        [SerializeField] private UltEvent OnNeedReward, OnNoNeedReward;

        [SerializeField] private CustomLevelAction[] _rewardActions;

        public event UnityAction<int> OnReceiveReward;

        private LionMaxSdk _provider;

        private int _currentPlacementIndex;

        private bool _needOnce;

        private const string _key = "reward";

        private void Awake()
        {
            _spawner.OnStartLevel += () => 
            {
                (NeedRewardVideo() ? OnNeedReward : OnNoNeedReward)?.Invoke();
            };
        }

        private void Start()
        {
            StartCoroutine(CheckMaxLoad());
        }

        private IEnumerator CheckMaxLoad()
        {
            while(MaxSdk.IsInitialized() == false) 
            {
                yield return new WaitForSeconds(0.1f);
            }

            _provider = new LionMaxSdk();

            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += (string arg1, MaxSdkBase.Reward arg2, MaxSdkBase.AdInfo arg3) => 
            {
                if (_needOnce)
                {
                    PlayerPrefs.SetInt(_key, _counter.GetShowingLevel());
                    PlayerPrefs.Save();

                    _needOnce = false;
                }

                OnReceiveReward?.Invoke(_currentPlacementIndex);

                foreach (var action in _rewardActions)
                {
                    if(action.ID == _currentPlacementIndex)
                    {
                        action.Action?.Invoke();
                    }
                }
            };

            LionDebugger.Hide();

            LoadAdd();
            Debug.Log("Called LoadAds");
        }
        
        public void LoadAdd()
        {
            _provider.LoadAds(LionAdTypeFlag.All);
        }

        public void ShowReward(int index)
        {
            Show(LionAdType.Rewarded, index);

            _currentPlacementIndex = index;
        }

        public void ShowBanner(int index)
        {
            Show(LionAdType.Banner, index);

            _currentPlacementIndex = index;
        }

        public void ShowInterstitial(int index)
        {
            Show(LionAdType.Interstitial, index);

            _currentPlacementIndex = index;
        }

        public string GetPlacementName(int index) => _placements[index];

        public void Show(LionAdType type, int index)
        {
            if (_provider.IsAdReady(type))
            {
                Debug.Log("Ad is ready");
                //LionAds.ShowInterstitial<LionMaxSdk>(_placement);
                _provider.ShowAd(type, _placements[index]);
            }
            else
            {
                Debug.Log("Ad not ready");
            }
        }

        public void ShowRewardOnce(int index)
        {
            if(NeedRewardVideo())
            {
                ShowReward(index);
                _needOnce = true;
            }
            else
            {
                OnReceiveReward?.Invoke(index);
            }
        }

        private bool NeedRewardVideo() => PlayerPrefs.GetInt(_key, -1) != _counter.GetShowingLevel();

        public void HideBanner()
        {
            LionAds.HideBanner<LionMaxSdk>(_placements[_currentPlacementIndex]);
        }
    }
}
#endif