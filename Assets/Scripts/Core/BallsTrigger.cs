using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

using UltEvents;

using wzebra.kit.core;
using wzebra.kit.data;
using wzebra.kit.utils;

using wzebra.drawballs.data;

namespace wzebra.drawballs.core
{
    [RequireComponent(typeof(TriggerAction))]
    public class BallsTrigger : MonoBehaviour
    {
        [InfoBox("Count can't equals", VisibleIf = "@_color.Count == 0", InfoMessageType = InfoMessageType.Error)]
        [SerializeField] protected ColorCount _color;

        [SerializeField] protected bool _repeat;

        [SerializeField] protected bool _counting = true;

        [SerializeField, FoldoutGroup("Events")] protected UltEvent _action, _onCountUpdate;

        [SerializeField] protected Renderer _rendererToColor;

        public event UnityAction<Ball> OnBallCollide;
        public event UnityAction OnAction;
        public event UnityAction<int> OnCountUpdate;

        protected TriggerAction _trigger;

        protected int _currentCount;

        protected BallsDrawer _drawer;

        protected List<Ball> _created;

        private void Start()
        {
            _trigger = GetComponent<TriggerAction>();

            _created = new List<Ball>();

            _trigger.onCollisionEnter += OnObjectCollide;
            _trigger.onTriggerEnter += OnObjectCollide;

            SetCurrentCount(_color.Count);

            OnAction += () => { _action?.Invoke(); };
            OnCountUpdate += (int count) => { _onCountUpdate?.Invoke(); };

            if (_rendererToColor != null)
            {
                _rendererToColor.material.SetColor("_BaseColor", ColorPallete.Instance.GetColor(_color.Color));
            }

            AfterStart();

            SetCurrentCount(_currentCount);
        }

        protected virtual void AfterStart() { }

        private void OnObjectCollide(GameObject go)
        {
            if(go.TryGetComponent(out Ball ball))
            {
                if(ball.GetColor() == _color.Color || _color.Color == GameColor.Any)
                {
                    if (_created.Contains(ball))
                    {
                        return;
                    }

                    AddToCreatedList(ball);

                    OnBallCollide?.Invoke(ball);
                    CollideTick();
                }
            }
        }

        protected void AddToCreatedList(Ball ball)
        {
            _created.Add(ball);

            if (ball.TryGetComponent(out BehaviourEvents events))
            {
                events.onDisable += () => { _created.Remove(ball); };
            }
        }

        protected void CollideTick()
        {
            if(_counting == false)
            {
                return;
            }

            _currentCount--;

            if(_currentCount < 0)
            {
                _currentCount = 0;
            }

            OnCountUpdate?.Invoke(_currentCount);

            if (_currentCount == 0)
            {
                OnAction?.Invoke();

                if (_repeat)
                {
                    SetCurrentCount(_color.Count);
                }
            }
        }

        protected void SetCurrentCount(int count)
        {
            _currentCount = count;
            OnCountUpdate?.Invoke(_currentCount);
        }

        public int GetCurrentCount() => _currentCount;

        public void SetDrawer(BallsDrawer drawer)
        {
            _drawer = drawer;
        }
    }
}