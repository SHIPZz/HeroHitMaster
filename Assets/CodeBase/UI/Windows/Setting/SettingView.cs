using System;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Setting
{
    public class SettingView : MonoBehaviour
    {
        [SerializeField] private Button _openShopButton;
        [SerializeField] private Button _closeShopButton;

        public event Action OpenedButtonClicked; 
        public event Action ClosedButtonClicked; 

        private void OnEnable()
        {
            _openShopButton.onClick.AddListener(OnOpened);
            _closeShopButton.onClick.AddListener(OnClosed);
        }

        private void OnDisable()
        {
            _openShopButton.onClick.RemoveListener(OnOpened);
            _closeShopButton.onClick.RemoveListener(OnClosed);
        }

        private void OnOpened() =>
            OpenedButtonClicked?.Invoke();

        private void OnClosed() =>
            ClosedButtonClicked?.Invoke();
    }
}