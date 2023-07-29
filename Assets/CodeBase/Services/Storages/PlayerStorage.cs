using System.Collections.Generic;
using CodeBase.Services.Providers;
using Enums;
using Gameplay.Character.Players;
using Services.Factories;
using Services.Providers;
using UnityEngine;

namespace CodeBase.Services.Storages
{
    public class PlayerStorage : IPlayerStorage
    {
        private readonly PlayerTypeIdStorageByWeaponType _playerTypeIdStorageByWeaponType;
        private readonly PlayerProvider _playerProvider;
        private readonly PlayerFactory _playerFactory;
        private readonly LocationProvider _locationProvider;
        private readonly Dictionary<PlayerTypeId, Player> _players = new();
        
        public PlayerStorage(PlayerFactory playerFactory, 
            LocationProvider locationProvider, 
            PlayerTypeIdStorageByWeaponType playerTypeIdStorageByWeaponType, 
            PlayerProvider playerProvider, List<PlayerTypeId> playerTypeIds)
        {
            _playerTypeIdStorageByWeaponType = playerTypeIdStorageByWeaponType;
            _playerProvider = playerProvider;
            _playerFactory = playerFactory;
            _locationProvider = locationProvider;
            FillDictionary(playerTypeIds);
        }

        public Player Get(PlayerTypeId playerTypeId)
        {
            SetActive(false);
            Enable(playerTypeId);
            _playerProvider.CurrentPlayer = _players[playerTypeId];
            return _players[playerTypeId];
        }

        public Player Get(WeaponTypeId weaponTypeId)
        {
            SetActive(false);
            PlayerTypeId playerTypeId = _playerTypeIdStorageByWeaponType.Get(weaponTypeId);
            Enable(playerTypeId);
            _playerProvider.CurrentPlayer = _players[playerTypeId];
            Debug.Log(_players[playerTypeId]);
            return _players[playerTypeId];
        }

        private void Enable(PlayerTypeId playerTypeId) =>
            _players[playerTypeId].gameObject.SetActive(true);

        private void SetActive(bool isActive)
        {
            foreach (var player in _players.Values)
            {
                player.gameObject.SetActive(isActive);
            }
        }

        private void FillDictionary(List<PlayerTypeId> playerTypeIds)
        {
            foreach (var player in playerTypeIds)
            {
                Player createdPlayer = _playerFactory.
                   Create(player, _locationProvider.PlayerSpawnPoint.position);
                  _players[createdPlayer.PlayerTypeId] = createdPlayer;
            }
        }
    }
}