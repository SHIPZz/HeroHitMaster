using CodeBase.Enums;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Windows.Popup
{
    public class PopupPresenter : MonoBehaviour
    {
        [SerializeField] private Button _adButton;
        [SerializeField] private PopupInfoView _popupInfoView;
        
        private WindowService _windowService;

        [Inject]
        private void Construct(WindowService windowService) => 
            _windowService = windowService;

        private void OnEnable() => 
            _adButton.onClick.AddListener(ShowAd);

        private void OnDisable() => 
            _adButton.onClick.RemoveListener(ShowAd);

        public void Init()
        {
            _windowService.CloseAll();
            _windowService.Open(WindowTypeId.Popup);
        }

        private void ShowAd() => 
            _popupInfoView.Show();
    }
}