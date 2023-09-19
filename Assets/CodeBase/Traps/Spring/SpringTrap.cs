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

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        public void Spring() => 
            Collider.enabled = true;

        protected override void Kill(Collider collider)
        {
            if (!collider.gameObject.TryGetComponent(out Enemy enemy))
                return;

            ThrowEnemy(enemy);
            _animator.SetBool(_isSprung, true);
            AutoDisable();
        }

        private void ThrowEnemy(Enemy enemy)
        {
            var enemyRigidbody = enemy.GetComponent<Rigidbody>();
            enemy.GetComponent<NavMeshAgent>().enabled = false;
            enemyRigidbody.isKinematic = false;
            enemyRigidbody.AddForce(Vector3.up * _force, ForceMode.Impulse);
        }

        private void AutoDisable()
        {
            DOTween.Sequence().AppendInterval(1.5f).OnComplete(() =>
            {
                Collider.enabled = false;
                _animator.SetBool(_isSprung, false);
            });
        }
    }
}