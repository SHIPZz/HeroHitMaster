using System;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Spawners;
using CodeBase.Services.Providers;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

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

        private void Set(Enemy enemy) => 
            enemy.transform.rotation = Quaternion.Euler(_target.position);
    }
}