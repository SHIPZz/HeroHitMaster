using System;
using System.Collections.Generic;
using CodeBase.Enums;

namespace CodeBase.Services.SaveSystems.Data
{
    [Serializable]
    public class AdWeaponsData
    {
        public Dictionary<WeaponTypeId, int> WatchedAdsToBuyWeapons = new();
    }
}