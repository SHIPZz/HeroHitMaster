using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Bullet;
using UnityEngine;

namespace CodeBase.Services.Data
{
    public class BulletStaticDataService
    {
        private readonly Dictionary<BulletTypeId, BulletData> _bulletDatas;

        public BulletStaticDataService()
        {
            _bulletDatas = Resources.LoadAll<BulletData>("Prefabs/BulletData")
                .ToDictionary(x => x.BulletTypeId, x => x);
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

        public BulletData GetBy(BulletTypeId bulletTypeId) =>
            !_bulletDatas.TryGetValue(bulletTypeId, out BulletData bulletData) ? 
                null : 
                bulletData;
        
        public BulletData GetBy(WeaponTypeId weaponType) => 
            _bulletDatas.First(x => x.Value.WeaponTypeId == weaponType).Value;
    }
}