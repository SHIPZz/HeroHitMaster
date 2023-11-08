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

        public event Action<WeaponTypeId> Choosen;

        private void OnEnable()
        {
            if (_button == null)
                return;
            
            _button.onClick.AddListener(OnImageClicked);
        }

        private void OnDisable()
        {
            if (_button == null)
                return;
            
            _button.onClick.RemoveListener(OnImageClicked);
        }

        private void OnImageClicked() => 
            Choosen?.Invoke(_weaponTypeId);
    }
}