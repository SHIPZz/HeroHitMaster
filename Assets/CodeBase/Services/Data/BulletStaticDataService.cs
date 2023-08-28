using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Bullet;
using UnityEngine;

namespace CodeBase.Services.Data
{
    public class BulletStaticDataService
    {
        private readonly Dictionary<WeaponTypeId, BulletData> _bulletDatas;

        public BulletStaticDataService()
        {
            _bulletDatas = Resources.LoadAll<BulletData>("Prefabs/BulletData")
                .ToDictionary(x => x.WeaponTypeId, x => x);
        }

        public List<BulletData> GetAll()
        {
            var bulletDatas = new List<BulletData>();

            foreach (var bulletDat in _bulletDatas.Values)
            {
                bulletDatas.Add(bulletDat);
            }

            return bulletDatas;
        }

        public BulletData GetBy(WeaponTypeId weaponType) =>
            _bulletDatas[weaponType];
    }
}