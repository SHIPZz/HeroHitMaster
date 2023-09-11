using System;
using CodeBase.Infrastructure;
using CodeBase.Services.SaveSystems;
using CodeBase.UI.Windows.Victory;
using Zenject;

namespace CodeBase.Gameplay.LoadNextLevel
{
    public class LoadNextLevelPresenter : IInitializable, IDisposable
    {
        private readonly ContinueButtonView _continueButtonView;
        private readonly IGameStateMachine _gameStateMachine;
        private ISaveSystem _saveSystem;

        public LoadNextLevelPresenter(ContinueButtonView continueButtonView, IGameStateMachine gameStateMachine,
            ISaveSystem saveSystem)
        {
            _continueButtonView = continueButtonView;
            _gameStateMachine = gameStateMachine;
            _saveSystem = saveSystem;
        }

        public void Initialize()
        {
            _continueButtonView.Clicked += Load;
        }

        public void Dispose()
        {
            _continueButtonView.Clicked -= Load;
        }

        private void Load()
        {
            _gameStateMachine.ChangeState<BootstrapState>();
        }
    }
}