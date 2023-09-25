using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Death
{
    public class ContinueADButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public event Action Clicked;

        private void OnEnable() => 
            _button.onClick.AddListener(OnButtonClicked);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnButtonClicked);

        private void OnButtonClicked() => 
            Clicked?.Invoke();
    }
}