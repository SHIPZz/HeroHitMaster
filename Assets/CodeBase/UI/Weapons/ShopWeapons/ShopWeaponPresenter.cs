using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.CheckOut;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class ShopWeaponPresenter : IInitializable, IDisposable
    {
        private readonly List<WeaponSelectorView> _weaponSelectorViews;
        private readonly WeaponStaticDataService _weaponStaticDataService;
        private readonly ShopWeaponInfoView _shopWeaponInfoView;
        private readonly IProvider<WeaponIconsProvider> _weaponIconsProvider;
        private readonly CheckOutService _checkOutService;
        private readonly AdWatchCounter _adWatchCounter;
        private readonly ISaveSystem _saveSystem;

        private Image _lastWeaponIcon;
        private WeaponTypeId _lastWeaponType;
        private PlayerData _playerData;

        public ShopWeaponPresenter(IProvider<List<WeaponSelectorView>> weaponSelectorViewsProvider,
            WeaponStaticDataService weaponStaticDataService,
            ShopWeaponInfoView shopWeaponInfoView,
            IProvider<WeaponIconsProvider> weaponIconsesProvider,
            ISaveSystem saveSystem, CheckOutService checkOutService,
            AdWatchCounter adWatchCounter)
        {
            _adWatchCounter = adWatchCounter;
            _checkOutService = checkOutService;
            _saveSystem = saveSystem;
            _weaponIconsProvider = weaponIconsesProvider;
            _shopWeaponInfoView = shopWeaponInfoView;
            _weaponStaticDataService = weaponStaticDataService;
            _weaponSelectorViews = weaponSelectorViewsProvider.Get();
        }

        public void Initialize()
        {
            _weaponSelectorViews.ForEach(x => x.Choosen += SetWeaponDataToView);
            _checkOutService.Succeeded += DisablePurchasedWeaponInfo;
            _adWatchCounter.AdWatched += SetAdWeaponInfo;
            _adWatchCounter.AllAdWatched += SetAdWeaponInfo;
        }

        public void Dispose()
        {
            _weaponSelectorViews.ForEach(x => x.Choosen -= SetWeaponDataToView);
            _checkOutService.Succeeded -= DisablePurchasedWeaponInfo;
            _adWatchCounter.AdWatched -= SetAdWeaponInfo;
            _adWatchCounter.AllAdWatched -= SetAdWeaponInfo;
        }

        public async void Init(WeaponTypeId weaponTypeId)
        {
            WeaponData weaponData;
            weaponData = _weaponStaticDataService.Get(weaponTypeId);
            var worldData = await _saveSystem.Load<WorldData>();
            _playerData = worldData.PlayerData;

            if (weaponData.Price.PriceTypeId == PriceTypeId.Popup)
            {
                await SetLastNotPopupWeapon();
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
                return;
            }

            _shopWeaponInfoView.SetMoneyWeaponInfo(weaponData, true);
        }

        private async UniTask SetLastNotPopupWeapon()
        {
            WeaponData weaponData;
            var playerData = await _saveSystem.Load<PlayerData>();
            weaponData = _weaponStaticDataService.Get(playerData.LastNotPopupWeaponId);
            Debug.Log(weaponData.WeaponTypeId);
            SetWeaponDataToView(weaponData.WeaponTypeId);
        }

        private void SetAdWeaponInfo(WeaponTypeId weaponTypeId)
        {
            WeaponData weaponData = _weaponStaticDataService.Get(weaponTypeId);
            _shopWeaponInfoView.ShowEffectOnPurchasedWeapon(weaponTypeId);
            _shopWeaponInfoView.SetAdWeaponInfo(weaponData, true, weaponData.Price.AdQuantity);
        }

        private void SetAdWeaponInfo(WeaponTypeId weaponTypeId, int watchedAds)
        {
            WeaponData weaponData = _weaponStaticDataService.Get(weaponTypeId);
            _shopWeaponInfoView.SetAdWeaponInfo(weaponData, false, watchedAds);
        }

        private void DisablePurchasedWeaponInfo()
        {
            WeaponData weaponData = _weaponStaticDataService.Get(_lastWeaponType);
            _shopWeaponInfoView.ShowEffectOnPurchasedWeapon(weaponData.WeaponTypeId);
            _shopWeaponInfoView.SetMoneyWeaponInfo(weaponData, true);
        }

        private void SetWeaponDataToView(WeaponTypeId weaponTypeId)
        {
            if (SameWeaponChoosen(weaponTypeId))
                return;

            WeaponData weaponData = GetWeaponData(weaponTypeId);
            _lastWeaponType = weaponTypeId;
            SetMainShopWeaponIcon(weaponTypeId);

            if (IsWeaponAd(weaponData))
            {
                SetAdWeaponInfoToView(weaponTypeId, _playerData, weaponData);
                return;
            }

            if (!HasEnoughMoneyToBuy(_playerData, weaponData)) 
                _shopWeaponInfoView.DisableBuyButtons();

            if (HasPlayerThisWeapon(_playerData, weaponData))
            {
                _shopWeaponInfoView.SetMoneyWeaponInfo(weaponData, true);
                return;
            }

            if (!HasPlayerThisWeapon(_playerData, weaponData))
                _shopWeaponInfoView.SetMoneyWeaponInfo(weaponData, false);
        }

        private async void SetAdWeaponInfoToView(WeaponTypeId weaponTypeId, PlayerData playerData,
            WeaponData weaponData)
        {
            var worldData = await _saveSystem.Load<WorldData>();

            worldData.AdWeaponsData.WatchedAdsToBuyWeapons.TryAdd(weaponTypeId, 0);

            if (IsWeaponAdBought(playerData, weaponTypeId))
            {
                _shopWeaponInfoView.SetAdWeaponInfo(weaponData, true,
                    worldData.AdWeaponsData.WatchedAdsToBuyWeapons[weaponTypeId]);
                return;
            }

            _shopWeaponInfoView.SetAdWeaponInfo(weaponData, false, worldData.AdWeaponsData.WatchedAdsToBuyWeapons[weaponTypeId]);
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
            _lastWeaponIcon?.gameObject.SetActive(false);
            Image weaponIcon = _weaponIconsProvider.Get().ShopWeaponIcons[weaponTypeId];
            weaponIcon.gameObject.SetActive(true);
            _lastWeaponIcon = weaponIcon;
        }

        private bool HasPlayerThisWeapon(PlayerData playerData, WeaponData weaponData) =>
            playerData.PurchasedWeapons.Contains(weaponData.WeaponTypeId);

        private bool HasEnoughMoneyToBuy(PlayerData playerData, WeaponData weaponData) =>
            playerData.Money >= weaponData.Price.Value;
    }
}