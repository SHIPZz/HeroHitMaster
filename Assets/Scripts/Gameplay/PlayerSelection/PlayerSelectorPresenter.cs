using System;
using Zenject;

namespace Gameplay.PlayerSelection
{
    public class PlayerSelectorPresenter : IInitializable, IDisposable
    {
        private readonly PlayerSelectorView _playerSelectorView;
        private readonly PlayerSelector _playerSelector;

        public PlayerSelectorPresenter(PlayerSelectorView playerSelectorView, PlayerSelector playerSelector)
        {
            _playerSelectorView = playerSelectorView;
            _playerSelector = playerSelector;
        }

        public void Initialize()
        {
            _playerSelectorView.ApplyButtonClicked += _playerSelector.ApplyPlayer;
            _playerSelectorView.RightArrowClicked += _playerSelector.SelectNextPlayer;
            _playerSelectorView.LeftArrowClicked += _playerSelector.SelectPreviousPlayer;
            _playerSelector.PlayerChanged += _playerSelectorView.Show;
        }

        public void Dispose()
        {
            _playerSelector.PlayerChanged -= _playerSelectorView.Show;
            _playerSelectorView.ApplyButtonClicked -= _playerSelector.ApplyPlayer;
            _playerSelectorView.RightArrowClicked -= _playerSelector.SelectNextPlayer;
            _playerSelectorView.LeftArrowClicked -= _playerSelector.SelectPreviousPlayer;
        }
    }
}