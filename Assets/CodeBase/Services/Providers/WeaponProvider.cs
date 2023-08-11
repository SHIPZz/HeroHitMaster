using CodeBase.Gameplay.Weapons;

namespace CodeBase.Services.Providers
{
    public class WeaponProvider : IProvider<Weapon>
    {
        private Weapon _weapon;

        public Weapon Get() => 
            _weapon;

        public void Set(Weapon camera) => 
            _weapon = camera;
    }
}