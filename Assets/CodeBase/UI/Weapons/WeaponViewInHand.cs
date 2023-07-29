using Enums;
using UnityEngine;

namespace Weapons
{
    public class WeaponViewInHand : MonoBehaviour
    {
        [SerializeField] private WeaponTypeId _weaponTypeId;

        public WeaponTypeId WeaponTypeId => _weaponTypeId;
    }
}