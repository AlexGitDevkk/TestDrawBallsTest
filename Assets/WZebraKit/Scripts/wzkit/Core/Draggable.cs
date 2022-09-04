using UnityEngine;

using UltEvents;

namespace wzebra.kit.core
{
    [System.Serializable]
    public class MouseEvents
    {
        public UltEvent OnMouseEnter, OnMouseExit, OnMouseDown, OnMouseUp;
    }

    [RequireComponent(typeof(Collider2D))]
    public class Draggable : MonoBehaviour
    {
        [Range(0, 0.999f)]
        [SerializeField] private float _interpolate;

        [SerializeField] private MouseEvents _events;

        private Camera _camera;

        private bool _isDragged;

        private Vector3 _offset;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_isDragged)
            {
                Vector3 position = _camera.ScreenToWorldPoint(Input.mousePosition);

                position.z = transform.position.z;

                transform.position = Vector3.Lerp(transform.position, position + _offset, 1 - _interpolate);
            }
        }

        private void OnMouseEnter()
        {
            _events.OnMouseEnter?.Invoke();
        }

        private void OnMouseExit()
        {
            _events.OnMouseExit?.Invoke();
        }

        private void OnMouseDown()
        {
            _isDragged = true;
            _events.OnMouseDown?.Invoke();

            _offset = transform.position - _camera.ScreenToWorldPoint(Input.mousePosition);
            _offset.z = 0;
        }

        private void OnMouseUp()
        {
            _isDragged = false;
            _events.OnMouseUp?.Invoke();
        }
    }
}