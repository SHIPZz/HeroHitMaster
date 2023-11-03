using System;
using CodeBase.Enums;
using Zenject;

namespace CodeBase.UI.Windows.Leaderboards
{
    public class AccuracyLeaderboardPresenter : IInitializable, IDisposable
    {
        private readonly AccuracyLeaderboard _accuracyLeaderboard;
        private readonly AccuracyLeaderboardOpenerButton _accuracyLeaderboardOpener;
        private readonly WindowService _windowService;

        public AccuracyLeaderboardPresenter(AccuracyLeaderboard accuracyLeaderboard,
            AccuracyLeaderboardOpenerButton accuracyLeaderboardOpener, WindowService windowService)
        {
            _windowService = windowService;
            _accuracyLeaderboard = accuracyLeaderboard;
            _accuracyLeaderboardOpener = accuracyLeaderboardOpener;
        }

        public void Initialize()
        {
            _accuracyLeaderboardOpener.Opened += OnOpened;
            _accuracyLeaderboard.Closed += OnClosed;
        }

        public void Dispose()
        {
            _accuracyLeaderboardOpener.Opened -= OnOpened;
            _accuracyLeaderboard.Closed -= OnClosed;
        }

        private void OnOpened()
        {
            _windowService.CloseAll();
            _windowService.Open(WindowTypeId.Leaderboard);
        }

        private void OnClosed() => 
            _windowService.CloseAll(() => _windowService.Open(WindowTypeId.SettingWindow));
    }
}