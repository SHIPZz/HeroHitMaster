﻿using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Character.Players.Shoot;
using CodeBase.Services.Storages.Character;
using CodeBase.UI.Windows.Play;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EnemyMovementPresenter : IInitializable, IDisposable
    {
        private readonly EnemyFollower _enemyFollower;
        private readonly PlayButtonView _playButtonView;

        public EnemyMovementPresenter(EnemyFollower enemyFollower, PlayButtonView playButtonView)
        {
            _playButtonView = playButtonView;
            _enemyFollower = enemyFollower;
        }

        public void Initialize() =>
            _playButtonView.Clicked += _enemyFollower.Unblock;

        public void Dispose() =>
            _playButtonView.Clicked -= _enemyFollower.Unblock;
    }
}