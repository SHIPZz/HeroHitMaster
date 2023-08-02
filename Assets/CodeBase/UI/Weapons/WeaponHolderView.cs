using System.Collections.Generic;
using CodeBase.Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.UI.Weapons
{
    public class WeaponHolderView : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<WeaponTypeId, WeaponViewInHand> _weaponViews;
        
        public void SetLastWeaponViewActive(bool isActive, WeaponTypeId weaponTypeId)
        {
            WeaponViewInHand weaponView = _weaponViews[weaponTypeId];
            weaponView.gameObject.SetActive(isActive);
        }
                        
        public bool TryShow(WeaponTypeId weaponTypeId)
        {
            if (!_weaponViews.TryGetValue(weaponTypeId, out var weaponViewInHand))
            {
                Debug.Log("Not found weapons");
                return false;
            }

            SetActiveBy(weaponTypeId);
            return true;
        }

        private void SetActiveBy(WeaponTypeId weaponTypeId)
        {
            SetActive(false);

            _weaponViews[weaponTypeId].gameObject.SetActive(true);
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