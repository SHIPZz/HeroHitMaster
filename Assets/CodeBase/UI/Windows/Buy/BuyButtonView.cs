using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Buy
{
    public class BuyButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public event Action Clicked;

        private void OnEnable() => 
            _button.onClick.AddListener(OnClicked);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnClicked);

        private void OnClicked() =>
            Clicked?.Invoke();
    }
}