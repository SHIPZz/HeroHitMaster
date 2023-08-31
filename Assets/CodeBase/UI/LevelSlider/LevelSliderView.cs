using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI.LevelSlider
{
    public class LevelSliderView : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Slider _slider;
        [SerializeField] private float _decreaseDuration = 0.5f;
        [SerializeField] private float _fillDuration = 1f;
        
        private bool _isFilled;

        public void SetMaxValue(int value) =>
            _slider.maxValue = value;

        public async void Decrease(int value)
        {
            while (!_isFilled)
            {
                await UniTask.Yield();
            }
            
            _slider.DOValue(_slider.value - value, _decreaseDuration).SetEase(Ease.InQuint);
        }

        public void SetValue(int value) =>
            _slider.DOValue(value, _fillDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => _isFilled = true);
    }
}