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
            SetToData(_weaponSelector.LastWeaponId);

        public void Save(WeaponTypeId weaponTypeId) => 
            SetToData(weaponTypeId);

        private async void SetToData(WeaponTypeId weaponTypeId)
        {
            var worldData = await _saveSystem.Load<WorldData>();
            worldData.PlayerData.PurchasedWeapons.Add(weaponTypeId);
        }
    }
}