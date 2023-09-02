using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI.LevelSlider
{
    public class LevelSliderView : MonoBehaviour
    {
        private const int LevelFinish = 1;
        
        [SerializeField] private UnityEngine.UI.Slider _slider;
        [SerializeField] private float _decreaseDuration = 0.5f;
        [SerializeField] private float _fillDuration = 1f;

        private bool _isFilled;

        public void SetMaxValue(int enemyCount) => 
        _slider.maxValue = enemyCount;

        public async void Decrease(int value)
        {
            while (!_isFilled)
            {
                await UniTask.Yield();
            }

            var targetValue = _slider.value - value;

            _slider.DOValue(targetValue, _decreaseDuration).SetEase(Ease.InQuint);
            
            HideIfSliderNull(targetValue);
        }

        public void SetValue(int value) =>
            _slider.DOValue(value, _fillDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => _isFilled = true);

        private async void HideIfSliderNull(float targetValue)
        {
            if (targetValue != 0)
                return;
            
            await UniTask.WaitForSeconds(1.5f);
            transform
                .DOScaleX(0, 1f)
                .SetEase(Ease.InQuint)
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}