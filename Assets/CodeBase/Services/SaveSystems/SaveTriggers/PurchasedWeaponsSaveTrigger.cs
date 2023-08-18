using System;
using CodeBase.Enums;
using CodeBase.Services.CheckOut;
using CodeBase.Services.SaveSystems.Data;
using Zenject;

namespace CodeBase.Services.SaveSystems.SaveTriggers
{
    public class PurchasedWeaponsSaveTrigger : IInitializable, IDisposable
    {
        private readonly CheckOutService _checkOutService;
        private readonly ISaveSystem _saveSystem;
        private WeaponTypeId _lastWeaponIdSelected;

        public PurchasedWeaponsSaveTrigger(CheckOutService сheckOutService, ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _checkOutService = сheckOutService;
        }

        public void Initialize() => 
        _checkOutService.Succeeded += AddToPlayerData;

        public void Dispose() => 
            _checkOutService.Succeeded -= AddToPlayerData;

        public void SetLastWeaponType(WeaponTypeId weaponTypeId) => 
            _lastWeaponIdSelected = weaponTypeId;

        private async void AddToPlayerData()
        {
            var playerData = await _saveSystem.Load<PlayerData>();
            playerData.PurchasedWeapons.Add(_lastWeaponIdSelected);
            _saveSystem.Save(playerData);
        }
    }
}