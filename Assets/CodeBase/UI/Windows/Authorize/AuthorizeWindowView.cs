using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Authorize
{
    public class AuthorizeWindowView : MonoBehaviour
    {
        [SerializeField] private Button _authorizeButton;
        [SerializeField] private Button _closeButton;

        public event Action AuthorizeButtonClicked;
        public event Action CloseButtonClicked;
        
        private void OnEnable()
        {
            _authorizeButton.onClick.AddListener(OnAuthorizeClicked);
            _closeButton.onClick.AddListener(OnCloseClicked);
        }

        private void OnDisable()
        {
            _authorizeButton.onClick.RemoveListener(OnAuthorizeClicked);
            _closeButton.onClick.RemoveListener(OnCloseClicked);
        }

        private void OnAuthorizeClicked()
        {
            _authorizeButton.gameObject.SetActive(false);
            AuthorizeButtonClicked?.Invoke();
        }

        private void OnCloseClicked() => 
            CloseButtonClicked?.Invoke();
    }
}