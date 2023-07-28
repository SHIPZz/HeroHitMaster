using Enums;
using Gameplay.Weapons;

namespace Services.Storages
{
    public interface IWeaponStorage
    {
        Weapon Get(WeaponTypeId weaponTypeId);
    }
}