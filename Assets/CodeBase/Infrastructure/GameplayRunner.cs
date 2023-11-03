using System;
using System.Collections.Generic;
using CodeBase.UI.Windows.Play;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameplayRunner : IInitializable, IDisposable
    {
        private readonly IEnumerable<IGameplayRunnable> _gameplayRunnables;
        private readonly PlayButtonView _playButtonView;

        public GameplayRunner(IEnumerable<IGameplayRunnable> gameplayRunnables, PlayButtonView playButtonView)
        {
            _playButtonView = playButtonView;
            _gameplayRunnables = gameplayRunnables;
        }

        public void Initialize() => 
            _playButtonView.Clicked += Do;

        public void Dispose() => 
            _playButtonView.Clicked -= Do;

        private void Do()
        {
            foreach (var gameplayRunnable in _gameplayRunnables)
            {
                gameplayRunnable.Run();
            }
        }
    }
}