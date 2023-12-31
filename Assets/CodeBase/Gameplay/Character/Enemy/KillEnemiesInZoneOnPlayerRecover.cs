﻿using System.Collections.Generic;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Spawners;
using CodeBase.Services.Providers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class KillEnemiesInZoneOnPlayerRecover : MonoBehaviour
    {
        [SerializeField] private List<EnemySpawner> _enemySpawners;

        private List<Enemy> _enemies = new();
        private PlayerProvider _playerProvider;
        private PlayerHealth _playerHealth;

        [Inject]
        private void Construct(IProvider<PlayerProvider> provider)
        {
            _playerProvider = provider.Get();
            _playerProvider.Changed += SetPlayer;
        }

        private void OnEnable() => 
            _enemySpawners.ForEach(x => x.Spawned += FillList);

        private void OnDisable() => 
            _enemySpawners.ForEach(x => x.Spawned -= FillList);

        private void SetPlayer(Player player)
        {
            _playerHealth = player.GetComponent<PlayerHealth>();
            _playerHealth.Recovered += Kill;
        }

        private async void Kill(int obj)
        {
            while (!_playerHealth.gameObject.activeSelf)
            {
                await UniTask.Yield();  
            }
        
            foreach (Enemy enemy in _enemies)
            {
                if (enemy.gameObject.activeSelf)
                {
                    enemy.Explode();
                }
            }
        }

        private void FillList(Enemy enemy) => 
            _enemies.Add(enemy);
    }
}