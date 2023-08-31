using System.Collections.Generic;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Character.Players.Shoot;
using CodeBase.Services.Storages.Character;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class ActivateEnemyMovementOnShoot : IInitializable
    {
        private readonly EnemyFollower _enemyFollower;
        private readonly List<Player> _players;
        private bool _isShooted;

        public ActivateEnemyMovementOnShoot(EnemyFollower enemyFollower, IPlayerStorage playerStorage)
        {
            _players = playerStorage.GetAll();
            _players.ForEach(x => x.GetComponent<ShootingOnAnimationEvent>().Shooted += ActivateMovement);
            _enemyFollower = enemyFollower;
        }

        public void Initialize() => 
            _enemyFollower.Block();

        private void ActivateMovement()
        {
            if (_isShooted)
                return;

            _isShooted = true;
            _enemyFollower.Unblock();
        }
    }
}