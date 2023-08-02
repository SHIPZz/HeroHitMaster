using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Players;

namespace CodeBase.Services.Storages.Character
{
    public interface IPlayerStorage
    {
        Player GetById(PlayerTypeId playerTypeId);
        Player GetByWeapon(WeaponTypeId weaponTypeId);
        List<Player> GetAll();
    }
}