using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.CheckOut;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
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
        private AdCheckOutService _adCheckOutService;

        public ShopWeaponPresenter(IProvider<List<WeaponSelectorView>> weaponSelectorViewsProvider,
            WeaponStaticDataService weaponStaticDataService,
            ShopWeaponInfoView shopWeaponInfoView,
            IProvider<WeaponTypeId, Image> provider,
            ISaveSystem saveSystem, CheckOutService checkOutService, AdCheckOutService adCheckOutService)
        {
            _adCheckOutService = adCheckOutService;
            _checkOutService = checkOutService;
            _saveSystem = saveSystem;
            _provider = provider;
            _shopWeaponInfoView = shopWeaponInfoView;
            _weaponStaticDataService = weaponStaticDataService;
            _weaponSelectorViews = weaponSelectorViewsProvider.Get();
        }

        public void Init(WeaponTypeId weaponTypeId)
        {
            if(_provider.Get(weaponTypeId) == null)
                return;
            
            SetWeaponDataToView(weaponTypeId);
        }

        public void Initialize()
        {
            _weaponSelectorViews.ForEach(x => x.Choosed += SetWeaponDataToView);
            _checkOutService.Succeeded += DisablePurchasedWeaponInfo;
            _adCheckOutService.Succeeded += DisablePurchasedWeaponInfo;
        }

        public void Dispose()
        {
            _weaponSelectorViews.ForEach(x => x.Choosed -= SetWeaponDataToView);
            _checkOutService.Succeeded -= DisablePurchasedWeaponInfo;
            _adCheckOutService.Succeeded -= DisablePurchasedWeaponInfo;
        }

        private void DisablePurchasedWeaponInfo()
        {
            WeaponData weaponData = _weaponStaticDataService.Get(_lastWeaponType);
            _shopWeaponInfoView.DisableBuyInfo(weaponData, false);
            _shopWeaponInfoView.ShowEffectOnPurchasedWeapon(weaponData.WeaponTypeId);
        }

        private async void SetWeaponDataToView(WeaponTypeId weaponTypeId)
        {
            if (SameWeaponChoosed(weaponTypeId))
                return;

            _lastWeaponType = weaponTypeId;

            WeaponData weaponData = GetWeaponData(weaponTypeId);

            var playerData = await _saveSystem.Load<PlayerData>();

            if (!HasEnoughMoneyToBuy(playerData, weaponData) && !HasPlayerThisWeapon(playerData, weaponData))
                return;

            if (HasPlayerThisWeapon(playerData, weaponData))
                return;

            _shopWeaponInfoView.SetWeaponData(weaponData);
        }

        private bool SameWeaponChoosed(WeaponTypeId weaponTypeId) => 
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

        private bool HasPlayerThisWeapon(PlayerData playerData, WeaponData weaponData)
        {
            if (playerData.PurchasedWeapons.Contains(weaponData.WeaponTypeId))
            {
                _shopWeaponInfoView.DisableBuyInfo(weaponData, false);
                return true;
            }

            return false;
        }

        private bool HasEnoughMoneyToBuy(PlayerData playerData, WeaponData weaponData)
        {
            if (playerData.Money >= weaponData.Price.Value)
                return true;

            _shopWeaponInfoView.DisableBuyButton(weaponData);
            return false;
        }
    }
}