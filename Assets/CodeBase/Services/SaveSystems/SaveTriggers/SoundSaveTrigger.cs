using System;
using CodeBase.Services.Inputs.InputService;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.UI.Windows.Audio;
using UnityEngine.InputSystem;
using Zenject;

namespace CodeBase.Services.SaveSystems.SaveTriggers
{
    public class SoundSaveOnTrigger : IInitializable, IDisposable
    {
        private readonly AudioView _audioView;
        private readonly ISaveSystem _saveSystem;
        private readonly IInputService _inputService;
        private float _targetVolume;
        private bool _isSaved;

        public SoundSaveOnTrigger(AudioView audioView, 
            ISaveSystem saveSystem, IInputService inputService)
        {
            _inputService = inputService;
            _saveSystem = saveSystem;
            _audioView = audioView;
        }

        public void Initialize()
        {
            _audioView.ValueChanged += SetVolume;
            _inputService.PlayerFire.performed += Save;
        }

        public void Dispose()
        {
            _audioView.ValueChanged -= SetVolume;
            _inputService.PlayerFire.performed -= Save;
        }

        private async void Save(InputAction.CallbackContext callbackContext)
        {
            if(_isSaved)
                return;
            
            var settinsData = await _saveSystem.Load<SettingsData>();
            settinsData.Volume = _targetVolume;
            _saveSystem.Save(settinsData);
            _isSaved = true;
        }

        private void SetVolume(float volume) =>
            _targetVolume = volume;
    }
}