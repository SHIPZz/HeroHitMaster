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

        public event Action Closed;

        private void Awake() =>
            DontDestroyOnLoad(this);

        private void OnDisable() =>
            _loadingSlider.value = 0;

        public void Show(float loadSliderDuration)
        {
            if (_loadingSlider.value != 0) 
                _loadingSlider.value = 0;

            _canvasGroup.gameObject.SetActive(true);
            _loadingSlider.DOValue(_loadingSlider.maxValue, loadSliderDuration).SetUpdate(true);
            _canvasGroup.alpha = 1;
        }

        public async void Hide()
        {
            while (Mathf.Approximately(_loadingSlider.value, _loadingSlider.maxValue) == false)
                await UniTask.Yield();

            _canvasGroup
                .DOFade(0, CloseDuration).SetUpdate(true)
                .OnComplete(() =>
                {
                    _canvasGroup.GetComponent<Canvas>().enabled = false;
                    Closed?.Invoke();
                });
        }
    }
}