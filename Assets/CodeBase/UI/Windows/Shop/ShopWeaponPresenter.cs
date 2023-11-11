using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.CheckOut;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.UI.Weapons;
using CodeBase.UI.Weapons.ShopWeapons;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopWeaponPresenter : IInitializable, IDisposable
    {
        private readonly List<WeaponSelectorView> _weaponSelectorViews;
        private readonly WeaponStaticDataService _weaponStaticDataService;
        private readonly ShopWeaponInfoView _shopWeaponInfoView;
        private readonly IProvider<WeaponIconsProvider> _weaponIconsProvider;
        private readonly CheckOutService _checkOutService;
        private readonly WeaponAdWatchCounter _weaponAdWatchCounter;
        private readonly Wallet.Wallet _wallet;
        private readonly IWorldDataService _worldDataService;

        private Image _lastWeaponIcon;
        private WeaponTypeId _lastWeaponType;

        public ShopWeaponPresenter(IProvider<List<WeaponSelectorView>> weaponSelectorViewsProvider,
            WeaponStaticDataService weaponStaticDataService,
            ShopWeaponInfoView shopWeaponInfoView,
            IProvider<WeaponIconsProvider> weaponIconsProvider,
            IWorldDataService worldDataService
            , CheckOutService checkOutService,
            WeaponAdWatchCounter weaponAdWatchCounter,
            Wallet.Wallet wallet)
        {
            _worldDataService = worldDataService;
            _wallet = wallet;
            _weaponAdWatchCounter = weaponAdWatchCounter;
            _checkOutService = checkOutService;
            _weaponIconsProvider = weaponIconsProvider;
            _shopWeaponInfoView = shopWeaponInfoView;
            _weaponStaticDataService = weaponStaticDataService;
            _weaponSelectorViews = weaponSelectorViewsProvider.Get();
        }

        public void Initialize()
        {
            _weaponSelectorViews.ForEach(x => x.Choosen += SetWeaponDataToView);
            _checkOutService.Succeeded += DisablePurchasedWeaponInfo;
            _weaponAdWatchCounter.AdWatched += SetWeaponAdWeaponInfoAfterWeaponAdWatching;
            _weaponAdWatchCounter.AllAdWatched += SetWeaponAdWeaponInfoAfterPurchasing;
        }

        public void Dispose()
        {
            _weaponSelectorViews.ForEach(x => x.Choosen -= SetWeaponDataToView);
            _checkOutService.Succeeded -= DisablePurchasedWeaponInfo;
            _weaponAdWatchCounter.AdWatched -= SetWeaponAdWeaponInfoAfterWeaponAdWatching;
            _weaponAdWatchCounter.AllAdWatched -= SetWeaponAdWeaponInfoAfterPurchasing;
        }

        public void Init(WeaponTypeId weaponTypeId)
        {
            WeaponData weaponData = _weaponStaticDataService.Get(weaponTypeId);

            if (weaponData.Price.PriceTypeId == PriceTypeId.Popup)
            {
                SetLastNotPopupWeapon();
                return;
            }

            _lastWeaponType = weaponTypeId;
            SetInitialWeaponDataToView(weaponData);
            SetMainShopWeaponIcon(weaponData.WeaponTypeId);
        }

        private void SetInitialWeaponDataToView(WeaponData weaponData)
        {
            if (weaponData.Price.PriceTypeId == PriceTypeId.Ad)
            {
                _shopWeaponInfoView.SetAdWeaponInfo(weaponData, true, 0);
                _shopWeaponInfoView.SetActiveAcceptWeaponButton(false);
                return;
            }

            _shopWeaponInfoView.SetMoneyWeaponInfo(weaponData.WeaponTypeId, false, true);
            _shopWeaponInfoView.SetMoneyWeaponPriceInfo(weaponData, false);
            _shopWeaponInfoView.DisableBuyButtons();
            _shopWeaponInfoView.SetActiveAcceptWeaponButton(false);
        }

        private void SetLastNotPopupWeapon()
        {
            WorldData worldData = _worldDataService.WorldData;
            WeaponData weaponData = _weaponStaticDataService.Get(worldData.PlayerData.LastNotPopupWeaponId);
            SetWeaponDataToView(weaponData.WeaponTypeId);
        }

        private void SetWeaponAdWeaponInfoAfterPurchasing(WeaponTypeId weaponTypeId)
        {
            WeaponData weaponData = _weaponStaticDataService.Get(weaponTypeId);
            _shopWeaponInfoView.ShowEffects();
            _shopWeaponInfoView.SetAdWeaponInfo(weaponData, true, weaponData.Price.AdQuantity);
        }

        private void SetWeaponAdWeaponInfoAfterWeaponAdWatching(WeaponTypeId weaponTypeId, int watchedAds)
        {
            WeaponData weaponData = _weaponStaticDataService.Get(weaponTypeId);
            _shopWeaponInfoView.SetAdWeaponInfo(weaponData, false, watchedAds);
        }

        private void DisablePurchasedWeaponInfo()
        {
            WeaponData weaponData = _weaponStaticDataService.Get(_lastWeaponType);
            _shopWeaponInfoView.ShowEffects();
            _shopWeaponInfoView.SetMoneyWeaponInfo(weaponData.WeaponTypeId, false, true);
            _shopWeaponInfoView.SetMoneyWeaponPriceInfo(weaponData, false);
        }

        private void SetWeaponDataToView(WeaponTypeId weaponTypeId)
        {
            if (SameWeaponChoosen(weaponTypeId))
                return;

            WeaponData weaponData = GetWeaponData(weaponTypeId);
            WorldData worldData = _worldDataService.WorldData;
            PlayerData playerData = worldData.PlayerData;
            _lastWeaponType = weaponTypeId;
            SetMainShopWeaponIcon(weaponTypeId);

            if (IsWeaponAd(weaponData))
            {
                SetAdWeaponInfoToView(weaponTypeId, playerData, weaponData);
                return;
            }

            if (HasPlayerThisWeapon(playerData, weaponData))
            {
                _shopWeaponInfoView.SetMoneyWeaponInfo(weaponData.WeaponTypeId, false, true);
                _shopWeaponInfoView.SetMoneyWeaponPriceInfo(weaponData, false);
                return;
            }

            if (!HasPlayerThisWeapon(playerData, weaponData))
            {
                if (HasEnoughMoneyToBuy(weaponData))
                {
                    _shopWeaponInfoView.SetMoneyWeaponInfo(weaponData.WeaponTypeId, true, false);
                    _shopWeaponInfoView.SetMoneyWeaponPriceInfo(weaponData, true);
                    return;
                }

                _shopWeaponInfoView.SetMoneyWeaponInfo(weaponData.WeaponTypeId, false, false);
                _shopWeaponInfoView.SetMoneyWeaponPriceInfo(weaponData, true);
            }

            if (!HasEnoughMoneyToBuy(weaponData))
                _shopWeaponInfoView.DisableBuyButtons();
        }

        private void SetAdWeaponInfoToView(WeaponTypeId weaponTypeId, PlayerData playerData,
            WeaponData weaponData)
        {
            WorldData worldData = _worldDataService.WorldData;

            worldData.AdWeaponsData.WatchedAdsToBuyWeapons.TryAdd(weaponTypeId, 0);

            if (IsWeaponAdBought(playerData, weaponTypeId))
            {
                _shopWeaponInfoView.SetAdWeaponInfo(weaponData, true,
                    worldData.AdWeaponsData.WatchedAdsToBuyWeapons[weaponTypeId]);
                return;
            }

            _shopWeaponInfoView.SetAdWeaponInfo(weaponData, false,
                worldData.AdWeaponsData.WatchedAdsToBuyWeapons[weaponTypeId]);
        }

        private bool IsWeaponAd(WeaponData weaponData) =>
            weaponData.Price.PriceTypeId == PriceTypeId.Ad;

        private bool IsWeaponAdBought(PlayerData playerData, WeaponTypeId weaponTypeId) =>
            playerData.PurchasedWeapons.Contains(weaponTypeId);

        private bool SameWeaponChoosen(WeaponTypeId weaponTypeId) =>
            _lastWeaponType == weaponTypeId;

        private WeaponData GetWeaponData(WeaponTypeId weaponTypeId) =>
            _weaponStaticDataService.Get(weaponTypeId);

        private void SetMainShopWeaponIcon(WeaponTypeId weaponTypeId)
        {
            if (!_weaponIconsProvider.Get().ShopWeaponIcons.ContainsKey(weaponTypeId))
                return;

            _lastWeaponIcon?.gameObject.SetActive(false);

            Image weaponIcon = _weaponIconsProvider.Get().ShopWeaponIcons[weaponTypeId];
            weaponIcon.gameObject.SetActive(true);
            _lastWeaponIcon = weaponIcon;
        }

        private bool HasPlayerThisWeapon(PlayerData playerData, WeaponData weaponData) =>
            playerData.PurchasedWeapons.Contains(weaponData.WeaponTypeId);

        private bool HasEnoughMoneyToBuy(WeaponData weaponData) =>
            _wallet.Money >= weaponData.Price.Value;
    }
}