using DG.Tweening;
using Enums;
using Gameplay.Bullet;
using Gameplay.Character.Player.Shoot;
using Services.Factories;
using UnityEngine;
using Zenject;

namespace Gameplay.Weapon
{
    public abstract class Weapon : MonoBehaviour, IInitializable, ITickable
    {
        protected float ReturnBulletDelay = 1.5f;
        protected float BulletMoveDuration = 0.2f;
        protected int BulletsCount = 30;

        [field: SerializeField] public WeaponTypeId WeaponTypeId { get; protected set; }

        protected IBulletMovement BulletMovement;

        protected BulletFactory BulletFactory;

        [Inject]
        private void Construct(BulletFactory bulletFactory)
        {
            BulletFactory = bulletFactory;
        }

        public virtual void Shoot(Vector3 target, Vector3 initialPosition)
        {
            IBullet bullet = BulletFactory.Pop();
            BulletMovement.Move(target, bullet, initialPosition, BulletMoveDuration,bullet.Rigidbody);
            DOTween.Sequence().AppendInterval(ReturnBulletDelay).OnComplete(() => BulletFactory.Push(bullet));
        }
        
        public virtual void Initialize()
        {
            BulletFactory.CreateBulletsBy(WeaponTypeId, transform,BulletsCount);
            BulletMovement = new DefaultBulletMovement();
        }

        public virtual void Tick()
        {
        }
    }
}