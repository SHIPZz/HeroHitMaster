using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine.UI;

namespace CodeBase.UI.Weapons
{
    public class WeaponIconsProvider : SerializedMonoBehaviour, 
        IProvider<Dictionary<WeaponTypeId, WeaponSelectorView>>, IProvider<WeaponTypeId, Image>
    {
        [OdinSerialize] public Dictionary<WeaponTypeId, WeaponSelectorView> Icons { get; private set; }
        [OdinSerialize] private Dictionary<WeaponTypeId, Image> _shopWeaponIcons;

        public Dictionary<WeaponTypeId, WeaponSelectorView> Get() => 
            Icons;

        public void Set(Dictionary<WeaponTypeId, WeaponSelectorView> t) => 
            Icons = t;

        public Image Get(WeaponTypeId id) => 
            _shopWeaponIcons[id];
    }
}