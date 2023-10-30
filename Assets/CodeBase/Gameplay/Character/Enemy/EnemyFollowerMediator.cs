using System;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Providers;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EnemyFollowerMediator : IInitializable, IDisposable
    {
        private readonly EnemyFollower _enemyFollower;
        private readonly PlayerProvider _playerProvider;
        private PlayerHealth _playerHealth;

        public EnemyFollowerMediator(EnemyFollower enemyFollower, IProvider<PlayerProvider> provider)
        {
            _enemyFollower = enemyFollower;
            _playerProvider = provider.Get();
        }

        public void Initialize() => 
            _playerProvider.Changed += SetPlayer;

        public void Dispose()
        {
            _playerProvider.Changed -= SetPlayer;
            _playerHealth.ValueZeroReached -= _enemyFollower.Block;
            _playerHealth.Recovered -= OnPlayerRecovered;
        }

        private void SetPlayer(Player player)
        {
            _playerHealth = player.GetComponent<PlayerHealth>();
            _playerHealth.ValueZeroReached += _enemyFollower.Block;
            _playerHealth.Recovered += OnPlayerRecovered;
        }

        private void OnPlayerRecovered(int obj) => 
            _enemyFollower.Unblock();
    }
}