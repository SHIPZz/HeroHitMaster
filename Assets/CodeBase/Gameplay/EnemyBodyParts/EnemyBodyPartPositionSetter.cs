using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Services.Storages;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyPartPositionSetter
    {
        private readonly IEnemyStorage _enemyStorage;
        private readonly EnemyBodyPartStorage _enemyBodyPartStorage;

        public EnemyBodyPartPositionSetter(IEnemyStorage enemyStorage, EnemyBodyPartStorage enemyBodyPartStorage)
        {
            _enemyStorage = enemyStorage;
            _enemyBodyPartStorage = enemyBodyPartStorage;
        }

        public void SetPosition(EnemyTypeId enemyTypeId)
        {
            Enemy enemy = _enemyStorage.Get(enemyTypeId);
            EnemyBodyPart enemyBodyPart = _enemyBodyPartStorage.Get(enemyTypeId);

            enemyBodyPart.transform.position = enemy.transform.position;
            enemyBodyPart.SetHeightPosition();
        }
    }
}