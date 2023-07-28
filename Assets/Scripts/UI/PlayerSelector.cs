using Enums;
using Services.Storages;

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