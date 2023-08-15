using System;
using System.Collections.Generic;
using CodeBase.Enums;

namespace CodeBase.Services.SaveSystems
{
    [Serializable]
    public class PlayerProgress
    {
        public int Money;
        public List<WeaponTypeId> PurchasedWeapons = new();
    }

    [Serializable]
    public class Settings
    {
        public float Volume;
    }

    [Serializable]
    public class Level
    {
        public float Id;
    }

    [Serializable]
    public class WorldData
    {
        public Settings Settings;
        public PlayerProgress PlayerProgress;
        public Level Level;
    }
}