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
        private readonly CameraZoomer _cameraZoomer;
        private readonly List<ExplosionBarrel.ExplosionBarrel> _explosionBarrels;
        private CameraData _cameraData;

        private bool _blockRotation;
        private Enemy _enemy;
        private Vector3 _lastEnemyPosition;
        private List<Enemy> _enemies = new();

        public RotateCameraOnEnemyKill(CameraZoomer cameraZoomer, IProvider<List<ExplosionBarrel.ExplosionBarrel>> explosionBarrelProvider)
        {
            _cameraZoomer = cameraZoomer;
            _explosionBarrels = explosionBarrelProvider.Get();
        }

        public void Init(CameraData cameraData)
        {
            _cameraData = cameraData;

            foreach (var enemy in _enemies)
            {
                enemy.Dead += Do;
                enemy.QuickDestroyed += Do;
                enemy.GetComponent<DieOnAnimationEvent>().Dead += Do;
            }
        }

        public void Initialize()
        {
            foreach (var explosionBarrel in _explosionBarrels)
            {
                if (explosionBarrel is null)
                    continue;

                explosionBarrel.Exploded += BlockRotation;
            }
        }

        public void Dispose()
        {
            foreach (var explosionBarrel in _explosionBarrels)
            {
                if (explosionBarrel is null)
                    continue;

                explosionBarrel.Exploded -= BlockRotation;
            }
        }

        public void FillList(Enemy enemy)
        {
            _enemies.Add(enemy);
        }

        private void BlockRotation()
        {
            _blockRotation = true;
        }

        private void Do(Enemy enemy)
        {
            _lastEnemyPosition = enemy.transform.position;

            if (_blockRotation)
            {
                _cameraZoomer.Zoom(75, 1f, 0.5f, Ease.Linear);
                return;
            }

            var directionToEnemy = _lastEnemyPosition - _cameraData.transform.position;
            directionToEnemy = directionToEnemy.normalized;
            
            float angle = Mathf.Atan2(directionToEnemy.x, directionToEnemy.z) * Mathf.Rad2Deg;
            
            _cameraZoomer.Zoom(37, 0.7f, 0.3f, Ease.Flash);

            _cameraData.transform.DORotate(new Vector3(0, angle, 0), 0.8f)
                .SetEase(Ease.Flash)
                .OnComplete(() => _cameraData.transform.DORotate(Vector3.zero, 0.6f)
                    .SetEase(Ease.Flash));
        }
    }
}