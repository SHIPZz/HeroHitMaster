using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using Zenject;

namespace CodeBase.Gameplay.Character.Players
{
    public class PlayerMovementMediator : IInitializable, IDisposable
    {
        private readonly List<EnemyQuantityInZone> _list;
        private readonly PlayerMovement _playerMovement;

        private PlayerMovementMediator(EnemyQuantityZonesProvider enemyQuantityZonesProvider, PlayerMovement playerMovement)
        {
            _list =  enemyQuantityZonesProvider.EnemyQuantityInZones;
            _playerMovement = playerMovement;
        }
        
        public void Initialize()
        {
            _list.ForEach(x => x.ZoneCleared += _playerMovement.MoveToNextZone);
        }

        public void Dispose()
        {
            _list.ForEach(x => x.ZoneCleared -= _playerMovement.MoveToNextZone);
        }
    }
}