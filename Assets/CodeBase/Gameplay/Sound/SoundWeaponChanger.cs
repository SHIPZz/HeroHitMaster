using System.Collections.Generic;
using CodeBase.Installers.ScriptableObjects;
using CodeBase.Services.Storages;
using Enums;
using Services.Providers;

namespace CodeBase.Gameplay.Sound
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