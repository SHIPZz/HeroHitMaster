using System;
using JetBrains.Annotations;

namespace CodeBase.Services.Ad
{
    public interface IAdService
    {
        event Action LongAdClosed;
        event Action LongAdOpened;
        event Action ShortAdClosed;
        event Action ShortAdOpened;

        void PlayShortAd([CanBeNull] Action startCallback,[CanBeNull] Action<bool> onCloseCallback);
        void PlayLongAd([CanBeNull] Action startCallback,[CanBeNull] Action endCallback);
    }
}