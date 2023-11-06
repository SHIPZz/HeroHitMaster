using System;
using Agava.YandexGames;
using CodeBase.Enums;
using CodeBase.UI.Windows.Leaderboards;
using Zenject;

namespace CodeBase.UI.Windows.Authorize
{
    public class AuthorizeWindowPresenter : IInitializable, IDisposable
    {
        private readonly AuthorizeWindowView _authorizeWindowView;
        private readonly WindowService _windowService;
        private readonly AccuracyLeaderboardOpenerButton _accuracyLeaderboardOpenerButton;
        private bool _isShown;

        public AuthorizeWindowPresenter(AuthorizeWindowView authorizeWindowView,
            WindowService windowService, AccuracyLeaderboardOpenerButton accuracyLeaderboardOpenerButton)
        {
            _accuracyLeaderboardOpenerButton = accuracyLeaderboardOpenerButton;
            _windowService = windowService;
            _authorizeWindowView = authorizeWindowView;
        }

        public void Initialize()
        {
            _authorizeWindowView.AuthorizeButtonClicked += OnAuthorizedClicked;
            _authorizeWindowView.CloseButtonClicked += OnClosed;
            _accuracyLeaderboardOpenerButton.Opened += OnLeaderboardOpenerButtonClicked;
        }

        private void OnAuthorizedClicked()
        {
            PlayerAccount.Authorize(() =>
            {
                _windowService.CloseAll();
                _windowService.Open(WindowTypeId.Leaderboard);
            });
        }

        public void Dispose()
        {
            _authorizeWindowView.AuthorizeButtonClicked -= OnAuthorizedClicked;
            _authorizeWindowView.CloseButtonClicked -= OnClosed;
            _accuracyLeaderboardOpenerButton.Opened -= OnLeaderboardOpenerButtonClicked;
        }

        private void OnClosed()
        {
            _windowService.Close(WindowTypeId.Authorize);
            _windowService.Open(WindowTypeId.SettingWindow);
        }

        private void OnLeaderboardOpenerButtonClicked()
        {
            if (PlayerAccount.IsAuthorized)
            {
                _windowService.Open(WindowTypeId.Leaderboard);
                return;
            }
        
            if (_isShown)
                return;
                
            _windowService.CloseAll();
            _windowService.Open(WindowTypeId.Authorize);
            _isShown = true;
        }
    }
}