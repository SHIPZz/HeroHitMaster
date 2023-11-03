using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.Providers;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Play;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI.LevelSlider
{
    public class LevelSliderPresenter : IDisposable
    {
        private readonly LevelSliderView _levelSliderView;
        private readonly Window _deathWindow;
        private readonly PlayButtonView _playButtonView;
        private readonly PlayerProvider _playerProvider;
        private readonly Window _victoryWindow;
        private List<Enemy> _enemies = new();
        private PlayerHealth _player;

        public LevelSliderPresenter(LevelSliderView levelSliderView,
            WindowProvider windowProvider,
            PlayButtonView playButtonView,
            IProvider<PlayerProvider> provider)
        {
            _playButtonView = playButtonView;
            _playerProvider = provider.Get();
            _playerProvider.Changed += SetPlayer;
            _levelSliderView = levelSliderView;
            _deathWindow = windowProvider.Windows[WindowTypeId.Death];
            _victoryWindow = windowProvider.Windows[WindowTypeId.Victory];
        }

        public void Init(List<Enemy> enemies)
        {
            _enemies = enemies;
            SubscribeToEnemyEvents();
            _deathWindow.StartedToOpen += _levelSliderView.Disable;
            _playButtonView.Clicked += ActivateSlider;
            _victoryWindow.StartedToOpen += _levelSliderView.Disable;
            InitView();
        }

        public void Dispose()
        {
            UnsubscribeFromEnemyEvents();
            _playButtonView.Clicked -= ActivateSlider;
            _deathWindow.StartedToOpen -= _levelSliderView.Disable;
            _player.Recovered -= _levelSliderView.Enable;
            _playerProvider.Changed -= SetPlayer;
            _victoryWindow.StartedToOpen -= _levelSliderView.Disable;
        }

        private void InitView()
        {
            _levelSliderView.gameObject.SetActive(true);
            _levelSliderView.SetMaxValue(_enemies.Count);
            _levelSliderView.transform.DOScale(0, 0);
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

        private void ActivateSlider() =>
            _levelSliderView.transform.DOScale(1, 1f);

        private void IncreaseSlider(Enemy obj) =>
            _levelSliderView.Increase(1);

        private void IncreaseSlider() =>
            _levelSliderView.Increase(1);
    }
}