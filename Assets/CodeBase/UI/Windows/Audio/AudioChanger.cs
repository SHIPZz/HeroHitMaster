using System.Collections.Generic;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages.Sound;
using UnityEngine;

namespace CodeBase.UI.Windows.Audio
{
    public class AudioChanger
    {
        private readonly List<AudioSource> _allSounds;
        private readonly IWorldDataService _worldDataService;

        public AudioChanger(ISoundStorage soundStorage, IWorldDataService worldDataService)
        {
            _worldDataService = worldDataService;
            _allSounds = soundStorage.GetAll();
        }

        public void Change(float value)
        {
            _allSounds.ForEach(x => x.volume = value);
            _worldDataService.WorldData.SettingsData.Volume = value;
        }
    }
}