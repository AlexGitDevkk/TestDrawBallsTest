using UnityEngine;

using UltEvents;

using wzebra.kit.data;

namespace wzebra.kit.core
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/LevelSpawnerAction")]
    public class LevelSpawnerAction : MonoBehaviour
    {
        [SerializeField] private LevelSpawner _spawner;

        public UltEvent OnStartLevel;
        public UltEvent OnEndLevel;
        public UltEvent<float> OnWin;
        public UltEvent OnLose;
        public CustomLevelAction[] CustomActions;
        public CustomLevelAction[] VariantLoseActions;

        [SerializeField] private WinAction[] _winActions;

        private void Awake()
        {
            _spawner.OnStartLevel += () => { OnStartLevel?.Invoke(); };
            _spawner.OnEndLevel += () => { OnEndLevel?.Invoke(); };
            _spawner.OnLose += () => { OnLose?.Invoke(); };

            _spawner.OnWin += (float success) => 
            { 
                OnWin?.Invoke(success);

                for (int i = 0; i < _winActions.Length; i++)
                {
                    int index = i;

                    if(_winActions[index].Success <= success)
                    {
                        _winActions[index].Action?.Invoke();
                    }
                }
            };

            _spawner.CustomAction += (int id) => 
            {
                for (int i = 0; i < CustomActions.Length; i++)
                {
                    if(CustomActions[i].ID == id)
                    {
                        CustomActions[i].Action?.Invoke();
                    }
                }
            };

            _spawner.VariantLose += (int id) =>
            {
                for (int i = 0; i < VariantLoseActions.Length; i++)
                {
                    if (VariantLoseActions[i].ID == id)
                    {
                        VariantLoseActions[i].Action?.Invoke();
                    }
                }
            };
        }
    }
}