﻿using System;
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
        private Image _lastWeaponIcon;
        private readonly ISaveSystem _saveSystem;
        private WeaponTypeId _lastWeaponType;
        private AdWatchCounter _adWatchCounter;

        public ShopWeaponPresenter(IProvider<List<WeaponSelectorView>> weaponSelectorViewsProvider,
            WeaponStaticDataService weaponStaticDataService,
            ShopWeaponInfoView shopWeaponInfoView,
            IProvider<WeaponTypeId, Image> provider,
            ISaveSystem saveSystem, CheckOutService checkOutService, AdWatchCounter adWatchCounter)
        {
            _adWatchCounter = adWatchCounter;
            _checkOutService = checkOutService;
            _saveSystem = saveSystem;
            _provider = provider;
            _shopWeaponInfoView = shopWeaponInfoView;
            _weaponStaticDataService = weaponStaticDataService;
            _weaponSelectorViews = weaponSelectorViewsProvider.Get();
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

            SetWeaponDataToView(weaponTypeId);
        }

        private async UniTask SetLastNotPopupWeapon()
        {
            WeaponData weaponData;
            var playerData = await _saveSystem.Load<PlayerData>();
            weaponData = _weaponStaticDataService.Get(playerData.LastNotPopupWeaponId);
            SetWeaponDataToView(weaponData.WeaponTypeId);
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

        private void SetAdWeaponInfo(WeaponTypeId weaponTypeId)
        {
            WeaponData weaponData = _weaponStaticDataService.Get(weaponTypeId);
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
            _shopWeaponInfoView.DisableBuyInfo(weaponData, false);
            _shopWeaponInfoView.ShowEffectOnPurchasedWeapon(weaponData.WeaponTypeId);
        }

        private async void SetWeaponDataToView(WeaponTypeId weaponTypeId)
        {
            WeaponData weaponData = GetWeaponData(weaponTypeId);

            if (SameWeaponChoosed(weaponTypeId))
                return;

            _lastWeaponType = weaponTypeId;

            var playerData = await _saveSystem.Load<PlayerData>();

            if (!HasEnoughMoneyToBuy(playerData, weaponData) && !HasPlayerThisWeapon(playerData, weaponData))
            {
                _shopWeaponInfoView.DisableBuyButton(weaponData);
                return;
            }

            if (HasPlayerThisWeapon(playerData, weaponData))
            {
                _shopWeaponInfoView.DisableBuyInfo(weaponData, false);
                return;
            }

            // if (IsWeaponPopup(weaponTypeId))
            // {
            //     await SetLastNotPopupWeapon();
            //     Debug.Log(weaponData.Price.PriceTypeId == PriceTypeId.Popup);
            //     return;
            // }

            _shopWeaponInfoView.SetWeaponData(weaponData);
        }

        private bool SameWeaponChoosed(WeaponTypeId weaponTypeId) =>
            _lastWeaponType == weaponTypeId;

        private bool IsWeaponPopup(WeaponTypeId weaponTypeId) =>
            _weaponStaticDataService.Get(weaponTypeId).Price.PriceTypeId == PriceTypeId.Popup;

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

        private bool HasEnoughMoneyToBuy(PlayerData playerData, WeaponData weaponData)
        {
            if (playerData.Money >= weaponData.Price.Value)
                return true;

            _shopWeaponInfoView.DisableBuyButton(weaponData);
            return false;
        }
    }
}