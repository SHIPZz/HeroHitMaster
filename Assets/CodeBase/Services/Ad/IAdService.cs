using System;
using JetBrains.Annotations;

namespace CodeBase.Services.Ad
{
    public interface IAdService
    {
        bool IsAdEnabled { get; }
        event Action AdFinished;
        void PlayShortAd([CanBeNull] Action startCallback,[CanBeNull] Action<bool> onCloseCallback,Action<string> onErrorCallback,
            Action onOfflineCallback);
        void PlayLongAd([CanBeNull] Action startCallback,[CanBeNull] Action endCallback,Action<string> onErrorCallback);
    }
}