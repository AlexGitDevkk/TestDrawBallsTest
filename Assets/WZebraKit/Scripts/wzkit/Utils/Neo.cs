using UnityEngine;

namespace wzebra.kit.utils
{
    public class Neo : MonoBehaviour
    {
        [Range(0.01f, 10)]
        [SerializeField] private float _time;

        [SerializeField] private KeyCode _key;

        private void Update()
        {
            if (Input.GetKeyDown(_key))
            {
                Time.timeScale = _time;
            }
            if (Input.GetKeyUp(_key))
            {
                Time.timeScale = 1;
            }
        }
    }
}