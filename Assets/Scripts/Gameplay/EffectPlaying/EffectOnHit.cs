using System;
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

        public EffectOnHit(IHealth health, [Inject(Id = ParticleTypeId.HitEffect)] ParticleSystem hitEffect)
        {
            _health = health;
            _hitEffect = hitEffect;
        }

        public void Initialize() => 
            _health.ValueChanged += PlayEffect;

        public void Dispose() => 
            _health.ValueChanged -= PlayEffect;

        private void PlayEffect(int obj) => 
            _hitEffect.Play();
    }
}