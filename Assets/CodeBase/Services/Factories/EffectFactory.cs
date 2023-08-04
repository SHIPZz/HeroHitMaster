using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Sound;
using CodeBase.Services.Providers;
using CodeBase.Services.Providers.AssetProviders;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories
{
    public class EffectFactory : IEffectFactory
    {
        private readonly DiContainer _diContainer;
        private readonly AssetProvider _assetProvider;
        private readonly LocationProvider _locationProvider;
        private readonly Dictionary<SoundTypeId, string> _sounds;

        public EffectFactory(DiContainer diContainer, AssetProvider assetProvider, 
            SoundsSettings soundsSettings, LocationProvider locationProvider)
        {
            _sounds = soundsSettings.SoundPathesByTypeId;
            _locationProvider = locationProvider;
            _assetProvider = assetProvider;
            _diContainer = diContainer;
        }
        
        public AudioSource Create(AudioSource audioSource) =>
            _diContainer.InstantiatePrefab(audioSource.gameObject, _locationProvider.SoundsParent)
                .GetComponent<AudioSource>();

        public ParticleSystem Create(ParticleSystem particleSystem) =>
            _diContainer.InstantiatePrefab(particleSystem.gameObject, _locationProvider.SoundsParent)
                .GetComponent<ParticleSystem>();

        public AudioSource Create(SoundTypeId soundTypeId)
        {
            if (!_sounds.TryGetValue(soundTypeId, out string path))
                throw new ArgumentException($"{soundTypeId} - ERROR");

            return Create(path);
        }

        private AudioSource Create(string path)
        {
            var audioPrefab = _assetProvider.GetAsset(path);
            Debug.Log(path);
            return _diContainer.InstantiatePrefabForComponent<AudioSource>(audioPrefab, _locationProvider.SoundsParent);
        }
    }
}