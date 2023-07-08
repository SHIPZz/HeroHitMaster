using DG.Tweening;
using Gameplay.Bullet;
using Gameplay.Web;
using UnityEngine;
using Zenject;

namespace Gameplay.Weapon
{
    public class WebShooter : Weapon
    {
        public override void Initialize()
        {
            BulletsCount = 50;
            BulletMoveDuration = 0.3f;
            ReturnBulletDelay = 10f;
            BulletFactory.CreateBulletsBy(WeaponTypeId, transform, BulletsCount);
            BulletMovement = new WebMovement();
        }

        public override void Shoot(Vector3 target, Vector3 initialPosition)
        {
            IBullet bullet = BulletFactory.Pop();
            BulletMovement.Move(target, bullet, initialPosition, BulletMoveDuration,bullet.Rigidbody);
            Debug.Log("shoot");
            // EffectOnShoot.PlayEffects();
            DOTween.Sequence().AppendInterval(ReturnBulletDelay).OnComplete(() => BulletFactory.Push(bullet));
        }
    }
}