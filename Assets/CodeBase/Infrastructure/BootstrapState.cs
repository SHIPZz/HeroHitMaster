using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems.Data;
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
            DOTween.KillAll();
            DOTween.Clear();
            DOTween.Init();
            
            await _worldDataService.Load();
            _gameStateMachine.ChangeState<LevelLoadState, WorldData>(_worldDataService.WorldData);
        }
    }
}