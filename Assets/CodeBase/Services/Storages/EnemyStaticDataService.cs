using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Installers.ScriptableObjects;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Services.Storages
{
    [CreateAssetMenu(fileName = "EnemyStaticDataService",menuName = "Gameplay/EnemyStaticDataService")]
    public class EnemyStaticDataService : SerializedScriptableObject
    {
       [OdinSerialize] private  Dictionary<EnemyTypeId, EnemyData> _enemyDatas;

       public EnemyData GetEnemyData(EnemyTypeId enemyTypeId) => 
            !_enemyDatas.TryGetValue(enemyTypeId, out EnemyData enemyData) ? 
                null :
                enemyData;
    }
}