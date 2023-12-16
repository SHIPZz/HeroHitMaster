using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Character;
using CodeBase.Services.Ad;
using CodeBase.Services.Pause;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages.Character;
using CodeBase.UI.Wallet;
using CodeBase.UI.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Services.Cheats
{
    public class CheatService : ITickable
    {
        private readonly IAdService _adService;
        private readonly Wallet _wallet;
        private readonly IWorldDataService _worldDataService;
        private readonly IPauseService _pauseService;
        private readonly IPlayerStorage _playerStorage;
        private readonly WindowService _windowService;

        public CheatService(IAdService adService, Wallet wallet, IWorldDataService worldDataService,
            IPauseService pauseService, IPlayerStorage playerStorage,
            WindowService windowService)
        {
            _windowService = windowService;
            _playerStorage = playerStorage;
            _pauseService = pauseService;
            _adService = adService;
            _wallet = wallet;
            _worldDataService = worldDataService;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.F8))
            {
                _worldDataService.Reset();
                UnityEngine.PlayerPrefs.DeleteAll();
                UnityEngine.PlayerPrefs.Save();
                
            }

            if (Input.GetKeyDown(KeyCode.F4))
            {
                _windowService.CloseAll(() => _windowService.Open(WindowTypeId.Popup));
            }

            if (Input.GetKeyDown(KeyCode.F9))
            {
                _adService.PlayShortAd(StartCallback, OnEndCallback);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                SceneManager.LoadScene(15);
            }

            if (Input.GetKeyDown(KeyCode.F10))
            {
                _adService.PlayLongAd(StartCallback, OnEndCallback);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                _playerStorage.CurrentPlayer.GetComponent<IDamageable>().TakeDamage(10000);
            }
            

            if (Input.GetKeyDown(KeyCode.Y))
            {
                _wallet.AddMoney(5000);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                _worldDataService.Save();
            }
        }

        private void OnEndCallback()
        {
            _pauseService.UnPause();
            AudioListener.volume = 1;
        }

        private void OnEndCallback(bool closed)
        {
            if (!closed)
                return;

            _pauseService.UnPause();
            AudioListener.volume = 1;
        }

        private void StartCallback()
        {
            _pauseService.Pause();
            AudioListener.volume = 0f;
        }
    }
}