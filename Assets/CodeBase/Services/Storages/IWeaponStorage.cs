using Enums;
using Gameplay.Weapons;

namespace CodeBase.Services.Storages
{
    public interface IWeaponStorage
    {
        Weapon Get(WeaponTypeId weaponTypeId);
    }
}