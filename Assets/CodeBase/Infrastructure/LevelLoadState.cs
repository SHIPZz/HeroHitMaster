using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class LevelLoadState : IState, IPayloadedEnter<int>, IExit
    {
        private IGameStateMachine _gameStateMachine;
        private ILoadingCurtain _loadingCurtain;

        public LevelLoadState(IGameStateMachine gameStateMachine, ILoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter(int payload)
        {
            _loadingCurtain.Show(1f);
            await SceneManager.LoadSceneAsync(payload);
            _loadingCurtain.Hide(null);
        }

        public void Exit()
        {
            
        }
    }
}