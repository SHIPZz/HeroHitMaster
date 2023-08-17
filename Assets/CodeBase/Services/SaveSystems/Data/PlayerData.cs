using System;
using System.Collections.Generic;
using CodeBase.Enums;

namespace CodeBase.Services.SaveSystems.Data
{
    [Serializable]
    public class PlayerData
    {
        public int Money;
        public List<WeaponTypeId> PurchasedWeapons = new() { WeaponTypeId.ThrowingKnifeShooter };
        public WeaponTypeId LastWeaponType;
    }
}