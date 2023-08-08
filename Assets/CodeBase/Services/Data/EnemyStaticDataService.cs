using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
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

        public List<EnemyData> GetAll()
        {
            var enemyDatas = new List<EnemyData>();

            foreach (var enemyData in _enemyDatas.Values)
            {
                enemyDatas.Add(enemyData);
            }

            return enemyDatas;
        }

        public EnemyData GetEnemyData(EnemyTypeId enemyTypeId) => 
            !_enemyDatas.TryGetValue(enemyTypeId, out EnemyData enemyData) ? 
                null :
                enemyData;
        
    }
}