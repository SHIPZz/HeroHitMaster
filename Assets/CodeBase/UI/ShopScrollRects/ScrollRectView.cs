using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ShopScrollRects
{
    public class ScrollRectView : MonoBehaviour
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private ScrollRectTypeId _scrollRectTypeId; 

        public Button Button => _openButton;
        
        public event Action<ScrollRectTypeId> Opened;
        
        private void OnEnable() => 
            _openButton.onClick.AddListener(OnOpenButtonClicked);

        private void OnDisable() => 
            _openButton.onClick.RemoveListener(OnOpenButtonClicked);

        private void OnOpenButtonClicked() => 
            Opened?.Invoke(_scrollRectTypeId);
    }
}