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
        private const float TransferToPercents = 100f;
        private readonly PlayerProvider _playerProvider;
        private readonly IWorldDataService _worldDataService;

        private Player _player;
        private int _totalShots;
        private int _successfulShots;
        private ShootingOnAnimationEvent _playerShootingOnAnimationEvent;
        private List<Enemy> _enemies;

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
                enemy.GetComponent<EnemyHealth>().ValueChanged += SetSuccessfulShots;
            }
        }

        public void Dispose()
        {
            _playerShootingOnAnimationEvent.Shot -= CountTotalShots;

            foreach (var enemy in _enemies)
            {
                enemy.GetComponent<EnemyHealth>().ValueChanged -= SetSuccessfulShots;
            }
        }

        private void SetSuccessfulShots(int obj)
        {
            _successfulShots++;
            float accuracy = (float)_successfulShots / _totalShots * TransferToPercents;
            _worldDataService.WorldData.PlayerData.ShootAccuracy = Mathf.RoundToInt(accuracy);
        }

        private void SetPlayer(Player player)
        {
            _player = player;
            _playerShootingOnAnimationEvent = _player.GetComponent<ShootingOnAnimationEvent>();
            _playerShootingOnAnimationEvent.Shot += CountTotalShots;
        }

        private void CountTotalShots() =>
            _totalShots++;
    }
}