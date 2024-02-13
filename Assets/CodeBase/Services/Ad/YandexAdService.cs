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

        public void PlayShortAd(Action startCallback, Action<bool> onCloseCallback, Action<string> onErrorCallback,
            Action onOfflineCallback)
        {
            InterstitialAd.Show(() => OnAdStartCallback(startCallback),
                closed => OnShortAdCloseCallback(onCloseCallback, closed),
                callback => OnAdErrorCallback(onErrorCallback, callback),
                () => OnShortAdOfflineCallback(onOfflineCallback));
        }

        private void OnShortAdOfflineCallback(Action onOfflineCallback)
        {
            IsAdEnabled = false;
            AdFinished?.Invoke();
            _pauseService.UnPause();
            onOfflineCallback?.Invoke();
        }

        private void OnAdErrorCallback(Action<string> onErrorCallback, string request)
        {
            IsAdEnabled = false;
            AdFinished?.Invoke();
            _pauseService.UnPause();
            onErrorCallback?.Invoke(request);
        }

        private void OnAdStartCallback(Action startCallback)
        {
            startCallback?.Invoke();
            _pauseService.Pause();
            AudioListener.volume = 0f;
            IsAdEnabled = true;
        }

        private void OnShortAdCloseCallback(Action<bool> onCloseCallback, bool closed)
        {
            onCloseCallback?.Invoke(closed);
            IsAdEnabled = false;
            AdFinished?.Invoke();
            _pauseService.UnPause();
        }

        public void PlayLongAd(Action startCallback, Action endCallback,Action<string> onErrorCallback)
        {
            VideoAd.Show(() => OnAdStartCallback(startCallback), null, () => OnCloseCallback(endCallback),
                error => OnAdErrorCallback(onErrorCallback, error));
        }

        private void OnCloseCallback(Action endCallback)
        {
            endCallback?.Invoke();
            AudioListener.volume = 1f;
            AdFinished?.Invoke();
            IsAdEnabled = false;
        }
    }
}