using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Play
{
    public class PlayButtonView : MonoBehaviour
    {
        private Button _button;

        public event Action Clicked;

        private void Awake() => 
            _button = GetComponent<Button>();

        private void OnEnable() => 
            _button.onClick.AddListener(OnClicked);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnClicked);

        private void OnClicked() =>
            Clicked?.Invoke();
    }
}