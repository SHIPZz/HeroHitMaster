using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Spawners;
using CodeBase.Services;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.SaveTriggers;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Windows.Victory
{
    public class VictoryInfoPresenter : IInitializable, IDisposable
    {
        private readonly VictoryInfoView _victoryInfoView;
        private readonly Window _victoryWindow;
        private readonly WindowService _windowService;
        private readonly SaveTriggerOnLevelEnd _saveTriggerOnLevel;
        private readonly CountEnemiesOnDeath _countEnemiesOnDeath;
        private readonly Level _level;
        
        private int _deadEnemiesCount;
        private List<EnemySpawner> _enemySpawners;

        public VictoryInfoPresenter(VictoryInfoView victoryInfoView, 
            WindowProvider windowProvider, WindowService windowService, 
            SaveTriggerOnLevelEnd saveTriggerOnLevel, CountEnemiesOnDeath countEnemiesOnDeath, Level level)
        {
            _level = level;
            _countEnemiesOnDeath = countEnemiesOnDeath;
            _victoryInfoView = victoryInfoView;
            _windowService = windowService;
            _saveTriggerOnLevel = saveTriggerOnLevel;
            _victoryWindow = windowProvider.Windows[WindowTypeId.Victory];
        }

        public void Initialize()
        {
            _victoryWindow.StartedToOpen += FillVictoryInfo;
            _saveTriggerOnLevel.PlayerEntered += OpenVictoryWindow;
        }

        public void Dispose()
        {
            _saveTriggerOnLevel.PlayerEntered -= OpenVictoryWindow;
            _victoryWindow.StartedToOpen -= FillVictoryInfo;
        }

        private void OpenVictoryWindow()
        {
            _windowService.CloseAll();
            _windowService.Open(WindowTypeId.Victory);
        }

        private void FillVictoryInfo()
        {
            _victoryInfoView.Show(_countEnemiesOnDeath.Quantity, _level.Reward);
        }
    }
}