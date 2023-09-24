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
        private ILoadingCurtain _loadingCurtain;

        public PlayerBlockShootInputMediator(PlayerShootInput playerShootInput, PlayButtonView playButtonView, ILoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
            _playButtonView = playButtonView;
            _playerShootInput = playerShootInput;
        }

        public void Initialize()
        {
            _playButtonView.Clicked += _playerShootInput.UnBlock;
        }

        public void Dispose()
        {
            _playButtonView.Clicked -= _playerShootInput.UnBlock;
        }
    }
}