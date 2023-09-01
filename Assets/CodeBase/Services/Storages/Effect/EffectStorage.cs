using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects;
using CodeBase.Services.Data;
using CodeBase.Services.Factories;
using UnityEngine;

namespace CodeBase.Services.Storages.Effect
{
    public class EffectStorage : IEffectStorage
    {
        private readonly IEffectFactory _effectFactory;
        private readonly EffectStaticDataService _effectStaticDataService;
        private Dictionary<ParticleTypeId, ParticleSystem> _effects = new();

        public EffectStorage(IEffectFactory effectFactory, EffectStaticDataService effectStaticDataService)
        {
            _effectFactory = effectFactory;
            _effectStaticDataService = effectStaticDataService;
            FillDictionary();
        }

        public ParticleSystem Get(ParticleTypeId particleTypeId) =>
            _effects[particleTypeId];

        private void FillDictionary()
        {
            foreach (EffectData effectData in _effectStaticDataService.GetAll().Values)
            {
                ParticleSystem targetEffect = _effectFactory.Create(effectData.ParticleSystem);
                ParticleSystem.MainModule targetEffectMain = targetEffect.main;
                targetEffectMain.playOnAwake = false;
                _effects[effectData.ParticleTypeId] = targetEffect;
            }
        }
    }
}