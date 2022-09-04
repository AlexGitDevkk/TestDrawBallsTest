using UnityEngine;

namespace wzebra.kit.utils
{
    [HelpURL("https://github.com/Ozick/WZebraKit/wiki/SpriteMaterialsSetter")]
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteMaterialsSetter : MonoBehaviour
    {
        [SerializeField] private Material[] _materials; 

        private void Start()
        {
            GetComponent<SpriteRenderer>().materials = _materials;
        }
    }
}