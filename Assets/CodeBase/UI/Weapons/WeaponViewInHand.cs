using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.UI.Weapons
{
    public class WeaponViewInHand : MonoBehaviour
    {
        [SerializeField] private WeaponTypeId _weaponTypeId;

        public WeaponTypeId WeaponTypeId => _weaponTypeId;
    }
}