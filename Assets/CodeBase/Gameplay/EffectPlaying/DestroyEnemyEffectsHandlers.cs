using System;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.MaterialChanger;
using Zenject;

namespace CodeBase.Gameplay.EffectPlaying
{
    public class DestroyEnemyEffectsHandler : IInitializable, IDisposable
    {
        private readonly EffectOnHit _hitEffect;
        private readonly DeathEffectOnHit _deathEffect;
        private readonly IMaterialChanger _materialChanger;
        private readonly IHealth _health;

        public DestroyEnemyEffectsHandler(EffectOnHit effectOnHit, DeathEffectOnHit deathEffectOnHit,
            IMaterialChanger materialChanger, IHealth health)
        {
            _materialChanger = materialChanger;
            _health = health;
            _hitEffect = effectOnHit;
            _deathEffect = deathEffectOnHit;
        }

        public void Initialize()
        {
            _materialChanger.Changed += Destroy;
            _health.ValueZeroReached += DestroyHitEffect;
        }

        public void Dispose()
        {
            _materialChanger.Changed -= Destroy;
            _health.ValueZeroReached -= DestroyHitEffect;
        }

        private void DestroyHitEffect() => 
            _hitEffect.Dispose();

        private void Destroy()
        {
            _hitEffect.Dispose();
            _deathEffect.Dispose();
        }
    }
}