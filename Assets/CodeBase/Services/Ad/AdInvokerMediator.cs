using System;
using CodeBase.Infrastructure;
using Zenject;

namespace CodeBase.Services.Ad
{
    public class AdInvokerMediator : IInitializable, IDisposable
    {
        private readonly IAdInvoker _adInvoker;
        private readonly ILoadingCurtain _loadingCurtain;

        public AdInvokerMediator(IAdInvoker adInvoker, ILoadingCurtain loadingCurtain)
        {
            _adInvoker = adInvoker;
            _loadingCurtain = loadingCurtain;
        }

        public void Initialize() =>
            _loadingCurtain.Closed += OnCurtainClosed;

        public void Dispose() =>
            _loadingCurtain.Closed -= OnCurtainClosed;

        private void OnCurtainClosed() =>
            _adInvoker.Init(null, null);
    }
}