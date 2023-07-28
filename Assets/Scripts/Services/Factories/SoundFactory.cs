using System;
using System.Collections.Generic;
using Constants;
using Enums;
using Services.Providers;
using Services.Providers.AssetProviders;
using UnityEngine;
using Zenject;

namespace Services.Factories
{
    public class SoundFactory : ISoundFactory
    {
        private readonly DiContainer _diContainer;
        private readonly AssetProvider _assetProvider;
        private readonly LocationProvider _locationProvider;
        
        private readonly Dictionary<SoundTypeId, string> _sounds = new()
        {
            {SoundTypeId.KnifeShootSound, AssetPath.KnifeShootSound},
            {SoundTypeId.WebShootSound, AssetPath.WebShootSound},
            {SoundTypeId.SpikeDieSound, AssetPath.SpikeDieSound},
            {SoundTypeId.SnakeletDieSound, AssetPath.SnakeletDieSound},
            {SoundTypeId.SnakenagaDieSound, AssetPath.SnakenagaDieSound},
            {SoundTypeId.DummyDieSound, AssetPath.DummyDieSound},
            {SoundTypeId.WolfPupDieSound, AssetPath.WolfPupDieSound},
            {SoundTypeId.WerewolfDieSound, AssetPath.WerewolfDieSound},
            {SoundTypeId.SnowBombDieSound, AssetPath.SnowBombDieSound},
            {SoundTypeId.EnemyBombDieSound, AssetPath.EnemyBombDieSound},
            {SoundTypeId.SunfloraDieSound, AssetPath.SunfloraDieSound},
            {SoundTypeId.FireBallSound, AssetPath.FireBallSound},
            
            {SoundTypeId.SpikeHitSound, AssetPath.SpikeHitSound},
            {SoundTypeId.SnakeletHitSound, AssetPath.SnakeletHitSound},
            {SoundTypeId.SnakenagaHitSound, AssetPath.SnakenagaHitSound},
            {SoundTypeId.DummyHitSound, AssetPath.DummyHitSound},
            {SoundTypeId.WolfPupHitSound, AssetPath.WolfPupHitSound},
            {SoundTypeId.WerewolfHitSound, AssetPath.WerewolfHitSound},
            {SoundTypeId.SnowBombHitSound, AssetPath.SnowBombHitSound},
            {SoundTypeId.EnemyBombHitSound, AssetPath.EnemyBombHitSound},
            {SoundTypeId.SunfloraHitSound, AssetPath.SunfloraHitSound},
        };
        
        public SoundFactory(DiContainer diContainer, AssetProvider assetProvider, LocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
            _assetProvider = assetProvider;
            _diContainer = diContainer;
        }

        public AudioSource Create(SoundTypeId soundTypeId)
        {
            if (!_sounds.TryGetValue(soundTypeId, out string path))
                throw new ArgumentException($"{soundTypeId} - ERROR");

            return Create(path);
        }

        private AudioSource Create(string path)
        {
            var audioPrefab = _assetProvider.GetAsset(path);
            return _diContainer.InstantiatePrefabForComponent<AudioSource>(audioPrefab, _locationProvider.SoundsParent);
        }
    }
}