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
        private readonly IProvider<WeaponTypeId, Image> _provider;
        private readonly CheckOutService _checkOutService;
        private readonly AdWatchCounter _adWatchCounter;
        private readonly ISaveSystem _saveSystem;

        private Image _lastWeaponIcon;
        private WeaponTypeId _lastWeaponType;

        public ShopWeaponPresenter(IProvider<List<WeaponSelectorView>> weaponSelectorViewsProvider,
            WeaponStaticDataService weaponStaticDataService,
            ShopWeaponInfoView shopWeaponInfoView,
            IProvider<WeaponTypeId, Image> provider,
            ISaveSystem saveSystem, CheckOutService checkOutService,
            AdWatchCounter adWatchCounter)
        {
            _adWatchCounter = adWatchCounter;
            _checkOutService = checkOutService;
            _saveSystem = saveSystem;
            _provider = provider;
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

            if (weaponData.Price.PriceTypeId == PriceTypeId.Popup)
            {
                await SetLastNotPopupWeapon();
                return;
            }

            SetInitialWeaponDataToView(weaponTypeId);
        }

        private void SetInitialWeaponDataToView(WeaponTypeId weaponTypeId)
        {
            WeaponData weaponData = GetWeaponData(weaponTypeId);

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

        private async void SetWeaponDataToView(WeaponTypeId weaponTypeId)
        {
            WeaponData weaponData = GetWeaponData(weaponTypeId);

            if (SameWeaponChoosen(weaponTypeId))
                return;

            _lastWeaponType = weaponTypeId;

            var playerData = await _saveSystem.Load<PlayerData>();

            if (IsWeaponAd(weaponTypeId, weaponData))
            {
                await SetAdWeaponInfoToView(weaponTypeId, playerData, weaponData);
                return;
            }

            if (HasPlayerThisWeapon(playerData, weaponData))
            {
                _shopWeaponInfoView.SetMoneyWeaponInfo(weaponData, true);
                return;
            }

            if (!HasPlayerThisWeapon(playerData, weaponData))
            {
                _shopWeaponInfoView.SetMoneyWeaponInfo(weaponData, false);
                return;
            }

            if (!HasEnoughMoneyToBuy(playerData, weaponData))
            {
                _shopWeaponInfoView.DisableBuyButtons();
                return;
            }

            _shopWeaponInfoView.SetMoneyWeaponInfo(weaponData, false);
        }

        private async UniTask SetAdWeaponInfoToView(WeaponTypeId weaponTypeId, PlayerData playerData,
            WeaponData weaponData)
        {
            var adsWeaponData = await _saveSystem.Load<AdWeaponsData>();

            adsWeaponData.WatchedAdsToBuyWeapons.TryAdd(weaponTypeId, 0);

            if (IsWeaponAdBought(playerData, weaponTypeId))
            {
                _shopWeaponInfoView.SetAdWeaponInfo(weaponData, true, adsWeaponData.WatchedAdsToBuyWeapons[weaponTypeId]);
                return;
            }

            _shopWeaponInfoView.SetAdWeaponInfo(weaponData, false, adsWeaponData.WatchedAdsToBuyWeapons[weaponTypeId]);
        }

        private bool IsWeaponAd(WeaponTypeId weaponTypeId, WeaponData weaponData) =>
            weaponData.Price.PriceTypeId == PriceTypeId.Ad;

        private bool IsWeaponAdBought(PlayerData playerData, WeaponTypeId weaponTypeId) =>
            playerData.PurchasedWeapons.Contains(weaponTypeId);

        private bool SameWeaponChoosen(WeaponTypeId weaponTypeId) =>
            _lastWeaponType == weaponTypeId;

        private WeaponData GetWeaponData(WeaponTypeId weaponTypeId)
        {
            WeaponData weaponData = _weaponStaticDataService.Get(weaponTypeId);
            SetMainShopWeaponIcon(weaponTypeId);
            return weaponData;
        }

        private void SetMainShopWeaponIcon(WeaponTypeId weaponTypeId)
        {
            _lastWeaponIcon?.gameObject.SetActive(false);
            Image weaponIcon = _provider.Get(weaponTypeId);
            weaponIcon.gameObject.SetActive(true);
            _lastWeaponIcon = weaponIcon;
        }

        private bool HasPlayerThisWeapon(PlayerData playerData, WeaponData weaponData) =>
            playerData.PurchasedWeapons.Contains(weaponData.WeaponTypeId);

        private bool HasEnoughMoneyToBuy(PlayerData playerData, WeaponData weaponData) =>
            playerData.Money >= weaponData.Price.Value;
    }
}