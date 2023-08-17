using System;
using System.Collections.Generic;
using CodeBase.Enums;

namespace CodeBase.Services.SaveSystems
{
    [Serializable]
    public class PlayerProgress
    {
        public int Money = 1000;
        public List<WeaponTypeId> PurchasedWeapons = new();
    }
}