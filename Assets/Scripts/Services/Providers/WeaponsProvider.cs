using System.Collections.Generic;
using Enums;
using ScriptableObjects;

namespace Services.Providers
{
    public class WeaponsProvider
    {
        private readonly List<WeaponSettings> _weaponSettings;
        public Dictionary<WeaponTypeId, WeaponSettings> WeaponConfigs { get; set; } = new();
        public List<WeaponTypeId> CharactersAvailableWeapons { get; set; } = new();
        
         public WeaponsProvider(List<WeaponSettings> weaponSettings)
         {
             _weaponSettings = weaponSettings;
             FillDictionary();
         }

         private void FillDictionary()
         {
             foreach (var weaponSetting in _weaponSettings)
                 WeaponConfigs[weaponSetting.WeaponTypeId] = weaponSetting;
         }
    }
}