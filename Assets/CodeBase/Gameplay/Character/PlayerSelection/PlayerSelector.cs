using CodeBase.Enums;
using CodeBase.Services.Storages.Character;
using UnityEngine;

namespace CodeBase.Gameplay.Character.PlayerSelection
{
    public class PlayerSelector
    {
        private readonly IPlayerStorage _playerStorage;

        public PlayerSelector(IPlayerStorage playerStorage) => 
            _playerStorage = playerStorage;

        public void Select(WeaponTypeId weaponTypeId)
        {
            var lastPlayer = _playerStorage.CurrentPlayer;

            if (lastPlayer == _playerStorage.GetByWeapon(weaponTypeId))
                return;
            
            _playerStorage.GetByWeapon(weaponTypeId);
        }
    }
}