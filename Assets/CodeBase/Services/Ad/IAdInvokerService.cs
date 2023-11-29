using System;

namespace CodeBase.Services.Ad
{
    public interface IAdInvokerService
    {
        void Invoke(Action onCloseCallback);
    }
}