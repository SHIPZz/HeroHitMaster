using System;
using Agava.WebUtility;
using CodeBase.Enums;
using CodeBase.Gameplay.Weapons;
using CodeBase.Infrastructure;
using CodeBase.Services.Pause;
using CodeBase.Services.Providers;
using CodeBase.UI.Windows.Popup;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Windows.Play
{
    public class PlayWindowPresenter : IInitializable, IDisposable, IGameplayRunnable
    {
        private readonly WindowService _windowService;
        private readonly PopupInfoView _popupInfoView;
        private readonly PlayWindowView _playWindowView;
        private readonly WeaponProvider _weaponProvider;
        private readonly IPauseService _pauseService;

        public PlayWindowPresenter(WindowService windowService,
            PopupInfoView popupInfoView,
            IProvider<WeaponProvider> provider,
            PlayWindowView playWindowView,
            IPauseService pauseService)
        {
            _pauseService = pauseService;
            _playWindowView = playWindowView;
            _weaponProvider = provider.Get();
            _popupInfoView = popupInfoView;
            _windowService = windowService;
        }

        public async void Initialize()
        {
            while (_weaponProvider.Initialized == false)
            {
                await UniTask.Yield();
            }

            _weaponProvider.Changed += OnWeaponChanged;
        }

        private void OnWeaponChanged(Weapon weapon) => 
            _playWindowView.SetCurrentWeapon(weapon.WeaponTypeId);

        public void Dispose()
        {
            _weaponProvider.Changed -= OnWeaponChanged;
        }

        public void Run() =>
            _windowService.Close(WindowTypeId.Play);

        public async void OnLoadingCurtainOnClosed()
        {
            _playWindowView.SetCurrentWeapon(_weaponProvider.Get().WeaponTypeId);

            while (_popupInfoView != null && _popupInfoView.isActiveAndEnabled)
                await UniTask.Yield();

            _windowService.Open(WindowTypeId.Play, _pauseService.UnPause);
        }
    }
}