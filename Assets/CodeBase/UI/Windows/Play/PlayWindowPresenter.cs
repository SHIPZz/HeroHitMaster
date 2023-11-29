using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Weapons;
using CodeBase.Infrastructure;
using CodeBase.Services.Pause;
using CodeBase.Services.Providers;
using CodeBase.UI.Weapons;
using CodeBase.UI.Windows.Popup;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.UI.Windows.Play
{
    public class PlayWindowPresenter : IInitializable, IDisposable, IGameplayRunnable
    {
        private readonly WindowService _windowService;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly PopupInfoView _popupInfoView;
        private readonly PlayWindowView _playWindowView;
        private readonly WeaponProvider _weaponProvider;
        private readonly IPauseService _pauseService;

        public PlayWindowPresenter(WindowService windowService,
            ILoadingCurtain loadingCurtain,
            PopupInfoView popupInfoView,
            IProvider<WeaponProvider> provider,
            PlayWindowView playWindowView,
            IPauseService pauseService)
        {
            _pauseService = pauseService;
            _playWindowView = playWindowView;
            _weaponProvider = provider.Get();
            _popupInfoView = popupInfoView;
            _loadingCurtain = loadingCurtain;
            _windowService = windowService;
        }

        public async void Initialize()
        {
            _loadingCurtain.Closed += OnLoadingCurtainOnClosed;

            while (_weaponProvider.Initialized == false)
            {
                await UniTask.Yield();
            }

            _weaponProvider.Changed += OnWeaponChanged;
            _playWindowView.SetCurrentWeapon(_weaponProvider.Get().WeaponTypeId);
        }

        private void OnWeaponChanged(Weapon weapon) => 
            _playWindowView.SetCurrentWeapon(weapon.WeaponTypeId);

        public void Dispose()
        {
            _loadingCurtain.Closed -= OnLoadingCurtainOnClosed;
            _weaponProvider.Changed -= OnWeaponChanged;
        }

        public void Run() =>
            _windowService.Close(WindowTypeId.Play);

        private async void OnLoadingCurtainOnClosed()
        {
            while (_popupInfoView != null && _popupInfoView.isActiveAndEnabled)
                await UniTask.Yield();

            if (_popupInfoView != null)
            {
                _windowService.Open(WindowTypeId.Play);
                _pauseService.UnPause();
            }
        }
    }
}