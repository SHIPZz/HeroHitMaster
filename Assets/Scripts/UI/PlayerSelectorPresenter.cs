using System;
using System.Collections.Generic;
using Weapons;
using Zenject;

public class PlayerSelectorPresenter : IInitializable, IDisposable
{
    private PlayerSelector _playerSelector;
    private List<WeaponSelectorView> _weaponSelectorViews;

    public PlayerSelectorPresenter(PlayerSelector playerSelector, List<WeaponSelectorView> weaponSelectorViews)
    {
        _playerSelector = playerSelector;
        _weaponSelectorViews = weaponSelectorViews;
    }

    public void Initialize()
    {
        foreach (var weaponSelectorView in _weaponSelectorViews)
        {
            weaponSelectorView.Choosed += _playerSelector.Select;
        }
    }

    public void Dispose()
    {
        foreach (var weaponSelectorView in _weaponSelectorViews)
        {
            weaponSelectorView.Choosed -= _playerSelector.Select;
        }
    }
}