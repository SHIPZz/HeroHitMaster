using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Play
{
    public class PlayButtonView : MonoBehaviour
    {
        private Button _button;

        public event Action Clicked;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.enabled = false;
        }

        private void OnEnable() => 
            _button.onClick.AddListener(OnClicked);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnClicked);

        public void Enable() =>
            _button.enabled = true;

        private void OnClicked() => 
            Clicked?.Invoke();
    }
}