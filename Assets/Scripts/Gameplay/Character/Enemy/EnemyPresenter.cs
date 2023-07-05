using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class EnemyPresenter : IInitializable, IDisposable
    {
        private readonly Enemy _enemy;
        private readonly EnemyView _enemyView;

        public EnemyPresenter(Enemy enemy, EnemyView enemyView)
        {
            _enemy = enemy;
            _enemyView = enemyView;
        }

        public void Initialize()
        {
            Debug.Log("dead");
            _enemy.Dead += _enemyView.ShowDeath;
        }

        public void Dispose()
        {
            _enemy.Dead -= _enemyView.ShowDeath;
        }
    }
}