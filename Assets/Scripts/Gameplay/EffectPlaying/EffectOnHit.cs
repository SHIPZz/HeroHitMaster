﻿using System;
using Enums;
using Gameplay.Character;
using UnityEngine;
using Zenject;

namespace Gameplay.EffectPlaying
{
    public class EffectOnHit : IInitializable, IDisposable
    {
        private readonly IHealth _health;
        private readonly ParticleSystem _hitEffect;
        private bool _canPlayEffect = true;

        public EffectOnHit(IHealth health, [Inject(Id = ParticleTypeId.HitEffect)] ParticleSystem hitEffect)
        {
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
        }

        private void BlockHitEffect() =>
            _canPlayEffect = false;
    }
}