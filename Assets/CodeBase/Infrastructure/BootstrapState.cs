namespace CodeBase.Infrastructure
{
    public class BootstrapState : IEnter
    {
        private IGameStateMachine _gameStateMachine;

        public BootstrapState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            //init sdk
            _gameStateMachine.ChangeState<LevelLoadState>();
        }
    }
}