using UnityEngine;

using DG.Tweening;

using TMPro;

using Sirenix.OdinInspector;

using wzebra.kit.core;

namespace wzebra.kit.ui
{
    [RequireComponent(typeof(RectTransform))]
    public class CoinRenderer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        [SerializeField] private CoinCounter _counter;

        [SerializeField] private bool _smooth;

        [SerializeField, ShowIf(nameof(_smooth))] private float _smoothDuration;

        private int _pastCount;

        private RectTransform _rect;

        private bool _firstUpdate;

        private void Awake()
        {
            _counter.OnUpdate += OnUpdate;
        }

        private void Start()
        {
            _rect = GetComponent<RectTransform>();
        }

        private void OnUpdate(int count)
        {
            if (_smooth && _firstUpdate)
            {
                int currentCount = _pastCount;
                DOTween.To(() => currentCount, x => currentCount = x, count, _smoothDuration).OnUpdate(() => 
                {
                    _text.text = currentCount.ToString();
                }).SetEase(Ease.InOutSine);
            }
            else
            {
                _text.text = count.ToString();

                _firstUpdate = true;
            }

            _pastCount = count;
        }

        public RectTransform GetRect() => _rect;
    }
}