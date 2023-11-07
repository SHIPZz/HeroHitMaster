using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Pause
{
    public class PauseWindowView : MonoBehaviour
    {
        [SerializeField] private Button _returnButton;

        public event Action ReturnButtonClicked;

        private void OnEnable() => 
            _returnButton.onClick.AddListener(OnPauseClicked);

        private void OnDisable() => 
            _returnButton.onClick.RemoveListener(OnPauseClicked);

        private void OnPauseClicked() =>
            ReturnButtonClicked?.Invoke();
    }
}