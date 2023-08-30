using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.MaterialChanger;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyPartPositionSetter : MonoBehaviour
    {
        private bool _canSetPosition = true;
        private EnemyPartFactory _enemyPartFactory;
        private IMaterialChanger _materialChanger;

        [Inject]
        public void Construct(EnemyPartFactory enemyPartFactory) => 
        _enemyPartFactory = enemyPartFactory;

        private void Awake()
        {
            _materialChanger = GetComponent<IMaterialChanger>();
            _materialChanger.StartedChanged += Disable;
        }

        public void SetPosition(Enemy enemy)
        {
            if(!_canSetPosition)
                return;
            
            EnemyBodyPart enemyBodyPart = _enemyPartFactory.CreateBy(enemy.EnemyTypeId, enemy.transform.position);

            enemyBodyPart.Enable();
            enemyBodyPart.SetHeightPosition();
        }

        private void Disable() => 
            _canSetPosition = false;
    }
}