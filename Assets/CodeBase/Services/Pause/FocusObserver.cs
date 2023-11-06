using System;
using Agava.WebUtility;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Pause
{
    public class FocusObserver : IInitializable, IDisposable
    {
        private readonly IPauseService _pauseService;

        public FocusObserver(IPauseService pauseService) =>
            _pauseService = pauseService;

        public void Initialize()
        {
            Application.focusChanged += OnFocusChanged;
            WebApplication.InBackgroundChangeEvent += OnFocusChanged;
        }

        public void Dispose()
        {
            Application.focusChanged -= OnFocusChanged;
            WebApplication.InBackgroundChangeEvent -= OnFocusChanged;
        }

        private void OnFocusChanged(bool hasFocus)
        {
            if (!hasFocus)
            {
                _pauseService.Pause();
                MuteAudio(true);
                return;
            }

            _pauseService.UnPause();
            MuteAudio(false);
        }

        private void MuteAudio(bool value)
        {
            AudioListener.pause = value;
            AudioListener.volume = value ? 0 : 1;
        }
    }
}