using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Character.Players.Shoot;
using CodeBase.Services.Storages.Character;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class ActivateEnemyMovementOnShoot : IInitializable, IDisposable
    {
        private readonly EnemyFollower _enemyFollower;
        private readonly List<Player> _players;
        private bool _isShot;

        public ActivateEnemyMovementOnShoot(EnemyFollower enemyFollower, IPlayerStorage playerStorage)
        {
            _players = playerStorage.GetAll();
            _players.ForEach(x => x.GetComponent<ShootingOnAnimationEvent>().Shooted += ActivateMovement);
            _enemyFollower = enemyFollower;
        }

        public void Initialize()
        {
            _enemyFollower.Block();
        }

        public void Dispose()
        {
            foreach (var player in _players)
            {
                if (player == null || player.gameObject == null || !player.gameObject.activeSelf)
                    continue;

                player.GetComponent<ShootingOnAnimationEvent>().Shooted -= ActivateMovement;
            }
        }

        private void ActivateMovement()
        {
            // if (_isShot)
            //     return;
            //
            // _isShot = true;
            // _enemyFollower.Unblock();
        }
    }
}