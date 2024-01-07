using System;

namespace CodeBase.Services.Ad
{
    public interface IAdInvokerService
    {
        bool AdEnabled { get; }
        void Invoke(Action onCloseCallback = null);
    }
}