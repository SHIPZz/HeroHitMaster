using CodeBase.Enums;
using DG.Tweening;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyPartActivator
    {
        private const float DisableDelay = 5f;
        private readonly EnemyBodyPartStorage _enemyBodyPartStorage;
        private bool _canActive = true;

        public EnemyBodyPartActivator(EnemyBodyPartStorage enemyBodyPartStorage)
        {
            _enemyBodyPartStorage = enemyBodyPartStorage;
        }

        public void Disable() =>
            _canActive = false;
        
        public void ActivateWithDisableDelay(EnemyTypeId enemyTypeId)
        {
            if(!_canActive)
                return;
            
            EnemyBodyPart enemyBodyPart = _enemyBodyPartStorage.Get(enemyTypeId);
            enemyBodyPart.Enable();
            DOTween.Sequence().AppendInterval(DisableDelay).OnComplete(enemyBodyPart.Disable);
        }
    }
}