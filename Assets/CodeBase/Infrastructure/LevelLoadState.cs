using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class LevelLoadState : IState, IPayloadedEnter<int>, IExit
    {
        private readonly ILoadingCurtain _loadingCurtain;

        public LevelLoadState(ILoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
        }

        public async void Enter(int payload)
        {
            _loadingCurtain.Show(1f);
            DOTween.Init();
            DOTween.RestartAll();
            await SceneManager.LoadSceneAsync(payload);
            _loadingCurtain.Hide();
        }

        public void Exit()
        {
            
        }
    }
}