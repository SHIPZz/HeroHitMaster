using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Players.Shoot;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages.Sound;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.EffectPlaying
{
    public class EffectOnShoot : IInitializable, IDisposable
    {
        private readonly ShootingOnAnimationEvent _shootingOnAnimationEvent;
        private readonly WeaponProvider _weaponProvider;
        private readonly WeaponShootEffectStorage _weaponShootEffectStorage;
        private Transform _initialShootPosition;

        public EffectOnShoot(WeaponProvider weaponProvider, 
            ShootingOnAnimationEvent shootingOnAnimationEvent, 
            WeaponShootEffectStorage weaponShootEffectStorage, 
            Transform initialShootPosition)
        {
            _initialShootPosition = initialShootPosition;
            _weaponShootEffectStorage = weaponShootEffectStorage;
            _weaponProvider = weaponProvider;
            _shootingOnAnimationEvent = shootingOnAnimationEvent;
        }

        public void Initialize() => 
            _shootingOnAnimationEvent.Shooted += PlayEffects;

        public void Dispose() => 
            _shootingOnAnimationEvent.Shooted -= PlayEffects;

        private void PlayEffects()
        {
            var weaponWeaponTypeId = _weaponProvider.CurrentWeapon.WeaponTypeId;
            AudioSource targetSound = _weaponShootEffectStorage.GetSoundBy(weaponWeaponTypeId);
            targetSound.transform.position = _initialShootPosition.position;
            targetSound.Play();
            
            if (!HasEffect(weaponWeaponTypeId)) 
                return;
            
            ParticleSystem targetEffect = _weaponShootEffectStorage.GetEffectBy(weaponWeaponTypeId);
            targetEffect.transform.position = _initialShootPosition.position;
            targetEffect.Play();
        }

        private bool HasEffect(WeaponTypeId weaponWeaponTypeId) => 
            _weaponShootEffectStorage.GetEffectBy(weaponWeaponTypeId) != null;
    }
}