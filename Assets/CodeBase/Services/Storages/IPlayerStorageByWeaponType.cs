using CodeBase.Gameplay.Character.Players;
using Enums;

namespace CodeBase.Services.Storages
{
    public interface IPlayerStorageByWeaponType
    {
        Player Get(WeaponTypeId weaponTypeId);
    }
}