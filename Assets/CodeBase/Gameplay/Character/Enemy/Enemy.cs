using System;
using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [field: SerializeField] public EnemyTypeId EnemyTypeId { get; private set; }

        public event Action<EnemyTypeId> Dead;

        private void OnDisable()
        {
            Dead?.Invoke(EnemyTypeId);
        }

        private void OnDestroy()
        {
            Dead?.Invoke(EnemyTypeId);
        }
    }
}