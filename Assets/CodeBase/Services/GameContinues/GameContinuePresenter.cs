using System;
using CodeBase.UI.Windows.Death;
using Zenject;

namespace CodeBase.Services.GameContinues
{
    public class GameContinuePresenter : IInitializable, IDisposable
    {
        private readonly GameContinue _gameContinue;
        private readonly ContinueADButtonView _continueADButtonView;

        public GameContinuePresenter(GameContinue gameContinue, ContinueADButtonView continueADButtonView)
        {
            _gameContinue = gameContinue;
            _continueADButtonView = continueADButtonView;
        }

        public void Initialize()
        {
            _continueADButtonView.Clicked += _gameContinue.Continue;
        }

        public void Dispose()
        {
            _continueADButtonView.Clicked -= _gameContinue.Continue;
        }
    }
}