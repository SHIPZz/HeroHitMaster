using System;
using CodeBase.Enums;
using CodeBase.Gameplay.ObjectBodyPart;
using UnityEngine;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class Enemy : MonoBehaviour, IDestroyable
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

        public void Destroy()
        {
            gameObject.SetActive(false);
            QuickDestroyed?.Invoke(this);
        }

        private void DisableEvents()
        {
            Dead = null;
            QuickDestroyed = null;
        }
    }
}