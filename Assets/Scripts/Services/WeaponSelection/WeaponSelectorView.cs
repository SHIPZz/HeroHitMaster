using System;
using System.Collections.Generic;
using Enums;
using ScriptableObjects;
using Services.Providers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Services.WeaponSelection
{
    public class WeaponSelectorView : MonoBehaviour
    {
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;
        [SerializeField] private Button _apply;
        
        private WeaponsProvider _weaponsProvider;
        private Dictionary<WeaponTypeId, Image> _icons = new();

        public event Action LeftArrowClicked;
        public event Action RightArrowClicked;
        public event Action ApplyButtonClicked;

        [Inject]
        private void Construct(WeaponsProvider weaponsProvider)
        {
            _weaponsProvider = weaponsProvider;

            FillDictionary();
        }

        private void OnEnable()
        {
            _rightArrow.onClick.AddListener(OnRightArrowClicked);
            _leftArrow.onClick.AddListener(OnLeftArrowClicked);
            _apply.onClick.AddListener(OnApplyButtonClicked);
        }

        private void OnDisable()
        {
            _apply.onClick.RemoveListener(OnApplyButtonClicked);
            _rightArrow.onClick.RemoveListener(OnRightArrowClicked);
            _leftArrow.onClick.RemoveListener(OnLeftArrowClicked);
        }

        private void FillDictionary()
        {
            foreach (WeaponSettings weaponConfig in _weaponsProvider.WeaponConfigs.Values)
            {
                Image image = Instantiate(weaponConfig.ImagePrefab, transform);
                image.gameObject.SetActive(false);
                _icons[weaponConfig.WeaponTypeId] = image;
            }
            
            _icons[WeaponTypeId.ShootSpiderHand].gameObject.SetActive(true);
        }

        public void ShowWeaponIcon(WeaponTypeId weaponTypeId)
        {
            DisableAll();
            _icons[weaponTypeId].gameObject.SetActive(true);
        }

        private void DisableAll()
        {
            foreach (var icon in _icons.Values)
            {
                icon.gameObject.SetActive(false);
            }
        }

        private void OnApplyButtonClicked() => 
            ApplyButtonClicked?.Invoke();

        private void OnRightArrowClicked() => 
            RightArrowClicked?.Invoke();

        private void OnLeftArrowClicked() => 
            LeftArrowClicked?.Invoke();
    }
}