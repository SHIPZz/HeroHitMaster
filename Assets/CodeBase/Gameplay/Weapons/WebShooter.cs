using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Weapons
{
    public class WebShooter : Weapon
    {
        public override void Initialize()
        {
            ReturnBulletDelay = 10f;
            Init(WeaponTypeId);
        }

        public override void Shoot(Vector3 target, Vector3 initialPosition)
        {
            var bullet = _bulletStorage.Pop(WeaponTypeId);
            bullet.Move(target,initialPosition);
            
            DOTween.Sequence().AppendInterval(ReturnBulletDelay).OnComplete(() =>
            {
                bullet.transform.DOLocalRotate(Vector3.zero, 0f);
                _bulletStorage.Push(bullet);
            });
            
        }
    }
}