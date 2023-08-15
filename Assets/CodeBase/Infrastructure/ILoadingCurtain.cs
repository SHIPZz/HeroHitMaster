using System;

namespace CodeBase.Infrastructure
{
    public interface ILoadingCurtain
    {
        event Action Closed;
        void Show();
        void Hide(Action callback);
    }
}