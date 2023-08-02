using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Installers.ScriptableObjects.Gun;
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
        
        public BulletData GetBy(BulletTypeId bulletTypeId) =>
            !_bulletDatas.TryGetValue(bulletTypeId, out BulletData bulletData) ? null : _bulletDatas[bulletTypeId];
    }
}