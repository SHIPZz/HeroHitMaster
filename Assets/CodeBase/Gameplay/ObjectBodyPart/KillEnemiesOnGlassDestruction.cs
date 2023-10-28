using System.Collections.Generic;
using System.Threading;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Collision;
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

        private DestroyableObject _destroyableObject;
        private bool _isDestroyed;
        private List<Enemy> _enemiesOnGlass = new();
        private CancellationTokenSource _cancellationToken;

        private void Awake() =>
            _destroyableObject = GetComponent<DestroyableObject>();

        private void OnEnable()
        {
            _destroyableObject.Destroyed += SetDestroyed;
            _enemyTrigger.Entered += SetEnemy;
            _enemyTrigger.Exited += RemoveEnemy;
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
        }

        private async void SetEnemy(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out Enemy enemy))
                _enemiesOnGlass.Add(enemy);

            if (collider.gameObject.TryGetComponent(out EnemyPartForKnifeHolder enemyPartForKnifeHolder))
            {
                if (!_enemiesOnGlass.Contains(enemyPartForKnifeHolder.Enemy))
                    _enemiesOnGlass.Add(enemyPartForKnifeHolder.Enemy);
            }

            while (_isDestroyed == false)
            {
                await UniTask.Yield();
            }

            Kill();
        }

        private async void Kill()
        {
            await UniTask.WaitForSeconds(_killDelay);

            foreach (Enemy activeEnemy in _enemiesOnGlass)
            {
                var navmeshAgent = activeEnemy.GetComponent<NavMeshAgent>();

                if (!activeEnemy.gameObject.TryGetComponent(out Rigidbody rigidbody))
                    activeEnemy.gameObject.AddComponent<Rigidbody>();

                navmeshAgent.updatePosition = false;
                navmeshAgent.enabled = false;
                DOTween.Sequence().AppendInterval(_explodeDelay).OnComplete(activeEnemy.Explode);
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