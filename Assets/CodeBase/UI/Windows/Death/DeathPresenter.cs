using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Storages.Character;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Windows.Death
{
    public class DeathPresenter : IInitializable, IDisposable
    {
        private readonly WindowService _windowService;
        private readonly DeathView _deathView;
        private readonly List<PlayerHealth> _playerHealths = new();
        private PlayerHealth _playerHealth;
        private bool _isOpened;

        public DeathPresenter(WindowService windowService, 
            DeathView deathView, 
            IPlayerStorage playerStorage)
        {
            playerStorage.GetAll().ForEach(x => _playerHealths.Add(x.GetComponent<PlayerHealth>()));
            _windowService = windowService;
            _deathView = deathView;
        }

        public void Initialize()
        {
            _playerHealths.ForEach(x =>
            {
                x.ValueZeroReached += ShowDeathWindow;
                x.Recovered += OnPlayerRecovered;
            });

            _deathView.RestartAdButtonClicked += DisableDeathWindowWithAd;
        }

        public void Dispose()
        {
            _playerHealths.ForEach(x =>
            {
                x.ValueZeroReached -= ShowDeathWindow;
                x.Recovered -= OnPlayerRecovered;
            });

            _deathView.RestartAdButtonClicked -= DisableDeathWindowWithAd;
        }

        private void DisableDeathWindowWithAd() => 
            _windowService.CloseAll();

        private void OnPlayerRecovered(int i)
        {
            _deathView.DisableAdButton();
            _windowService.OpenQuickly(WindowTypeId.Hud);
            _isOpened = false;
        }

        private void ShowDeathWindow()
        {
            if (_isOpened)
                return;

            _isOpened = true;
            
            _windowService.CloseAll(() => _windowService.Open(WindowTypeId.Death, () => _isOpened = false));
        }
    }
}