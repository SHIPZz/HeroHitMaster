using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Services.Providers;
using CodeBase.UI.Windows;

namespace CodeBase.UI.LevelSlider
{
    public class LevelSliderPresenter :  IDisposable
    {
        private readonly LevelSliderView _levelSliderView;
        private readonly Window _playWindow;
        private List<Enemy> _enemies = new();
        private bool _isBlocked;

        public LevelSliderPresenter(LevelSliderView levelSliderView, WindowProvider windowProvider)
        {
            _levelSliderView = levelSliderView;
            _playWindow = windowProvider.Windows[WindowTypeId.Play];
            _playWindow.Closed += FillSlider;
        }

        public void Init(Enemy enemy)
        {
            enemy.Dead += DecreaseSlider;
            enemy.QuickDestroyed += DecreaseSlider;
            var materialChanger = enemy.GetComponent<IMaterialChanger>();
            materialChanger.StartedChanged += DecreaseSlider;
            materialChanger.StartedChanged += BlockAnotherDecrease;
            _enemies.Add(enemy);
            _levelSliderView.SetMaxValue(_enemies.Count);
            _levelSliderView.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            foreach (var enemy in _enemies)
            {
                enemy.Dead -= DecreaseSlider;
                enemy.QuickDestroyed -= DecreaseSlider;
            }
            
            _playWindow.Closed -= FillSlider;
        }

        private void FillSlider()
        {
            _levelSliderView.gameObject.SetActive(true);
            _levelSliderView.SetValue(_enemies.Count);
        }

        private void DecreaseSlider(Enemy obj)
        {
            if (_isBlocked)
                return;

            _levelSliderView.Decrease(1);
        }

        private void DecreaseSlider() => 
        _levelSliderView.Decrease(1);

        private void BlockAnotherDecrease() =>
            _isBlocked = true;
    }
}