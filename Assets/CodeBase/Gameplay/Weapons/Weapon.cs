using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Bullet;
using CodeBase.Services.Storages.Bullet;
using Cysharp.Threading.Tasks;
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

        public bool Initialized { get; private set; }
        
        public event Action Shooted;

        
        [Inject]
        private void Construct(BulletStorage bulletStorage) => 
            _bulletStorage = bulletStorage;

        public virtual async void Initialize() => 
          await  Init(WeaponTypeId);

        public virtual void Shoot(Vector3 target, Vector3 initialPosition)
        {
            var bullet = _bulletStorage.Pop(WeaponTypeId);
            bullet.StartMovement(target, initialPosition);
            DOTween.Sequence().AppendInterval(ReturnBulletDelay).OnComplete(() => _bulletStorage.Push(bullet));
            Shooted?.Invoke();
        }

        protected async UniTask Init(WeaponTypeId weaponTypeId)
        {
            await  _bulletStorage.CreateBulletsBy(weaponTypeId);

            Initialized = true;
        }
    }
}