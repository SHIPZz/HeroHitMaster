using Enums;
using Gameplay.Weapon;

namespace Services.Storages
{
    public interface IWeaponStorage
    {
        Weapon Get(WeaponTypeId weaponTypeId);
    }
}