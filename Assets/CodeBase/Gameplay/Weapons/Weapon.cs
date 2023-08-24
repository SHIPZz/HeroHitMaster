using CodeBase.Enums;
using CodeBase.Gameplay.Bullet;
using CodeBase.Services.Storages.Bullet;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Weapons
{
    public class Weapon : MonoBehaviour, IInitializable
    {
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
            Init(WeaponTypeId);
        }

        public virtual void Shoot(Vector3 target, Vector3 initialPosition)
        {
            var bullet = _bulletStorage.Pop(WeaponTypeId);
            bullet.StartMovement(target, initialPosition);
            DOTween.Sequence().AppendInterval(ReturnBulletDelay).OnComplete(() => _bulletStorage.Push(bullet));
        }

        protected void Init(WeaponTypeId weaponTypeId)
        { ;
            _bulletStorage.CreateBulletsBy(weaponTypeId);
        }
    }
}