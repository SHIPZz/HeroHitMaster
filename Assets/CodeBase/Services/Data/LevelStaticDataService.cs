using System.Collections.Generic;
using System.Linq;
using CodeBase.Constants;
using CodeBase.ScriptableObjects.LevelDataSo;
using UnityEngine;

namespace CodeBase.Services.Data
{
    public class LevelStaticDataService 
    {
        private readonly Dictionary<string, LevelData> _levelDatas;

        public LevelStaticDataService()
        {
            _levelDatas = Resources.LoadAll<LevelData>(AssetPath.LevelData)
                .ToDictionary(x => x.Id, x => x);
        }

        public LevelData Get(string id) =>
            _levelDatas[id];
    }
}