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

namespace CodeBase.UI.LevelSlider
{
    public class LevelSliderPresenter : IDisposable
    {
        private readonly LevelSliderView _levelSliderView;
        private readonly Window _deathWindow;
        private readonly PlayButtonView _playButtonView;
        private Weapon _weapon;
        private List<Enemy> _enemies = new();
        private bool _isBlocked;
        private bool _isFilled;
        private PlayerHealth _player;
        private readonly PlayerProvider _playerProvider;

        public LevelSliderPresenter(LevelSliderView levelSliderView, WindowProvider windowProvider,
            PlayButtonView playButtonView, IProvider<PlayerProvider> provider)
        {
            _playButtonView = playButtonView;
            _playerProvider = provider.Get();
            _playerProvider.Changed += SetPlayer;
            _levelSliderView = levelSliderView;
            _deathWindow = windowProvider.Windows[WindowTypeId.Death];
        }

        private void SetPlayer(Player player)
        {
            _player = player.GetComponent<PlayerHealth>();
            _player.Recovered += _levelSliderView.Enable;
        }

        public void Init(Enemy enemy)
        {
            SubscribeToEnemyEvents(enemy);
            _enemies.Add(enemy);
            _deathWindow.StartedToOpen += _levelSliderView.Disable;
            _levelSliderView.gameObject.SetActive(true);
            _levelSliderView.SetMaxValue(_enemies.Count);
            _levelSliderView.transform.DOScale(0, 0);
            _playButtonView.Clicked += FillSlider;
        }

        public void Dispose()
        {
            foreach (var enemy in _enemies)
            {
                UnsubscribeFromEnemyEvents(enemy);
            }

            _playButtonView.Clicked -= FillSlider;
            _deathWindow.StartedToOpen -= _levelSliderView.Disable;
            _player.Recovered -= _levelSliderView.Enable;
            _playerProvider.Changed -= SetPlayer;
        }

        private void SubscribeToEnemyEvents(Enemy enemy)
        {
            enemy.Dead += DecreaseSlider;
            enemy.QuickDestroyed += DecreaseSlider;
            var materialChanger = enemy.GetComponent<IMaterialChanger>();
            materialChanger.StartedChanged += DecreaseSlider;
            materialChanger.StartedChanged += BlockAnotherDecrease;
        }

        private void UnsubscribeFromEnemyEvents(Enemy enemy)
        {
            enemy.Dead -= DecreaseSlider;
            enemy.QuickDestroyed -= DecreaseSlider;
        }

        private void FillSlider()
        {
            if (_isFilled)
                return;

            _isFilled = true;
            _levelSliderView.transform.DOScale(1, 0.3f);
            _levelSliderView.SetValue(_enemies.Count);
        }

        private void DecreaseSlider(Enemy obj)
        {
            if (_isBlocked)
                return;

            DecreaseSlider();
        }

        private void DecreaseSlider() => 
            _levelSliderView.Decrease(1);

        private void BlockAnotherDecrease() => 
            _isBlocked = true;
    }
}
