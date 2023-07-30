using System;
using CodeBase.Gameplay.MaterialChanger;
using Zenject;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyActivatorDisabler : IInitializable, IDisposable
    {
        private readonly EnemyBodyPartActivator _enemyBodyPartActivator;
        private readonly IMaterialChanger _skinnedMaterialChanger;

        public EnemyBodyActivatorDisabler(EnemyBodyPartActivator enemyBodyPartActivator, IMaterialChanger skinnedMaterialChanger)
        {
            _enemyBodyPartActivator = enemyBodyPartActivator;
            _skinnedMaterialChanger = skinnedMaterialChanger;
        }

        public void Initialize() => 
            _skinnedMaterialChanger.Changed += _enemyBodyPartActivator.Disable;

        public void Dispose() => 
            _skinnedMaterialChanger.Changed -= _enemyBodyPartActivator.Disable;
    }
}