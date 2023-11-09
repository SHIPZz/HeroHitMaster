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
        private readonly IPauseService _pauseService;
        private readonly List<Window> _allWindows;

        public FocusService(IPauseService pauseService, WindowProvider windowProvider)
        {
            _allWindows = windowProvider.Windows.Values.ToList();
            _pauseService = pauseService;
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
            {
                _pauseService.Pause();
                MuteAudio(true);
                return;
            }

            foreach (Window window in _allWindows)
            {
                if (window.WindowTypeId == WindowTypeId.Play)
                {
                    MuteAudio(false);
                    continue;
                }

                if (window.WindowTypeId == WindowTypeId.Hud)
                    continue;

                if(window.gameObject.activeSelf)
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