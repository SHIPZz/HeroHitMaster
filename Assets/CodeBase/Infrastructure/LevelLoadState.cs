using CodeBase.Services.Ad;
using CodeBase.Services.Pause;
using CodeBase.Services.SaveSystems.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class LevelLoadState : IState, IPayloadedEnter<WorldData>
    {
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IPauseService _pauseService;
        private readonly IAdInvokerService _adInvokerService;

        public LevelLoadState(ILoadingCurtain loadingCurtain, IPauseService pauseService, IAdInvokerService adInvokerService)
        {
            _adInvokerService = adInvokerService;
            _pauseService = pauseService;
            _loadingCurtain = loadingCurtain;
        }

        public async void Enter(WorldData payload)
        {
            _loadingCurtain.Show(2f);

            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(payload.LevelData.Id);

            while (!loadSceneAsync.isDone)
                await UniTask.Yield();

            _loadingCurtain.Hide(() => _adInvokerService.Invoke(_pauseService.UnPause));
        }
    }
}