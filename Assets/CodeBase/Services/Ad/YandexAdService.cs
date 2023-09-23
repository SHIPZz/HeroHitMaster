using System;
using Agava.YandexGames;
using CodeBase.Services.Pause;

namespace CodeBase.Services.Ad
{
    public class YandexAdService : IAdService
    {
        private readonly IPauseService _pauseService;
        
        public event Action LongAdClosed;
        public event Action LongAdOpened;
        public event Action ShortAdClosed;
        public event Action ShortAdOpened;

        public YandexAdService(IPauseService pauseService) => 
            _pauseService = pauseService;

        public void PlayShortAd(Action startCallback, Action<bool> onCloseCallback)
        {
            InterstitialAd.Show(() =>
            {
                startCallback?.Invoke();
                _pauseService.Pause();
                ShortAdOpened?.Invoke();
            },  closed =>
            {
                onCloseCallback?.Invoke(closed);
                _pauseService.UnPause();
                ShortAdClosed?.Invoke();
            });
        }

        public void PlayLongAd(Action startCallback, Action endCallback)
        {
            VideoAd.Show(() =>
            {
                startCallback?.Invoke();
                _pauseService.Pause();
                LongAdOpened?.Invoke();
            },  () =>
            {
                endCallback?.Invoke();
                _pauseService.UnPause();
                LongAdClosed?.Invoke();
            });
        }
    }
}