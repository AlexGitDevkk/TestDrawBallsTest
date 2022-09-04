using UnityEngine;

using TMPro;

using DG.Tweening;

using wzebra.kit.core;

namespace wzebra.kit.ui
{
    public class CoinsAnimator : MonoBehaviour
    {
        [SerializeField] private CoinRenderer _renderer;

        [SerializeField] private GameObject _prefab;

        [SerializeField] private RectTransform _canvas;

        [SerializeField] private float _curveAmplitude = 2.5f, _jumpDuration = 0.7f, _waitDuration = 0.7f;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        public Tween AddCoins(int count, Transform target)
        {
            GameObject go = Instantiate(_prefab, transform);

            CoinElement coin = go.GetComponent<CoinElement>();
            coin.SetCount(count);

            Vector2 viewPort = _camera.WorldToViewportPoint(target.position);

            go.GetComponent<RectTransform>().anchoredPosition = (viewPort * _canvas.sizeDelta) - (_canvas.sizeDelta * 0.5f);

            Tween animation = go.GetComponent<RectTransform>().DOJump(_renderer.GetRect().position, _curveAmplitude, 1, _jumpDuration).SetEase(Ease.InOutSine).OnStart(coin.StartJump);

            Sequence seq = DOTween.Sequence();

            seq.AppendInterval(_waitDuration);
            seq.Append(animation);
            seq.AppendCallback(() => 
            {
                coin.EndJump();
            });

            seq.Play();

            return animation;
        }

        public void AddCoins(int count, RectTransform target)
        {

        }
    }
}