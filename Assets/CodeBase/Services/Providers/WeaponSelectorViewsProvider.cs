using System.Collections.Generic;
using CodeBase.UI.Weapons;
using UnityEngine;

namespace CodeBase.Services.Providers
{
    public class WeaponSelectorViewsProvider : MonoBehaviour, IProvider<List<WeaponSelectorView>>
    {
        [SerializeField] private List<WeaponSelectorView> _weaponSelectorViews;

        public List<WeaponSelectorView> Get() => 
        _weaponSelectorViews;

        public void Set(List<WeaponSelectorView> t) => 
        _weaponSelectorViews = t;
    }
}