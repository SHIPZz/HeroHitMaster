using System;
using CodeBase.Enums;
using CodeBase.Infrastructure;
using CodeBase.Services.SaveSystems.SaveTriggers;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Windows.Gameover
{
    public class GameOverPresenter : IInitializable, IDisposable
    {
        private readonly SaveTriggerOnLevelEnd _saveTriggerOnLevelEnd;
        private readonly GameOverView _gameOverView;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly WindowService _windowService;

        public GameOverPresenter(SaveTriggerOnLevelEnd saveTriggerOnLevelEnd, GameOverView gameOverView,
            IGameStateMachine gameStateMachine, WindowService windowService)
        {
            _windowService = windowService;
            _saveTriggerOnLevelEnd = saveTriggerOnLevelEnd;
            _gameOverView = gameOverView;
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            _saveTriggerOnLevelEnd.LastLevelAchieved += OpenGameOverView;
            _gameOverView.Disabled += _gameStateMachine.ChangeState<BootstrapState>;
        }

        public void Dispose()
        {
            _saveTriggerOnLevelEnd.LastLevelAchieved -= OpenGameOverView;
            _gameOverView.Disabled -= _gameStateMachine.ChangeState<BootstrapState>;
        }

        private void OpenGameOverView()
        {
            _windowService.CloseAll(() => _windowService.Open(WindowTypeId.GameOver));
            _gameOverView.Init();
        }
    }
}