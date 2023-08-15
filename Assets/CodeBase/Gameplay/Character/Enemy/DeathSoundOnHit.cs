using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Sound;
using CodeBase.Services.Storages.Sound;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class DeathSoundOnHit : IInitializable, IDisposable
    {
        private readonly IHealth _health;
        private readonly ISoundStorage _soundStorage;
        private readonly DieOnAnimationEvent _dieOnAnimationEvent;
        private readonly Dictionary<EnemyTypeId, SoundTypeId> _audioSources;
        private readonly Enemy _enemy;
        
        private AudioSource _dieSound;

        public DeathSoundOnHit(SoundsSettings soundsSettings, ISoundStorage soundStorage,
            DieOnAnimationEvent dieOnAnimationEvent, Enemy enemy)
        {
            _enemy = enemy;
            _soundStorage = soundStorage;
            _dieOnAnimationEvent = dieOnAnimationEvent;
            _audioSources = soundsSettings.DieEnemySounds;
        }

        public void Initialize()
        {
            _dieOnAnimationEvent.Dead += PlaySound;
            _enemy.QuickDestroyed += PlaySound;
        }

        public void Dispose()
        {
            _dieOnAnimationEvent.Dead -= PlaySound;
            _enemy.QuickDestroyed -= PlaySound;
        }

        private void PlaySound(Enemy enemy)
        {
            SoundTypeId soundTypeId = _audioSources[enemy.EnemyTypeId];
            _dieSound = _soundStorage.Get(soundTypeId);
            _dieSound.Play();
        }
    }
}
