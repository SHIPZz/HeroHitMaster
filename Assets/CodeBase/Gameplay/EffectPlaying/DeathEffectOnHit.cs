using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character;
using CodeBase.Installers.ScriptableObjects;
using CodeBase.ScriptableObjects.Sound;
using CodeBase.Services.Storages;
using CodeBase.Services.Storages.Sound;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.EffectPlaying
{
    public class DeathEffectOnHit : IInitializable, IDisposable
    {
        private readonly IHealth _health;
        private readonly ParticleSystem _dieEffect;

        private Dictionary<EnemyTypeId, SoundTypeId> _audioSources;

        private EnemyTypeId _enemyTypeId;
        private AudioSource _dieSound;

        public DeathEffectOnHit(IHealth health, [Inject(Id = ParticleTypeId.DieEffect)] ParticleSystem dieEffect,
            EnemyTypeId enemyTypeId, SoundsSettings soundsSettings, ISoundStorage soundStorage)
        {
            _enemyTypeId = enemyTypeId;
            _health = health;
            _dieEffect = dieEffect;
            Init(soundsSettings, soundStorage);
        }

        public void Initialize() =>
            _health.ValueZeroReached += PlayEffect;

        public void Dispose() =>
            _health.ValueZeroReached -= PlayEffect;

        private void PlayEffect()
        {
            _dieSound.Play();
            _dieEffect.Play();
        }

        private void Init(SoundsSettings soundsSettings, ISoundStorage soundStorage)
        {
            _audioSources = soundsSettings.DieEnemySounds;
            SoundTypeId soundTypeId = _audioSources[_enemyTypeId];
            _dieSound = soundStorage.Get(soundTypeId);
        }
    }
}