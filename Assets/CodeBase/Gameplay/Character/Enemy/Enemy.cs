using System;
using CodeBase.Enums;
using CodeBase.Gameplay.ObjectBodyPart;
using UnityEngine;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class Enemy : MonoBehaviour, IDestroyable
    {
        [field: SerializeField] public EnemyTypeId EnemyTypeId { get; private set; }

        public event Action<EnemyTypeId> Dead;
        public event Action<Enemy> QuickDestroyed;

        private void OnDisable()
        {
            Dead?.Invoke(EnemyTypeId);
        }

        private void OnDestroy()
        {
            Dead?.Invoke(EnemyTypeId);
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
            QuickDestroyed?.Invoke(this);
        }
    }
}