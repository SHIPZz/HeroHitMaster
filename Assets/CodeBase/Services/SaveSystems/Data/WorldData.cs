using System;

namespace CodeBase.Services.SaveSystems.Data
{
    [Serializable]
    public class WorldData
    {
        public AdWeaponsData AdWeaponsData;
        public LevelData LevelData;
        public PlayerData PlayerData;
        public SettingsData SettingsData;
        public TranslatedWeaponNameData TranslatedWeaponNameData;
    }
}