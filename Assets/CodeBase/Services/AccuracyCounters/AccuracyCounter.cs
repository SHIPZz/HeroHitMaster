using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Character.Players.Shoot;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.AccuracyCounters
{
    public class AccuracyCounter : IDisposable
    {
        private const float MaxDistance = 70f;
        private const float TransferToPercents = 100f;
        private readonly PlayerProvider _playerProvider;

        private Player _player;
        private int _totalShots;
        private int _successfulShots;
        private ShootingOnAnimationEvent _playerShootingOnAnimatonEvent;
        private List<Enemy> _enemies;
        private IWorldDataService _worldDataService;

        public AccuracyCounter(IProvider<PlayerProvider> playerProvider, IWorldDataService worldDataService)
        {
            _worldDataService = worldDataService;
            _playerProvider = playerProvider.Get();
            _playerProvider.Changed += SetPlayer;
        }

        public void Init(List<Enemy> enemies)
        {
            _enemies = enemies;

            foreach (var enemy in _enemies)
            {
                enemy.GetComponent<EnemyHealth>().ValueChanged += SetSuccesfulShots;
            }
        }

        public void Dispose()
        {
            _playerShootingOnAnimatonEvent.Shot -= CountTotalShots;

            foreach (var enemy in _enemies)
            {
                enemy.GetComponent<EnemyHealth>().ValueChanged -= SetSuccesfulShots;
            }
        }

        private void SetSuccesfulShots(int obj)
        {
            _successfulShots++;
            float accuracy = (float)_successfulShots / _totalShots * TransferToPercents;
            Debug.Log(Mathf.RoundToInt(accuracy));
            // _worldDataService.WorldData.PlayerData.ShootAccuracy = Mathf.RoundToInt(accuracy);
        }

        private void SetPlayer(Player player)
        {
            _player = player;
            _playerShootingOnAnimatonEvent = _player.GetComponent<ShootingOnAnimationEvent>();
            _playerShootingOnAnimatonEvent.Shot += CountTotalShots;
        }

        private void CountTotalShots() =>
            _totalShots++;
    }
}