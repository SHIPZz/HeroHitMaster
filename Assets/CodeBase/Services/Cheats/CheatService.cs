using System;
using CodeBase.Services.Ad;
using CodeBase.Services.Pause;
using CodeBase.Services.Providers;
using CodeBase.UI.Wallet;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Cheats
{
    public class CheatService : ITickable
    {
        private readonly IAdService _adService;
        private readonly Wallet _wallet;
        private readonly IWorldDataService _worldDataService;
        private readonly IPauseService _pauseService;

        public CheatService(IAdService adService, Wallet wallet, IWorldDataService worldDataService,
            IPauseService pauseService)
        {
            _pauseService = pauseService;
            _adService = adService;
            _wallet = wallet;
            _worldDataService = worldDataService;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.F8))
            {
                UnityEngine.PlayerPrefs.DeleteAll();
                UnityEngine.PlayerPrefs.Save();
            }

            if (Input.GetKeyDown(KeyCode.F9))
            {
                _adService.PlayShortAd(StartCallback, OnEndCallback);
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