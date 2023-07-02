using Enums;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class WindowSelectorHandler : MonoBehaviour
    {
        [SerializeField] private Button _applyWeaponButton;
        [SerializeField] private Button _applyPlayerButton;
        
        private WindowService _windowService;

        [Inject]
        private void Construct(WindowService windowService)
        {
            _windowService = windowService;
            _windowService.Close(WindowTypeId.WeaponSelectorWindow);
        }
        
        private void OnEnable()
        {
            _applyPlayerButton.onClick.AddListener(OnApplyPlayerButtonClicked);
            _applyWeaponButton.onClick.AddListener(OnApplyWeaponButtonClicked);
        }

        private void OnDisable()
        {
            _applyPlayerButton.onClick.RemoveListener(OnApplyPlayerButtonClicked);
            _applyWeaponButton.onClick.RemoveListener(OnApplyWeaponButtonClicked);
        }

        private void OnApplyPlayerButtonClicked()
        {
            _windowService.Close(WindowTypeId.PlayerSelectorWindow);
            _windowService.Open(WindowTypeId.WeaponSelectorWindow);
        }

        private void OnApplyWeaponButtonClicked()
        {
            _windowService.Close(WindowTypeId.WeaponSelectorWindow);
        }
    }
}