using System.Collections.Generic;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Providers;
using Cysharp.Threading.Tasks;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class KillActiveEnemiesOnPlayerRecover
    {

        private List<Enemy> _enemies = new();
        private PlayerProvider _playerProvider;
        private PlayerHealth _playerHealth;

        public KillActiveEnemiesOnPlayerRecover(IProvider<PlayerProvider> provider)
        {
            _playerProvider = provider.Get();
            _playerProvider.Changed += SetPlayer;
        }

        public void Init(Enemy enemy) => 
            _enemies.Add(enemy);

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
    }
}