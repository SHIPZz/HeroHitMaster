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

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Slider _loadingSlider;

        private Canvas _canvas;

        public event Action Closed;

        private void Awake()
        {
            _canvas = _canvasGroup.GetComponent<Canvas>();
            DontDestroyOnLoad(this);
        }

        private void OnDisable() =>
            _loadingSlider.value = 0;

        public void FillHalf(float fillDuration)
        {
            _loadingSlider.value = 0;
            _canvas.enabled = true;
            _loadingSlider.DOValue(_loadingSlider.maxValue / 2, fillDuration).SetUpdate(true);
            _canvasGroup.alpha = 1;
        }

        public void Show(float loadSliderDuration)
        {
            _canvasGroup.alpha = 1;
            _canvas.enabled = true;
            _loadingSlider.DOValue(_loadingSlider.maxValue, loadSliderDuration).SetUpdate(true);
        }

        public async void Hide(Action callback = null)
        {
            while (Mathf.Approximately(_loadingSlider.value, _loadingSlider.maxValue) == false)
                await UniTask.Yield();

            _canvasGroup
                .DOFade(0, CloseDuration)
                .OnComplete(() =>
                {
                    _canvas.enabled = false;
                    callback?.Invoke();
                    Closed?.Invoke();
                }).SetUpdate(true);
        }
    }
}