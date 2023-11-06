using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Services.Providers;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Camera
{
    public class RotateCameraOnLastEnemyKilled : IInitializable, IDisposable
    {
        private readonly UnityEngine.Camera _camera;
        private readonly CameraZoomer _cameraZoomer;
        private readonly List<ExplosionBarrel.ExplosionBarrel> _explosionBarrels;
        private CameraData _cameraData;

        private bool _blockRotation;
        private Enemy _enemy;
        private Vector3 _lastEnemyPosition;
        private List<Enemy> _enemies;
        private readonly List<EnemyQuantityInZone> _enemyQuantityZones;
        public bool IsRotating { get; private set; }


        public RotateCameraOnLastEnemyKilled(CameraZoomer cameraZoomer,
            IProvider<List<ExplosionBarrel.ExplosionBarrel>> explosionBarrelProvider,
            EnemyQuantityZonesProvider enemyQuantityZonesProvider)
        {
            _cameraZoomer = cameraZoomer;
            _enemyQuantityZones = enemyQuantityZonesProvider.EnemyQuantityInZones;
            _explosionBarrels = explosionBarrelProvider.Get();
        }

        public void Init(CameraData cameraData)
        {
            _cameraData = cameraData;

            foreach (var enemy in _enemies)
            {
                enemy.Dead += SetLastKilledEnemy;
                enemy.QuickDestroyed += SetLastKilledEnemy;
                enemy.GetComponent<DieOnAnimationEvent>().Dead += SetLastKilledEnemy;
            }
        }

        public void Initialize()
        {
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
            _enemyQuantityZones.ForEach(x => x.ZoneCleared -= Do);

            foreach (var explosionBarrel in _explosionBarrels)
            {
                if (explosionBarrel is null)
                    continue;

                explosionBarrel.Exploded -= BlockRotation;
            }
        }

        public void Init(List<Enemy> enemies) =>
            _enemies = enemies;

        private void SetLastKilledEnemy(Enemy enemy)
        {
            _lastEnemyPosition = enemy.transform.position;
        }

        private void BlockRotation() =>
            _blockRotation = true;

        private void Do()
        {
            if (_blockRotation)
            {
                _cameraZoomer.Zoom(75, 1f, 0.5f, Ease.Linear);
                return;
            }

            Vector3 directionToEnemy = _lastEnemyPosition - _cameraData.Camera.transform.localPosition;
            directionToEnemy = directionToEnemy.normalized;

            IsRotating = true;
            float angle = Mathf.Atan2(directionToEnemy.x, directionToEnemy.z) * Mathf.Rad2Deg;

            _cameraZoomer.Zoom(37, 0.7f, 0.3f, Ease.Flash);

            Vector3 lastEulerAngles = _cameraData.Camera.transform.localEulerAngles;
            
            _cameraData.Camera.transform.DOLocalRotate(new Vector3(lastEulerAngles.x, angle, lastEulerAngles.z), 0.8f)
                .SetEase(Ease.Flash)
                .OnComplete(() => _cameraData.Camera.transform.DOLocalRotate(lastEulerAngles, 0.6f)
                    .OnComplete(() => IsRotating = false)
                    .SetEase(Ease.Flash));
        }
    }
}