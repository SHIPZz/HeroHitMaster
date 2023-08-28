﻿using System.Collections;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages.Sound;
using CodeBase.UI.Weapons;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.UI.Windows.Popup
{
    public class PopupInfoView : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<WeaponTypeId, Image> _whiteFrames;

        private Dictionary<WeaponTypeId, WeaponSelectorView> _weaponIcons;
        private AudioSource _lastWeaponSelectedSound;
        private AudioSource _chooseWeaponSound;
        private List<WeaponSelectorView> _randomIcons = new();
        private Image _lastFrame;
        private int _count;
        private WeaponSelectorView _lastWeaponSelectorView;
        private List<ParticleSystem> _selectedWeaponEffects;

        [Inject]
        private void Construct(IProvider<WeaponIconsProvider> provider, ISoundStorage soundStorage, EffectsProvider effectsProvider)
        {
            _lastWeaponSelectedSound = soundStorage.Get(SoundTypeId.SelectedWeapon);
            _chooseWeaponSound = soundStorage.Get(SoundTypeId.ChooseWeapon);
            _weaponIcons = provider.Get().PopupIcons;
            _selectedWeaponEffects = effectsProvider.ChoosedPopupWeaponEffects;
        }

        private void Awake()
        {
            foreach (var weaponSelector in _weaponIcons.Values)
            {
                weaponSelector.gameObject.SetActive(false);
            }

            foreach (var whiteFrame in _whiteFrames.Values)
            {
                Color currentColor = whiteFrame.color;

                Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0f);

                whiteFrame.color = newColor;
            }
        }

        private void OnEnable()
        {
            List<WeaponSelectorView> randomIcons = GetRandomWeaponIcons(3);

            foreach (var icon in randomIcons)
            {
                icon.gameObject.SetActive(true);
            }
        }

        [Button]
        public void Show()
        {
            StartCoroutine(StartChooseRandomWeapon());
        }

        private IEnumerator StartChooseRandomWeapon()
        {
            for (int i = 0; i < _randomIcons.Count; i++)
            {
                WeaponSelectorView currentWeaponView = GetRandomWeaponView();

                if (_lastWeaponSelectorView is not null &&
                    _lastWeaponSelectorView.WeaponTypeId == currentWeaponView.WeaponTypeId)
                {
                    currentWeaponView = GetRandomWeaponView();
                }

                _lastWeaponSelectorView = currentWeaponView;

                if (i == _randomIcons.Count - 1)
                {
                    _lastWeaponSelectedSound.Play();

                    var randomParticleId = Random.Range(0, _selectedWeaponEffects.Count - 1);
                    ParticleSystem targetParticle = _selectedWeaponEffects[randomParticleId];
                    
                    targetParticle.transform.position = _lastWeaponSelectorView.transform.position;
                    targetParticle.Play();
                }
                else
                {
                    _chooseWeaponSound.Play();
                }

                yield return new WaitForSeconds(2f);
            }
        }

        private WeaponSelectorView GetRandomWeaponView()
        {
            if (_lastFrame is not null)
            {
                Color currentCollor = new Color(_lastFrame.color.r, _lastFrame.color.g, _lastFrame.color.b, 0f);
                _lastFrame.color = currentCollor;
            }

            int randomWeaponId = Random.Range(0, _randomIcons.Count);

            WeaponSelectorView weaponSelectorView = _randomIcons[randomWeaponId];

            SetFrameAlpha(weaponSelectorView);

            return weaponSelectorView;
        }

        private void SetFrameAlpha(WeaponSelectorView weaponSelectorView)
        {
            var whiteFrame = _whiteFrames[weaponSelectorView.WeaponTypeId];

            Color currentColor = whiteFrame.color;

            var newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
            whiteFrame.color = newColor;
            _lastFrame = whiteFrame;
        }

        private List<WeaponSelectorView> GetRandomWeaponIcons(int count)
        {
            List<WeaponSelectorView> allIcons = new List<WeaponSelectorView>(_weaponIcons.Values);
            int totalIcons = allIcons.Count;

            for (int i = 0; i < count; i++)
            {
                if (totalIcons == 0)
                    break;

                int randomIndex = Random.Range(0, totalIcons);
                _randomIcons.Add(allIcons[randomIndex]);
                allIcons.RemoveAt(randomIndex);
                totalIcons--;
            }

            return _randomIcons;
        }
    }
}