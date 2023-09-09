using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Buy
{
    public class AdBuyButtonView : MonoBehaviour
    {
        [SerializeField] private Button _adButton;
        
        public event Action Clicked;

        private void OnEnable() => 
            _adButton.onClick.AddListener(OnClicked);

        private void OnDisable() => 
            _adButton.onClick.RemoveListener(OnClicked);

        private void OnClicked() =>
            Clicked?.Invoke();
    }
}