using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.PlayerSettings;
using CodeBase.Services.Storages;
using CodeBase.UI.Weapons;

namespace CodeBase.Gameplay.Character.Players
{
    public class SetterWeaponToPlayerHand : IDisposable
    {
        private readonly Dictionary<PlayerTypeId, PlayerWithWeaponInHandTypeId> _playersIdsWithWeaponInHand;
        private readonly List<Player> _playersWithWeaponsInHand = new();
        private readonly List<WeaponHolderView> _weaponViewStorages = new();
        private readonly WeaponSelector _weaponSelector;

        public SetterWeaponToPlayerHand(PlayerSettings playerSettings, WeaponSelector weaponSelector,
            IPlayerStorage playerStorage)
        {
            _weaponSelector = weaponSelector;
            _playersIdsWithWeaponInHand = playerSettings.PlayerWithWeaponInHands;

            FillLists(playerStorage);

            _weaponSelector.NewWeaponChanged += SetNewWeapon;
        }

        public void Dispose()
        {
            _weaponSelector.NewWeaponChanged -= SetNewWeapon;
        }

        private void SetNewWeapon(WeaponTypeId weaponTypeId)
        {
            foreach (var weaponViewStorage in _weaponViewStorages)
            {
                if (!weaponViewStorage.TryShow(weaponTypeId))
                    return;

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
                _weaponViewStorages.Add(player.GetComponentInChildren<WeaponHolderView>());
            }
        }
    }
}