using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Spawners;
using CodeBase.Services.Providers;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Camera
{
    public class RotateCameraOnEnemyKill : IInitializable, IDisposable
    {
        private readonly UnityEngine.Camera _camera;
        private readonly List<EnemySpawner> _enemySpawners;
        private readonly List<EnemyQuantityInZone> _enemyQuantityZones;
        private readonly CameraZoomer _cameraZoomer;
        private readonly IProvider<CameraData> _cameraProvider;
        private readonly List<ExplosionBarrel.ExplosionBarrel> _explosionBarrels;

        private bool _blockRotation;
        private Enemy _enemy;

        public RotateCameraOnEnemyKill(IProvider<CameraData> cameraProvider,
            IProvider<List<EnemySpawner>> enemySpawnersProvider,
            EnemyQuantityZonesProvider enemyQuantityZonesProvider,
            CameraZoomer cameraZoomer, IProvider<List<ExplosionBarrel.ExplosionBarrel>> explosionBarrelProvider)
        {
            _cameraZoomer = cameraZoomer;
            _cameraProvider = cameraProvider;
            _enemySpawners = enemySpawnersProvider.Get();
            _explosionBarrels = explosionBarrelProvider.Get();
            _enemyQuantityZones = enemyQuantityZonesProvider.EnemyQuantityInZones;
        }

        public void Initialize()
        {
            _enemySpawners.ForEach(x => x.Disabled += SetLastKilledEnemy);
            _enemyQuantityZones.ForEach(x => x.ZoneCleared += Do);
            _explosionBarrels.ForEach(x => x.StartedExplode += BlockRotation);
        }

        public void Dispose()
        {
            _enemySpawners.ForEach(x => x.Disabled -= SetLastKilledEnemy);
            _enemyQuantityZones.ForEach(x => x.ZoneCleared -= Do);
            _explosionBarrels.ForEach(x => x.StartedExplode -= BlockRotation);
        }

        private void BlockRotation()
        {
            _blockRotation = true;
            Debug.Log("block rotation");
        }

        private async void Do()
        {
            while (_enemy is null)
            {
                await UniTask.Yield();
            }

            if (_blockRotation)
            {
                _cameraZoomer.Zoom(75, 1f, 0.5f,Ease.Linear);
                Debug.Log("block rotation");
                return;
            }

            float angle = Mathf.Atan2(_cameraProvider.Get().transform.position.x, _enemy.transform.position.z) *
                          Mathf.Rad2Deg;

            _cameraZoomer.Zoom(37, 0.7f, 0.3f, Ease.Flash);
            
            _cameraProvider.Get().transform.DORotate(new Vector3(0, angle, 0), 0.8f)
                .SetEase(Ease.Flash)
                .OnComplete(() => _cameraProvider.Get().transform.DORotate(new Vector3(0, 0, 0), 0.6f)
                    .SetEase(Ease.Flash));
        }

        private void SetLastKilledEnemy(Enemy enemy) =>
            _enemy = enemy;
    }
}