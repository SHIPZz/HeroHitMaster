using System;
using System.Collections.Generic;
using System.Threading;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Collision;
using CodeBase.Gameplay.Spawners;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    [RequireComponent(typeof(DestroyableObject))]
    public class KillEnemiesOnGlassDestruction : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _enemyTrigger;
        [SerializeField] private float _explodeDelay = 2f;
        [SerializeField] private float _killDelay = 1.5f;
        [SerializeField] private List<EnemySpawner> _enemySpawners;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inActiveColor;

        private MeshRenderer _meshRenderer;
        private Collider _collider;
        private DestroyableObject _destroyableObject;
        private bool _isDestroyed;
        private List<Enemy> _enemiesOnGlass = new();
        private CancellationTokenSource _cancellationToken;
        private bool _isExploded;

        private void Awake()
        {
            _destroyableObject = GetComponent<DestroyableObject>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.sharedMaterial.color = _inActiveColor;
            _collider = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            _destroyableObject.Destroyed += SetDestroyed;
            _enemyTrigger.Entered += SetEnemy;
            _enemyTrigger.Exited += RemoveEnemy;
            _collider.enabled = false;
        }

        private void OnDisable()
        {
            _enemyTrigger.Entered -= SetEnemy;
            _enemyTrigger.Exited -= RemoveEnemy;
            _destroyableObject.Destroyed -= SetDestroyed;
        }

        private void SetDestroyed(DestroyableObjectTypeId obj)
        {
            _isDestroyed = true;
            _collider.enabled = false;
            Kill();
        }

        private void SetEnemy(Collider collider)
        {
            if (_isDestroyed)
                return;

            if (collider.gameObject.TryGetComponent(out Enemy enemy) && !_enemiesOnGlass.Contains(enemy))
                _enemiesOnGlass.Add(enemy);

            if (collider.gameObject.TryGetComponent(out EnemyPartForKnifeHolder enemyPartForKnifeHolder))
            {
                if (!_enemiesOnGlass.Contains(enemyPartForKnifeHolder.Enemy))
                    _enemiesOnGlass.Add(enemyPartForKnifeHolder.Enemy);
            }

            if (_enemiesOnGlass.Count >= _enemySpawners.Count)
            {
                _meshRenderer.sharedMaterial.DOColor(_activeColor, 0.5f);
                if (!_collider.enabled)
                    _collider.enabled = true;
            }
        }

        private async void Kill()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_killDelay));

            foreach (Enemy activeEnemy in _enemiesOnGlass)
            {
                var navmeshAgent = activeEnemy.GetComponent<NavMeshAgent>();
                _collider.enabled = false;

                if (!activeEnemy.gameObject.TryGetComponent(out Rigidbody rigidbody))
                {
                    activeEnemy.gameObject.AddComponent<Rigidbody>();
                }

                var enemyRb = activeEnemy.GetComponent<Rigidbody>();
                enemyRb.interpolation = RigidbodyInterpolation.Interpolate;
                navmeshAgent.updatePosition = false;
                navmeshAgent.enabled = false;
                _collider.enabled = false;
                enemyRb.AddForce(Vector3.down * 10f, ForceMode.Impulse);

                enemyRb.DOMoveY(Vector3.down.y * 3f, 1).OnComplete(activeEnemy.Explode);
            }
        }


        private void RemoveEnemy(Collider enemy)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();

            if (enemyComponent != null)
                _enemiesOnGlass.Remove(enemyComponent);
        }
    }
}