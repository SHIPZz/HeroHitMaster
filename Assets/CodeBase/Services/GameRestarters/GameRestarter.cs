using CodeBase.Infrastructure;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.GameRestarters
{
    public class GameRestarter
    {
        private readonly IGameStateMachine _gameStateMachine;

        public GameRestarter(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        public void Restart()
        {
            _gameStateMachine.ChangeState<BootstrapState>();
        }
    }
}