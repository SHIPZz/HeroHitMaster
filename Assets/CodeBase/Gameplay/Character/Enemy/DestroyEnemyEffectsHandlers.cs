using System;
using CodeBase.Gameplay.MaterialChanger;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class DestroyEnemyEffectsHandler : IInitializable, IDisposable
    {
        private readonly EffectOnHit _hitEffect;
        private readonly DeathSoundOnHit _deathSound;
        private readonly IMaterialChanger _materialChanger;
        private readonly IHealth _health;

        public DestroyEnemyEffectsHandler(EffectOnHit effectOnHit, DeathSoundOnHit deathSoundOnHit,
            IMaterialChanger materialChanger, IHealth health)
        {
            _materialChanger = materialChanger;
            _health = health;
            _hitEffect = effectOnHit;
            _deathSound = deathSoundOnHit;
        }

        public void Initialize()
        {
            _materialChanger.StartedChanged += Destroy;
            _health.ValueZeroReached += DestroyHitEffect;
        }

        public void Dispose()
        {
            _materialChanger.StartedChanged -= Destroy;
            _health.ValueZeroReached -= DestroyHitEffect;
        }

        private void DestroyHitEffect() => 
            _hitEffect.Dispose();

        private void Destroy()
        {
            _hitEffect.Dispose();
            _deathSound.Dispose();
        }
    }
}