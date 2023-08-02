using CodeBase.Enums;

namespace CodeBase.Services.Storages.Weapon
{
    public interface IWeaponStorage
    {
        Gameplay.Weapons.Weapon Get(WeaponTypeId weaponTypeId);
    }
}