using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Leaderboards
{
    [RequireComponent(typeof(Button))]
    public class AccuracyLeaderboardOpenerButton : MonoBehaviour
    {
        private Button _button;

        public event Action Opened;

        private void Awake() => 
            _button = GetComponent<Button>();

        private void OnEnable() => 
            _button.onClick.AddListener(OnOpenClicked);

        private void OnOpenClicked() => 
            Opened?.Invoke();
    }
}