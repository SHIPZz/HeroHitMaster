using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using DG.Tweening;

namespace CodeBase.Infrastructure
{
    public class BootstrapState : IState, IEnter
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISaveSystem _saveSystem;

        public BootstrapState(IGameStateMachine gameStateMachine, ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            DOTween.Clear();
            DOTween.Init();
            DOTween.RestartAll();
            
            var worldData = await _saveSystem.Load<WorldData>();
            
            _gameStateMachine.ChangeState<LevelLoadState, int>(worldData.LevelData.Id);
        }
    }
}