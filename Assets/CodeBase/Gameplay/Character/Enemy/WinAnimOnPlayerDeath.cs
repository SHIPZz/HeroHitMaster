using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Storages;
using CodeBase.Services.Storages.Character;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class WinAnimOnPlayerDeath : IInitializable, IDisposable
    {
        private readonly EnemyAnimator _enemyAnimator;
        private readonly List<PlayerHealth> _playerHealths = new();

        public WinAnimOnPlayerDeath(IPlayerStorage playerStorage, EnemyAnimator enemyAnimator)
        {
            playerStorage.GetAll().ForEach(x => _playerHealths.Add(x.GetComponent<PlayerHealth>()));
            _enemyAnimator = enemyAnimator;
        }

        public void Initialize()
        {
            _playerHealths.ForEach(x => x.ValueZeroReached += PlayWinAnimation);
        }

        public void Dispose()
        {
            _playerHealths.ForEach(x => x.ValueZeroReached -= PlayWinAnimation);
        }

        private void PlayWinAnimation() => 
            _enemyAnimator.SetVictory();
    }
}