using System.Collections.Generic;
using CodeBase.Enums;
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
        private readonly Dictionary<MusicTypeId, AudioSource> _musics = new();

        public SoundStorage(IEffectFactory effectFactory, SoundStaticDataService soundStaticDataService)
        {
            _soundStaticDataService = soundStaticDataService;
            _effectFactory = effectFactory;
            FillDictionary();
        }

        public AudioSource Get(SoundTypeId soundTypeId) =>
            _sounds[soundTypeId];

        public AudioSource Get(MusicTypeId musicTypeId) =>
            _musics[musicTypeId];

        public List<AudioSource> GetAll()
        {
            var audioSources = new List<AudioSource>();

            foreach (AudioSource audioSource in _sounds.Values)
            {
                audioSources.Add(audioSource);
            }

            return audioSources;
        }

        private void FillDictionary()
        {
            foreach (SoundData soundData in _soundStaticDataService.GetAll().Values)
            {
                AudioSource audioSource = _effectFactory.Create(soundData.AudioSource);
                audioSource.playOnAwake = false;

                if (soundData.SoundTypeId != SoundTypeId.Music)
                    _sounds[soundData.SoundTypeId] = audioSource;
                else
                    _musics[soundData.MusicTypeId] = audioSource;

            }
        }

        public void Initialize() { }
    }
}