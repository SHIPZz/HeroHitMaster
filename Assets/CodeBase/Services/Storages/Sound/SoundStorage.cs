using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Installers.ScriptableObjects;
using CodeBase.ScriptableObjects.Sound;
using CodeBase.Services.Factories;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Storages.Sound
{
    public class SoundStorage : IInitializable, ISoundStorage
    {
        private readonly Dictionary<SoundTypeId, AudioSource> _sounds = new();
        private readonly IEffectFactory _effectFactory;
        private readonly List<SoundTypeId> _soundTypeIds;
        
        public SoundStorage(IEffectFactory effectFactory, SoundsSettings soundsSettings)
        {
            _soundTypeIds = soundsSettings.SoundTypeIds;
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
            foreach (var soundTypeId in _soundTypeIds)
            {
                AudioSource audioSource = _effectFactory.Create(soundTypeId);
                _sounds[soundTypeId] = audioSource;
            }
        }

        public void Initialize()
        {
            
        }
    }
}