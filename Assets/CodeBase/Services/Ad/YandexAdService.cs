using System;

namespace CodeBase.Services.Ad
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
}