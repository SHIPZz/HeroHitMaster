using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
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
        private bool _changed = false;
        private bool _isChanging;
        private bool _isChangingValue;
        private Coroutine _changeSliderCoroutine;

        public void SetMaxValue(int enemyCount) =>
            _slider.maxValue = enemyCount;

        [Button]
        public async void Decrease(int value)
        {
            while (!_isFilled)
            {
                await UniTask.Yield();
            }

            var targetValue = _slider.value - value;

            if (_changeSliderCoroutine != null)
                StopCoroutine(_changeSliderCoroutine);

            _changeSliderCoroutine = StartCoroutine(ChangeSliderValueCoroutine(targetValue));
        }

        private IEnumerator ChangeSliderValueCoroutine(float targetValue)
        {
            while (Mathf.Abs(_slider.value - targetValue) > 0.01)
            {
                _slider.value = Mathf.Lerp(_slider.value, targetValue, 7 * Time.deltaTime);

                yield return null;
            }

            _slider.value = targetValue;

            if (_slider.value == 0)
                transform
                    .DOScaleX(0, 1f)
                    .SetEase(Ease.InQuint)
                    .OnComplete(() => gameObject.SetActive(false));
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
        }
    }
}