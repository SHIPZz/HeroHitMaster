using System;
using System.Collections.Generic;
using Enums;
using Gameplay.Character.Player;
using ScriptableObjects.PlayerSettings;
using Services.Providers;
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

        private List<PlayerSettings> _playerSettingsList;
        private Dictionary<PlayerTypeId, Player> _playerSettings = new();
        private LocationProvider _locationProvider;

        public event Action LeftArrowClicked;
        public event Action RightArrowClicked;
        public event Action ApplyButtonClicked;

        [Inject]
        private void Construct(List<PlayerSettings> playerSettingsList, LocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
            _playerSettingsList = playerSettingsList;
            FillDictionary();
            DisableAll();
            SetActiveInitialPlayer();
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
            Player targetPlayer = _playerSettings[playerTypeId];
            targetPlayer.gameObject.SetActive(true);
        }

        private void DisableAll()
        {
            foreach (var playerSetting in _playerSettings)
            {
                playerSetting.Value.gameObject.SetActive(false);
            }
        }

        private void SetActiveInitialPlayer()
        {
            Player player = _playerSettings[PlayerTypeId.Spider];
            player.gameObject.SetActive(true);
        }

        private void OnApplyButtonClicked() => 
            ApplyButtonClicked?.Invoke();

        private void OnRightArrowClicked() => 
            RightArrowClicked?.Invoke();

        private void OnLeftArrowClicked() => 
            LeftArrowClicked?.Invoke();

        private void FillDictionary()
        {
            foreach (var playerSetting in _playerSettingsList)
            {
                Player player = Instantiate(playerSetting.PlayerViewPrefab);
                player.transform.position = _locationProvider.PlayerSpawnPoint.position;
                _playerSettings[player.PlayerTypeId] = player;
            }
        }
    }
}