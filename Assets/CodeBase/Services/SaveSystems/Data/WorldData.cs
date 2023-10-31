using System;

namespace CodeBase.Services.SaveSystems.Data
{
    [Serializable]
    public class WorldData
    {
        public AdWeaponsData AdWeaponsData = new();
        public LevelData LevelData = new();
        public PlayerData PlayerData = new();
        public SettingsData SettingsData = new();
        public TranslatedWeaponNameData TranslatedWeaponNameData = new();
    }
}