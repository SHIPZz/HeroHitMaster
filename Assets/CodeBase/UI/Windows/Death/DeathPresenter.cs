﻿using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Ad;
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
        private bool _isOpened;

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
            _isOpened = false;
        }

        private void ShowDeathWindow()
        {
            if (_isOpened)
                return;

            _isOpened = true;
            
            _windowService.CloseAll(() =>
            {
                _windowService.Open(WindowTypeId.Death, () => _isOpened = false);
            });
        }
    }
}