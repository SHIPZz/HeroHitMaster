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

        public event Action Shot;

        [Inject]
        private void Construct(BulletStorage bulletStorage) =>
            _bulletStorage = bulletStorage;

        public virtual async void Initialize() =>
            await Init(WeaponTypeId);

        public virtual void Shoot(Vector3 target, Vector3 initialPosition)
        {
            Bullet.Bullet bullet = _bulletStorage.Pop(WeaponTypeId);
            PrepareBullet(target, initialPosition, bullet);
            bullet.StartMovement(target, initialPosition);
            DOTween.Sequence().AppendInterval(ReturnBulletDelay).OnComplete(() => _bulletStorage.Push(bullet));
            Shot?.Invoke();
        }

        private void PrepareBullet(Vector3 target, Vector3 initialPosition, Bullet.Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            Vector3 moveDirection = target - initialPosition;
            moveDirection = moveDirection.normalized;
            bullet.transform.forward = moveDirection;
            DOTween.Sequence().AppendInterval(0.010f).OnComplete(() => bullet.gameObject.SetActive(true));
        }

        protected async UniTask Init(WeaponTypeId weaponTypeId)
        {
            await _bulletStorage.CreateBulletsBy(weaponTypeId);

            Initialized = true;
        }
    }
}