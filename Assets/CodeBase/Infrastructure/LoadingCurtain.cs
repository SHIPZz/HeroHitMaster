﻿using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        private const float CloseDuration = 1.5f;
        private const float SliderValueDuration = 1f;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Slider _loadingSlider;

        public event Action Closed;
        
        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Show()
        {
            _canvasGroup.gameObject.SetActive(true);
            _loadingSlider.DOValue(_loadingSlider.maxValue, SliderValueDuration).SetEase(Ease.InQuint)
                .OnComplete(() => Hide(null));
            _canvasGroup.alpha = 1;
        }

        public async void Hide(Action callback)
        {
            while (_loadingSlider.value != _loadingSlider.maxValue)
                await UniTask.Yield();

            _canvasGroup
                .DOFade(0, CloseDuration)
                .OnComplete(() =>
                {
                    _canvasGroup.gameObject.SetActive(false);
                    Closed?.Invoke();
                    callback?.Invoke();
                }).SetAutoKill(true);
        }
    }
}