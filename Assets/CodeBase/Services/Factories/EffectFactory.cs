using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Sound;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using CodeBase.Services.Providers.AssetProviders;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories
{
    public class EffectFactory : IEffectFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IProvider<LocationTypeId, Transform> _locationProvider;

        public EffectFactory(DiContainer diContainer,
            IProvider<LocationTypeId, Transform> locationProvider)
        {
            _locationProvider = locationProvider;
            _diContainer = diContainer;
        }

        public AudioSource Create(AudioSource audioSource) =>
            _diContainer
                .InstantiatePrefab(audioSource.gameObject,
                    _locationProvider.Get(LocationTypeId.SoundsParent))
                .GetComponent<AudioSource>();

        public ParticleSystem Create(ParticleSystem particleSystem) =>
            _diContainer
                .InstantiatePrefab(particleSystem.gameObject,
                    _locationProvider.Get(LocationTypeId.SoundsParent))
                .GetComponent<ParticleSystem>();
    }
}