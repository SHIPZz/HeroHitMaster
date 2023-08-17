using System;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Audio;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.SaveSystems.Sound
{
    public class SoundSaveOnTrigger : IInitializable, IDisposable
    {
        private readonly AudioView _audioView;
        private readonly Window _playWindow;
        private ISaveSystem _saveSystem;

        public SoundSaveOnTrigger(AudioView audioView, WindowProvider windowProvider, ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _playWindow = windowProvider.Windows[WindowTypeId.Play];
            _audioView = audioView;
        }

        public async void Initialize()
        {
            _audioView.ValueChanged += Save;
            SettingsData settinsData = await _saveSystem.Load<SettingsData>();
            Debug.Log(settinsData.Volume);
        }

        public void Dispose()
        {
            _audioView.ValueChanged -= Save;
        }

        private async void Save(float volume)
        {
            var settinsData = await _saveSystem.Load<SettingsData>();
            settinsData.Volume = volume;
            _saveSystem.Save(settinsData);
            Debug.Log(settinsData.Volume);
        }
    }

    [Serializable]
    public class SettingsData
    {
        public float Volume;
    }
}