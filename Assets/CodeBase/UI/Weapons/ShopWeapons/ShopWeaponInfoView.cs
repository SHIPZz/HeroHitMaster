﻿using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Weapon;
using DG.Tweening;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class ShopWeaponInfoView : MonoBehaviour
    {
        private const float ButtonScaleDelay = 0.1f;

        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private TextMeshProUGUI _adPrice;
        [SerializeField] private Button _adButton;
        [SerializeField] private Button _buyButton;

        private readonly Dictionary<PriceTypeId, bool> _priceCheckerForAd = new()
        {
            { PriceTypeId.Ad, true },
            { PriceTypeId.Money, false }
        };

        private readonly Dictionary<PriceTypeId, TextMeshProUGUI> _weaponPrices = new();

        private void Awake()
        {
            _weaponPrices[PriceTypeId.Ad] = _adPrice;
            _weaponPrices[PriceTypeId.Money] = _price;
        }

        public void DisableBuyButton(WeaponData weaponData)
        {
            if (_priceCheckerForAd[weaponData.Price.PriceTypeId])
            {
                return;
            }

            SetButtonScale(_buyButton, false, false, true);
            UpdateWeaponInfo(weaponData, true, weaponData.Price.Value.ToString());
        }

        public void DisableBuyInfo(WeaponData weaponData, bool needAnimation)
        {
            UpdateWeaponInfo(weaponData, true, "Куплен");

            SetButtonScale(_adButton, false, false, needAnimation);
            SetButtonScale(_buyButton, false, false, needAnimation);
        }

        public void SetWeaponData(WeaponData weaponData)
        {
            if (_priceCheckerForAd[weaponData.Price.PriceTypeId])
            {
                SetButtonScale(_buyButton, false, false, true);
                SetButtonScale(_adButton, false, false, false);

                _adButton.transform.DOScaleX(1, ButtonScaleDelay)
                    .OnComplete(() => SetButtonScale(_adButton, true, true, true));

                UpdateWeaponInfo(weaponData, true, "Посмотреть рекламу");

                return;
            }

            SetButtonScale(_adButton, false, false, true);
            SetButtonScale(_buyButton, false, false, true);

            _buyButton.transform.DOScaleX(1, ButtonScaleDelay)
                .OnComplete(() => SetButtonScale(_buyButton, true, true, true));
            UpdateWeaponInfo(weaponData, true, weaponData.Price.Value.ToString());
        }

        private void SetActivePrice(bool isActive, PriceTypeId pricePriceTypeId)
        {
            foreach (var keyValue in _weaponPrices)
            {
                if(keyValue.Key != pricePriceTypeId)
                    keyValue.Value.transform.parent.gameObject.SetActive(false);
            }
            
            _weaponPrices[pricePriceTypeId].transform.parent.gameObject.SetActive(isActive);
        }

        private void SetButtonScale(Button button, bool isInteractable, bool isVisible, bool needAnimation)
        {
            button.interactable = isInteractable;

            if (!needAnimation)
            {
                button.gameObject.transform.DOScaleX(0, 0);
                return;
            }

            button.gameObject.transform.DOScaleX(isVisible ? 1 : 0, ButtonScaleDelay);
        }

        private void UpdateWeaponInfo(WeaponData weaponData, bool showPrice, string priceText)
        {
            _name.transform.DOScaleX(0, 0f);
            _name.text = weaponData.Name;
            _name.transform.DOScaleX(1, 0.2f);
            SetActivePrice(showPrice, weaponData.Price.PriceTypeId);

            if (showPrice)
            {
                TextMeshProUGUI targetPrice = _weaponPrices[weaponData.Price.PriceTypeId];
                targetPrice.text = $"<color=#ffdc30> {priceText}</color>";
            }
        }
    }
}