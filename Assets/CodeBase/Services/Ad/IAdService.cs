﻿using System;

namespace CodeBase.Services.Ad
{
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