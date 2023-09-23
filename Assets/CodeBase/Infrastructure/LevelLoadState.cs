using CodeBase.Services.Pause;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class LevelLoadState : IState, IPayloadedEnter<int>
    {
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IPauseService _pauseService;

        public LevelLoadState(ILoadingCurtain loadingCurtain, IPauseService pauseService)
        {
            _pauseService = pauseService;
            _loadingCurtain = loadingCurtain;
        }

        public async void Enter(int payload)
        {
            _loadingCurtain.Show(1f);
            _pauseService.UnPause();
            DOTween.Init();
            DOTween.RestartAll();
            await SceneManager.LoadSceneAsync(payload);
            _loadingCurtain.Hide();
        }
    }
}