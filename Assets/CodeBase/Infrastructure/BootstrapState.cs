using System.Collections;
using Agava.YandexGames;
using CodeBase.Services;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace CodeBase.Infrastructure
{
    public class BootstrapState : IState, IEnter
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISaveSystem _saveSystem;
        private bool _gameStarted;

        public BootstrapState(IGameStateMachine gameStateMachine, ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            // while (!YandexGamesSdk.IsInitialized)
            // {
            //     await UniTask.Yield();
            // }

            DOTween.Clear();

            var levelData = await _saveSystem.Load<LevelData>();
            _gameStateMachine.ChangeState<LevelLoadState, int>(levelData.Id);
        }
    }
}