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

        public LoadNextLevelPresenter(ContinueButtonView continueButtonView, IGameStateMachine gameStateMachine)
        {
            _continueButtonView = continueButtonView;
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize() =>
            _continueButtonView.Clicked += Load;

        public void Dispose() =>
            _continueButtonView.Clicked -= Load;

        private void Load() =>
            _gameStateMachine.ChangeState<BootstrapState>();
    }
}