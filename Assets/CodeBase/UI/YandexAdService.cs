using System;

namespace CodeBase.UI
{
    public class YandexAdService : IAdService
    {
        public event Action LongAdClosed;
        public event Action LongAdOpened;
        public event Action ShortAdClosed;
        public event Action ShortAdOpened;

        public void PlayShortAd()
        {
            
        }

        public void PlayLongAd()
        {
            
        }
    }

    public interface IAdService
    {
        event Action LongAdClosed;
        event Action LongAdOpened;
        event Action ShortAdClosed;
        event Action ShortAdOpened;

        void PlayShortAd();
        void PlayLongAd();
    }
}