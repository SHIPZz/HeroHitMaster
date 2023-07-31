using System;
using CodeBase.UI.Weapons;
using Zenject;

namespace CodeBase.Gameplay.Sound
{
    public class SoundWeaponPresenter : IInitializable, IDisposable
    {
        private readonly WeaponSelector _weaponSelector;
        private readonly SoundWeaponChanger _soundWeaponChanger;

        public SoundWeaponPresenter(WeaponSelector weaponSelector, SoundWeaponChanger soundWeaponChanger)
        {
            _weaponSelector = weaponSelector;
            _soundWeaponChanger = soundWeaponChanger;
        }

        public void Initialize() => 
            _weaponSelector.NewWeaponChanged += _soundWeaponChanger.SetCurrentSound;

        public void Dispose() => 
            _weaponSelector.NewWeaponChanged -= _soundWeaponChanger.SetCurrentSound;
    }
}