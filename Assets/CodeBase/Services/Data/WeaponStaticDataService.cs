﻿using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
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

        public WeaponData Get(WeaponTypeId weaponTypeId) => 
            !_weaponDatas.TryGetValue(weaponTypeId, out WeaponData weaponData) ? 
                null :
                _weaponDatas[weaponTypeId];
    }
}