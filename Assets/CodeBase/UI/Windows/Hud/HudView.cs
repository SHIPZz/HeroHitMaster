using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Hud
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;

        public event Action PauseButtonClicked;

        private void OnEnable() => 
            _pauseButton.onClick.AddListener(OnPauseClicked);

        private void OnDisable() => 
            _pauseButton.onClick.RemoveListener(OnPauseClicked);

        private void OnPauseClicked() =>
            PauseButtonClicked?.Invoke();
    }
}