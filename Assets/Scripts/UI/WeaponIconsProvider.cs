using System.Collections.Generic;
using Enums;
using Services.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponIconsProvider
    {
        private readonly UIFactory _uiFactory;

        public WeaponIconsProvider(UIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            FillDictionaries();
        }

        public Dictionary<WeaponTypeId, WeaponSelectorView> Icons { get; private set; } = new();

        private void FillDictionaries()
        {
            Icons = _uiFactory.CreateWeaponIcons();
        }
    }
}