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
        [SerializeField] private float _disableEnemiesDelay = 0.5f;
        [SerializeField] private float _explodeDelay = 2f;

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
            GetComponent<Collider>().enabled = false;
            _isDestroyed = true;
        }

        private async void SetEnemy(Collider enemy)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();

            if (enemyComponent != null)
                _enemiesOnGlass.Add(enemyComponent);

            if (!_isDestroyed)
                return;
            
            await UniTask.WaitForSeconds(_disableEnemiesDelay);

            foreach (Enemy activeEnemy in _enemiesOnGlass)
            {
                var navmeshAgent = activeEnemy.GetComponent<NavMeshAgent>();
                var enemyRigidBody = activeEnemy.GetComponent<Rigidbody>();
                enemyRigidBody.isKinematic = false;
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