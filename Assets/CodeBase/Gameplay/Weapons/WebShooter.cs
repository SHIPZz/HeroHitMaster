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
            Init(WeaponTypeId,  BulletMovementStorage.GetBulletMovementBy(WeaponTypeId));
        }

        public override void Shoot(Vector3 target, Vector3 initialPosition)
        {
            IBullet bullet = _bulletStorage.Pop(WeaponTypeId);
            BulletMovement.Move(target, bullet, initialPosition,bullet.Rigidbody);
            DOTween.Sequence().AppendInterval(ReturnBulletDelay).OnComplete(() =>
            {
                bullet.GameObject.transform.DOLocalRotate(Vector3.zero, 0f);
                _bulletStorage.Push(bullet);
            });
            
        }
    }
}