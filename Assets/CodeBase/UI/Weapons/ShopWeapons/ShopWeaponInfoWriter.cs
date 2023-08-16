using CodeBase.Enums;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using UnityEngine.UI;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class ShopWeaponInfoWriter
    {
        private readonly WeaponStaticDataService _weaponStaticDataService;
        private readonly IProvider<WeaponTypeId, Image> _provider;
        private readonly ShopWeaponInfo _shopWeaponInfo;
        private Image _lastWeaponIcon;

        public ShopWeaponInfoWriter(WeaponStaticDataService weaponStaticDataService,
            IProvider<WeaponTypeId, Image> provider,
            ShopWeaponInfo shopWeaponInfo)
        {
            _shopWeaponInfo = shopWeaponInfo;
            _weaponStaticDataService = weaponStaticDataService;
            _provider = provider;
            _lastWeaponIcon = _provider.Get(WeaponTypeId.ThrowingKnifeShooter);
            Write(WeaponTypeId.ThrowingKnifeShooter);
        }

        public void TryWrite(WeaponTypeId weaponTypeId)
        {
            if (_lastWeaponIcon == _provider.Get(weaponTypeId))
                return;

            Write(weaponTypeId);
        }

        public void Write(WeaponTypeId weaponTypeId)
        {
            _lastWeaponIcon.gameObject.SetActive(false);
            var name = _weaponStaticDataService.Get(weaponTypeId).Name;
            var price = _weaponStaticDataService.Get(weaponTypeId).Price;

            _shopWeaponInfo
                .SetActivePrice(_weaponStaticDataService.Get(weaponTypeId).WeaponRankId ==
                                           WeaponRankId.Common);

            _shopWeaponInfo.SetInfo(name, price);
            Image shopWeaponIcon = _provider.Get(weaponTypeId);
            shopWeaponIcon.gameObject.SetActive(true);
            _lastWeaponIcon = shopWeaponIcon;
        }
    }
}