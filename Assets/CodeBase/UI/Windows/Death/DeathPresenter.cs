using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Services;
using CodeBase.Services.Ad;
using CodeBase.Services.Storages;
using CodeBase.Services.Storages.Character;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Windows.Death
{
    public class DeathPresenter : IInitializable, IDisposable
    {
        private readonly WindowService _windowService;
        private readonly DeathView _deathView;
        private readonly IAdService _adService;
        private PlayerHealth _playerHealth;
        private readonly List<PlayerHealth> _playerHealths = new();

        public DeathPresenter(WindowService windowService, DeathView deathView, IAdService adService,
            IPlayerStorage playerStorage)
        {
            playerStorage.GetAll().ForEach(x => _playerHealths.Add(x.GetComponent<PlayerHealth>()));
            _adService = adService;
            _windowService = windowService;
            _deathView = deathView;
        }

        public void Initialize()
        {
            _playerHealths.ForEach(x => x.ValueZeroReached += ShowDeathWindow);
            _deathView.RestartAdButtonClicked += DisableDeathWindowWithAd;
            _deathView.RestartButtonClicked += DisableDeathWindow;
        }

        public void Dispose()
        {
            _playerHealths.ForEach(x => x.ValueZeroReached += ShowDeathWindow);
            _deathView.RestartAdButtonClicked -= DisableDeathWindowWithAd;
            _deathView.RestartButtonClicked -= DisableDeathWindow;
        }

        private void DisableDeathWindow() => 
            _windowService.Close(WindowTypeId.Death);

        private void DisableDeathWindowWithAd()
        {
            DisableDeathWindow();
            _adService.PlayLongAd();
        }

        private void ShowDeathWindow()
        {
            _windowService.CloseAll();
            _windowService.Open(WindowTypeId.Death);
        }
    }
}