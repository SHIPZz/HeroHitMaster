using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        private const float CloseDuration = 1f;
        private const float SliderValueDuration = 1f;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Slider _loadingSlider;

        private Tweener _tweener;

        public event Action Closed;

        private void Awake() =>
            DontDestroyOnLoad(this);

        public void Show(float loadSliderDuration)
        {
            if (_loadingSlider.value != 0)
            {
                _loadingSlider.value = 0;
            }

            _canvasGroup.gameObject.SetActive(true);
            _loadingSlider.DOValue(_loadingSlider.maxValue, loadSliderDuration);
            _canvasGroup.alpha = 1;
        }

        public async void Hide()
        {
            while (Mathf.Approximately(_loadingSlider.value, _loadingSlider.maxValue) == false)
                await UniTask.Yield();
            
            _canvasGroup
                .DOFade(0, CloseDuration)
                .OnComplete(() =>
                {
                    _canvasGroup.gameObject.SetActive(false);
                    Closed?.Invoke();
                });
        }
    }
}