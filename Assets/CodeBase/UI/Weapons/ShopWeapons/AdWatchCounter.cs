using System;
using CodeBase.Enums;
using CodeBase.Services.Ad;
using CodeBase.Services.Data;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class AdWatchCounter
    {
        private readonly ShopWeaponInfoView _shopWeaponInfoView;
        private readonly ISaveSystem _saveSystem;
        private readonly WeaponStaticDataService _weaponStaticDataService;
        private readonly IAdService _adService;
        private readonly WeaponSelector _weaponSelector;

        public event Action<WeaponTypeId> AllAdWatched;
        public event Action<WeaponTypeId, int> AdWatched;

        public AdWatchCounter(ISaveSystem saveSystem,
            WeaponStaticDataService weaponStaticDataService,
            IAdService adService, WeaponSelector weaponSelector)
        {
            _weaponSelector = weaponSelector;
            _adService = adService;
            _saveSystem = saveSystem;
            _weaponStaticDataService = weaponStaticDataService;
        }

        public async void Count()
        {
            WeaponTypeId targetWeaponId = _weaponSelector.LastWeaponId;
            var adWeaponsData = await _saveSystem.Load<AdWeaponsData>();
            int watchedAds = 0;

            if (adWeaponsData.WatchedAdsToBuyWeapons.TryGetValue(targetWeaponId, out var alreadyWatchedAds))
                watchedAds = alreadyWatchedAds;

            int targetAds = _weaponStaticDataService.Get(targetWeaponId).Price.AdQuantity;

            _adService.PlayLongAd(null, () => OnAdEndedCallback(watchedAds, adWeaponsData, targetWeaponId, targetAds));
        }

        private void OnAdEndedCallback(int watchedAds, AdWeaponsData adWeaponsData, WeaponTypeId targetWeaponId,
            int targetAds)
        {
            watchedAds++;
            adWeaponsData.WatchedAdsToBuyWeapons[targetWeaponId] = watchedAds;
            _saveSystem.Save(adWeaponsData);

            if (watchedAds == targetAds)
            {
                AllAdWatched?.Invoke(targetWeaponId);
                return;
            }

            AdWatched?.Invoke(targetWeaponId, watchedAds);
        }
    }
}