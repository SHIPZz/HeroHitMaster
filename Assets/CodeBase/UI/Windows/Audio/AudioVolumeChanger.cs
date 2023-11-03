using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages.Sound;
using UnityEngine;

namespace CodeBase.UI.Windows.Audio
{
    public class AudioVolumeChanger
    {
        private readonly List<AudioSource> _allSounds;
        private readonly IWorldDataService _worldDataService;
        private readonly AudioSource _music;

        public AudioVolumeChanger(ISoundStorage soundStorage, IWorldDataService worldDataService)
        {
            _worldDataService = worldDataService;
            _allSounds = soundStorage.GetAll();
            _music = soundStorage.Get(MusicTypeId.Hotline);
        }

        public void ChangeMusic(float value)
        {
            _music.volume = value;
            _worldDataService.WorldData.SettingsData.MusicVolume = value;
        }

        public void Change(float value)
        {
            _allSounds.ForEach(x => x.volume = value);
            _worldDataService.WorldData.SettingsData.Volume = value;
        }
    }
}