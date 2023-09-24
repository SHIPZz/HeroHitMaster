using System;
using CodeBase.UI.Windows.Play;
using Zenject;

namespace CodeBase.UI.Windows
{
    public class AllWindowsPresenter : IInitializable, IDisposable
    {
        private readonly PlayButtonView _playButtonView;
        private readonly WindowService _windowService;

        public AllWindowsPresenter(PlayButtonView playButtonView, WindowService windowService)
        {
            _windowService = windowService;
            _playButtonView = playButtonView;
        }

        public void Initialize() => 
            _playButtonView.Clicked += _windowService.CloseAll;

        public void Dispose() => 
            _playButtonView.Clicked -= _windowService.CloseAll;
    }
}