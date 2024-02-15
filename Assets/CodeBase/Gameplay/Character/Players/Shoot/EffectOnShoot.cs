using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages.Weapon;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Players.Shoot
{
    public class EffectOnShoot : IInitializable, IDisposable
    {
        private readonly ShootingOnAnimationEvent _shootingOnAnimationEvent;
        private readonly IProvider<Weapon> _weaponProvider;
        private readonly WeaponShootEffectStorage _weaponShootEffectStorage;
        private readonly Transform _initialShootPosition;

        public EffectOnShoot(IProvider<Weapon> weaponProvider, 
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
            _shootingOnAnimationEvent.Shot += PlayEffects;

        public void Dispose() => 
            _shootingOnAnimationEvent.Shot -= PlayEffects;

        private void PlayEffects()
        {
            WeaponTypeId weaponWeaponTypeId = _weaponProvider.Get().WeaponTypeId;
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