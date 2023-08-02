using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Installers.ScriptableObjects;
using CodeBase.ScriptableObjects.Enemy;
using UnityEngine;

namespace CodeBase.Services.Data
{
    public class EnemyStaticDataService 
    {
        private readonly Dictionary<EnemyTypeId, EnemyData> _enemyDatas;

        public EnemyStaticDataService()
        {
            _enemyDatas = Resources.LoadAll<EnemyData>("Prefabs/EnemyData")
                .ToDictionary(x => x.EnemyTypeId, x => x);
        }
        
        public EnemyData GetEnemyData(EnemyTypeId enemyTypeId) => 
            !_enemyDatas.TryGetValue(enemyTypeId, out EnemyData enemyData) ? 
                null :
                enemyData;
        
    }
}