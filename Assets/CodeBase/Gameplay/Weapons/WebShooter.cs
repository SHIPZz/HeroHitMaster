using CodeBase.Gameplay.Bullet;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Weapons
{
    public class WebShooter : Weapon
    {
        public override void Initialize()
        {
            ReturnBulletDelay = 10f;
            Init(WeaponTypeId, null, 50, BulletMovement);
        }

        public override void Shoot(Vector3 target, Vector3 initialPosition)
        {
            IBullet bullet = BulletFactory.Pop();
            BulletMovement.Move(target, bullet, initialPosition,bullet.Rigidbody);
            DOTween.Sequence().AppendInterval(ReturnBulletDelay).OnComplete(() =>
            {
                bullet.GameObject.transform.DOLocalRotate(Vector3.zero, 0f);
                BulletFactory.Push(bullet);
            });
            
        }
    }
}