using System;
using System.Collections.Generic;
using CodeBase.Enums;

namespace CodeBase.Services.SaveSystems.Data
{
    [Serializable]
    public class PlayerData 
    {
        public PlayerTypeId LastPlayerId = PlayerTypeId.Wolverine;
        public int Money;
        public List<WeaponTypeId> PurchasedWeapons = new() { WeaponTypeId.ThrowingKnifeShooter };
        public WeaponTypeId LastWeaponId = WeaponTypeId.ThrowingKnifeShooter;
        public WeaponTypeId LastNotPopupWeaponId = WeaponTypeId.ThrowingKnifeShooter;
        // public int ShootAccuracy;
    }
}