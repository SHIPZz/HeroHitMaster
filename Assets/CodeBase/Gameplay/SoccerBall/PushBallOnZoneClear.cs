using CodeBase.Gameplay.Character.Enemy;
using UnityEngine;

namespace CodeBase.Gameplay.SoccerBall
{
    public class PushBallOnZoneClear : MonoBehaviour
    {
        [SerializeField] private EnemyQuantityInZone _enemyQuantityInZone;
        [SerializeField] private float _force = 1000;
        [SerializeField] private Vector3 _direction;

        private Rigidbody _rigidbody;

        private void Awake() => 
            _rigidbody = GetComponent<Rigidbody>();

        private void OnEnable() => 
            _enemyQuantityInZone.ZoneCleared += Push;

        private void OnDisable() => 
            _enemyQuantityInZone.ZoneCleared -= Push;

        private void Push() => 
            _rigidbody.AddForce(_direction * _force);
    }
}