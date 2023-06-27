using System.Collections.Generic;
using Gameplay.Web;

namespace Databases
{
    public class WeaponsProvider
    {
        private Dictionary<int, IWeapon> _weapons = new Dictionary<int, IWeapon>();

        public IReadOnlyDictionary<int, IWeapon> Weapons =>
            _weapons;

        public void Add(IWeapon weapon) =>
            _weapons[weapon.Id] = weapon;

        public IWeapon Get(int id) =>
            _weapons[id];
    }
}