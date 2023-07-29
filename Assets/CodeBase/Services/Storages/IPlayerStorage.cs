using Enums;
using Gameplay.Character.Players;

namespace CodeBase.Services.Storages
{
    public interface IPlayerStorage
    {
        Player Get(PlayerTypeId playerTypeId);
        Player Get(WeaponTypeId weaponTypeId);
    }
}