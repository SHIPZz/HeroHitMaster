using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Gameplay.EffectsData;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Data
{
    public class EffectDataStorage
    {
        private readonly DiContainer _diContainer;
        private readonly Dictionary<EffectTypeId, ParticleSystem> _effects = new();

        public EffectDataStorage(DiContainer diContainer, BulletStaticDataService bulletStaticDataService)
        {
            _diContainer = diContainer;
            var effectDatas = Resources.LoadAll<EffectData>("Prefabs/Effects").ToList();

            FillDictionary(effectDatas);
        }

        public ParticleSystem GetBy(EffectTypeId effectTypeId) => 
            !_effects.TryGetValue(effectTypeId, out var effect) ? 
                null : 
                _effects[effectTypeId];

        private void FillDictionary(List<EffectData> effectDatas)
        {
            foreach (EffectData effectData in effectDatas)
            {
                var targetEffectData = _diContainer.InstantiatePrefabForComponent<EffectData>(effectData);
                _effects[targetEffectData.EffectTypeId] = targetEffectData.Effect;
            }
        }
    }
}