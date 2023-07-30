using CodeBase.Gameplay.Weapons;
using Enums;

namespace CodeBase.Services.Storages
{
    public interface IWeaponStorage
    {
        Weapon Get(WeaponTypeId weaponTypeId);
    }
}