using System;
using CodeBase.Enums;
using CodeBase.Gameplay.ObjectBodyPart;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class Enemy : MonoBehaviour, IExplodable
    {
        [field: SerializeField] public EnemyTypeId EnemyTypeId { get; private set; }

        public event Action<Enemy> Dead;
        public event Action<Enemy> QuickDestroyed;

        private void Awake()
        {
            Application.quitting += DisableEvents;
        }

        private void OnDestroy()
        {
            Dead?.Invoke(this);
            Application.quitting -= DisableEvents;
        }

        public void Explode()
        {
            QuickDestroyed?.Invoke(this);
            gameObject.SetActive(false);
        }

        private void DisableEvents()
        {
            Dead = null;
            QuickDestroyed = null;
        }
    }
}