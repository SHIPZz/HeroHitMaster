using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Weapon;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class ShopWeaponInfo : MonoBehaviour
    {
        private const float ButtonScaleDelay = 0.1f;

        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Button _adButton;
        [SerializeField] private Button _buyButton;

        private readonly Dictionary<PriceTypeId, Func<bool>> _priceCheckers = new Dictionary<PriceTypeId, Func<bool>>
        {
            { PriceTypeId.Ad, () => true },
            { PriceTypeId.Money, () => false }
        };

        private void SetButtonScale(Button button, bool isInteractable, bool isVisible, bool needAnimation)
        {
            button.interactable = isInteractable;

            if (!needAnimation)
            {
                button.gameObject.transform.DOScaleX( 0, 0);
                return;
            }

            button.gameObject.transform.DOScaleX(isVisible ? 1 : 0, ButtonScaleDelay);
        }

        private void UpdateWeaponInfo(WeaponData weaponData, bool showPrice)
        {
            _name.transform.DOScaleX(0, 0f);
            _name.text = weaponData.Name;
            _name.transform.DOScaleX(1, 0.2f);
            SetActivePrice(showPrice);

            if (showPrice)
            {
                _price.text = $"<color=#ffdc30> {weaponData.Price.Value.ToString()}</color>";
            }
        }

        public void DisableBuyButton(WeaponData weaponData)
        {
            if (_priceCheckers[weaponData.Price.PriceTypeId]?.Invoke() == true)
            {
                return;
            }

            SetButtonScale(_buyButton, false, false, true);
            UpdateWeaponInfo(weaponData, true);
        }

        public void DisableBuyInfo(WeaponData weaponData, bool needAnimation)
        {
            UpdateWeaponInfo(weaponData, false);

            if (_priceCheckers[weaponData.Price.PriceTypeId]?.Invoke() == true)
            {
                SetButtonScale(_adButton, false, false,needAnimation);

                return;
            }

            SetButtonScale(_buyButton, false, false,needAnimation);
        }

        public void SetWeaponData(WeaponData weaponData)
        {
            if (_priceCheckers[weaponData.Price.PriceTypeId]?.Invoke() == true)
            {
                SetButtonScale(_buyButton, false, false, true);
                SetButtonScale(_adButton, true, true, true);

                _adButton.transform.DOScaleX(1, ButtonScaleDelay)
                    .OnComplete(() => SetButtonScale(_adButton, true, true, true));
                UpdateWeaponInfo(weaponData, false);

                return;
            }

            SetButtonScale(_adButton, false, false, true);
            SetButtonScale(_buyButton, false, false, true);

            _buyButton.transform.DOScaleX(1, ButtonScaleDelay)
                .OnComplete(() => SetButtonScale(_buyButton, true, true, true));
            UpdateWeaponInfo(weaponData, true);
        }

        private void SetActivePrice(bool isActive) =>
            _price.transform.parent.gameObject.SetActive(isActive);
    }
}