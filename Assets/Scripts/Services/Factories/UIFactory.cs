using System.Collections.Generic;
using Enums;
using Gameplay.Character.Player;
using ScriptableObjects.PlayerSettings;
using Services.Providers;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Services.Factories
{
    public class UIFactory
    {
        private readonly DiContainer _diContainer;
        private readonly List<PlayerSettings> _playerSettingsList;
        private readonly LocationProvider _locationProvider;
        private readonly WeaponsProvider _weaponsProvider;

        public UIFactory(DiContainer diContainer, List<PlayerSettings> playerSettingsList,
            LocationProvider locationProvider,
            WeaponsProvider weaponsProvider)
        {
            _diContainer = diContainer;
            _playerSettingsList = playerSettingsList;
            _locationProvider = locationProvider;
            _weaponsProvider = weaponsProvider;
        }

        public Dictionary<WeaponTypeId, WeaponSelectorView> CreateWeaponIcons()
        {
            var weaponIcons = new Dictionary<WeaponTypeId, WeaponSelectorView>();
            Transform parent = _locationProvider.WeaponIconParentTransform;

            foreach (var weaponSetting in _weaponsProvider.WeaponConfigs.Values)
            {
                var weaponIcon =
                    _diContainer.InstantiatePrefabForComponent<WeaponSelectorView>(weaponSetting.WeaponSelectorView, parent);

                weaponIcons[weaponSetting.WeaponTypeId] = weaponIcon;
            }

            return weaponIcons;
        }

        public Dictionary<PlayerTypeId, Player> CreatePlayersView()
        {
            var playersView = new Dictionary<PlayerTypeId, Player>();
            Transform parent = _locationProvider.PlayerParentTransform;
            Transform targetSpawnPosition = _locationProvider.PlayerSpawnPoint;
            parent.transform.position = targetSpawnPosition.position;

            foreach (PlayerSettings playerSetting in _playerSettingsList)
            {
                var player =
                    _diContainer.InstantiatePrefabForComponent<Player>(playerSetting.PlayerViewPrefab,
                        parent);

                playersView[player.PlayerTypeId] = player;
            }

            return playersView;
        }
    }
}