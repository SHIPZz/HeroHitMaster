using System;
using CodeBase.Constants;
using CodeBase.Enums;
using CodeBase.Gameplay.Collision;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.Data;
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
        private WeaponStaticDataService _weaponStaticDataService;

        public event Action PlayerEntered;
        public event Action LastLevelAchieved;

        [Inject]
        private void Construct(IPauseService pauseService, ISaveSystem saveSystem, Level level, WeaponStaticDataService weaponStaticDataService)
        {
            _weaponStaticDataService = weaponStaticDataService;
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

        private async void OnPlayerEntered(Collider player)
        {
            var worldData =  await _saveSystem.Load<WorldData>();
            WeaponData currentWeapon = _weaponStaticDataService.Get(worldData.PlayerData.LastWeaponId);
            worldData.LevelData.Id = SceneManager.GetActiveScene().buildIndex + 0;
            worldData.LevelData.Id++;

            if (currentWeapon.Price.PriceTypeId == PriceTypeId.Popup)
                worldData.LevelData.LevelsWithPopupWeapon++;
            
            worldData.PlayerData.Money += _level.Reward;

            if (SceneManager.sceneCountInBuildSettings < worldData.LevelData.Id)
            {
                worldData.LevelData.Id = 0;
                PlayerEntered = null;
                LastLevelAchieved?.Invoke();
            }

            _saveSystem.Save(worldData);
            PlayerEntered?.Invoke();
            _pauseService.Pause();
        }
    }
}