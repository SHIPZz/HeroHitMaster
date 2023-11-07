using System;
using CodeBase.Enums;
using CodeBase.Infrastructure;
using CodeBase.Services.Pause;
using Zenject;

namespace CodeBase.UI.Windows.Hud
{
    public class HudPresenter : IInitializable, IDisposable, IGameplayRunnable
    {
        private readonly WindowService _windowService;
        private readonly HudView _hudView;
        private readonly IPauseService _pauseService;

        public HudPresenter(WindowService windowService, HudView hudView, IPauseService pauseService)
        {
            _pauseService = pauseService;
            _windowService = windowService;
            _hudView = hudView;
        }

        public void Initialize()
        {
            _hudView.PauseButtonClicked += OnPauseClicked;
        }

        public void Dispose()
        {
            _hudView.PauseButtonClicked -= OnPauseClicked;
        }

        public void Run()
        {
            _windowService.Open(WindowTypeId.Hud);
        }

        private void OnPauseClicked()
        {
            _windowService.Close(WindowTypeId.Hud);
            _windowService.Open(WindowTypeId.Pause);
            _pauseService.Pause();
        }
    }
}