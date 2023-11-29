using System;
using Agava.YandexGames;
using CodeBase.Services.Pause;

namespace CodeBase.Services.Ad
{
    public class YandexAdService : IAdService
    {
        private readonly IPauseService _pauseService;

        public YandexAdService(IPauseService pauseService) =>
            _pauseService = pauseService;

        public void PlayShortAd(Action startCallback, Action<bool> onCloseCallback) => 
            InterstitialAd.Show(() =>  OnShortAdStartCallback(startCallback), closed => OnShortAdCloseCallback(onCloseCallback, closed));

        private void OnShortAdStartCallback(Action startCallback)
        {
            startCallback?.Invoke();
            _pauseService.Pause();
        }

        private void OnShortAdCloseCallback(Action<bool> onCloseCallback, bool closed)
        {
            onCloseCallback?.Invoke(closed);
            _pauseService.UnPause();
        }

        public void PlayLongAd(Action startCallback, Action endCallback) =>
            VideoAd.Show(() => OnOpenCallback(startCallback), null, () => OnCloseCallback(endCallback));

        private void OnCloseCallback(Action endCallback)
        {
            endCallback?.Invoke();
            _pauseService.UnPause();
        }

        private void OnOpenCallback(Action startCallback)
        {
            startCallback?.Invoke();
            _pauseService.Pause();
        }
    }
}