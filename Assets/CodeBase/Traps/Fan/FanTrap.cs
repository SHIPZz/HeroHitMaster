using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Services.Storages.Sound;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Traps.Fan
{
    public class FanTrap : Trap
    {
        [SerializeField] private float _killEnemyDelay = 1f;
        [SerializeField] private float _force = 5;
        [SerializeField] private float _rotationForce = 4f;
        [SerializeField] private Animator _animator;
        [SerializeField] private Vector3 _direction;
        [SerializeField] private Vector3 _rotateDirection;
        [SerializeField] private bool _freezePositionX;
        [SerializeField] private bool _freezePositionY;
        [SerializeField] private bool _freezePositionZ;

        private static readonly int _isFanned = Animator.StringToHash("IsFanned");

        private Coroutine _rotateCoroutine;
        private Coroutine _moveCoroutine;
        private AudioSource _blowSound;
        private List<Enemy> _killedEnemies = new();


        [Inject]
        private void Construct(ISoundStorage soundStorage)
        {
            _blowSound = soundStorage.Get(SoundTypeId.Blow);
        }
        
        public override void Activate() =>
            Collider.enabled = true;

        protected override void Awake()
        {
            base.Awake();
            Collider.enabled = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                Activate();
        }

        protected override void Kill(Collider collider)
        {
            if (!collider.gameObject.TryGetComponent(out Enemy enemy))
                return;
            
            if (_killedEnemies.Contains(enemy))
                return;

            _animator.SetBool(_isFanned, true);
            _blowSound.Play();
            _killedEnemies.Add(enemy);
            BlowEnemy(enemy);
            DOTween.Sequence().AppendInterval(_killEnemyDelay).OnComplete(enemy.Explode);
            AutoDisable();
        }

        private IEnumerator MoveUpEnemyCoroutine(Rigidbody enemyRigidbody)
        {
            while (enemyRigidbody.velocity != _direction * _force)
            {
                enemyRigidbody.velocity =
                    Vector3.Lerp(enemyRigidbody.velocity, _direction * _force, 8 * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator RotateEnemyCoroutine(Rigidbody enemyRigidbody)
        {
            float timeStep = 0f;

            while (Math.Abs(timeStep - 10f) > 0.1f)
            {
                enemyRigidbody.angularVelocity = Vector3.Lerp(enemyRigidbody.angularVelocity,
                    _rotateDirection * _rotationForce, 8 * Time.deltaTime);
                timeStep += 0.1f;
                yield return new WaitForFixedUpdate();
            }
        }

        private void BlowEnemy(Enemy enemy)
        {
            enemy.GetComponent<NavMeshAgent>().enabled = false;
            enemy.GetComponent<EnemyFollower>().Block();
            enemy.gameObject.AddComponent<Rigidbody>();
            var enemyRigidbody = enemy.GetComponent<Rigidbody>();
            enemyRigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            CheckOnSetConstraints(enemyRigidbody);

            if (_moveCoroutine is not null)
                StopCoroutine(_moveCoroutine);
            
            if(_rotateCoroutine is not null)
                StopCoroutine(_rotateCoroutine);
            
            _moveCoroutine = StartCoroutine(MoveUpEnemyCoroutine(enemyRigidbody));
            _rotateCoroutine = StartCoroutine(RotateEnemyCoroutine(enemyRigidbody));
        }

        private void CheckOnSetConstraints(Rigidbody enemyRigidbody)
        {
            if (_freezePositionX)
                SetConstraint(enemyRigidbody, RigidbodyConstraints.FreezePositionX);

            if (_freezePositionZ)
                SetConstraint(enemyRigidbody, RigidbodyConstraints.FreezePositionZ);

            if (_freezePositionY)
                SetConstraint(enemyRigidbody, RigidbodyConstraints.FreezePositionY);
        }

        private void SetConstraint(Rigidbody enemyRigidbody, RigidbodyConstraints rigidbodyConstraints) =>
            enemyRigidbody.constraints = rigidbodyConstraints;

        private void AutoDisable() =>
            DOTween.Sequence().AppendInterval(DisableDelay).OnComplete(() =>
            {
                Collider.enabled = false;
                _animator.SetBool(_isFanned, false);
            });
    }
}