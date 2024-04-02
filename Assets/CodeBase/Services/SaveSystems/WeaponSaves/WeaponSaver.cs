using CodeBase.Enums;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.UI.Weapons;

namespace CodeBase.Services.SaveSystems.WeaponSaves
{
    public class WeaponSaver
    {
        private readonly WeaponSelector _weaponSelector;
        private readonly IWorldDataService _worldDataService;

        public WeaponSaver(WeaponSelector weaponSelector, IWorldDataService worldDataService)
        {
            _worldDataService = worldDataService;
            _weaponSelector = weaponSelector;
        }

        public void Save() => 
            SetToData(_weaponSelector.LastWeaponId);

        public void Save(WeaponTypeId weaponTypeId) => 
            SetToData(weaponTypeId);

        private void SetToData(WeaponTypeId weaponTypeId)
        {
            WorldData worldData = _worldDataService.WorldData;
            worldData.PlayerData.PurchasedWeapons.Add(weaponTypeId);
        }
    }
}