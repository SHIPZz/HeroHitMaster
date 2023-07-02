using System;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class EnemyPresenter : IInitializable, IDisposable
    {
        private Enemy _enemy;
        private EnemyView _enemyView;

        public EnemyPresenter(Enemy enemy, EnemyView enemyView)
        {
            _enemy = enemy;
            _enemyView = enemyView;
        }

        public void Initialize()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}