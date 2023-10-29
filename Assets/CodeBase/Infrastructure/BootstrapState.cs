using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.Services.Core;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class BootstrapState : IState, IEnter
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISaveSystem _saveSystem;

        public BootstrapState(IGameStateMachine gameStateMachine, ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            DOTween.Clear();
            DOTween.Init();
            DOTween.RestartAll();
            
            var levelData = await _saveSystem.Load<LevelData>();
            await UnityServices.InitializeAsync();

            while (UnityServices.State == ServicesInitializationState.Uninitialized)
            {
                await UniTask.Yield();
            }
            
            _gameStateMachine.ChangeState<LevelLoadState, int>(levelData.Id);
        }
    }
}