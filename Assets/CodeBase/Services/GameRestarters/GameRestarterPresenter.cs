using System;
using CodeBase.UI.Windows.Death;
using Zenject;

namespace CodeBase.Services.GameRestarters
{
    public class GameRestarterPresenter : IInitializable, IDisposable
    {
        private readonly RestartButtonView _restartButtonView;
        private readonly GameRestarter _gameRestarter;

        public GameRestarterPresenter(RestartButtonView restartButtonView, GameRestarter gameRestarter)
        {
            _restartButtonView = restartButtonView;
            _gameRestarter = gameRestarter;
        }

        public void Initialize()
        {
            _restartButtonView.Clicked += _gameRestarter.Restart;
        }

        public void Dispose()
        {
            _restartButtonView.Clicked -= _gameRestarter.Restart;
        }
    }
}