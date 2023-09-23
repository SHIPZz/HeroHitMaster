using CodeBase.Services.CheckOut;
using CodeBase.Services.Data;
using CodeBase.UI.Weapons;

namespace CodeBase.UI.Windows.Buy
{
    public class WeaponBuyer
    {
        private readonly WeaponStaticDataService _weaponStaticDataService;
        private readonly CheckOutService _checkOutService;
        private readonly WeaponSelector _weaponSelector;

        public WeaponBuyer(WeaponStaticDataService weaponStaticDataService, CheckOutService checkOutService, WeaponSelector weaponSelector)
        {
            _weaponStaticDataService = weaponStaticDataService;
            _checkOutService = checkOutService;
            _weaponSelector = weaponSelector;
        }

        public void Buy()
        {
            int cost = _weaponStaticDataService.Get(_weaponSelector.LastWeaponId).Price.Value;
            
            _checkOutService.Buy(cost);
        }
    }
}