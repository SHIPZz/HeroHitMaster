using System;
using Agava.WebUtility;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages.Sound;
using CodeBase.UI.Windows;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.MusicHandlerSystem
{
    public class MusicHandler : IInitializable, IDisposable
    {
        private readonly Window _pauseWindow;
        private readonly AudioSource _music;
        private readonly Window _gameOverWindow;

        public MusicHandler(WindowProvider windowProvider, ISoundStorage soundStorage)
        {
            _pauseWindow = windowProvider.Windows[WindowTypeId.Pause];
            _gameOverWindow = windowProvider.Windows[WindowTypeId.GameOver];
            _music = soundStorage.Get(MusicTypeId.Hotline);
        }

        public void Initialize()
        {
            _pauseWindow.StartedToOpen += _music.Pause;
            _gameOverWindow.StartedToOpen += _music.Pause;
            _pauseWindow.Closed += _music.Play;
            Application.focusChanged += OnFocusChanged;
            WebApplication.InBackgroundChangeEvent += OnFocusChanged;
        }

        public void Dispose()
        {
            _pauseWindow.StartedToOpen -= _music.Pause;
            _gameOverWindow.StartedToOpen -= _music.Pause;
            _pauseWindow.Closed -= _music.Play;
            Application.focusChanged -= OnFocusChanged;
            WebApplication.InBackgroundChangeEvent -= OnFocusChanged;
        }

        private void OnFocusChanged(bool hasFocus)
        {
            if(!hasFocus)
                _music.Pause();
        }

        public void Play() =>
            _music.Play();
    }
}