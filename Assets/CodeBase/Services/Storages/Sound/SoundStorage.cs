using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Installers.ScriptableObjects;
using CodeBase.ScriptableObjects.Sound;
using CodeBase.Services.Data;
using CodeBase.Services.Factories;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Storages.Sound
{
    public class SoundStorage : IInitializable, ISoundStorage
    {
        private readonly Dictionary<SoundTypeId, AudioSource> _sounds = new();
        private readonly IEffectFactory _effectFactory;
        private readonly SoundStaticDataService _soundStaticDataService;

        public SoundStorage(IEffectFactory effectFactory, SoundStaticDataService soundStaticDataService)
        {
            _soundStaticDataService = soundStaticDataService;
            _effectFactory = effectFactory;
            FillDictionary();
        }
        
        public AudioSource Get(SoundTypeId soundTypeId) =>
            _sounds[soundTypeId];

        public List<AudioSource> GetAll()
        {
            var audioSources = new List<AudioSource>();

            foreach (var audioSource in _sounds.Values)
            {
                audioSources.Add(audioSource);
            }

            return audioSources;
        }

        private void FillDictionary()
        {
            foreach (var soundData in _soundStaticDataService.GetAll().Values)
            {
                AudioSource audioSource = _effectFactory.Create(soundData.AudioSource);
                audioSource.playOnAwake = false;
                _sounds[soundData.SoundTypeId] = audioSource;
            }
        }

        public void Initialize()
        {
            
        }
    }
}