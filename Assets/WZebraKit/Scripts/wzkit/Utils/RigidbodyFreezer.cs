using UnityEngine;

using Sirenix.OdinInspector;

namespace wzebra.kit.utils
{
    public class RigidbodyFreezer : MonoBehaviour
    {
        [SerializeField, Required] private Rigidbody _rigidbody;

        [SerializeField] private RigidbodyConstraints _freezeState, _unfreezeState;

        public void Freeze()
        {
            _rigidbody.constraints = _freezeState;
        }

        public void Unfreeze()
        {
            _rigidbody.constraints = _unfreezeState;
        }

        public Rigidbody GetRigidbody() => _rigidbody;
    }
}