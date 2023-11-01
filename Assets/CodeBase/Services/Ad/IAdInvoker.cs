using System;

namespace CodeBase.Services.Ad
{
    public interface IAdInvoker
    {
        void Init(Action onStartCallback, Action onCloseCallback);
    }
}