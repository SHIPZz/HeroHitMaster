using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Death
{
    public class DeathView : MonoBehaviour
    {
        [SerializeField] private Button _restart;
        [SerializeField] private Button _adRestart;

        public event Action RestartButtonClicked;
        public event Action RestartAdButtonClicked;
        
        private void OnEnable()
        {
            _restart.onClick.AddListener(OnRestartButtonClicked);
            _adRestart.onClick.AddListener(OnAdRestartButtonClicked);
        }

        private void OnDisable()
        {
            _restart.onClick.RemoveListener(OnRestartButtonClicked);
            _adRestart.onClick.RemoveListener(OnAdRestartButtonClicked);
        }

        private void OnAdRestartButtonClicked() => 
            RestartAdButtonClicked?.Invoke();

        private void OnRestartButtonClicked() => 
            RestartButtonClicked?.Invoke();

        public void DisableAdButton() =>
            _adRestart.gameObject.SetActive(false);
    }
}