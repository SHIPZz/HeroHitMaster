using System;
using CodeBase.Enums;
using CodeBase.Services.Pause;
using Zenject;

namespace CodeBase.UI.Windows.Pause
{
    public class PauseWindowPresenter : IInitializable, IDisposable
    {
        private readonly PauseWindowView _pauseWindowView;
        private readonly IPauseService _pauseService;
        private readonly WindowService _windowService;

        public PauseWindowPresenter(PauseWindowView pauseWindowView, IPauseService pauseService,
            WindowService windowService)
        {
            _pauseWindowView = pauseWindowView;
            _pauseService = pauseService;
            _windowService = windowService;
        }

        public void Initialize() => 
            _pauseWindowView.ReturnButtonClicked += OnReturned;

        public void Dispose() => 
        _pauseWindowView.ReturnButtonClicked -= OnReturned;

        private void OnReturned()
        {
            _windowService.Close(WindowTypeId.Pause, 
                () => _windowService.Open(WindowTypeId.Hud, 
                    () => _pauseService.UnPause()));
        }
    }
}