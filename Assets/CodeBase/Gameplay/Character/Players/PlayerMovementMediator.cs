using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Players
{
    public class PlayerMovementMediator : MonoBehaviour
    {
        private List<EnemyQuantityInZone> _list;
        private PlayerMovement _playerMovement;

        [Inject]
        private void Construct(EnemyQuantityZonesProvider enemyQuantityZonesProvider, PlayerMovement playerMovement)
        {
            _list =  enemyQuantityZonesProvider.EnemyQuantityInZones;
            _playerMovement = playerMovement;
        }

        private void Awake() => 
            _playerMovement = GetComponent<PlayerMovement>();

        public void OnEnable() => 
            _list.ForEach(x => x.ZoneCleared += _playerMovement.MoveToNextZone);

        public void OnDisable() => 
            _list.ForEach(x => x.ZoneCleared -= _playerMovement.MoveToNextZone);
    }
}