using CodeBase.Enums;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.UI.Weapons;

namespace CodeBase.Services.SaveSystems.WeaponSaves
{
    public class WeaponSaver
    {
        private readonly WeaponSelector _weaponSelector;
        private readonly ISaveSystem _saveSystem;

        public WeaponSaver(WeaponSelector weaponSelector, ISaveSystem saveSystem)
        {
            _weaponSelector = weaponSelector;
            _saveSystem = saveSystem;
        }

        public void Save() => 
            SaveToData(_weaponSelector.LastWeaponId);

        public void Save(WeaponTypeId weaponTypeId) => 
            SaveToData(weaponTypeId);

        private async void SaveToData(WeaponTypeId weaponTypeId)
        {
            var playerData = await _saveSystem.Load<PlayerData>();
            playerData.PurchasedWeapons.Add(weaponTypeId);
            _saveSystem.Save(playerData);
        }
    }
}