using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class ShopWeaponPresenter : IInitializable, IDisposable
    {
        private readonly List<WeaponSelectorView> _weaponSelectorViews;
        private WeaponStaticDataService _weaponStaticDataService;
        private ShopWeaponInfo _shopWeaponInfo;
        private IProvider<WeaponTypeId, Image> _provider;
        private Image _lastWeaponIcon;

        public ShopWeaponPresenter(List<WeaponSelectorView> weaponSelectorViews, 
            WeaponStaticDataService weaponStaticDataService,
            ShopWeaponInfo shopWeaponInfo, IProvider<WeaponTypeId, Image> provider)
        {
            _provider = provider;
            _shopWeaponInfo = shopWeaponInfo;
            _weaponStaticDataService = weaponStaticDataService;
            _weaponSelectorViews = weaponSelectorViews;
        }

        public void Initialize()
        {
            _weaponSelectorViews.ForEach(x => x.Choosed += SetWeaponDataToView);
            SetWeaponDataToView(WeaponTypeId.ThrowingKnifeShooter);
        }

        public void Dispose()
        {
            _weaponSelectorViews.ForEach(x => x.Choosed -= SetWeaponDataToView);
        }

        private void SetWeaponDataToView(WeaponTypeId weaponTypeId)
        {
            _lastWeaponIcon?.gameObject.SetActive(false);
            WeaponData weaponData = _weaponStaticDataService.Get(weaponTypeId);
            Image weaponIcon = _provider.Get(weaponTypeId);
            weaponIcon.gameObject.SetActive(true);
            _lastWeaponIcon = weaponIcon;
            _shopWeaponInfo.SetWeaponData(weaponData);
        }
    }
}