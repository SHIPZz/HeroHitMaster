using System;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class AttackOnAnimationEvent : MonoBehaviour
    {
        private EnemyAttacker _enemyAttacker;

        [Inject]
        private void Construct(EnemyAttacker enemyAttacker) =>
            _enemyAttacker = enemyAttacker;

        public void OnAnimationAttacked()
        {
            _enemyAttacker.Attack();
        }
    }
}