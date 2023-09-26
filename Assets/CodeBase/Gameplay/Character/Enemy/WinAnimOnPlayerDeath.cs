using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Storages;
using CodeBase.Services.Storages.Character;
using DG.Tweening;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class WinAnimOnPlayerDeath : IInitializable, IDisposable
    {
        private readonly EnemyAnimator _enemyAnimator;
        private readonly List<PlayerHealth> _playerHealths = new();
        private readonly Enemy _enemy;

        public WinAnimOnPlayerDeath(IPlayerStorage playerStorage, EnemyAnimator enemyAnimator, Enemy enemy)
        {
            _enemy = enemy;
            playerStorage.GetAll().ForEach(x => _playerHealths.Add(x.GetComponent<PlayerHealth>()));
            _enemyAnimator = enemyAnimator;
        }

        public void Initialize() =>
            _playerHealths.ForEach(x => x.ValueZeroReached += PlayWinAnimation);

        public void Dispose() =>
            _playerHealths.ForEach(x => x.ValueZeroReached -= PlayWinAnimation);

        private void PlayWinAnimation()
        {
            if(!_enemy.gameObject.activeSelf)
                return;
            
            DOTween.Sequence()
                .AppendInterval(0.5f)
                .OnComplete(() => _enemyAnimator.SetVictory());
        }
    }
}