using System;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.Services.SaveSystems.SaveTriggers;
using Zenject;

namespace CodeBase.UI.Windows.Leaderboards
{
    public class AccuracyLeaderboardPresenter : IInitializable, IDisposable
    {
        private readonly AccuracyLeaderboard _accuracyLeaderboard;
        private readonly AccuracyLeaderboardOpenerButton _accuracyLeaderboardOpener;
        private readonly WindowService _windowService;
        private readonly IWorldDataService _worldDataService;
        private readonly SaveTriggerOnLevelEnd _saveTriggerOnLevelEnd;

        public AccuracyLeaderboardPresenter(AccuracyLeaderboard accuracyLeaderboard,
            AccuracyLeaderboardOpenerButton accuracyLeaderboardOpener, 
            WindowService windowService, 
            IWorldDataService worldDataService, 
            SaveTriggerOnLevelEnd saveTriggerOnLevelEnd)
        {
            _saveTriggerOnLevelEnd = saveTriggerOnLevelEnd;
            _worldDataService = worldDataService;
            _windowService = windowService;
            _accuracyLeaderboard = accuracyLeaderboard;
            _accuracyLeaderboardOpener = accuracyLeaderboardOpener;
        }

        public void Initialize()
        {
            _accuracyLeaderboardOpener.Opened += OnOpenedClicked;
            _accuracyLeaderboard.Closed += OnClosed;
            _saveTriggerOnLevelEnd.PlayerEntered += OnLevelFinished;
        }

        public void Dispose()
        {
            _accuracyLeaderboardOpener.Opened -= OnOpenedClicked;
            _accuracyLeaderboard.Closed -= OnClosed;
            _saveTriggerOnLevelEnd.PlayerEntered -= OnLevelFinished;
        }

        private void OnLevelFinished() => 
            _accuracyLeaderboard.SetScore(_worldDataService.WorldData.PlayerData.ShootAccuracy);

        private void OnOpenedClicked()
        {
            WorldData worldData = _worldDataService.WorldData;
            PlayerData playerData = _worldDataService.WorldData.PlayerData;

            _accuracyLeaderboard.SetInfo(worldData.TranslatedWeaponNameData.Names[playerData.LastNotPopupWeaponId],
                playerData.LastNotPopupWeaponId);
        }

        private void OnClosed() => 
            _windowService.CloseAll(() => _windowService.Open(WindowTypeId.SettingWindow));
    }
}