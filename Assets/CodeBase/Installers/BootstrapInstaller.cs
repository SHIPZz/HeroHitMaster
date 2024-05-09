using System.Collections;
using Agava.YandexGames;
using CodeBase.Infrastructure;
using CodeBase.Services;
using CodeBase.Services.Ad;
using CodeBase.Services.Inputs.InputService;
using CodeBase.Services.Pause;
using CodeBase.Services.Providers;
using CodeBase.Services.Providers.AssetProviders;
using CodeBase.Services.SaveSystems;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        private bool _initialized;

        public override void InstallBindings()
        {
            BindAssetProvider();
            BindInputService();
            BindAdService();
            BindCoroutineStarter();
            BindLoadingCurtain();
            BindGameStateMachine();
            BindStateFactory();
            BindGlobalSlowMotionSystem();
            BindPauseService();
            Container.BindInterfacesTo<BootstrapInstaller>()
                .FromInstance(this).AsSingle();
        }

        public async void Initialize()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            await InitYandexSDK();
            YandexGamesSdk.CallbackLogging = false;
#endif

            BindSaveSystem();
            BindWorldDataService();
            BindAdInvoker();

            var gameStateMachine = Container.Resolve<IGameStateMachine>();
            gameStateMachine.ChangeState<BootstrapState>();
        }

        private async UniTask InitYandexSDK()
        {
            var coroutineStarter = Container.Resolve<ICoroutineStarter>();
            coroutineStarter.StartCoroutine(InitSDK());

            while (!_initialized)
            {
                await UniTask.Yield();
            }
        }

        private void BindWorldDataService() =>
            Container
                .Bind<IWorldDataService>()
                .To<WorldDataService>()
                .AsSingle();

        private void BindSaveSystem()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized)
                Container.Bind<ISaveSystem>().To<YandexSaveSystem>().AsSingle();
#endif

#if UNITY_EDITOR
            Container.Bind<ISaveSystem>().To<PlayerPrefsSaveSystem>().AsSingle();
#endif
        }

        private IEnumerator InitSDK()
        {
            yield return YandexGamesSdk.Initialize(() => _initialized = true);
        }

        private void BindAdInvoker() =>
            Container.Bind<IAdInvokerService>().To<AdInvokerService>().AsSingle();

        private void BindPauseService() =>
            Container.Bind<IPauseService>()
                .To<PauseService>()
                .AsSingle();

        private void BindGlobalSlowMotionSystem() =>
            Container
                .Bind<GlobalSlowMotionSystem>()
                .AsSingle();


        private void BindStateFactory() =>
            Container
                .Bind<IStateFactory>()
                .To<StateFactory>()
                .AsSingle();

        private void BindGameStateMachine() =>
            Container
                .Bind<IGameStateMachine>()
                .To<GameStateMachine>()
                .AsSingle();

        private void BindLoadingCurtain() =>
            Container.Bind<ILoadingCurtain>()
                .To<LoadingCurtain>()
                .FromInstance(GetComponent<LoadingCurtain>())
                .AsSingle();

        private void BindCoroutineStarter() =>
            Container.BindInstance(GetComponent<ICoroutineStarter>());

        private void BindAdService() =>
            Container.BindInterfacesAndSelfTo<YandexAdService>()
                .AsSingle();

        private void BindInputService() =>
            Container
                .Bind<IInputService>().To<InputService>()
                .AsSingle();

        private void BindAssetProvider() =>
            Container
                .Bind<AssetProvider>()
                .AsSingle();
    }
}