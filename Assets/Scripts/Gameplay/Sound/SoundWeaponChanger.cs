using System.Collections.Generic;
using Enums;
using Services.Providers;
using Services.Storages;

namespace Gameplay.Sound
{
    public class SoundWeaponChanger
    {
        private readonly ISoundStorage _soundStorage;
        private readonly SoundProvider _soundProvider;

        private readonly Dictionary<WeaponTypeId, SoundTypeId> _audioSources;

        public SoundWeaponChanger(SoundProvider soundProvider, ISoundStorage soundStorage, SoundsSettings soundsSettings)
        {
            _audioSources = soundsSettings.WeaponShootSounds;
            _soundProvider = soundProvider;
            _soundStorage = soundStorage;
        }
        
        public void SetCurrentSound(WeaponTypeId weaponTypeId)
        {
            SoundTypeId soundTypeId = _audioSources[weaponTypeId];
            _soundProvider.ShootSound = _soundStorage.Get(soundTypeId);
        }
    }
}