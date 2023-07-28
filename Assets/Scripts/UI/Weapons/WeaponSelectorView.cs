﻿using System;
using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
    public class WeaponSelectorView : MonoBehaviour
    {
        [SerializeField] private WeaponTypeId _weaponTypeId;
        [SerializeField] private Button _button;
        
        public event Action<WeaponTypeId> Choosed;

        private void OnEnable() => 
            _button.onClick.AddListener(OnImageClicked);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnImageClicked);

        private void OnImageClicked() => 
            Choosed?.Invoke(_weaponTypeId);
    }
}