using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

using wzebra.kit.core;

namespace wzebra.kit.ui
{
    public class EnergyRenderer : MonoBehaviour
    {
        [SerializeField] private EnergyCounter _energy;
        [SerializeField] private Image[] _images;

        private float _bounceAmplitude = 1.2f, _bounceDuration = 0.15f;

        private void Start()
        {
            if(_images.Length != _energy.GetMaxCount())
            {
                throw new System.ArgumentOutOfRangeException("Energy images count don't equal max energy count.");
            }

            _energy.OnCounterUpdate += OnCounterUpdate;
            _energy.OnTimerUpdate += OnTimerUpdate;
            _energy.OnRegeneratedOne += OnRegeneratedOne;
        }

        private void OnRegeneratedOne()
        {
            _images[_energy.GetCurrentCount()].transform.DOScale(_bounceAmplitude, _bounceDuration).SetLoops(2, LoopType.Yoyo);
        }

        private void OnTimerUpdate(float value)
        {
            _images[_energy.GetCurrentCount()].fillAmount = value;
        }

        private void OnCounterUpdate(int count)
        {
            for (int i = 0; i < _images.Length; i++)
            {
                _images[i].fillAmount = i < count ? 1 : 0;
            }
        }
    }
}