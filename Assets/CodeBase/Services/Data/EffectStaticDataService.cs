using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.ScriptableObjects;
using CodeBase.Services.Storages.Effect;
using UnityEngine;

namespace CodeBase.Services.Data
{
    public class EffectStaticDataService
    {
        private readonly Dictionary<ParticleTypeId, EffectData> _effectDatas;

        public EffectStaticDataService()
        {
            _effectDatas = Resources.LoadAll<EffectData>("Prefabs/EffectData")
                .ToDictionary(x => x.ParticleTypeId, x => x);
        }

        public Dictionary<ParticleTypeId, EffectData> GetAll() =>
            _effectDatas;

        public EffectData Get(ParticleTypeId particleTypeId) =>
            _effectDatas[particleTypeId];
    }
}