using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Infrastructure;
using CodeBase.Services.Providers;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Popup;
using DG.Tweening;

namespace CodeBase.UI.LevelSlider
{
    public class LevelSliderPresenter : IDisposable, IGameplayRunnable
    {
        private readonly LevelSliderView _levelSliderView;
        private readonly Window _deathWindow;
        private readonly PlayerProvider _playerProvider;
        private readonly Window _victoryWindow;
        private List<Enemy> _enemies = new();
        private PlayerHealth _player;
        private readonly Window _pauseWindow;
        private readonly PopupTimerService _popupTimerService;

        public LevelSliderPresenter(LevelSliderView levelSliderView,
            WindowProvider windowProvider,
            IProvider<PlayerProvider> provider,
            PopupTimerService popupTimerService)
        {
            _popupTimerService = popupTimerService;
            _playerProvider = provider.Get();
            _playerProvider.Changed += SetPlayer;
            _levelSliderView = levelSliderView;
            _deathWindow = windowProvider.Windows[WindowTypeId.Death];
            _victoryWindow = windowProvider.Windows[WindowTypeId.Victory];
            _pauseWindow = windowProvider.Windows[WindowTypeId.Pause];
        }

        public void Run()
        {
            _levelSliderView.gameObject.SetActive(true);
            _levelSliderView.transform.DOScale(1, 1f).SetUpdate(true);
        }

        public void Init(List<Enemy> enemies)
        {
            _enemies = enemies;
            SubscribeToEnemyEvents();
            _popupTimerService.Initialized += _levelSliderView.Disable;
            _deathWindow.StartedToOpen += _levelSliderView.Disable;
            _victoryWindow.StartedToOpen += _levelSliderView.Disable;
            _pauseWindow.StartedToOpen += _levelSliderView.Disable;
            _pauseWindow.Closed += _levelSliderView.Enable;
            InitView();
        }

        public void Dispose()
        {
            UnsubscribeFromEnemyEvents();
            _deathWindow.StartedToOpen -= _levelSliderView.Disable;
            _popupTimerService.Initialized -= _levelSliderView.Disable;
            _player.Recovered -= _levelSliderView.Enable;
            _playerProvider.Changed -= SetPlayer;
            _pauseWindow.Closed -= _levelSliderView.Enable;
            _pauseWindow.StartedToOpen -= _levelSliderView.Disable;
            _victoryWindow.StartedToOpen -= _levelSliderView.Disable;
        }

        private void InitView()
        {
            _levelSliderView.SetMaxValue(_enemies.Count);
            _levelSliderView.transform.DOScale(0, 0);
            _levelSliderView.gameObject.SetActive(false);
        }

        private void SetPlayer(Player player)
        {
            _player = player.GetComponent<PlayerHealth>();
            _player.Recovered += _levelSliderView.Enable;
        }

        private void SubscribeToEnemyEvents()
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.Dead += IncreaseSlider;
                enemy.QuickDestroyed += IncreaseSlider;
                var materialChanger = enemy.GetComponent<IMaterialChanger>();
                materialChanger.StartedChanged += IncreaseSlider;
            }
        }

        private void UnsubscribeFromEnemyEvents()
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.Dead -= IncreaseSlider;
                enemy.QuickDestroyed -= IncreaseSlider;
            }
        }

        private void IncreaseSlider(Enemy obj) =>
            _levelSliderView.Increase(1);

        private void IncreaseSlider() =>
            _levelSliderView.Increase(1);
    }
}