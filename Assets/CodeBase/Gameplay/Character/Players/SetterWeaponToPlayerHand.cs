using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.PlayerSettings;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.Services.Storages;
using CodeBase.Services.Storages.Character;
using CodeBase.UI.Weapons;
using Zenject;

namespace CodeBase.Gameplay.Character.Players
{
    public class SetterWeaponToPlayerHand : IInitializable, IDisposable
    {
        private readonly Dictionary<PlayerTypeId, PlayerWithWeaponInHandTypeId> _playersIdsWithWeaponInHand;
        private readonly List<Player> _playersWithWeaponsInHand = new();
        private readonly List<WeaponHolderView> _weaponViews = new();
        private WeaponSelector _weaponSelector;

        public SetterWeaponToPlayerHand(PlayerSettings playerSettings, WeaponSelector weaponSelector,
            IPlayerStorage playerStorage)
        {
            _weaponSelector = weaponSelector;
            _playersIdsWithWeaponInHand = playerSettings.PlayerWithWeaponInHands;

            FillLists(playerStorage);
        }

        public void Init(WeaponTypeId weaponTypeId) =>
            TrySetNewWeapon(weaponTypeId);
        
        public void Initialize() => 
            _weaponSelector.NewWeaponChanged += TrySetNewWeapon;

        public void Dispose() => 
            _weaponSelector.NewWeaponChanged -= TrySetNewWeapon;

        private void TrySetNewWeapon(WeaponTypeId weaponTypeId)
        {
            foreach (var weaponViewStorage in _weaponViews)
            {
                weaponViewStorage.TryShow(weaponTypeId);
            }
        }

        private void FillLists(IPlayerStorage playerStorage)
        {
            foreach (Player player in playerStorage.GetAll())
            {
                if (!_playersIdsWithWeaponInHand.ContainsKey(player.PlayerTypeId))
                    continue;

                _playersWithWeaponsInHand.Add(player);
                _weaponViews.Add(player.GetComponentInChildren<WeaponHolderView>());
            }
        }
    }
}