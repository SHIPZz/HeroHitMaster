using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.UI.Windows.Buy;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class ShopWeaponPresenter : IInitializable, IDisposable
    {
        private readonly List<WeaponSelectorView> _weaponSelectorViews;
        private readonly WeaponStaticDataService _weaponStaticDataService;
        private readonly ShopWeaponInfo _shopWeaponInfo;
        private readonly IProvider<WeaponTypeId, Image> _provider;
        private Image _lastWeaponIcon;
        private readonly ISaveSystem _saveSystem;
        private BuyWeaponPresenter _buyWeaponPresenter;

        public ShopWeaponPresenter(List<WeaponSelectorView> weaponSelectorViews,
            WeaponStaticDataService weaponStaticDataService,
            ShopWeaponInfo shopWeaponInfo,
            IProvider<WeaponTypeId, Image> provider,
            ISaveSystem saveSystem, BuyWeaponPresenter buyWeaponPresenter)
        {
            _buyWeaponPresenter = buyWeaponPresenter;
            _saveSystem = saveSystem;
            _provider = provider;
            _shopWeaponInfo = shopWeaponInfo;
            _weaponStaticDataService = weaponStaticDataService;
            _weaponSelectorViews = weaponSelectorViews;
        }

        public void Init(WeaponTypeId weaponTypeId)
        {
            SetWeaponDataToView(weaponTypeId);
        }

        public void Initialize()
        {
            _weaponSelectorViews.ForEach(x => x.Choosed += SetWeaponDataToView);
            _buyWeaponPresenter.Succeeded += DisablePurchasedWeaponInfo;
        }

        public void Dispose()
        {
            _weaponSelectorViews.ForEach(x => x.Choosed -= SetWeaponDataToView);
            _buyWeaponPresenter.Succeeded -= DisablePurchasedWeaponInfo;
        }

        private void DisablePurchasedWeaponInfo(WeaponTypeId weaponTypeId)
        {
            var weaponData = _weaponStaticDataService.Get(weaponTypeId);
            _shopWeaponInfo.DisableBuyInfo(weaponData);
        }

        private async void SetWeaponDataToView(WeaponTypeId weaponTypeId)
        {
            WeaponData weaponData = GetWeaponData(weaponTypeId);

            var playerData = await _saveSystem.Load<PlayerData>();

            if (!HasEnoughMoneyToBuy(playerData, weaponData) && !HasPlayerThisWeapon(playerData, weaponData))
                return;
            
            if (HasPlayerThisWeapon(playerData, weaponData)) 
                return;

            _shopWeaponInfo.SetWeaponData(weaponData);
        }

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
                _shopWeaponInfo.DisableBuyInfo(weaponData);
                return true;
            }

            return false;
        }

        private bool HasEnoughMoneyToBuy(PlayerData playerData, WeaponData weaponData)
        {
            if (playerData.Money >= weaponData.Price.Value)
                return true;
            
            _shopWeaponInfo.DisableBuyButton(weaponData);
            return false;
        }
    }
}