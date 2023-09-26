using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.UI.LevelSlider
{
    public class LevelSliderView : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Slider _slider;
        [SerializeField] private float _fillDuration = 1f;

        private bool _isFilled;
        private bool _isChanging;

        public void SetMaxValue(int enemyCount) =>
            _slider.maxValue = enemyCount;

        public void Enable(int i) =>
            gameObject.SetActive(true);

        public void Disable() =>
            gameObject.SetActive(false);

        public async void Decrease(int value)
        {
            while (_isChanging)
                await UniTask.Yield();

            _isChanging = true;

            while (!_isFilled)
            {
                await UniTask.Yield();
            }

            float targetValue = _slider.value - value;

            await ChangeSliderValue(targetValue);

            _isChanging = false;
            HideIfSliderNull(_slider.value);
        }

        private async UniTask ChangeSliderValue(float targetValue)
        {
            while (Mathf.Abs(_slider.value - targetValue) > 0.01)
            {
                _slider.value = Mathf.Lerp(_slider.value, targetValue, 7 * Time.deltaTime);

                await UniTask.DelayFrame(1);
            }

            _slider.value = targetValue;
        }

        public void SetValue(int value) =>
            _slider.DOValue(value, _fillDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => _isFilled = true);

        private async void HideIfSliderNull(float targetValue)
        {
            if (targetValue != 0)
                return;

            await UniTask.WaitForSeconds(0.5f);

            transform
                .DOScaleX(0, 1f)
                .SetEase(Ease.InQuint)
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}