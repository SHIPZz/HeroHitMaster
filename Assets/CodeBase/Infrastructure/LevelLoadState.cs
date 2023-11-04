using CodeBase.Services.Ad;
using CodeBase.Services.Pause;
using CodeBase.Services.SaveSystems.Data;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class LevelLoadState : IState, IPayloadedEnter<WorldData>
    {
        private const int TargetAdInvoke = 3;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IPauseService _pauseService;
        private readonly IAdInvoker _adInvoker;

        private bool _canContinue;

        public LevelLoadState(ILoadingCurtain loadingCurtain, IPauseService pauseService, IAdInvoker adInvoker)
        {
            _adInvoker = adInvoker;
            _pauseService = pauseService;
            _loadingCurtain = loadingCurtain;
        }

        public async void Enter(WorldData payload)
        {
            _loadingCurtain.Show(1f);

            TryInvokeAd(payload);
            
            while (!_canContinue) 
                await UniTask.Yield();

            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(payload.LevelData.Id);

            while (!loadSceneAsync.isDone)
                await UniTask.Yield();

            _loadingCurtain.Hide();
            _pauseService.UnPause();
        }

        private void TryInvokeAd(WorldData payload)
        {
            if (payload.LevelData.Id % TargetAdInvoke == 0)
                _adInvoker.Init(() => _canContinue = false, () => _canContinue = true);
            else
                _canContinue = true;
        }
    }
}