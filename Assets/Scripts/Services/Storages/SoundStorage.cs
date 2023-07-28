using System.Collections.Generic;
using Enums;
using Services.Factories;
using UnityEngine;
using Zenject;

namespace Services.Storages
{
    public class SoundStorage : IInitializable, ISoundStorage
    {
        private readonly Dictionary<SoundTypeId, AudioSource> _sounds = new();
        private readonly ISoundFactory _soundFactory;
        private readonly List<SoundTypeId> _soundTypeIds;
        
        public SoundStorage(ISoundFactory soundFactory, SoundsSettings soundsSettings)
        {
            _soundTypeIds = soundsSettings.SoundTypeIds;
            _soundFactory = soundFactory;
            FillDictionary();
        }

        public AudioSource Get(SoundTypeId soundTypeId) =>
            _sounds[soundTypeId];

        public List<AudioSource> Get()
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
                AudioSource audioSource = _soundFactory.Create(soundTypeId);
                _sounds[soundTypeId] = audioSource;
            }
        }

        public void Initialize()
        {
            
        }
    }
}