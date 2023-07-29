using System.Collections.Generic;
using CodeBase.Services.Storages;
using UnityEngine;

namespace Windows.Audio
{
    public class AudioChanger
    {
        private readonly ISoundStorage _soundStorage;

        public AudioChanger(ISoundStorage soundStorage)
        {
            _soundStorage = soundStorage;
        }

        public void Change(float value)
        {
            List<AudioSource> allSounds = _soundStorage.GetAll();
            allSounds.ForEach(x => x.volume = value);
        }
    }
}