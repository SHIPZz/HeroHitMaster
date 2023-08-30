using CodeBase.Gameplay.Character.Enemy;
using UnityEngine;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyPartMediator : MonoBehaviour
    {
        [SerializeField] private EnemyBodyPartPositionSetter _enemyBodyPartPositionSetter;
        [SerializeField] private Enemy _enemy;

        public void OnEnable()
        {
            _enemy.Dead += _enemyBodyPartPositionSetter.SetPosition;
        }
    }
}