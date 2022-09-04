using UnityEngine;

namespace wzebra.kit.utils
{
    [RequireComponent(typeof(MeshCollider))]
    public class MeshColliderSetter : MonoBehaviour
    {
        [SerializeField] private MeshFilter _filter;

        private void Start()
        {
            GetComponent<MeshCollider>().sharedMesh = _filter.mesh;
        }
    }
}