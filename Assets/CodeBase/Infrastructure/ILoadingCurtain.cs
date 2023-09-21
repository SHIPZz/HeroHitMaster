using System;

namespace CodeBase.Infrastructure
{
    public interface ILoadingCurtain
    {
        event Action Closed;
        void Show(float sliderDuration);
        void Hide();
    }
}