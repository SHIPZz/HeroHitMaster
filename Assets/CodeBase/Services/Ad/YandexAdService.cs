using System;
using Agava.YandexGames;

namespace CodeBase.Services.Ad
{
    public class YandexAdService : IAdService
    {
        public event Action LongAdClosed;
        public event Action LongAdOpened;
        public event Action ShortAdClosed;
        public event Action ShortAdOpened;

        public void PlayShortAd(Action startCallback, Action<bool> onCloseCallback)
        {
            InterstitialAd.Show(() =>
            {
                startCallback?.Invoke();
                ShortAdOpened?.Invoke();
            },  closed =>
            {
                onCloseCallback?.Invoke(closed);
                ShortAdClosed?.Invoke();
            });
        }

        public void PlayLongAd(Action startCallback, Action endCallback)
        {
            VideoAd.Show(() =>
            {
                startCallback?.Invoke();
                LongAdOpened?.Invoke();
            },  () =>
            {
                endCallback?.Invoke();
                LongAdClosed?.Invoke();
            });
        }
    }
}