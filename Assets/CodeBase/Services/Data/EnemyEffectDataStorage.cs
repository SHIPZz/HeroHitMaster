using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Gameplay;
using CodeBase.Gameplay.EffectsData;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Data
{
    public class EnemyEffectDataStorage
    {
        private readonly DiContainer _diContainer;
        private readonly Dictionary<EnemyTypeId, ParticleSystem> _deathEnemyEffects = new();
        private readonly LocationProvider _locationProvider;

        public EnemyEffectDataStorage(DiContainer diContainer, LocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
            _diContainer = diContainer;
             var deathEnemyEffects = Resources.LoadAll<DeathEnemyEffect>("Prefabs/DeathEnemyEffects")
                .ToList();

            FillDictionary(deathEnemyEffects);
        }

        public ParticleSystem GetDeathEnemyEffect(EnemyTypeId enemyTypeId) =>
            _deathEnemyEffects[enemyTypeId];

        private void FillDictionary(List<DeathEnemyEffect> deathEnemyEffects)
        {
            foreach (DeathEnemyEffect deathEnemyEffect in deathEnemyEffects)
            {
                var targetEffect = _diContainer
                    .InstantiatePrefabForComponent<DeathEnemyEffect>(deathEnemyEffect, _locationProvider.Values[LocationTypeId.SoundsParent]);
                _deathEnemyEffects[targetEffect.EnemyTypeId] = targetEffect.DieEffect;
            }
        }
    }
}