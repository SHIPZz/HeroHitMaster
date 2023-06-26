using System;
using System.Collections.Generic;
using Gameplay.Web;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class WeaponSelectorInput : MonoBehaviour
    {
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;
        
        private event Action LeftArrowClicked;
        private event Action RightArrowClicked;

        private void OnEnable()
        {
            _rightArrow.onClick.AddListener(OnRightArrowClicked);
            _leftArrow.onClick.AddListener(OnLeftArrowClicked);
        }

        private void OnDisable()
        {
            _rightArrow.onClick.RemoveListener(OnRightArrowClicked);
            _leftArrow.onClick.RemoveListener(OnLeftArrowClicked);
        }

        private void OnRightArrowClicked()
        {
            RightArrowClicked?.Invoke();
        }

        private void OnLeftArrowClicked()
        {
            LeftArrowClicked?.Invoke();
        }
    }

    public class WeaponSelectorHandler
    {
        private List<IWeapon> _weapons = new List<IWeapon>();
        private WeaponSelectorInput _weaponSelectorInput;

        public WeaponSelectorHandler(WeaponSelectorInput weaponSelectorInput)
        {
            _weaponSelectorInput = weaponSelectorInput;
        }
        
    }
}