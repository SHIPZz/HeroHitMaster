using System;
using CodeBase.Gameplay.Weapons;

namespace CodeBase.Services.Providers
{
    public class WeaponProvider : IProvider<Weapon>, IProvider<WeaponProvider>
    {
        private Weapon _weapon;

        public bool Initialized = false;
        
        public event Action<Weapon> Changed;

        public Weapon Get() => 
            _weapon;

        WeaponProvider IProvider<WeaponProvider>.Get() => 
        this;

        public void Set(WeaponProvider t)
        {
            
        }

        public void Set(Weapon weapon)
        {
            _weapon = weapon;
            Changed?.Invoke(_weapon);
            Initialized = true;
        }
    }
}