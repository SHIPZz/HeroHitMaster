using CodeBase.Gameplay.Character.Enemy;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyPartPositionSetter
    {
        private readonly EnemyBodyPartStorage _enemyBodyPartStorage;
        private bool _canSetPosition = true;

        public EnemyBodyPartPositionSetter(EnemyBodyPartStorage enemyBodyPartStorage)
        {
            _enemyBodyPartStorage = enemyBodyPartStorage;
        }

        public void SetPosition(Enemy enemy)
        {
            if(!_canSetPosition)
                return;
            
            EnemyBodyPart enemyBodyPart = _enemyBodyPartStorage.Get(enemy.EnemyTypeId);

            enemyBodyPart.transform.position = enemy.transform.position;
            enemyBodyPart.SetHeightPosition();
        }

        public void Disable() => 
            _canSetPosition = false;
    }
}