using System.Linq;

using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

using wzebra.kit.tweener;
using wzebra.kit.data;
#if MAXADS
using wzebra.kit.ads;
#endif

namespace wzebra.kit.core
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/LevelSpawner")]
    [RequireComponent(typeof(LevelCounter))]
    public abstract class LevelSpawner : MonoBehaviour
    {
        [SerializeField, TabGroup("General"), Searchable(FilterOptions = SearchFilterOptions.ValueToString), TableList(ShowIndexLabels = true, DrawScrollView = true)] private LevelGO[] _levels;

        [SerializeField, TabGroup("General")] private bool _autoReload;

        [SerializeField, TabGroup("General")] protected LevelTransitionType _transition = LevelTransitionType.None;

        [SerializeField, TabGroup("General"), ShowIf("@_transition==LevelTransitionType.Fader", animate: true)] private ImageFader _fader;

        [SerializeField, TabGroup("General"), ShowIf("@_transition==LevelTransitionType.Scaling", animate: true), Range(1, 30)] private float _newLevelScale = 10;

        [SerializeField, TabGroup("General"), ShowIf("@_transition==LevelTransitionType.Screen", animate: true)] private ui.TransitionScreen _screen;

#if MAXADS
        [SerializeField, TabGroup("General", order: 1)] private MaxAds _ads;
#endif

        private LevelCounter _counter;

        public event UnityAction OnPrepareToStartLevel;
        public event UnityAction OnStartLevel;
        public event UnityAction OnRestartLevel;
        public event UnityAction OnEndLevel;
        public event UnityAction<float> OnWin;
        public event UnityAction OnLose;
        public event UnityAction<int> CustomAction;
        public event UnityAction<int> VariantLose;

        private GameObject _currentLevelGO;

        private int _pastLevel = -1;

        private void Awake()
        {
            OnWin += (float success) => { OnEndLevel?.Invoke(); };
            OnLose += () => { OnEndLevel?.Invoke(); };
            VariantLose += (int id) => { OnLose?.Invoke(); };

            AfterAwake();
        }

        private void Start()
        {
            _counter = GetComponent<LevelCounter>();

#if UNITY_EDITOR
            int startFrom = PlayerPrefs.GetInt("StartFrom", -1);

            if(startFrom != -1)
            {
                PlayerPrefs.DeleteKey("StartFrom");
                PlayerPrefs.Save();

                for (int i = 0; i < _levels.Length; i++)
                {
                    if(_levels[i].Level.transform.GetInstanceID() == startFrom)
                    {
                        _counter.SetLevel(i, false);
                    }
                }
            }
#endif
            if (_transition == LevelTransitionType.Fader)
            {
                _fader.OnEndOutFading += () => 
                {
                    SpawnLevelAction();
                    _fader.FadeIn();
                };
            }

            AfterStart();

            SpawnLevelAction();
        }
        protected virtual void AfterAwake() { }

        protected virtual void AfterStart() { }

        public void SpawnLevel()
        {
            switch (_transition)
            {
                case LevelTransitionType.None:
                    SpawnLevelAction();
                    break;
                case LevelTransitionType.Fader:
                    _fader.FadeOut();
                    break;
                case LevelTransitionType.Scaling:
                    if(_currentLevelGO == null)
                    {
                        SpawnLevelAction();
                    }
                    else if(_currentLevelGO.TryGetComponent(out ScaleFader fader))
                    {
                        GameObject level = _currentLevelGO;
                        fader.FadeOut().onComplete += () => { DestoyLevel(level); };

                        SpawnLevelAction();
                    }

                    break;
                case LevelTransitionType.Screen:
                    // Soon...
                    break;
            }
        }

        private void SpawnLevelAction()
        {
            int levelIndex = _counter.GetActualLevel();

            if(_transition != LevelTransitionType.Scaling && _currentLevelGO != null)
            {
                DestroyCurrentLevel();
            }

            GameObject go = Instantiate(_levels[levelIndex].Level, transform);

            if(_transition == LevelTransitionType.Scaling && go.TryGetComponent(out ScaleFader fader))
            {
                fader.ChangeState(_newLevelScale);
                fader.FadeIn();
            }

            go.name = _levels[levelIndex].Level.name;

            _currentLevelGO = go;

            if(go.TryGetComponent(out LevelBase level))
            {
                level.SetSpawner(this);
#if MAXADS
                _ads.OnReceiveReward += level.ReceiveRewardActionInvoke;
                level.OnWinInvoke += (float success) => { _ads.OnReceiveReward -= level.ReceiveRewardActionInvoke; };
                level.OnLoseInvoke += () => { _ads.OnReceiveReward -= level.ReceiveRewardActionInvoke; };
#endif
            }

            AfterSpawnLevel();

            OnPrepareToStartLevel?.Invoke();

            OnStartLevel?.Invoke();

            if (levelIndex == _pastLevel)
            {
                OnRestartLevel?.Invoke();
            }

            _pastLevel = levelIndex;
        }

        protected virtual void AfterSpawnLevel() { }

        public void LoseInvoke()
        {
            OnLose?.Invoke();

            if (_autoReload)
            {
                SpawnLevel();
            }
        }

        public void VariantLoseInvoke(int id)
        {
            VariantLose?.Invoke(id);
        }

        public void WinInvoke(float success)
        {
            if (success < 0 || success > 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            OnWin?.Invoke(success);
        }

        public void WinInvoke()
        {
            WinInvoke(1);
        }

        /// <summary>
        /// Не использовать! Только для тестов
        /// </summary>
        public void WinInvokeForTesters()
        {
            OnWin?.Invoke(1);
        }

        public void CustomActionInvoke(int id)
        {
            CustomAction?.Invoke(id);
        }

        private void DestroyCurrentLevel()
        {
            DestoyLevel(_currentLevelGO);
        }

        private void DestoyLevel(GameObject level)
        {
            DestroyImmediate(level);
        }

        public GameObject[] GetLevels() => _levels.Select(l => l.Level).ToArray();

        public int GetLevelCount() => GetLevels().Length;

        public GameObject GetCurrentLevelGO() => _currentLevelGO;
        
        public T GetCurrentLevel<T>()
        {
            return _currentLevelGO.GetComponent<T>();
        }
    }
}