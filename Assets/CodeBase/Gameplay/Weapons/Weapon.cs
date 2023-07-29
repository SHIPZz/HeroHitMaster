using System;
using CodeBase.Services.Factories;
using DG.Tweening;
using Enums;
using Gameplay.Bullet;
using Services.Factories;
using UnityEngine;
using Zenject;

namespace Gameplay.Weapons
{
    public class Weapon : MonoBehaviour, IInitializable
    {
        [SerializeReference] protected IBulletMovement BulletMovement;
        [field: SerializeField] public WeaponTypeId WeaponTypeId { get; protected set; }

        protected float ReturnBulletDelay = 15f;
        protected float BulletMoveDuration = 0.2f;
        protected int BulletsCount = 30;
        protected BulletFactory BulletFactory;

        public event Action Shooted;

        public GameObject GameObject => gameObject;

        [Inject]
        private void Construct(BulletFactory bulletFactory)
        {
            BulletFactory = bulletFactory;
        }

        public virtual void Initialize()
        {
            Init(WeaponTypeId, gameObject.transform, BulletsCount, BulletMovement);
        }

        public virtual void Shoot(Vector3 target, Vector3 initialPosition)
        {
            IBullet bullet = BulletFactory.Pop();
            BulletMovement.Move(target, bullet, initialPosition, bullet.Rigidbody);
            DOTween.Sequence().AppendInterval(ReturnBulletDelay).OnComplete(() => BulletFactory.Push(bullet));
            Shooted?.Invoke();
        }

        protected void Init(WeaponTypeId weaponTypeId, Transform parent, int bulletsCount,
            IBulletMovement bulletMovement)
        {
            BulletMovement = bulletMovement;
            BulletFactory.CreateBulletsBy(weaponTypeId,  bulletsCount);
        }
    }
}