using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using DG.Tweening;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyPartActivator
    {
        private const float DisableDelay = 5f;
        private readonly EnemyBodyPartStorage _enemyBodyPartStorage;
        private bool _canActivate = true;

        public EnemyBodyPartActivator(EnemyBodyPartStorage enemyBodyPartStorage)
        {
            _enemyBodyPartStorage = enemyBodyPartStorage;
        }

        public void Disable() =>
            _canActivate = false;
        
        public void ActivateWithDisableDelay(Enemy enemy)
        {
            if(!_canActivate)
                return;
            
            EnemyBodyPart enemyBodyPart = _enemyBodyPartStorage.Get(enemy.EnemyTypeId);
            enemyBodyPart.Enable();
            DOTween.Sequence().AppendInterval(DisableDelay).OnComplete(enemyBodyPart.Disable);
        }
    }
}