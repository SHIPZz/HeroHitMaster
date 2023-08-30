using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Spawners;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Windows.Victory
{
    public class VictoryInfoPresenter : IInitializable, IDisposable
    {
        private readonly VictoryInvoView _victoryInvoView;
        private readonly Window _victoryWindow;
        private readonly WindowService _windowService;
        private readonly SaveTriggerOnLevelEnd _saveTriggerOnLevel;
        
        private int _deadEnemiesCount;
        private List<EnemySpawner> _enemySpawners;

        public VictoryInfoPresenter(VictoryInvoView victoryInvoView, 
            WindowProvider windowProvider, WindowService windowService, SaveTriggerOnLevelEnd saveTriggerOnLevel)
        {
            _victoryInvoView = victoryInvoView;
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
            _enemySpawners.ForEach(x => x.Destroyed -= CountDeadEnemies);
            _saveTriggerOnLevel.PlayerEntered -= OpenVictoryWindow;
            _victoryWindow.StartedToOpen -= FillVictoryInfo;
        }

        public void Init(List<EnemySpawner> enemySpawners)
        {
            _enemySpawners = enemySpawners;
            _enemySpawners.ForEach(x => x.Destroyed += CountDeadEnemies);
        }

        private void OpenVictoryWindow()
        {
            _windowService.CloseAll();
            _windowService.Open(WindowTypeId.Victory);
        }

        private void FillVictoryInfo()
        {
            _victoryInvoView.Show(_deadEnemiesCount, 100);
        }

        private void CountDeadEnemies()
        {
            _deadEnemiesCount++;
        }
    }
}