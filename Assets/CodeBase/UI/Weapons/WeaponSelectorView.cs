using System;
using CodeBase.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Weapons
{
    public class WeaponSelectorView : MonoBehaviour
    {
        [SerializeField] private WeaponTypeId _weaponTypeId;
        [SerializeField] private Button _button;

        public WeaponTypeId WeaponTypeId => _weaponTypeId;

        public event Action<WeaponTypeId> Choosed;

        private void OnEnable() => 
            _button.onClick.AddListener(OnImageClicked);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnImageClicked);

        private void OnImageClicked() => 
            Choosed?.Invoke(_weaponTypeId);
    }
}