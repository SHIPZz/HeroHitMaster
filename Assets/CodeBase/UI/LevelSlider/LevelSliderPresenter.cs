using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.Providers;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI.LevelSlider
{
    public class LevelSliderPresenter :  IDisposable
    {
        private readonly LevelSliderView _levelSliderView;
        private readonly WeaponProvider _weaponProvider;
        private List<Enemy> _enemies = new();
        private bool _isBlocked;
        private bool _isFilled;
        private Weapon _weapon;
        private int _enemyCount;

        public LevelSliderPresenter(LevelSliderView levelSliderView, IProvider<WeaponProvider> weaponProvider)
        {
            _levelSliderView = levelSliderView;
            _weaponProvider = weaponProvider.Get();
            _weaponProvider.Changed += SetWeapon;
        }

        public void Init(Enemy enemy)
        {
            enemy.Dead += DecreaseSlider;
            enemy.QuickDestroyed += DecreaseSlider;
            var materialChanger = enemy.GetComponent<IMaterialChanger>();
            materialChanger.StartedChanged += DecreaseSlider;
            materialChanger.StartedChanged += BlockAnotherDecrease;
            _enemies.Add(enemy);
            _levelSliderView.gameObject.SetActive(true);
            _levelSliderView.SetMaxValue(_enemies.Count);
            _levelSliderView.transform.DOScale(0,0);
        }

        public void Dispose()
        {
            foreach (var enemy in _enemies)
            {
                enemy.Dead -= DecreaseSlider;
                enemy.QuickDestroyed -= DecreaseSlider;
            }
            
            _weapon.Shooted -= FillSlider;
        }

        private void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
            _weapon.Shooted += FillSlider;
        }

        private void FillSlider()
        {
            if(_isFilled)
                return;
            
            _isFilled = true;
            _levelSliderView.transform.DOScale(1,0.3f);
            _levelSliderView.SetValue(_enemies.Count);
        }

        private void DecreaseSlider(Enemy obj)
        {
            _enemyCount++;
            
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