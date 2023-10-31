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
        private readonly  WeaponAdWatchCounter _weaponAdWatchCounter;

        public WeaponSaverMediator(WeaponSaver weaponSaver, CheckOutService checkOutService, WeaponAdWatchCounter weaponAdWatchCounter)
        {
            _weaponSaver = weaponSaver;
            _checkOutService = checkOutService;
            _weaponAdWatchCounter = weaponAdWatchCounter;
        }

        public void Initialize()
        {
            _checkOutService.Succeeded += _weaponSaver.Save;
            _weaponAdWatchCounter.AllAdWatched += _weaponSaver.Save;
        }

        public void Dispose()
        {
            _checkOutService.Succeeded -= _weaponSaver.Save;
            _weaponAdWatchCounter.AllAdWatched -= _weaponSaver.Save;
        }
    }
}