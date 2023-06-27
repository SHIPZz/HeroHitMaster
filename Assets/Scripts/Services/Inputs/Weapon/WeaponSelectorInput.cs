using System;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class WeaponSelectorInput : MonoBehaviour
    {
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;
        [SerializeField] private Button _apply;
        
        public event Action LeftArrowClicked;
        public event Action RightArrowClicked;
        public event Action ApplyButtonClicked;

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

        private void OnApplyButtonClicked()
        {
            ApplyButtonClicked?.Invoke();
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
}