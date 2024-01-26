using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        private const float CloseDuration = 2f;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Slider _loadingSlider;
        
        private Canvas _canvas;
        private Tween _tween;

        public event Action Closed;

        private void Awake()
        {
            _canvas = _canvasGroup.GetComponent<Canvas>();
            DontDestroyOnLoad(this);
        }

        private void OnDisable() =>
            _loadingSlider.value = 0;

        public void Show(float loadSliderDuration)
        {
            _loadingSlider.value = 0;
            _canvas.enabled = true;
            _tween = _loadingSlider.DOValue(_loadingSlider.maxValue, loadSliderDuration).SetUpdate(true);
            _canvasGroup.alpha = 1;
        }

        public async void Hide(Action callback = null)
        {
            while (Mathf.Approximately(_loadingSlider.value, _loadingSlider.maxValue) == false)
                await UniTask.Yield();

            _tween = _canvasGroup
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