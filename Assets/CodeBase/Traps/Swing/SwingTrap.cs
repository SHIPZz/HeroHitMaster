using CodeBase.Gameplay.Character.Enemy;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Traps.Swing
{
    public class SwingTrap : Trap
    {
        private const float KillDelay = 0.3f;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private float _force = 20f;
        
        private static readonly int _isSwung = Animator.StringToHash("IsSwung");
        private bool _isPushed;

        public override void Activate()
        {
            _animator.SetBool(_isSwung, true);
            DOTween.Sequence().AppendInterval(DisableDelay).OnComplete(() => _animator.SetBool(_isSwung, false));
        }

        protected override void Kill(Collider collider)
        {
            if (!collider.gameObject.TryGetComponent(out Enemy enemy))
                return;
        
            enemy.GetComponent<EnemyFollower>().Block();
            Vector3 pushDirection = (enemy.transform.position - transform.position).normalized;
            enemy.gameObject.AddComponent<Rigidbody>();
            PushEnemy(enemy, pushDirection);
            DOTween.Sequence().AppendInterval(KillDelay).OnComplete(enemy.Explode);
        }

        private void PushEnemy(Enemy enemy, Vector3 pushDirection)
        {
            var enemyRigidBody = enemy.GetComponent<Rigidbody>();
            enemyRigidBody.interpolation = RigidbodyInterpolation.Interpolate;
            enemyRigidBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            enemyRigidBody.AddForce(pushDirection * _force, ForceMode.Impulse);
        }
    }
}