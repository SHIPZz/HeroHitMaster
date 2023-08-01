using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Installers.ScriptableObjects.Gun;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Services.Storages
{
    [CreateAssetMenu(fileName = "BulletStaticDataService", menuName = "Gameplay/BulletStaticDataService")]
    public class BulletStaticDataService : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<BulletTypeId, BulletData> _bulletDatas;

        public BulletData GetBy(BulletTypeId bulletTypeId) => 
            !_bulletDatas.TryGetValue(bulletTypeId, out BulletData bulletData) ? 
                null :
                _bulletDatas[bulletTypeId];
    }
}