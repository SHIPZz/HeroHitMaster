using Enums;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
   public class ShopViewHandler : MonoBehaviour
    {
        [SerializeField] private Button _openShopButton;
        [SerializeField] private Button _closeShopButton;
    
        private WindowService _windowService;

        [Inject]
        private void Inject(WindowService windowService)
        {
            _windowService = windowService;
            _windowService.Close(WindowTypeId.Shop);
        }

        private void OnEnable()
        {
            _openShopButton.onClick.AddListener(Enable);
            _closeShopButton.onClick.AddListener(Disable);
        }

        private void OnDisable()
        {
            _openShopButton.onClick.RemoveListener(Enable);
            _closeShopButton.onClick.RemoveListener(Disable);
        }

        private void Disable() => 
            _windowService.Close(WindowTypeId.Shop);

        private void Enable() => 
            _windowService.Open(WindowTypeId.Shop);
    }
}