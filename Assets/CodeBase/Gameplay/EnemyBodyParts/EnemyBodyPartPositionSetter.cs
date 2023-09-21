using System;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.MaterialChanger;
using Zenject;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyPartPositionSetter : IInitializable, IDisposable
    {
        private bool _canSetPosition = true;
        private EnemyPartFactory _enemyPartFactory;
        private readonly IMaterialChanger _materialChanger;

        public EnemyBodyPartPositionSetter(IMaterialChanger materialChanger)
        {
            _materialChanger = materialChanger;
        }

        [Inject]
        public void Construct(EnemyPartFactory enemyPartFactory) =>
            _enemyPartFactory = enemyPartFactory;

        public void SetPosition(Enemy enemy)
        {
            if (!_canSetPosition)
                return;

            EnemyBodyPart enemyBodyPart = _enemyPartFactory.CreateBy(enemy.EnemyTypeId, enemy.transform.position);

            enemyBodyPart.Enable();
            enemyBodyPart.SetHeightPosition();
        }

        private void Disable() =>
            _canSetPosition = false;

        public void Initialize()
        {
            _materialChanger.StartedChanged += Disable;
        }

        public void Dispose()
        {
            _materialChanger.StartedChanged -= Disable;
        }
    }
}