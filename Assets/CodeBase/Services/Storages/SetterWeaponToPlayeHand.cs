using System.Collections.Generic;
using CodeBase.Enums;
using Enums;
using ScriptableObjects.PlayerSettings;
using Services.Providers;
using Zenject;

namespace CodeBase.Services.Storages
{
    public class SetterWeaponToPlayerHand : IInitializable
    {
        private readonly Dictionary<PlayerTypeId, PlayerWithWeaponInHandTypeId> _playersWithWeaponInHand;
        private readonly WeaponProvider _weaponProvider;
        private readonly PlayerProvider _playerProvider;

        public SetterWeaponToPlayerHand(WeaponProvider weaponProvider, PlayerProvider playerProvider,
            PlayerSettings playerSettings)
        {
            _weaponProvider = weaponProvider;
            _playerProvider = playerProvider;
            _playersWithWeaponInHand = playerSettings.PlayerWithWeaponInHands;
        }

        public void Initialize()
        {
            if (!_playersWithWeaponInHand.TryGetValue(_playerProvider.CurrentPlayer.PlayerTypeId,
                    out var playerWithWeaponInd))
                return;

            var weaponViewStorage = _playerProvider.CurrentPlayer.GetComponentInChildren<WeaponViewStorage>();
            weaponViewStorage.Get(_weaponProvider.CurrentWeapon.WeaponTypeId);
        }
    }
}