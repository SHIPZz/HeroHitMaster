using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using DG.Tweening;
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
            // while (!YandexGamesSdk.IsInitialized)
            // {
            //     await UniTask.Yield();
            // }
            
            DOTween.Clear();

            var levelData = await _saveSystem.Load<LevelData>();
            levelData.Id = 4;
            // _saveSystem.Save(levelData);
            _gameStateMachine.ChangeState<LevelLoadState, int>(levelData.Id);
        }
    }
}