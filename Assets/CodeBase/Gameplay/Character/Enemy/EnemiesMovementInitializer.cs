using CodeBase.Infrastructure;
using CodeBase.Services.Providers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EnemiesMovementInitializer : IGameplayRunnable
    {
        private readonly IEnemyProvider _enemyProvider;

        private bool _allEnemiesInitialized;

        public EnemiesMovementInitializer(IEnemyProvider enemyProvider) =>
            _enemyProvider = enemyProvider;

        public async void Run()
        {
            while (!_allEnemiesInitialized)
                await UniTask.Yield();

            _enemyProvider.Enemies.ForEach(x => x.GetComponent<EnemyFollower>().Unblock());
        }

        public void Init() =>
            _allEnemiesInitialized = true;
    }
}