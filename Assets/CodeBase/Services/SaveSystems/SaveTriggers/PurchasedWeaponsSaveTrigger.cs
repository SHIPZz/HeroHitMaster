using System;
using CodeBase.Enums;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.UI.Windows.Buy;
using Zenject;

namespace CodeBase.Services.SaveSystems.SaveTriggers
{
    public class PurchasedWeaponsSaveTrigger : IInitializable, IDisposable
    {
        private readonly BuyWeaponPresenter _buyWeaponPresenter;
        private readonly ISaveSystem _saveSystem;

        public PurchasedWeaponsSaveTrigger(BuyWeaponPresenter buyWeaponPresenter, ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _buyWeaponPresenter = buyWeaponPresenter;
        }

        public void Initialize() => 
            _buyWeaponPresenter.Succeeded += AddToPlayerData;

        public void Dispose() => 
            _buyWeaponPresenter.Succeeded -= AddToPlayerData;

        private async void AddToPlayerData(WeaponTypeId weaponTypeId)
        {
            var playerData = await _saveSystem.Load<PlayerData>();
            playerData.PurchasedWeapons.Add(weaponTypeId);
            _saveSystem.Save(playerData);
        }
    }
}