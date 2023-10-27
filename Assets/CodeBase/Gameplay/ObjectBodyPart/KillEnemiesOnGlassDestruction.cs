using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Collision;
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
        private Enemy _lastEnemy;

        private void Awake()
        {
            _destroyableObject = GetComponent<DestroyableObject>();
            _triggerObserver = GetComponent<TriggerObserver>();
        }

        private void OnEnable()
        {
            _destroyableObject.Destroyed += SetDestroyed;
            _enemyTrigger.Entered += SetEnemy;
            _triggerObserver.CollisionEntered += OnCollisionEntered;
        }

        private void OnDisable()
        {
            _enemyTrigger.Entered -= SetEnemy;
            _destroyableObject.Destroyed -= SetDestroyed;
            _triggerObserver.CollisionEntered -= OnCollisionEntered;
        }

        private void SetEnemy(Collider enemy) =>
            _lastEnemy = enemy.GetComponent<Enemy>();

        private void SetDestroyed(DestroyableObjectTypeId obj) =>
            _isDestroyed = true;

        private void OnCollisionEntered(UnityEngine.Collision collision)
        {
            if (!_isDestroyed)
                return;

            if (_lastEnemy is null) 
                return;
            
            _lastEnemy.GetComponent<NavMeshAgent>().enabled = false;
            _lastEnemy.GetComponent<Rigidbody>().isKinematic = false;
            DOTween.Sequence().AppendInterval(2f).OnComplete(_lastEnemy.Explode);
        }
    }
}