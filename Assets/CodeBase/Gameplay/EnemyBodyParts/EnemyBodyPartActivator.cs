using DG.Tweening;
using Enums;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyPartActivator
    {
        private const float DisableDelay = 5f;
        private readonly EnemyBodyPartStorage _enemyBodyPartStorage;

        public EnemyBodyPartActivator(EnemyBodyPartStorage enemyBodyPartStorage)
        {
            _enemyBodyPartStorage = enemyBodyPartStorage;
        }
        
        public void ActivateWithDisableDelay(EnemyTypeId enemyTypeId)
        {
            EnemyBodyPart enemyBodyPart = _enemyBodyPartStorage.Get(enemyTypeId);
            enemyBodyPart.Enable();
            DOTween.Sequence().AppendInterval(DisableDelay).OnComplete(enemyBodyPart.Disable);
        }
    }
}