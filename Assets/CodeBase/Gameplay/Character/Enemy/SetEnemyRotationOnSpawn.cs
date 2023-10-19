using CodeBase.Gameplay.Spawners;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Gameplay.Character.Enemy
{
    [RequireComponent(typeof(EnemySpawner))]
    public class SetEnemyRotationOnSpawn : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        
        private EnemySpawner _enemySpawner;

        private void Awake() => 
            _enemySpawner = GetComponent<EnemySpawner>();

        private void OnEnable() => 
            _enemySpawner.Spawned += Set;

        private void OnDisable() => 
            _enemySpawner.Spawned -= Set;

        private void Set(Enemy enemy)
        {
            var angle = Mathf.Atan2(_target.position.z, transform.position.x) * Mathf.Rad2Deg;
            enemy.transform.rotation = Quaternion.Euler(new Vector3(0, angle,0));
        }
    }
}