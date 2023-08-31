using System;
using CodeBase.Gameplay.MaterialChanger;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class DeathAnimOnMaterialChange : IInitializable, IDisposable
    {
        private readonly IMaterialChanger _materialChanger;
        private readonly EnemyAnimator _enemyAnimator;

        public DeathAnimOnMaterialChange(IMaterialChanger materialChanger, EnemyAnimator enemyAnimator)
        {
            _materialChanger = materialChanger;
            _enemyAnimator = enemyAnimator;
        }

        public void Initialize() => 
        _materialChanger.StartedChanged += PlayDeathAnimation;

        public void Dispose() => 
            _materialChanger.StartedChanged -= PlayDeathAnimation;

        private void PlayDeathAnimation() => 
            _enemyAnimator.SetDeath();
    }
}