using Agava.YandexGames;
using CodeBase.Infrastructure;
using CodeBase.Services;
using CodeBase.Services.Ad;
using CodeBase.Services.Inputs.InputService;
using CodeBase.Services.Pause;
using CodeBase.Services.Providers.AssetProviders;
using CodeBase.Services.SaveSystems;
using Zenject;

namespace CodeBase.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAssetProvider();
            BindInputService();
            BindAdService();
            BindCoroutineStarter();
            BindLoadingCurtain();
            BindGameStateMachine();
            BindStateFactory();
            BindSaveSystem();
            BindGlobalSlowMotionSystem();
            BindPauseService();
            BindPauseOnFocusChanged();
        }

        private void BindPauseOnFocusChanged() =>
            Container
                .BindInterfacesTo<PauseOnFocusChanged>()
                .AsSingle();

        private void BindPauseService() =>
            Container.Bind<IPauseService>()
                .To<PauseService>()
                .AsSingle();

        private void BindGlobalSlowMotionSystem() =>
            Container
                .Bind<GlobalSlowMotionSystem>()
                .AsSingle();

        private void BindSaveSystem()
        {
            // if (PlayerAccount.IsAuthorized)
            //     Container.Bind<ISaveSystem>()
            //         .To<YandexSaveSystem>()
            //         .AsSingle();
            // else
                Container.Bind<ISaveSystem>()
                    .To<PlayerPrefsSaveSystem>()
                    .AsSingle();
        }

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
                .BindInterfacesAndSelfTo<InputService>()
                .AsSingle();

        private void BindAssetProvider()
        {
            Container
                .BindInterfacesAndSelfTo<AssetProvider>()
                .AsSingle();
        }
    }
}