using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Character.Players.Shoot;
using CodeBase.Services.Storages.Character;
using CodeBase.UI.Windows.Play;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class ActivateEnemyMovementOnPlayButtonClick : IInitializable, IDisposable
    {
        private readonly EnemyFollower _enemyFollower;
        private readonly PlayButtonView _playButtonView;

        public ActivateEnemyMovementOnPlayButtonClick(EnemyFollower enemyFollower, PlayButtonView playButtonView)
        {
            _playButtonView = playButtonView;
            _enemyFollower = enemyFollower;
        }

        public void Initialize()
        {
            _enemyFollower.Block();
            _playButtonView.Clicked += ActivateMovement;
        }

        public void Dispose() => 
            _playButtonView.Clicked -= ActivateMovement;

        private void ActivateMovement() => 
        _enemyFollower.Unblock();
    }
}