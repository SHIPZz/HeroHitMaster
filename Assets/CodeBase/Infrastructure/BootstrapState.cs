using CodeBase.Services.Providers;
using DG.Tweening;

namespace CodeBase.Infrastructure
{
    public class BootstrapState : IState, IEnter
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IWorldDataService _worldDataService;

        public BootstrapState(IGameStateMachine gameStateMachine, IWorldDataService worldDataService)
        {
            _worldDataService = worldDataService;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            DOTween.Clear();
            DOTween.Init();
            DOTween.RestartAll();
            
            await _worldDataService.Load();
            
            _gameStateMachine.ChangeState<LevelLoadState, int>(_worldDataService.WorldData.LevelData.Id);
        }
    }
}