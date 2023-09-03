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

        private bool _isExploded;
        
        public event Action<Enemy> Dead;
        public event Action<Enemy> QuickDestroyed;

        private void Awake() => 
            Application.quitting += DisableEvents;

        private void OnDestroy()
        {
            Application.quitting -= DisableEvents;
            
            if (_isExploded)
                return;
            
            Dead?.Invoke(this);
        }

        public void Explode()
        {
            if (_isExploded)
                return;
                
            _isExploded = true;
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