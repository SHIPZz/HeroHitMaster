using System.Collections.Generic;
using Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Weapons;

namespace CodeBase.Services.Storages
{
    public class WeaponViewStorage : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<WeaponTypeId, WeaponViewInHand> _weaponViews;

        private WeaponViewInHand _lastWeaponView;

        public void SetLastWeaponViewActive(bool isActive) => 
            _lastWeaponView?.gameObject.SetActive(isActive);

        public WeaponViewInHand Get(WeaponTypeId weaponTypeId)
        {
            SetActive(false);

            if (!_weaponViews.TryGetValue(weaponTypeId, out var weaponViewInHand))
            {
                Debug.Log("Not found weapons");
                return null;
            }
            
            weaponViewInHand.gameObject.SetActive(true);
            _lastWeaponView = weaponViewInHand;
            return weaponViewInHand;
        }

        private void SetActive(bool isActive)
        {
            foreach (var weaponView in _weaponViews.Values)
            {
                weaponView.gameObject.SetActive(isActive);
            }
        }
    }
}