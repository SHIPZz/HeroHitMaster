using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine.UI;

namespace CodeBase.UI.Weapons
{
    public class WeaponIconsProvider : SerializedMonoBehaviour, 
        IProvider<Dictionary<WeaponTypeId, WeaponSelectorView>>, IProvider<WeaponTypeId, Image>, IProvider<WeaponIconsProvider>
    {
        [OdinSerialize] public Dictionary<WeaponTypeId, WeaponSelectorView> Icons { get; private set; }
        [OdinSerialize] public Dictionary<WeaponTypeId, WeaponSelectorView> PopupIcons { get; private set; }
        [OdinSerialize] private Dictionary<WeaponTypeId, Image> _shopWeaponIcons;

        public Dictionary<WeaponTypeId, Image> ShopWeaponIcons => _shopWeaponIcons;

        public Dictionary<WeaponTypeId, WeaponSelectorView> Get() => 
            Icons;

        WeaponIconsProvider IProvider<WeaponIconsProvider>.Get() => 
            this;
        
        public Image Get(WeaponTypeId id) => 
            !_shopWeaponIcons.TryGetValue(id, out Image image) ? 
                null : 
                _shopWeaponIcons[id];

        public void Set(Dictionary<WeaponTypeId, WeaponSelectorView> t) => 
            Icons = t;

        public void Set(WeaponIconsProvider t)
        {
        }
    }
}