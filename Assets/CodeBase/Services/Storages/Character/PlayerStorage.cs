using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Players;
using CodeBase.ScriptableObjects.PlayerSettings;
using CodeBase.Services.Factories;
using CodeBase.Services.Providers;
using UnityEngine;

namespace CodeBase.Services.Storages.Character
{
    public class PlayerStorage : IPlayerStorage
    {
        private readonly PlayerProvider _playerProvider;
        private readonly PlayerFactory _playerFactory;
        private readonly LocationProvider _locationProvider;
        private readonly Dictionary<PlayerTypeId, Player> _players = new();
        private readonly Dictionary<WeaponTypeId, PlayerTypeId> _playerTypeIdsByWeapon;
        
        public PlayerStorage(PlayerFactory playerFactory, 
            LocationProvider locationProvider, 
            PlayerProvider playerProvider, PlayerSettings playerSettings)
        {
            _playerProvider = playerProvider;
            _playerFactory = playerFactory;
            _locationProvider = locationProvider;
            FillDictionary(playerSettings.PlayerTypeIds);
            _playerTypeIdsByWeapon = playerSettings.PlayerTypeIdsByWeapon;
        }

        public Player GetById(PlayerTypeId playerTypeId)
        {
            SetActive(false);
            Enable(playerTypeId);
            _playerProvider.CurrentPlayer = _players[playerTypeId];
            return _players[playerTypeId];
        }

        public Player GetByWeapon(WeaponTypeId weaponTypeId)
        {
            SetActive(false);
            PlayerTypeId playerTypeId = _playerTypeIdsByWeapon[weaponTypeId];
            Enable(playerTypeId);
            _playerProvider.CurrentPlayer = _players[playerTypeId];
            return _players[playerTypeId];
        }

        public List<Player> GetAll()
        {
            var players = new List<Player>();

            foreach (var player in _players.Values)
            {
                players.Add(player);
            }

            return players;
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
                   Create(player, _locationProvider.Values[LocationTypeId.PlayerSpawnPoint].position);
                  _players[createdPlayer.PlayerTypeId] = createdPlayer;
            }
        }
    }
}