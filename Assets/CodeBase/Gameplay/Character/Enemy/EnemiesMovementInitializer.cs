using CodeBase.Infrastructure;
using CodeBase.Services.Providers;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EnemiesMovementInitializer : IGameplayRunnable
    {
        private readonly IEnemyProvider _enemyProvider;

        public EnemiesMovementInitializer(IEnemyProvider enemyProvider) => 
            _enemyProvider = enemyProvider;

        public void Run() => 
            _enemyProvider.Enemies.ForEach(x =>x.GetComponent<EnemyFollower>().Unblock());
    }
}