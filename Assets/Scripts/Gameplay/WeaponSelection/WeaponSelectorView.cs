using System;
using System.Collections.Generic;
using Enums;
using ScriptableObjects;
using Services.Factories;
using Services.Providers;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.WeaponSelection
{
    public class WeaponSelectorView : MonoBehaviour
    {
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;
        [SerializeField] private Button _apply;
        [field: SerializeField] public SelectorViewTypeId SelectorViewTypeId { get; private set; }
        
        private Dictionary<WeaponTypeId, Image> _icons = new();

        public event Action LeftArrowClicked;
        public event Action RightArrowClicked;
        public event Action ApplyButtonClicked;

        [Inject]
        private void Construct(UIFactory uiFactory)
        {
            _icons = uiFactory.CreateWeaponIcons();
            ShowWeaponIcon(WeaponTypeId.ShootSpiderHand);
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