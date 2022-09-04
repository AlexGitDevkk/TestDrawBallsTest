using UnityEngine;

using Sirenix.OdinInspector;

namespace wzebra.kit.utils
{
    public class MeshSetter : MonoBehaviour
    {
        [SerializeField] private Mesh _mesh;

        [SerializeField, InfoBox("Optional")] private Material _material;

        [SerializeField] private bool _childs;

        [SerializeField, HideIf(nameof(_childs))] private MeshFilter[] _meshFilters;

        [SerializeField] private bool _setOnStart;

        private void Start()
        {
            if (_setOnStart)
            {
                SetMeshes();
            }
        }

        public void SetMesh(Mesh mesh)
        {
            _mesh = mesh;
        }

        public void SetMaterial(Material material)
        {
            _material = material;
        }

        [Button("Set meshes")]
        public void SetMeshes()
        {
            MeshFilter[] filters = _childs ? GetComponentsInChildren<MeshFilter>(true) : _meshFilters;

            for (int i = 0; i < filters.Length; i++)
            {
                filters[i].mesh = _mesh;

                if(_material != null)
                {
                    filters[i].GetComponent<MeshRenderer>().material = _material;
                }
            }
        }
    }
}