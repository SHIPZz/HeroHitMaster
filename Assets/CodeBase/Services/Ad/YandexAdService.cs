using System;
using Agava.YandexGames;
using CodeBase.Services.Pause;
using UnityEngine;

namespace CodeBase.Services.Ad
{
    public class YandexAdService : IAdService
    {
        private readonly IPauseService _pauseService;

        public YandexAdService(IPauseService pauseService) =>
            _pauseService = pauseService;

        public bool IsAdEnabled { get; private set; }
        public event Action AdFinished;

        public void PlayShortAd(Action startCallback, Action<bool> onCloseCallback) => 
            InterstitialAd.Show(() =>  OnShortAdStartCallback(startCallback), closed => OnShortAdCloseCallback(onCloseCallback, closed));

        private void OnShortAdStartCallback(Action startCallback)
        {
            startCallback?.Invoke();
            _pauseService.Pause();
            IsAdEnabled = true;
        }

        private void OnShortAdCloseCallback(Action<bool> onCloseCallback, bool closed)
        {
            onCloseCallback?.Invoke(closed);
            IsAdEnabled = false;
            AdFinished?.Invoke();
            _pauseService.UnPause();
        }

        public void PlayLongAd(Action startCallback, Action endCallback) =>
            VideoAd.Show(() => OnOpenCallback(startCallback), null, () => OnCloseCallback(endCallback));

        private void OnCloseCallback(Action endCallback)
        {
            endCallback?.Invoke();
            AudioListener.volume = 1f;
            AdFinished?.Invoke();
            IsAdEnabled = false;
        }

        private void OnOpenCallback(Action startCallback)
        {
            IsAdEnabled = true;
            startCallback?.Invoke();
            AudioListener.volume = 0f;
        }
    }
}