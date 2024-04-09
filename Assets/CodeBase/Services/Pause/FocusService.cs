using System;
using System.Collections.Generic;
using System.Linq;
using Agava.WebUtility;
using CodeBase.Enums;
using CodeBase.Services.Ad;
using CodeBase.Services.Providers;
using CodeBase.UI.Windows;
using Cysharp.Threading.Tasks;
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
        private readonly IAdService _adService;

        public FocusService(IPauseService pauseService, WindowProvider windowProvider, IAdService adService)
        {
            _adService = adService;
            _pauseService = pauseService;
            _allWindows = windowProvider.Windows.Values.ToList();
        }

        public void Initialize()
        {
            Application.focusChanged += OnFocusChanged;
            _adService.AdFinished += HandleFocusGained;
            AudioListener.pause = false;
            AudioListener.volume = AudioUnmuteVolume;
        }

        public void Dispose()
        {
            _adService.AdFinished -= HandleFocusGained;
            Application.focusChanged -= OnFocusChanged;
        }

        private async void OnFocusChanged(bool hasFocus)
        {
            while (_adService.IsAdEnabled)
                await UniTask.Yield();
            
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

        public void HandleFocusGained()
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
                    _pauseService.Pause();
                    MuteAudio(false);
                    break;

                case WindowTypeId.Shop:
                    MuteAudio(false);
                    break;

                case WindowTypeId.Popup:
                    MuteAudio(false);
                    _pauseService.Pause();
                    break;

                case WindowTypeId.Hud:
                    MuteAudio(false);
                    _pauseService.UnPause();
                    break;

                case WindowTypeId.Play:
                    MuteAudio(false);
                    _pauseService.UnPause();
                    break;

                default:
                    _pauseService.UnPause();
                    MuteAudio(false);
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