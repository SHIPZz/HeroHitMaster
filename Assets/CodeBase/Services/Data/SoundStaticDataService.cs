using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Sound;
using UnityEngine;

namespace CodeBase.Services.Data
{
    public class SoundStaticDataService 
    {
        private readonly Dictionary<SoundTypeId, SoundData> _soundDatas;

        public SoundStaticDataService()
        {
            _soundDatas = Resources.LoadAll<SoundData>("Prefabs/SoundData")
                .ToDictionary(x => x.SoundTypeId, x => x);
        }

        public Dictionary<SoundTypeId, SoundData> GetAll() =>
            _soundDatas;
        
        public SoundData Get(SoundTypeId soundTypeId) =>
            _soundDatas[soundTypeId];
    }
}