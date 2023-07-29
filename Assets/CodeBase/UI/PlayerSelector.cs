﻿using CodeBase.Services.Storages;
using Enums;

public class PlayerSelector
{
    private readonly IPlayerStorage _playerStorage;

    public PlayerSelector(IPlayerStorage playerStorage)
    {
        _playerStorage = playerStorage;
    }

    public void Select(WeaponTypeId weaponTypeId)
    {
        _playerStorage.Get(weaponTypeId);
    }
}