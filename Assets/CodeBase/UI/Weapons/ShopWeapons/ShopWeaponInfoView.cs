﻿using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages.Sound;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class ShopWeaponInfoView : MonoBehaviour
    {
        private const float ButtonScaleDelay = 0.3f;

        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private TextMeshProUGUI _adPrice;
        [SerializeField] private Button _adButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private List<ParticleSystem> _effects;
        [SerializeField] private TextMeshProUGUI _purchasedText;

        private Dictionary<WeaponTypeId, Image> _shopWeaponIcons;
        private AudioSource _purchasedWeaponSound;

        public event Action AdButtonClicked;

        [Inject]
        private void Construct(IProvider<WeaponIconsProvider> provider, ISoundStorage soundStorage)
        {
            _shopWeaponIcons = provider.Get().ShopWeaponIcons;
            _purchasedWeaponSound = soundStorage.Get(SoundTypeId.PurchasedWeapon);
        }

        private void OnEnable() =>
            _adButton.onClick.AddListener(OnAddButtonClicked);

        private void OnDisable() =>
            _adButton.onClick.RemoveListener(OnAddButtonClicked);

        public void DisableBuyButtons()
        {
            SetButtonScale(_buyButton, false, false, true);
            SetButtonScale(_adButton, false, false, false);
        }

        public void ShowEffectOnPurchasedWeapon(WeaponTypeId weaponDataWeaponTypeId)
        {
            ParticleSystem randomEffect = _effects[Random.Range(0, _effects.Count - 1)];
            randomEffect.transform.position = _shopWeaponIcons[weaponDataWeaponTypeId].transform.position;
            randomEffect.Play();
            _purchasedWeaponSound.Play();
            DisableBuyButtons();
        }

        public void SetAdWeaponInfo(WeaponData weaponData, bool isBought, int watchedAds)
        {
            _price.gameObject.SetActive(false);
            _adPrice.gameObject.SetActive(true);

            SetWeaponNameInfo(weaponData);
            
            if (isBought)
            {
                DisableBuyButtons();
                SetAdWeaponPriceInfo(weaponData, true, watchedAds);
                return;
            }

            SetAdWeaponPriceInfo(weaponData, false, watchedAds);
            SetButtonScale(_adButton, true, true, true);
            SetButtonScale(_buyButton, false, false, true);
        }

        public void SetMoneyWeaponInfo(WeaponData weaponData, bool isBought)
        {
            _adPrice.gameObject.SetActive(false);
            SetButtonScale(_adButton, false, false, false);
            _price.gameObject.SetActive(true);
            SetWeaponNameInfo(weaponData);

            if (isBought)
            {
                SetButtonScale(_buyButton, false, false, false);
                SetMoneyWeaponPriceInfo(weaponData, isBought);
                return;
            }

            SetButtonScale(_buyButton, true, true, true);
            SetMoneyWeaponPriceInfo(weaponData, false);
        }

        private void OnAddButtonClicked() =>
            AdButtonClicked?.Invoke();

        private void SetButtonScale(Button button, bool isInteractable, bool isVisible, bool needAnimation)
        {
            button.interactable = isInteractable;

            if (!needAnimation)
            {
                button.gameObject.transform.DOScaleX(0, 0);
                return;
            }

            if (button.gameObject.transform.localScale.x != 0)
            {
                button.gameObject.transform.DOScaleX(0, 0.1f)
                    .OnComplete(() => button.gameObject.transform.DOScaleX(isVisible ? 1 : 0, ButtonScaleDelay));
                return;
            }

            button.gameObject.transform.DOScaleX(isVisible ? 1 : 0, ButtonScaleDelay);
        }

        private void SetWeaponNameInfo(WeaponData weaponData)
        {
            _name.transform.DOScaleX(0, 0f);
            _name.text = weaponData.Name;
            _name.transform.DOScaleX(1, 0.2f);
        }

        private void SetAdWeaponPriceInfo(WeaponData weaponData, bool isBought, int watchedAds)
        {
            if (isBought)
            {
                _adPrice.text = $"<color=#ffdc30> {_purchasedText.text}</color>";
                return;
            }

            _adPrice.text = $"{watchedAds}/{weaponData.Price.AdQuantity}";
        }

        private void SetMoneyWeaponPriceInfo(WeaponData weaponData, bool isBought)
        {
            if (isBought)
            {
                _price.text = $"<color=#ffdc30> {_purchasedText.text}</color>";
                return;
            }

            _price.text = $"<color=#ffdc30> {weaponData.Price.Value}</color>";
        }
    }
}