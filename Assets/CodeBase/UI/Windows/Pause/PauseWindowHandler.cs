using System;
using Agava.WebUtility;
using CodeBase.Enums;
using CodeBase.Gameplay.MusicHandlerSystem;
using CodeBase.Services.Pause;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Windows.Pause
{
    public class PauseWindowHandler : IInitializable, IDisposable
    {
        private readonly PauseWindowView _pauseWindowView;
        private readonly IPauseService _pauseService;
        private readonly WindowService _windowService;
        private readonly Window _hud;
        private bool _isOpened;

        public PauseWindowHandler(PauseWindowView pauseWindowView, IPauseService pauseService,
            WindowService windowService, WindowProvider windowProvider)
        {
            _hud = windowProvider.Windows[WindowTypeId.Hud];
            _pauseWindowView = pauseWindowView;
            _pauseService = pauseService;
            _windowService = windowService;
        }

        public void Initialize()
        {
            _pauseWindowView.ReturnButtonClicked += OnReturned;
            Application.focusChanged += OnFocusChanged;
            WebApplication.InBackgroundChangeEvent += OnFocusChanged;
        }

        public void Dispose()
        {
            _pauseWindowView.ReturnButtonClicked -= OnReturned;
            Application.focusChanged -= OnFocusChanged;
            WebApplication.InBackgroundChangeEvent -= OnFocusChanged;
        }

        private void OnFocusChanged(bool hasFocus)
        {
            if (_isOpened)
            {
                _pauseService.Pause();
                return;
            }

            if (!hasFocus && _hud.gameObject.activeSelf)
            {
                _pauseService.Pause();
                _windowService.Close(WindowTypeId.Hud, () => _windowService.OpenQuickly(WindowTypeId.Pause));
                _isOpened = true;
            }
        }

        private void OnReturned()
        {
            _windowService.CloseAll(() => _windowService.OpenQuickly(WindowTypeId.Hud));
            AudioListener.pause = false;
            AudioListener.volume = 1f;
            _pauseService.UnPause();
            _isOpened = false;
        }
    }
}