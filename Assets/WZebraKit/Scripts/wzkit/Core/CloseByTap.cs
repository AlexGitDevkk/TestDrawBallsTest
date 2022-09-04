using System.Collections;

using UnityEngine;

namespace wzebra.kit.core
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/CloseByTap")]
    public class CloseByTap : MonoBehaviour
    {
        [SerializeField] private float _minLifetime;

        private bool _canCloseTutorial;

        private void OnEnable()
        {
            _canCloseTutorial = false;
            StartCoroutine(LifetimeDelay());
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && _canCloseTutorial)
            {
                gameObject.SetActive(false);
            }
        }

        private IEnumerator LifetimeDelay()
        {
            yield return new WaitForSeconds(_minLifetime);
            _canCloseTutorial = true;
        }
    }
}