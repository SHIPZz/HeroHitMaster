using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Bullet;
using CodeBase.Services.Factories;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Weapons
{
    public class Weapon : MonoBehaviour, IInitializable
    {
        [SerializeReference] protected IBulletMovement BulletMovement;
        [field: SerializeField] public WeaponTypeId WeaponTypeId { get; protected set; }

        protected float ReturnBulletDelay = 15f;
        protected BulletStorage _bulletStorage;
        
        [Inject]
        private void Construct(BulletStorage bulletStorage)
        {
            _bulletStorage = bulletStorage;
        }

        public virtual void Initialize()
        {
            Init(WeaponTypeId, BulletMovement);
        }

        public virtual void Shoot(Vector3 target, Vector3 initialPosition)
        {
            IBullet bullet = _bulletStorage.Pop(WeaponTypeId);
            BulletMovement.Move(target, bullet, initialPosition, bullet.Rigidbody);
            DOTween.Sequence().AppendInterval(ReturnBulletDelay).OnComplete(() => _bulletStorage.Push(bullet));
        }

        protected void Init(WeaponTypeId weaponTypeId,
            IBulletMovement bulletMovement)
        {
            BulletMovement = bulletMovement;
            _bulletStorage.CreateBulletsBy(weaponTypeId);
        }
    }
}