using System;
using System.Collections.Generic;
using CodeBase.UI.Windows.Audio;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Storages.Weapon
{
    public class ChangeWeaponShootSoundVolumeHandler : IInitializable, IDisposable
    {
        private readonly WeaponShootEffectStorage _weaponShootEffectStorage;
        private readonly AudioVolumeChanger _audioVolumeChanger;

        public ChangeWeaponShootSoundVolumeHandler(WeaponShootEffectStorage weaponShootEffectStorage,
            AudioVolumeChanger audioVolumeChanger)
        {
            _weaponShootEffectStorage = weaponShootEffectStorage;
            _audioVolumeChanger = audioVolumeChanger;
        }

        public void Initialize()
        {
            _audioVolumeChanger.VolumeChanged += Set;
        }

        public void Dispose()
        {
            _audioVolumeChanger.VolumeChanged -= Set;
        }

        private void Set(float volume)
        {
            IEnumerable<AudioSource> allShootSounds = _weaponShootEffectStorage.GetAllSounds();

            foreach (AudioSource audioSource in allShootSounds)
            {
                audioSource.volume = volume;
            }
        }
    }
}