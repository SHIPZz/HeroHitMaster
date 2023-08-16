using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class ActivateConcreteButtonOnChoosedWeaponInShop : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button _adButton;
        [SerializeField] private UnityEngine.UI.Button _buyButton;

        private List<WeaponSelectorView> _weaponSelectorViews;
        private WeaponStaticDataService _weaponStaticDataService;
        
        [Inject]
        private void Construct(List<WeaponSelectorView> weaponSelectorViews,
            WeaponStaticDataService weaponStaticDataService)
        {
            _weaponStaticDataService = weaponStaticDataService;
            _weaponSelectorViews = weaponSelectorViews;
        }

        private void OnEnable()
        {
            _weaponSelectorViews.ForEach(x => x.Choosed += SetActive);
        }

        private void OnDisable()
        {
            _weaponSelectorViews.ForEach(x => x.Choosed -= SetActive);
        }

        private void SetActive(WeaponTypeId id)
        {
            var weapon = _weaponStaticDataService.Get(id);

            if (weapon.WeaponRankId == WeaponRankId.Common)
            {
                _buyButton.gameObject.SetActive(true);
                _adButton.gameObject.SetActive(false);
                return;
            }

            _buyButton.gameObject.SetActive(false);
            _adButton.gameObject.SetActive(true);
        }
    }
}