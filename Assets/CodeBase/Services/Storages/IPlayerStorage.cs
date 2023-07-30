using CodeBase.Gameplay.Character.Players;
using Enums;

namespace CodeBase.Services.Storages
{
    public interface IPlayerStorage : IPlayerStorageByWeaponType
    {
        Player Get(PlayerTypeId playerTypeId);
    }
}