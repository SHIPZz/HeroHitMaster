using System;
using CodeBase.Enums;
using CodeBase.Infrastructure;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages.Sound;
using CodeBase.UI.Windows;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class PlayMusicPresenter : IInitializable, IDisposable, IGameplayRunnable
    {
        private readonly Window _deathWindow;
        private readonly Window _victoryWindow;
        private readonly AudioSource _music;

        public PlayMusicPresenter(WindowProvider windowProvider,ISoundStorage soundStorage)
        {
            _music = soundStorage.Get(MusicTypeId.Hotline);
            _deathWindow = windowProvider.Windows[WindowTypeId.Death];
            _victoryWindow = windowProvider.Windows[WindowTypeId.Victory];
        }
        
        public void Initialize()
        {
            _deathWindow.StartedToOpen += _music.Stop;
            _victoryWindow.StartedToOpen += _music.Stop;
        }

        public void Dispose()
        {
            _deathWindow.StartedToOpen -= _music.Stop;
            _victoryWindow.StartedToOpen -= _music.Stop;
        }

        public void Run() => 
            _music.Play();
    }
}