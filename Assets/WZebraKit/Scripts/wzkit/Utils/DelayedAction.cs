using System.Collections;

using UnityEngine;
using UnityEngine.Events;

namespace wzebra.kit.utils
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/DelayedAction")]
    public class DelayedAction
    {
        public DelayedAction(MonoBehaviour behaviour, UnityAction action, float delay)
        {
            behaviour.StartCoroutine(Delay(action, delay));
        }

        private IEnumerator Delay(UnityAction action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}