using System;
using Agava.WebUtility;
using CodeBase.Enums;
using CodeBase.Services.Storages.Sound;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.EffectsData
{
    public class PlaySoundOnFocusChanged : IInitializable, IDisposable
    {
        private readonly AudioSource _audioSource;

        public PlaySoundOnFocusChanged(ISoundStorage soundStorage) => 
        _audioSource = soundStorage.Get(SoundTypeId.FocusChanged);

        public void Initialize()
        {
            Application.focusChanged += Play;
            WebApplication.InBackgroundChangeEvent += Play;
        }

        public void Dispose()
        {
            Application.focusChanged -= Play;
            WebApplication.InBackgroundChangeEvent -= Play;
        }

        private void Play(bool onFocusChanged)
        {
            if (!onFocusChanged)
            {
                // _audioSource.Play();
            }
        }
    }
}