using System;
using CodeBase.Gameplay.MaterialChanger;
using Zenject;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyActivatorDisabler : IInitializable, IDisposable
    {
        private readonly EnemyBodyPartActivator _enemyBodyPartActivator;
        private readonly IMaterialChanger _skinnedMaterialChanger;
        private EnemyBodyPartPositionSetter _enemyBodyPartPositionSetter;

        public EnemyBodyActivatorDisabler(EnemyBodyPartActivator enemyBodyPartActivator, IMaterialChanger skinnedMaterialChanger,
            EnemyBodyPartPositionSetter enemyBodyPartPositionSetter)
        {
            _enemyBodyPartPositionSetter = enemyBodyPartPositionSetter;
            _enemyBodyPartActivator = enemyBodyPartActivator;
            _skinnedMaterialChanger = skinnedMaterialChanger;
        }

        public void Initialize()
        {
            _skinnedMaterialChanger.StartedChanged += _enemyBodyPartActivator.Disable;
            _skinnedMaterialChanger.StartedChanged += _enemyBodyPartPositionSetter.Disable;
        }

        public void Dispose()
        {
            // _skinnedMaterialChanger.Changed -= _enemyBodyPartActivator.Disable;
            // _skinnedMaterialChanger.Changed -= _enemyBodyPartPositionSetter.Disable;
        }
    }
}