using System;
using Enums;
using Services.Factories;
using Services.Providers;
using UnityEngine;

namespace Gameplay.PlayerSelection
{
    public class PlayerSelector
    {
        private readonly PlayerProvider _playerProvider;
        private readonly PlayerFactory _playerFactory;
        private PlayerTypeId _playerTypeId;
        private  int _currentPlayerId;

        public event Action<PlayerTypeId> PlayerChanged;
        public event Action<PlayerTypeId> PlayerSelected;

        public PlayerSelector(PlayerProvider playerProvider, PlayerFactory playerFactory)
        {
            _playerProvider = playerProvider;
            _playerFactory = playerFactory;
        }

        public void SelectNextPlayer()
        {
            _currentPlayerId++;

            if (_currentPlayerId >= _playerProvider.PlayerConfigs.Count)
                _currentPlayerId = 0;
            
            PlayerChanged?.Invoke((PlayerTypeId)_currentPlayerId);
        }

        public void SelectPreviousPlayer()
        {
            _currentPlayerId--;

            if (_currentPlayerId < 0)
                _currentPlayerId = _playerProvider.PlayerConfigs.Count - 1;
            
            PlayerChanged?.Invoke((PlayerTypeId)_currentPlayerId);
        }

        public void ApplyPlayer()
        {
            _playerTypeId = (PlayerTypeId)_currentPlayerId;
            PlayerSelected?.Invoke(_playerTypeId);
        }
    }
}