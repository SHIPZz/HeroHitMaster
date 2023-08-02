using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Installers.ScriptableObjects;
using CodeBase.ScriptableObjects.Sound;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages;
using CodeBase.Services.Storages.Sound;

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