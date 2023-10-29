using CodeBase.Services.Ad;
using CodeBase.Services.Pause;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class LevelLoadState : IState, IPayloadedEnter<int>
    {
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IPauseService _pauseService;
        private readonly IAdInvoker _adInvoker;

        public LevelLoadState(ILoadingCurtain loadingCurtain, IPauseService pauseService, IAdInvoker adInvoker)
        {
            _adInvoker = adInvoker;
            _pauseService = pauseService;
            _loadingCurtain = loadingCurtain;
        }

        public async void Enter(int payload)
        {
            _loadingCurtain.Show(1f);
            _adInvoker.Init();

            AsyncOperation loadSceneAsync =  SceneManager.LoadSceneAsync(payload);

            while (!loadSceneAsync.isDone) 
                await UniTask.Yield();

            _loadingCurtain.Hide();
            _pauseService.UnPause();
        }
    }
}