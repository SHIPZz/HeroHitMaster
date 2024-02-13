using System;
using CodeBase.Enums;
using CodeBase.Services.Ad;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.UI.Windows.Shop;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class WeaponAdWatchCounter
    {
        private readonly ShopWeaponInfoView _shopWeaponInfoView;
        private readonly WeaponStaticDataService _weaponStaticDataService;
        private readonly IAdService _adService;
        private readonly WeaponSelector _weaponSelector;
        private readonly IWorldDataService _worldDataService;

        public event Action<WeaponTypeId> AllAdWatched;
        public event Action<WeaponTypeId, int> AdWatched;

        public WeaponAdWatchCounter(IWorldDataService worldDataService,
            WeaponStaticDataService weaponStaticDataService,
            IAdService adService, WeaponSelector weaponSelector)
        {
            _worldDataService = worldDataService;
            _weaponSelector = weaponSelector;
            _adService = adService;
            _weaponStaticDataService = weaponStaticDataService;
        }

        public void Count()
        {
            WeaponTypeId targetWeaponId = _weaponSelector.LastWeaponId;
            int watchedAds = 0;

            if (_worldDataService.WorldData.AdWeaponsData.WatchedAdsToBuyWeapons.TryGetValue(targetWeaponId, out var alreadyWatchedAds))
                watchedAds = alreadyWatchedAds;

            int targetAds = _weaponStaticDataService.Get(targetWeaponId).Price.AdQuantity;

            _adService.PlayLongAd(null, () => OnAdEndedCallback(watchedAds, _worldDataService.WorldData, targetWeaponId, targetAds),
                error => OnAdEndedCallback(watchedAds, _worldDataService.WorldData, targetWeaponId, targetAds));
        }

        private void OnAdEndedCallback(int watchedAds, WorldData worldData, WeaponTypeId targetWeaponId,
            int targetAds)
        {
            watchedAds++;
            worldData.AdWeaponsData.WatchedAdsToBuyWeapons[targetWeaponId] = watchedAds;
            _worldDataService.Save();

            if (watchedAds == targetAds)
            {
                AllAdWatched?.Invoke(targetWeaponId);
                return;
            }

            AdWatched?.Invoke(targetWeaponId, watchedAds);
        }
    }
}