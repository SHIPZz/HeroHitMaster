using DG.Tweening;

namespace CodeBase.Infrastructure
{
    public class LevelLoadState : IState, IEnter
    {
        private IGameStateMachine _gameStateMachine;
        private ILoadingCurtain _loadingCurtain;

        public LevelLoadState(IGameStateMachine gameStateMachine, ILoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            DOTween.Sequence().AppendInterval(4f).OnComplete(() =>
            {
                _loadingCurtain.Hide(() => _gameStateMachine.ChangeState<GameState>());
            });
        }
    }
}