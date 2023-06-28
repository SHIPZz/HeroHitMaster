using System.Collections.Generic;
using Enums;
using Gameplay.Web;

namespace Databases
{
    public class WeaponsProvider
    {
        private Dictionary<WeaponTypeId, IWeapon> _weapons = new Dictionary<WeaponTypeId, IWeapon>();

        public IReadOnlyDictionary<WeaponTypeId, IWeapon> Weapons =>
            _weapons;
        
        public IWeapon CurrentWeapon { get; set; }

        public void Add(IWeapon weapon) =>
            _weapons[weapon.WeaponTypeId] = weapon;

        public IWeapon Get(WeaponTypeId weaponTypeId) =>
            _weapons[weaponTypeId];
    }
}