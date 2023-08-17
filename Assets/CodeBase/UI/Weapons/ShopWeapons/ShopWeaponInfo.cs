using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Weapon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class ShopWeaponInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Button _adButton;
        [SerializeField] private Button _buyButton;

        private Dictionary<PriceTypeId, Predicate<PriceTypeId>> _priceCheckers = new()
        {
            { PriceTypeId.Ad, needAd => true },
            { PriceTypeId.Money, needAd => false }
        };

        public void SetWeaponData(WeaponData weaponData)
        {
            if (_priceCheckers[weaponData.Price.PriceTypeId]?.Invoke(PriceTypeId.Ad) == true)
            {
                _adButton.gameObject.SetActive(true);
                _name.text = weaponData.Name;
                _buyButton.gameObject.SetActive(false);
                SetActivePrice(false);
                return;
            }

            _name.text = weaponData.Name;
            _price.text = weaponData.Price.Value.ToString();
            _adButton.gameObject.SetActive(false);
            _buyButton.gameObject.SetActive(true);
            SetActivePrice(true);
        }

        public void SetActivePrice(bool isActive) =>
            _price.transform.parent.gameObject.SetActive(isActive);
    }
}