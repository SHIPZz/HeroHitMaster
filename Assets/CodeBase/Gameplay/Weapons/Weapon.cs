using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Bullet;
using CodeBase.Services.Factories;
using CodeBase.Services.Storages;
using CodeBase.Services.Storages.Bullet;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Weapons
{
    public class Weapon : MonoBehaviour, IInitializable
    {
        protected IBulletMovement BulletMovement;
        [field: SerializeField] public WeaponTypeId WeaponTypeId { get; protected set; }

        protected float ReturnBulletDelay = 15f;
        protected BulletStorage _bulletStorage;
        protected BulletMovementStorage BulletmovementStorage;
        
        [Inject]
        private void Construct(BulletStorage bulletStorage, BulletMovementStorage bulletMovementStorage)
        {
            _bulletStorage = bulletStorage;
            BulletmovementStorage = bulletMovementStorage;
        }
        
        public virtual void Initialize()
        {
            Init(WeaponTypeId, BulletmovementStorage.GetBulletMovementBy(WeaponTypeId));
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