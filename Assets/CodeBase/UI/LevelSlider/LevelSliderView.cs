using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI.LevelSlider
{
    public class LevelSliderView : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Slider _slider;
        [SerializeField] private float _fillDuration = 1f;

        private bool _isChanging;

        public void SetMaxValue(int enemyCount) =>
            _slider.maxValue = enemyCount;

        public void Enable(int i) =>
            gameObject.SetActive(true);

        public void Disable() =>
            gameObject.SetActive(false);

        public async void Increase(int value)
        {
            while (_isChanging)
                await UniTask.Yield();

            _isChanging = true;

            float targetValue = _slider.value + value;

            await _slider.DOValue(targetValue, 1f).AsyncWaitForCompletion();

            _isChanging = false;
            
            if(_slider.value == _slider.maxValue)
                transform
                    .DOScaleX(0, 1f)
                    .SetEase(Ease.InQuint)
                    .OnComplete(() => gameObject.SetActive(false));
        }
    }
}