using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Weapons;
using CodeBase.Infrastructure;
using CodeBase.Services.Providers;
using Zenject;

namespace CodeBase.UI.Windows.Play
{
    public class PlayWindowPresenter : IInitializable, IDisposable
    {
        private readonly WindowService _windowService;
        private readonly WeaponProvider _weaponProvider;
        private readonly ILoadingCurtain _loadingCurtain;
        private Weapon _weapon;

        public PlayWindowPresenter(WindowService windowService, IProvider<WeaponProvider> provider, ILoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
            _weaponProvider = provider.Get();
            _weaponProvider.Changed += SetWeapon;
            _windowService = windowService;
        }

        private void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
            _weapon.Shooted += Disable;
        }

        public void Initialize()
        {
            _loadingCurtain.Closed += Enable;
        }

        public void Dispose()
        {
            _weapon.Shooted -= Disable;
            _loadingCurtain.Closed -= Enable;
        }

        private void Enable() => 
            _windowService.Open(WindowTypeId.Play);

        private void Disable() =>
            _windowService.Close(WindowTypeId.Play);
    }
}