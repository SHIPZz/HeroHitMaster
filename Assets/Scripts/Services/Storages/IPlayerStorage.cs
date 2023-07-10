using Enums;
using Gameplay.Character.Player;

namespace Services.Storages
{
    public interface IPlayerStorage
    {
        Player Get(PlayerTypeId playerTypeId);
        Player Get(WeaponTypeId weaponTypeId);
    }
}