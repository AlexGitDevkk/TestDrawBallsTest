using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

using wzebra.kit.tweener;
using wzebra.kit.data;
using wzebra.kit.utils;

namespace wzebra.drawballs.core
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
        [SerializeField, Required] private Bouncer _bouncer;

        [SerializeField, Required] private RigidbodyFreezer _freezer;

        [SerializeField, Required] private MeshRenderer _renderer;

        public event UnityAction OnBucketEnter;

        private float _scale;

        private GameColor _color;

        public void OnBucketEnterInvoke()
        {
            OnBucketEnter?.Invoke();
        }

        public void SetScale(float scale, bool ignoreStartBounce = false)
        {
            _scale = scale;

            transform.localScale = Vector3.one * _scale;

            _bouncer.Bounce(ignoreStartBounce);
        }

        public void SetColor(GameColor color)
        {
            _color = color;
            _renderer.material.SetColor("_BaseColor", ColorPallete.Instance.GetColor(color));
        }

        public float GetScale() => _scale;

        public RigidbodyFreezer GetFreezer() => _freezer;

        public GameColor GetColor() => _color;
    }
}