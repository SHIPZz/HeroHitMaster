using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.ObjectPool;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages.Effect;
using CodeBase.Services.Storages.Sound;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.WaterSplash
{
    public class WaterSplashPoolInitializer
    {
        private const int Count = 50;
        private readonly DiContainer _diContainer;
        private readonly ISoundStorage _soundStorage;
        private readonly IEffectStorage _effectStorage;
        private readonly IProvider<EffectsPoolProvider> _provider;
        private readonly Transform _effectsParentTransform;

        public WaterSplashPoolInitializer(DiContainer diContainer, IProvider<EffectsPoolProvider> provider,
            IEffectStorage effectStorage, ISoundStorage soundStorage, IProvider<LocationTypeId, Transform> locationProvider)
        {
            _effectsParentTransform = locationProvider.Get(LocationTypeId.SoundsParent);
            _diContainer = diContainer;
            _effectStorage = effectStorage;
            _provider = provider;
            _soundStorage = soundStorage;
        }

        public void Init()
        {
            Dictionary<ParticleTypeId, GameObjectPool> effectPool = _provider.Get().EffectsPool;
            Dictionary<SoundTypeId, GameObjectPool> soundPool = _provider.Get().SoundPools;

            var sound = _soundStorage.Get(SoundTypeId.WaterSplash);
            var effect = _effectStorage.Get(ParticleTypeId.WaterSplash);

            effectPool[ParticleTypeId.WaterSplash] =
                new GameObjectPool(() => _diContainer.InstantiatePrefab(effect, _effectsParentTransform), Count);

            soundPool[SoundTypeId.WaterSplash] =
                new GameObjectPool(() => _diContainer.InstantiatePrefab(sound,_effectsParentTransform), Count);
        }
    }
}