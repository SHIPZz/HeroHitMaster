using System.Collections.Generic;
using Enums;
using Gameplay.Character.Player;
using ScriptableObjects.PlayerSettings;
using Services.Factories;
using Services.Providers;

namespace Services.GameObjectsPoolAccess
{
    public class PlayerStorage
    {
        private readonly PlayerStorageByWeaponType _playerStorageByWeaponType;
        private readonly PlayerProvider _playerProvider;
        private readonly PlayerFactory _playerFactory;
        private readonly LocationProvider _locationProvider;
        private readonly Dictionary<PlayerTypeId, Player> _players = new();
        
        private IReadOnlyDictionary<PlayerTypeId, Player> Players => _players;

        public PlayerStorage(List<PlayerSettings> playerSettings,
            PlayerFactory playerFactory, 
            LocationProvider locationProvider, 
            PlayerStorageByWeaponType playerStorageByWeaponType, 
            PlayerProvider playerProvider)
        {
            _playerStorageByWeaponType = playerStorageByWeaponType;
            _playerProvider = playerProvider;
            _playerFactory = playerFactory;
            _locationProvider = locationProvider;
            FillDictionary(playerSettings);
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
            PlayerTypeId playerTypeId = _playerStorageByWeaponType.Get(weaponTypeId);
            Enable(playerTypeId);
            _playerProvider.CurrentPlayer = _players[playerTypeId];
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

        private void FillDictionary(List<PlayerSettings> playerSettingsList)
        {
            foreach (var player in playerSettingsList)
            {
                  Player createdPlayer = _playerFactory.
                   Create(player.PlayerTypeId, _locationProvider.PlayerSpawnPoint.position);
                  _players[createdPlayer.PlayerTypeId] = createdPlayer;
            }
        }
    }
}