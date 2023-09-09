using System;
using CodeBase.Constants;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Pause;
using CodeBase.Services.SaveSystems.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.SaveSystems.SaveTriggers
{
    public class SaveTriggerOnLevelEnd : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private int _level;
        
        private IPauseService _pauseService;
        private ISaveSystem _saveSystem;

        public event Action PlayerEntered;

        [Inject]
        private void Construct(IPauseService pauseService, ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _pauseService = pauseService;
        }

        private void Awake() => 
            gameObject.layer = LayerId.SaveTrigger;

        private void OnEnable() => 
            _triggerObserver.Entered += OnPlayerEntered;

        private void OnDisable() =>
            _triggerObserver.Entered -= OnPlayerEntered;

        private async void OnPlayerEntered(Collider obj)
        {
            var levelData =  await _saveSystem.Load<LevelData>();
            levelData.Id = _level;
            _saveSystem.Save(levelData);
            PlayerEntered?.Invoke();
            _pauseService.Pause();
        }
    }
}