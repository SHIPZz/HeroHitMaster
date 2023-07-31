using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Storages;
using Zenject;

namespace CodeBase.UI.Windows.Death
{
    public class DeathPresenter : IInitializable, IDisposable
    {
        private readonly WindowService _windowService;
        private readonly DeathView _deathView;
        private readonly IAdService _adService;
        private PlayerHealth _playerHealth;
        private readonly List<Player> _players;

        public DeathPresenter(WindowService windowService, DeathView deathView, IAdService adService,
            IPlayerStorage playerStorage)
        {
            _players = playerStorage.GetAll();
            _adService = adService;
            _windowService = windowService;
            _deathView = deathView;
        }

        public void Initialize()
        {
            _players.ForEach(x => x.GetComponent<PlayerHealth>().ValueZeroReached += ShowDeathWindow);
            _deathView.RestartAdButtonClicked += DisableDeathWindowWithAd;
            _deathView.RestartButtonClicked += DisableDeathWindow;
        }

        public void Dispose()
        {
            _players.ForEach(x => x.GetComponent<PlayerHealth>().ValueZeroReached -= ShowDeathWindow);
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