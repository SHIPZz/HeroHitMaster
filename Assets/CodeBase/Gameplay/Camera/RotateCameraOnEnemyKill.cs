using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Spawners;
using CodeBase.Services.Providers;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Gameplay.Camera
{
    public class RotateCameraOnEnemyKill : IInitializable, IDisposable
    {
        private readonly UnityEngine.Camera _camera;
        private readonly List<EnemySpawner> _enemySpawners;
        private readonly List<EnemyQuantityInZone> _enemyQuantityZones;
        private readonly CameraZoomer _cameraZoomer;
        private readonly List<ExplosionBarrel.ExplosionBarrel> _explosionBarrels;
        private CameraData _cameraData;

        private bool _blockRotation;
        private Enemy _enemy;
        private Vector3 _lastEnemyPosition;

        public RotateCameraOnEnemyKill(IProvider<CameraData> cameraProvider,
            IProvider<List<EnemySpawner>> enemySpawnersProvider,
            EnemyQuantityZonesProvider enemyQuantityZonesProvider,
            CameraZoomer cameraZoomer, IProvider<List<ExplosionBarrel.ExplosionBarrel>> explosionBarrelProvider)
        {
            _cameraZoomer = cameraZoomer;
            _enemySpawners = enemySpawnersProvider.Get();
            _explosionBarrels = explosionBarrelProvider.Get();
            _enemyQuantityZones = enemyQuantityZonesProvider.EnemyQuantityInZones;
        }

        public void Init(CameraData cameraData) =>
            _cameraData = cameraData;

        public void Initialize()
        {
            _enemySpawners.ForEach(x => x.Disabled += SetLastKilledEnemy);
            _enemyQuantityZones.ForEach(x => x.ZoneCleared += Do);

            foreach (var explosionBarrel in _explosionBarrels)
            {
                if (explosionBarrel is null)
                    continue;

                explosionBarrel.Exploded += BlockRotation;
            }
        }

        public void Dispose()
        {
            _enemySpawners.ForEach(x => x.Disabled -= SetLastKilledEnemy);
            _enemyQuantityZones.ForEach(x => x.ZoneCleared -= Do);

            foreach (var explosionBarrel in _explosionBarrels)
            {
                if (explosionBarrel is null)
                    continue;

                explosionBarrel.Exploded -= BlockRotation;
            }
        }

        private void BlockRotation()
        {
            _blockRotation = true;
        }

        private async void Do()
        {
            while (_enemy is null)
            {
                await UniTask.Yield();
            }

            if (_blockRotation)
            {
                _cameraZoomer.Zoom(75, 1f, 0.5f, Ease.Linear);
                return;
            }

            float angle = Mathf.Atan2(_cameraData.transform.position.x, _lastEnemyPosition.z);

            _cameraZoomer.Zoom(37, 0.7f, 0.3f, Ease.Flash);

            _cameraData.transform.DORotate(new Vector3(0, angle, 0), 0.8f)
                .SetEase(Ease.Flash)
                .OnComplete(() => _cameraData.transform.DORotate(Vector3.zero, 0.6f)
                    .SetEase(Ease.Flash));
        }

        private void SetLastKilledEnemy(Enemy enemy)
        {
            _lastEnemyPosition = enemy.transform.position;
            _enemy = enemy;
        }
    }
}