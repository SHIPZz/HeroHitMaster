using System;
using System.Collections.Generic;
using System.Linq;
using Agava.WebUtility;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using CodeBase.UI.Windows;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Pause
{
    public class FocusService : IInitializable, IDisposable
    {
        private const float AudioMuteVolume = 0f;
        private const float AudioUnmuteVolume = 1f;
        
        private readonly IPauseService _pauseService;
        private readonly List<Window> _allWindows;
        
        public FocusService(IPauseService pauseService, WindowProvider windowProvider)
        {
            _pauseService = pauseService;
            _allWindows = windowProvider.Windows.Values.ToList();
        }

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
                HandleFocusLost();
            else
                HandleFocusGained();
        }

        private void HandleFocusLost()
        {
            _pauseService.Pause();
            MuteAudio(true);
        }

        private void HandleFocusGained()
        {
            foreach (Window window in _allWindows.Where(window => window.gameObject.activeSelf))
            {
                HandleWindowType(window.WindowTypeId);
            }
        }

        private void HandleWindowType(WindowTypeId windowTypeId)
        {
            switch (windowTypeId)
            {
                case WindowTypeId.Pause:
                    MuteAudio(false);
                    break;
                
                case WindowTypeId.Play:
                    MuteAudio(false);
                    _pauseService.UnPause();
                    break;
            }
        }

        private void MuteAudio(bool value)
        {
            AudioListener.pause = value;
            AudioListener.volume = value ? AudioMuteVolume : AudioUnmuteVolume;
        }
    }
}
