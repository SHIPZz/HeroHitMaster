using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Bullet.DamageDealers;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.ObjectPool;
using CodeBase.Services.Providers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Camera
{
    public class CameraShakeMediator : IDisposable
    {
        private readonly IProvider<Dictionary<WeaponTypeId, GameObjectPool>> _bulletsPoolProvider;
        private readonly List<ExplosionBarrel.ExplosionBarrel> _explosionBarrels;
        private readonly WeaponProvider _weaponProvider;
        
        private readonly List<WeaponTypeId> _recoilWeapons = new()
        {
            { WeaponTypeId.BlueWeapon },
            { WeaponTypeId.GreenWeapon },
            { WeaponTypeId.OrangeWeapon },
        };
        
        private CameraShake _cameraShake;
        private IProvider<Weapon> _provider;
        private Weapon _weapon;
        private List<Enemy> _enemies = new();
        private bool _canShake = true;

        public CameraShakeMediator(IProvider<WeaponProvider> weaponProvider,
            IProvider<Dictionary<WeaponTypeId, GameObjectPool>> bulletsPoolProvider,
            IProvider<List<ExplosionBarrel.ExplosionBarrel>> barrelsProvider)
        {
            _bulletsPoolProvider = bulletsPoolProvider;
            _weaponProvider = weaponProvider.Get();
            _weaponProvider.Changed += SetWeapon;
            _explosionBarrels = barrelsProvider.Get();
            _explosionBarrels.ForEach(x => x.Exploded += MakeHardShake);
        }

        public void SetCamerShake(CameraShake cameraShake) =>
            _cameraShake = cameraShake;

        public void Dispose() =>
            _explosionBarrels.ForEach(x => x.Exploded -= MakeHardShake);

        public void InitEnemies(Enemy enemy)
        {
            enemy.Dead += MakeShakeAfterEnemyDeath;
            enemy.QuickDestroyed += MakeShakeAfterEnemyDeath;
            enemy.GetComponent<IMaterialChanger>().StartedChanged += BlockShake;
            _enemies.Add(enemy);
        }

        public async void Init()
        {
            while (!_weapon.Initialized)
            {
                await UniTask.Yield();
            }

            TrySubscribeOnRecoilWeaponShoot();

            TrySubscribeOnDestructionBulletKill();
        }

        private void BlockShake() => 
            _canShake = false;

        private void MakeShakeAfterEnemyDeath(Enemy obj)
        {
            if (!_canShake)
                return;
            
            Debug.Log("makeShake");
            MakeShake(0.5f);
        }

        private void MakeHardShake()
        {
            MakeShake(15);
            _canShake = false;
        }

        private void MakeShakeAfterDynamiteBulletKill()
        {
            MakeShake(3);
            _canShake = false;
        }

        private void MakeShake(float perlinNoiseTimeScale) =>
            _cameraShake.MakeShake(perlinNoiseTimeScale);

        private async void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
            
            while (!_weapon.Initialized)
            {
                await UniTask.Yield();
            }
            
            TrySubscribeOnRecoilWeaponShoot();
            TrySubscribeOnDestructionBulletKill();
        }

        private void TrySubscribeOnRecoilWeaponShoot()
        {
            if (_recoilWeapons.Contains(_weapon.WeaponTypeId))
                _weapon.Shooted += _cameraShake.MakeRecoil;
        }

        private void TrySubscribeOnDestructionBulletKill()
        {
            if (_weapon.WeaponTypeId != WeaponTypeId.ThrowingDynamiteShooter)
                return;

            List<GameObject> bullets = _bulletsPoolProvider.Get()[_weapon.WeaponTypeId].GetAll();

            foreach (GameObject gameObject in bullets)
            {
                var bullet = gameObject.GetComponent<DestructionDamageDealer>();
                bullet.Done += MakeShakeAfterDynamiteBulletKill;
            }
        }
    }
}