using System;
using System.Collections.Generic;
using Enums;
using Gameplay.Character.Player;
using Services.Factories;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.PlayerSelection
{
    public class PlayerSelectorView : MonoBehaviour
    {
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;
        [SerializeField] private Button _applyButton;
        [field: SerializeField] public SelectorViewTypeId SelectorViewTypeId { get; private set; }
        
        private Dictionary<PlayerTypeId, Player> _playerIcons = new();

        public event Action LeftArrowClicked;
        public event Action RightArrowClicked;
        public event Action ApplyButtonClicked;

        [Inject]
        private void Construct(UIFactory uiFactory)
        {
            _playerIcons = uiFactory.CreatePlayersView();
            Show(PlayerTypeId.Spider);
        }

        private void OnEnable()
        {
            _leftArrow.onClick.AddListener(OnLeftArrowClicked);
            _rightArrow.onClick.AddListener(OnRightArrowClicked);
            _applyButton.onClick.AddListener(OnApplyButtonClicked);
        }

        private void OnDisable()
        {
            _leftArrow.onClick.RemoveListener(OnLeftArrowClicked);
            _rightArrow.onClick.RemoveListener(OnRightArrowClicked);
            _applyButton.onClick.RemoveListener(OnApplyButtonClicked);
        }

        public void Show(PlayerTypeId playerTypeId)
        {
            DisableAll();
            Player targetPlayer = _playerIcons[playerTypeId];
            targetPlayer.gameObject.SetActive(true);
        }

        private void DisableAll()
        {
            foreach (var playerSetting in _playerIcons)
            {
                playerSetting.Value.gameObject.SetActive(false);
            }
        }

        private void OnApplyButtonClicked()
        {
            _playerIcons[PlayerTypeId.Spider].transform.parent.gameObject.SetActive(false);
            ApplyButtonClicked?.Invoke();
        }

        private void OnRightArrowClicked() => 
            RightArrowClicked?.Invoke();

        private void OnLeftArrowClicked() => 
            LeftArrowClicked?.Invoke();
    }
}