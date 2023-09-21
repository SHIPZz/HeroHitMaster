using System.Collections;
using Agava.YandexGames;
using CodeBase.Services;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class BootstrapState : IState, IEnter
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISaveSystem _saveSystem;
        private readonly ICoroutineStarter _coroutineStarter;
        private bool _gameStarted;

        public BootstrapState(IGameStateMachine gameStateMachine, ISaveSystem saveSystem,
            ICoroutineStarter coroutineStarter)
        {
            _coroutineStarter = coroutineStarter;
            _saveSystem = saveSystem;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            _coroutineStarter.StartCoroutine(Init());

            while (!_gameStarted)
            {
                await UniTask.Yield();
            }
            
            DOTween.Clear();

            var levelData = await _saveSystem.Load<LevelData>();
            levelData.Id = 1;
            _gameStateMachine.ChangeState<LevelLoadState, int>(levelData.Id);
        }

        private IEnumerator Init()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            _gameStarted = true;
            yield break;
#endif
            yield return YandexGamesSdk.Initialize();
            YandexGamesSdk.CallbackLogging = false;
            _gameStarted = true;
        }
    }
}