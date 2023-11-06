using System;
using CodeBase.Infrastructure;
using CodeBase.UI.Windows.Death;
using Zenject;

namespace CodeBase.Services.GameRestarters
{
    public class GameRestarterMediator : IInitializable, IDisposable
    {
        private readonly RestartButtonView _restartButtonView;
        private readonly IGameStateMachine _gameStateMachine;

        public GameRestarterMediator(RestartButtonView restartButtonView, IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _restartButtonView = restartButtonView;
        }

        public void Initialize() => 
            _restartButtonView.Clicked += _gameStateMachine.ChangeState<BootstrapState>;

        public void Dispose() => 
            _restartButtonView.Clicked -= _gameStateMachine.ChangeState<BootstrapState>;
    }
}