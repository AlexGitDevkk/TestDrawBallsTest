using System.Collections;

using UnityEngine;

using wzebra.kit.core;

#if REMOTE

namespace wzebra.kit.remote
{
    public class RemoteIntAction : MonoBehaviour
    {
        [SerializeField] private CustomLevelAction[] _customActions;

        [SerializeField] private string _variableName;

        private void Start()
        {
            StartCoroutine(CheckMaxLoad());
        }

        private IEnumerator CheckMaxLoad()
        {
            while (MaxSdk.IsInitialized() == false)
            {
                yield return new WaitForSeconds(0.1f);
            }

            int variable = RemoteVariable.GetInt(_variableName);

            foreach (var item in _customActions)
            {
                if (item.ID == variable)
                {
                    item.Action?.Invoke();
                }
            }
        }
    }
}
#endif