using System;
using CodeBase.Enums;
using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Gameplay.ObjectBodyPart;
using UnityEngine;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class Enemy : MonoBehaviour, IExplodable
    {
        [field: SerializeField] public EnemyTypeId EnemyTypeId { get; private set; }

        private IMaterialChanger _skinnedMaterialChanger;
        private bool _isExploded;
        private bool _isDead;

        public Vector3 InitialScale { get; private set; }
        public event Action<Enemy> Dead;
        public event Action<Enemy> QuickDestroyed;

        private void Awake()
        {
            _skinnedMaterialChanger = GetComponent<IMaterialChanger>();
            Application.quitting += DisableEvents;
            InitialScale = transform.localScale;
            transform.localScale = Vector3.zero;
        }

        private void OnEnable() =>
            _skinnedMaterialChanger.StartedChanged += DisableEvents;

        private void OnDisable()
        {
            _skinnedMaterialChanger.StartedChanged -= DisableEvents;
            Application.quitting -= DisableEvents;
            
            if (_isExploded || _isDead)
                return;

            _isDead = true;
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