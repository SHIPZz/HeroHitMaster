using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Weapon;
using UnityEngine;

namespace CodeBase.Services.Data
{
    public class WeaponStaticDataService
    {
        private readonly Dictionary<WeaponTypeId, WeaponData> _weaponDatas;

        public WeaponStaticDataService()
        {
            _weaponDatas = Resources.LoadAll<WeaponData>("Prefabs/WeaponData")
                .ToDictionary(x => x.WeaponTypeId, x => x);
        }

        public List<WeaponData> GetAll()
        {
            var weaponDatas = new List<WeaponData>();
            
            foreach (var weaponData in _weaponDatas.Values)
            {
                weaponDatas.Add(weaponData);
            }

            return weaponDatas;
        }

        public WeaponData Get(WeaponTypeId weaponTypeId) => 
            !_weaponDatas.TryGetValue(weaponTypeId, out WeaponData weaponData) ? 
                null :
                weaponData;
    }
}