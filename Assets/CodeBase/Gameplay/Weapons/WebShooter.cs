using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Weapons
{
    public class WebShooter : Weapon
    {
        public override async void Initialize()
        {
            ReturnBulletDelay = 10f;
            await Init(WeaponTypeId);
        }

        public override void Shoot(Vector3 target, Vector3 initialPosition)
        {
            Bullet.Bullet bullet = _bulletStorage.Pop(WeaponTypeId);
            bullet.StartMovement(target, initialPosition);

            DOTween.Sequence().AppendInterval(ReturnBulletDelay).OnComplete(() => _bulletStorage.Push(bullet));
        }
    }
}