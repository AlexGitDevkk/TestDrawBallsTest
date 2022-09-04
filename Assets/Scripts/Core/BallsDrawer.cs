using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using wzebra.kit.data;
using wzebra.kit.utils;
using wzebra.kit.core;

using wzebra.drawballs.data;

using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

namespace wzebra.drawballs.core
{
    public class BallsDrawer : MonoBehaviour
    {
        [SerializeField] private LayerMask _mask;

        [SerializeField] private GameObject _ball;

        [SerializeField, MinMaxSlider(0, 10, showFields: true)] private Vector2 _scaleSizeRange = Vector2.one;

        [SerializeField] private bool _holdOnTap;

        [SerializeField] private float _holdDuration;

        [SerializeField] private LevelSpawner _spawner;

        [SerializeField] private Puller _puller;

        public event UnityAction<GameColor> OnSpawnBall;
        public event UnityAction OnLostAllBalls;

        private Camera _camera;

        private float _maxRaycastDistance = 30;

        private List<Ball> _balls;

        private List<Ball> _holded;

        private GameColor _currentColor = GameColor.Red;

        private ColorCount[] _restColors;

        private void Start()
        {
            _camera = Camera.main;

            _balls = new List<Ball>();
            _holded = new List<Ball>();

            _spawner.OnPrepareToStartLevel += OnPrepareToStartLevel;
        }

        private void OnPrepareToStartLevel()
        {
            _restColors = _spawner.GetCurrentLevel().ColorsData;

            _puller.ReleaseAll();
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) 
                return;

            if (Input.GetMouseButton(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if(Physics.Raycast(ray, out hit, _maxRaycastDistance, _mask.value))
                {
                    if(hit.collider.gameObject.TryGetComponent(out DrawerPlane plane))
                    {
                        if (plane.HasColor(_currentColor) && CanSpawnBall(_currentColor))
                        {
                            OnTap(hit.point);
                        }
                    }
                }
            }

            if(Input.GetMouseButtonUp(0) && _holdOnTap)
            {
                StartCoroutine(ReleaseBalls());
            }
        }

        private void OnTap(Vector3 position)
        {
            float scale = Random.Range(_scaleSizeRange.x, _scaleSizeRange.y);

            for (int i = 0; i < _balls.Count; i++)
            {
                if (Vector3.Distance(position, _balls[i].transform.position) < (_balls[i].GetScale() / 2) + (scale / 2))
                {
                    return;
                }
            }

            Ball ball = SpawnBall(position, scale, _currentColor);

            _balls.Add(ball);
            _holded.Add(ball);

            RemoveColorCount();

            OnSpawnBall?.Invoke(_currentColor);
        }

        private void RemoveColorCount()
        {
            for (int i = 0; i < _restColors.Length; i++)
            {
                if (_restColors[i].Color == _currentColor)
                {
                    _restColors[i].Count--;

                    if (_restColors.Where(c => c.Count > 0).Count() == 0)
                    {
                        OnLostAllBalls?.Invoke();
                    }
                }
            }
        }

        public int GetRestCountColor(GameColor color) => _restColors.Where(c => c.Color == color).First().Count;

        private bool CanSpawnBall(GameColor color) => GetRestCountColor(color) > 0;

        public Ball SpawnBall(Vector3 position, float scale, GameColor color, bool needScale = true)
        {
            GameObject ballGO = _puller.GetObject();

            ballGO.transform.position = position;

            Ball ball = ballGO.GetComponent<Ball>();

            ball.SetScale(scale, needScale);
            ball.SetColor(color);

            ball.GetFreezer().Freeze();

            if (_holdOnTap == false)
            {
                new DelayedAction(this, () => { ball.GetFreezer().Unfreeze(); }, _holdDuration);
            }

            return ballGO.GetComponent<Ball>();
        }

        private IEnumerator ReleaseBalls()
        {
            for (int i = 0; i < _holded.Count; i++)
            {
                _holded[i].GetFreezer().Unfreeze();
                yield return new WaitForSeconds(_holdDuration);
            }

            _holded = new List<Ball>();
        }

        public void SetColor(GameColor color)
        {
            _currentColor = color;
        }

        public Puller GetPuller() => _puller;
    }
}