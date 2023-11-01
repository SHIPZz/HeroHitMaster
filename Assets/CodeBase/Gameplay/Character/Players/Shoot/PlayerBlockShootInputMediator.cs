using System;
using CodeBase.Infrastructure;
using CodeBase.UI.Windows.Play;
using Zenject;

namespace CodeBase.Gameplay.Character.Players.Shoot
{
    public class PlayerBlockShootInputMediator : IInitializable, IDisposable
    {
        private readonly PlayerShootInput _playerShootInput;
        private readonly PlayButtonView _playButtonView;

        public PlayerBlockShootInputMediator(PlayerShootInput playerShootInput, PlayButtonView playButtonView)
        {
            _playButtonView = playButtonView;
            _playerShootInput = playerShootInput;
        }

        public void Initialize() =>
            _playButtonView.Clicked += _playerShootInput.UnBlock;

        public void Dispose() =>
            _playButtonView.Clicked -= _playerShootInput.UnBlock;
    }
}