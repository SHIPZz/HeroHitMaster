using System;
using CodeBase.Services.CheckOut;
using CodeBase.UI.Weapons;
using CodeBase.UI.Weapons.ShopWeapons;
using Zenject;

namespace CodeBase.Services.SaveSystems.WeaponSaves
{
    public class WeaponSaverMediator : IInitializable, IDisposable
    {
        private readonly WeaponSaver _weaponSaver;
        private readonly CheckOutService _checkOutService;
        private readonly  AdWatchCounter _adWatchCounter;

        public WeaponSaverMediator(WeaponSaver weaponSaver, CheckOutService checkOutService, AdWatchCounter adWatchCounter)
        {
            _weaponSaver = weaponSaver;
            _checkOutService = checkOutService;
            _adWatchCounter = adWatchCounter;
        }

        public void Initialize()
        {
            _checkOutService.Succeeded += _weaponSaver.Save;
            _adWatchCounter.AllAdWatched += _weaponSaver.Save;
        }

        public void Dispose()
        {
            _checkOutService.Succeeded -= _weaponSaver.Save;
            _adWatchCounter.AllAdWatched -= _weaponSaver.Save;
        }
    }
}