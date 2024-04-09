using CodeBase.Enums;
using CodeBase.Infrastructure;
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
        private readonly IGameStateMachine _gameStateMachine;

        public CheatService(IAdService adService, Wallet wallet, IWorldDataService worldDataService,
            IPauseService pauseService, IPlayerStorage playerStorage,
            WindowService windowService, IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _windowService = windowService;
            _playerStorage = playerStorage;
            _pauseService = pauseService;
            _adService = adService;
            _wallet = wallet;
            _worldDataService = worldDataService;
        }

        public void Tick()
        {
            // if (Input.GetKeyDown(KeyCode.F8))
            // {
            //     _worldDataService.Reset();
            //     PlayerPrefs.DeleteAll();
            //     PlayerPrefs.Save();
            //     _gameStateMachine.ChangeState<BootstrapState>();
            // }
            //
            // if (Input.GetKeyDown(KeyCode.F))
            // {
            //     _worldDataService.WorldData.LevelData.Id = Mathf.Clamp( _worldDataService.WorldData.LevelData.Id + 1, 1, SceneManager.sceneCountInBuildSettings -1);
            //     _worldDataService.Save();
            //     _gameStateMachine.ChangeState<BootstrapState>(); 
            // }
            //
            // if (Input.GetKeyDown(KeyCode.F4))
            // {
            //     _windowService.CloseAll(() => _windowService.Open(WindowTypeId.Popup));
            // }
            //
            // if (Input.GetKeyDown(KeyCode.G))
            // {
            //     _worldDataService.WorldData.LevelData.Id = SceneManager.sceneCountInBuildSettings - 1;
            //     _worldDataService.Save();
            //     SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
            // }
            //
            // if (Input.GetKeyDown(KeyCode.K))
            // {
            //     _worldDataService.Save();
            // }
        }
    }
}