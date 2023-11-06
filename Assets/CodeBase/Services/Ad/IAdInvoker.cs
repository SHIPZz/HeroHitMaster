using System;
using JetBrains.Annotations;

namespace CodeBase.Services.Ad
{
    public interface IAdInvoker
    {
        void Init([CanBeNull] Action onStartCallback,[CanBeNull] Action onCloseCallback);
    }
}