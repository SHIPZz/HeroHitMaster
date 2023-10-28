using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Play
{
    public class PlayButtonView : MonoBehaviour
    {
      [SerializeField]  private Button _button;

        public event Action Clicked;

        private void OnEnable() =>
            _button.onClick.AddListener(OnClicked);

        private void OnDisable() =>
            _button.onClick.RemoveListener(OnClicked);

        public void Disable() =>
            _button.enabled = false;

        public void Enable() =>
            _button.enabled = true;

        private void OnClicked() =>
            Clicked?.Invoke();
    }
}