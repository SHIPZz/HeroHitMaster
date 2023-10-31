using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.Services.Storages;
using CodeBase.Services.Storages.Sound;
using UnityEngine;

namespace CodeBase.UI.Windows.Audio
{
    public class AudioChanger
    {
        private readonly List<AudioSource> _allSounds;
        private readonly ISaveSystem _saveSystem;

        public AudioChanger(ISoundStorage soundStorage, ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _allSounds = soundStorage.GetAll();
        }

        public async void Change(float value)
        {
            _allSounds.ForEach(x => x.volume = value);
            var worldData = await _saveSystem.Load<WorldData>();
            worldData.SettingsData.Volume = value;
        }
    }
}