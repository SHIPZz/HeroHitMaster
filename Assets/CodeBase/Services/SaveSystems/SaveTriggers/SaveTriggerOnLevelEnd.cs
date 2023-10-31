using System;
using CodeBase.Constants;
using CodeBase.Enums;
using CodeBase.Gameplay.Collision;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.Data;
using CodeBase.Services.Pause;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.UI.Wallet;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Services.SaveSystems.SaveTriggers
{
    public class SaveTriggerOnLevelEnd : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        
        private IPauseService _pauseService;
        private Level _level;
        private WeaponStaticDataService _weaponStaticDataService;
        private Wallet _wallet;
        private IWorldDataService _worldDataService;

        public event Action PlayerEntered;
        public event Action LastLevelAchieved;

        [Inject]
        private void Construct(IPauseService pauseService, 
       IWorldDataService worldDataService,
            Level level,
            WeaponStaticDataService weaponStaticDataService, Wallet wallet)
        {
            _worldDataService = worldDataService;
            _wallet = wallet;
            _weaponStaticDataService = weaponStaticDataService;
            _level = level;
            _pauseService = pauseService;
        }

        private void Awake() => 
            gameObject.layer = LayerId.SaveTrigger;

        private void OnEnable() => 
            _triggerObserver.Entered += OnPlayerEntered;

        private void OnDisable() =>
            _triggerObserver.Entered -= OnPlayerEntered; 

        private void OnPlayerEntered(Collider player)
        {
            WorldData worldData = _worldDataService.WorldData;
            WeaponData currentWeapon = _weaponStaticDataService.Get(worldData.PlayerData.LastWeaponId);
            worldData.LevelData.Id = SceneManager.GetActiveScene().buildIndex + 0;
            worldData.LevelData.Id++;

            if (currentWeapon.Price.PriceTypeId == PriceTypeId.Popup)
                worldData.LevelData.LevelsWithPopupWeapon++;
            
            _wallet.AddMoney(_level.Reward);

            if (SceneManager.sceneCountInBuildSettings < worldData.LevelData.Id)
            {
                worldData.LevelData.Id = 0;
                PlayerEntered = null;
                LastLevelAchieved?.Invoke();
            }

            _worldDataService.Save();
            PlayerEntered?.Invoke();
            _pauseService.Pause();
        }
    }
}