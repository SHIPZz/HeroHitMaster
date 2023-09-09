using System;
using CodeBase.Constants;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Pause;
using CodeBase.Services.SaveSystems.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Services.SaveSystems.SaveTriggers
{
    public class SaveTriggerOnLevelEnd : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        
        private IPauseService _pauseService;
        private ISaveSystem _saveSystem;
        private Level _level;

        public event Action PlayerEntered;

        [Inject]
        private void Construct(IPauseService pauseService, ISaveSystem saveSystem, Level level)
        {
            _level = level;
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
            var playerData = await _saveSystem.Load<PlayerData>();
            print(SceneManager.GetActiveScene().buildIndex + 0);
            levelData.Id = SceneManager.GetActiveScene().buildIndex + 0;
            levelData.Id++;
            playerData.Money += _level.Reward;
            _saveSystem.Save(levelData);
            PlayerEntered?.Invoke();
            _pauseService.Pause();
        }
    }
}