using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Collision;
using CodeBase.Gameplay.MaterialChanger;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    [RequireComponent(typeof(TriggerObserver), typeof(DestroyableObject))]
    public class KillEnemiesOnGlassDestruction : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _enemyTrigger;

        private TriggerObserver _triggerObserver;
        private DestroyableObject _destroyableObject;
        private bool _isDestroyed;
        private List<Enemy> _enemiesOnGlass = new();

        private void Awake()
        {
            _destroyableObject = GetComponent<DestroyableObject>();
            _triggerObserver = GetComponent<TriggerObserver>();
        }

        private void OnEnable()
        {
            _destroyableObject.Destroyed += SetDestroyed;
            _enemyTrigger.Entered += SetEnemy;
            _enemyTrigger.Exited += RemoveEnemy; 
            _triggerObserver.CollisionEntered += OnCollisionEntered;
        }

        private void OnDisable()
        {
            _enemyTrigger.Entered -= SetEnemy;
            _enemyTrigger.Exited -= RemoveEnemy;
            _destroyableObject.Destroyed -= SetDestroyed;
            _triggerObserver.CollisionEntered -= OnCollisionEntered;
        }

        private void SetDestroyed(DestroyableObjectTypeId obj) => 
            _isDestroyed = true;

        private void SetEnemy(Collider enemy)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                _enemiesOnGlass.Add(enemyComponent);
            }
        }

        private void RemoveEnemy(Collider enemy)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            
            if (enemyComponent != null)
            {
                _enemiesOnGlass.Remove(enemyComponent);
            }
        }

        private void OnCollisionEntered(UnityEngine.Collision collision)
        {
            if (!_isDestroyed)
                return;
            
            foreach (Enemy enemy in _enemiesOnGlass)
            {
                enemy.GetComponent<NavMeshAgent>().enabled = false;
                enemy.GetComponent<Rigidbody>().isKinematic = false;
                DOTween.Sequence().AppendInterval(2f).OnComplete(enemy.Explode);
            }
        }
    }
}
