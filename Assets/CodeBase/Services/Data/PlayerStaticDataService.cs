using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Installers.ScriptableObjects.Player;
using UnityEngine;

namespace CodeBase.Services.Data
{
    public class PlayerStaticDataService 
    {
        private readonly Dictionary<PlayerTypeId, PlayerData> _playerDatas;

        public PlayerStaticDataService()
        {
            _playerDatas = Resources.LoadAll<PlayerData>("Prefabs/PlayerData")
                .ToDictionary(x => x.PlayerTypeId, x => x);
        }
        
        public PlayerData GetPlayerData(PlayerTypeId playerTypeId) => 
            !_playerDatas.TryGetValue(playerTypeId, out PlayerData enemyData) ? 
                null :
                enemyData;
        
    }
}