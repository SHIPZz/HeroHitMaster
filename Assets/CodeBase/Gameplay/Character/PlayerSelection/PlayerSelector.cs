﻿using CodeBase.Enums;
using CodeBase.Services.Storages;
using CodeBase.Services.Storages.Character;
using UnityEngine;

namespace CodeBase.Gameplay.Character.PlayerSelection
{
    public class PlayerSelector
    {
        private readonly IPlayerStorage _playerStorage;

        public PlayerSelector(IPlayerStorage playerStorage) => 
            _playerStorage = playerStorage;

        public void Select(WeaponTypeId weaponTypeId) => 
            _playerStorage.GetByWeapon(weaponTypeId);
    }
}