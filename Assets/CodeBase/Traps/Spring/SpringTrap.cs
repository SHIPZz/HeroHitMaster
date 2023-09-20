using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.Character.Enemy;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Traps.Spring
{
    public class SpringTrap : Trap
    {
        [SerializeField] private float _force = 3f;

        private static readonly int _isSprung = Animator.StringToHash("IsSprung");
        private Animator _animator;
        private bool _isKilled;

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        public void Spring() =>
            Collider.enabled = true;

        protected override void Kill(Collider collider)
        {
            if (!collider.gameObject.TryGetComponent(out Enemy enemy) || _isKilled)
                return;

            enemy.gameObject.AddComponent<Rigidbody>();
            ThrowEnemy(enemy);
            _animator.SetBool(_isSprung, true);
            AutoDisable();
            _isKilled = true;
        }

        private void ThrowEnemy(Enemy enemy)
        {
            var enemyRigidbody = enemy.GetComponent<Rigidbody>();
            enemy.GetComponent<NavMeshAgent>().enabled = false;
            enemyRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            enemyRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            enemyRigidbody.isKinematic = false;
            enemyRigidbody.AddForce(Vector3.up * _force, ForceMode.Impulse);
            DOTween.Sequence().AppendInterval(0.3f).OnComplete(enemy.Explode);
        }

        private void AutoDisable()
        {
            DOTween.Sequence().AppendInterval(1.5f).OnComplete(() =>
            {
                Collider.enabled = false;
                _animator.SetBool(_isSprung, false);
                _isKilled = false;
            });
        }
    }
}