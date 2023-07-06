using System;
using Enums;
using Gameplay.Character;
using UnityEngine;
using Zenject;

namespace Gameplay.EffectPlaying
{
    public class DeathEffectOnHit : IInitializable, IDisposable
    {
        private readonly IHealth _health;
        private readonly ParticleSystem _dieEffect;

        public DeathEffectOnHit(IHealth health,[Inject(Id = ParticleTypeId.DieEffect)] ParticleSystem dieEffect)
        {
            _health = health;
            _dieEffect = dieEffect;
        }
        
        public void Initialize() => 
            _health.ValueZeroReached += PlayEffect;

        public void Dispose() => 
            _health.ValueZeroReached -= PlayEffect;

        private void PlayEffect() => 
            _dieEffect.Play();
    }
}