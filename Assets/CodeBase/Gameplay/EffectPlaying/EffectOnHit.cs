using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Character;
using CodeBase.Installers.ScriptableObjects;
using CodeBase.Services.Storages;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.EffectPlaying
{
    public class EffectOnHit : IInitializable, IDisposable
    {
        private readonly IHealth _health;
        private readonly ParticleSystem _hitEffect;
        private bool _canPlayEffect = true;
        private readonly AudioSource _hitSound;

        public EffectOnHit(IHealth health, [Inject(Id = ParticleTypeId.HitEffect)] ParticleSystem hitEffect,
            SoundsSettings soundsSettings, ISoundStorage soundStorage, EnemyTypeId enemyTypeId)
        {
            SoundTypeId audioTypeId = soundsSettings.HitEnemySounds[enemyTypeId];
            _hitSound = soundStorage.Get(audioTypeId);
            _health = health;
            _hitEffect = hitEffect;
        }

        public void Initialize()
        {
            _health.ValueChanged += PlayEffect;
            _health.ValueZeroReached += BlockHitEffect;
        }

        public void Dispose()
        {
            _health.ValueChanged -= PlayEffect;
            _health.ValueZeroReached -= BlockHitEffect;
        }

        private void PlayEffect(int obj)
        {
            if(!_canPlayEffect)
                return;
            
            _hitEffect.Play();
            _hitSound.Play();
        }

        private void BlockHitEffect() =>
            _canPlayEffect = false;
    }
}